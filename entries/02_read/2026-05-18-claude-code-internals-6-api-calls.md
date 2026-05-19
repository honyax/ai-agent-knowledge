---
date: 2026-05-18
status: read
relevance: A
tags: [claude-code, internals, api, harness, tool-use, prompt-cache, anatomy]
source_urls:
  - https://qiita.com/sapeet-lin/items/f9143a90094601631b6b
experiment_dir: null
---

# Claude Code の裏側 — 1回の指示が「6ターン以上のAPI呼び出し」に展開される実装的解剖

## 3行要約

- Sapeet SWE 林氏（2026-05-13 Qiita）が、Claude Code の verbose ログを基に「このリポジトリの差分を確認してほしい」という一言が **6ターン以上のAPI呼び出しに展開される様子** を実例で解剖。Claude 本体は「テキストを受け取ってテキストを返す推論エンジン」に過ぎず、コマンド実行はローカルの Claude Code バイナリが担う、という分業がコードレベルで示されている
- ツール呼び出しの仕組みは **system prompt に約30個のツール定義（Bash/Read/Write 等）を埋め込み**、Claude が description から選んで JSON を返す → ローカルが実行 → 結果を user メッセージに混ぜて次ターンへ、という古典的な ReAct ループ。終了判定は `stop_reason: "end_turn"`、継続は `"tool_use"`
- **プロンプトキャッシュの効きが凄まじい**: Turn3 で 27,529 token をキャッシュ化し、以降の Turn4〜6 は同じプレフィックスを **90% オフ** で再利用。合計 27,964 token の読取が実現し、ツール定義の重さを実質ゼロに近づけている

## 自分への関連度: A

「ハーネス」という抽象概念で語られがちな Claude Code の動作を、verbose ログを使って実際の API レイヤーに落として解剖している。[[user_role]] のスキル的にも「内部API呼び出し回数とキャッシュヒット率」が分かるとデバッグ・コスト試算・カスタム実装で直結する。前から積んでいた [[harness-engineering-concept]] や [[rkaga-harness-engineering-talk]] の理論側を、初めて「具体的なバイト数とトークン数」で裏付けた感じ。

## 詳細

### 観測方法
- `claude --verbose` で system prompt とツール定義の生バイト列を取り出し、`/v1/messages` への JSON ペイロードを Turn ごとに並べて差分を取っている
- これにより各 Turn の `input_tokens` / `cache_creation_input_tokens` / `cache_read_input_tokens` / `output_tokens` を読める

### 1指示=6ターンの内訳（実測例）
1. Turn1: ユーザー指示受領 → Claude が `Bash(git status)` を要求
2. Turn2: ローカル実行結果を返す → Claude が `Bash(git diff)` を要求
3. Turn3: 結果を返す → Claude が `Read(変更ファイル)` を要求。**ここで初めてプロンプトキャッシュ生成**（27,529 token）
4. Turn4〜5: 複数ファイル Read。キャッシュヒット
5. Turn6: 統合した要約テキストを `end_turn` で返却

### ツールスキーマの常駐コスト
- システムプロンプト＋約30ツール定義で **約24K token** を毎回送る必要があるが、キャッシュにより 2回目以降は **約2.4K token 相当のコスト** に圧縮
- 「ハーネス側がいかにキャッシュフレンドリーな順序でツール呼び出しを並べるか」が実コストを決める設計判断

### Anthropic 公式の API 視点との対応
- 各ツール呼び出しは `tool_use` content block として Claude の出力に含まれ、ローカルは `tool_result` content block で返す
- `stop_reason` 列挙の意味と一致しており、Claude Agent SDK の挙動とも整合

## 試すなら

1. `claude --verbose --print "git diff の要約を出して"` で1指示を実行し、stderr に流れる JSON を保存
2. `jq '.usage'` で Turn ごとの token 内訳を集計し、cache_read / cache_creation の比率を可視化
3. 同じ指示を `/clear` 後に再実行し、初回キャッシュミス → 2回目キャッシュヒット の差を測る
4. ツール数を絞った subagent（Read のみ等）で同じ指示を流し、system prompt サイズの圧縮効果を比較
5. `claude -p` 経由（Agent SDK ライク）で同じことをやり、対話モードとの差分を確認

## ソース

- [いつも使っている Claude Code の裏側を覗いてみた #AI - Qiita（Sapeet SWE 林、2026-05-13）](https://qiita.com/sapeet-lin/items/f9143a90094601631b6b)

---

## 感想・考察

### 記事本文の誤認訂正
- 本エントリ初版の「観測方法」で `claude --verbose` と書いていたが、元記事が実際に使っているのは **claude-tap**（Anthropic API との間に挟む MITM プロキシ、Python 製 OSS）
- `--verbose` ではなく wire-level の HTTPS ペイロードを傍受しているからこそ、system prompt の生バイト・ツール定義の JSON Schema・ターン間の structural diff・`cache_creation_input_tokens` の正確な値まで取れている、という構造

### claude-tap を自分で試すかの判断
- claude-tap は OSS とはいえ Python の野良パッケージ。Pro プランの **OAuth アクセストークン（Anthropic アカウントに紐付く高価値クレデンシャル）が MITM プロキシに丸見え**になるため、サプライチェーン攻撃リスクを考えると気軽には導入したくない
- 同等のことを公式寄り／実績ある OSS で実現する選択肢:
  1. **mitmproxy**（OSS の本命。claude-tap が裏でやっているのとほぼ同じことを、はるかにレビュー人口の多いベースでできる。隔離 VM ＋使用後トークン rotate と組み合わせるのが現実的）
  2. **Claude Code 公式の OpenTelemetry**（`CLAUDE_CODE_ENABLE_TELEMETRY=1`。ペイロードは見えないが、ターン数・トークン消費・キャッシュヒット率は取れる）
  3. **`claude --debug` / `ANTHROPIC_LOG=debug`**（公式デバッグ出力、サードパーティ依存ゼロ。テキストダンプなので UI はない）
  4. **Bedrock / Vertex 経由**（CloudWatch / Cloud Logging に公式ログ。OAuth トークン無関与だがオーバーキル）
- 注意点: Anthropic Console の Activity ログは **API キー利用時のみ**で、Pro/Max の OAuth トラフィックは表示されない
- 公式が踏み込んだ可視化ツールを出さないのは、MITM = トークン露出をサポート対象にしたくないという力学が大きそう。OTel に寄せている設計判断は「ペイロードを露出させずに観測点だけ提供する」方向

### 自分の理解の整理 — Claude Code は「手足」だけではない
- 当初「Claude が判断・Claude Code は手足として実行」と捉えたが、この記事の本質はもう一段深く、Claude Code は単なる手足ではなく **「Claude に何を見せて何をさせるかを設計する舞台装置（ハーネス）」** でもある、という方が正確
- 分業の構造:
  - **判断（脳）**: Claude API 側の推論 — どのツールを呼ぶか、引数、いつ `end_turn` するか
  - **実行（手足）**: Claude Code ローカル — Bash/Read/Write の実際の実行と結果回収
  - **舞台設定（ハーネス）**: Claude Code ローカル — system prompt 構築、ツール選定、コンテキスト圧縮、権限確認、ループ制御、サブエージェント起動
- 記事の数字はこの舞台設定の重さを定量化している:
  - 約24K token のツール定義を毎ターン送り続ける → ハーネス側の設計判断
  - 同じプレフィックスを連続させてキャッシュを効かせる → ハーネスのお膳立て
  - 約30個のツールに絞る（多すぎると判断精度が落ちる／少なすぎると何もできない）→ バランス調整もハーネス
- 「Claude が賢く動いて見える」かどうかはハーネス設計でかなり決まる、という [[harness-engineering-concept]] / [[rkaga-harness-engineering-talk]] の主張を、初めてバイト数とトークン数で裏付けた位置付け
- 今後 Hooks / Skills / サブエージェントの話題が出てきたら「これは舞台装置側の改造か、判断側への指示か」で整理する
