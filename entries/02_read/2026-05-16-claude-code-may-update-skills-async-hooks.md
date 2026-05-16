---
date: 2026-05-16
status: read
relevance: S
tags: [claude-code, skills, hooks, async-hooks, http-hooks, skill-search, scaling, team, personal-pipeline]
source_urls:
  - https://qiita.com/creolab_dev/items/5f058d93b1f88c43f339
experiment_dir: null
---

# Claude Code 5月アップデート総括 — Skills 検索 / async hooks / HTTP hooks が個人開発から小規模チームへスケールさせる前提条件

## 3行要約

- creolab_dev 氏 (Qiita) が 2026-05 月の Claude Code 主要追加を「**Skills 検索**・**async hooks**・**HTTP hooks**」の3点に絞って整理。いずれも単独機能というより組合せて初めて効く設計で、「個人開発から少人数チーム規模へスケール」する構造的な前提条件が揃ったと評価
- **Skills 検索**: スラッシュメニューから skill 名 / description を曖昧検索可能。skill が数十〜100超になっても探索コストが伸びない設計で、**共有 skill カタログ運用が現実的に**
- **async hooks (`async: true`)**: Slack 通知・画像生成・外部 CI 起動など重い副作用を **処理完了を待たずバックグラウンド化**。**HTTP hooks** は hook から外部 web server に直接 POST 可能。1行で「個人 PC 内」から「社内インフラ連携」に拡張できる

## 自分への関連度: S

自分のナレッジパイプライン（catch-up / digest / try Skill 3層）がまさにスケール問題に直面しつつある段階で、極めて直接的に効く:

- **Skills 検索**: 現状は catch-up / digest / try / fewer-permission-prompts / claude-automation-recommender など 7-8 skill だが、今後 entry-template バリエーション / blender-mcp / unity-mcp 等を追加すると 15〜20 になる見込み。**カタログが膨らんだ段階で曖昧検索が機能する**
- **async hooks**: catch-up Skill 完了時の Discord 通知・try Skill での experiment 実行完了通知などを **「Claude を待たせない」形** で実装できる。現状は hook がブロックする設計のため避けていた
- **HTTP hooks**: hook から GitHub Actions / 自前 webhook / Discord に直接 POST。`/catch-up` 完了で webhook を叩いて他端末（Android）に通知、なども実現可能
- **設計思想の妥当性確認**: 「単独機能ではなく組合せ」「個人パイプラインの上限解放」というフレーミングは、自分が rkaga harness-engineering エントリ（2026-05-02）で得た「User-Side / Agent-Side」構造の中の「User-Side が計算的センサーで強くなる」観点と整合
- **チーム化の前提条件**: 自分は現状ソロ運用だが、CLAUDE.md の関心領域 1, 2 で「コミュニティの知見」を組織的に取り込みたい意図がある。Skill カタログ + HTTP hooks + async hooks が揃うと、たとえば「読書会向けの共有 skill セット」を組む選択肢が出てくる

## 詳細

### Skills 検索

- スラッシュコマンドで `/` を叩いた後の menu で **skill 名 / description を曖昧検索**
- 数十〜100超の skill でも探索コストが膨らまない
- 共有 skill カタログ（プラグイン経由配布 / git submodule での共有）を実用化する前提条件
- 自分の運用への影響: 現在の skill 一覧で description フィールドが空のものを埋める価値が一気に上がる

### Async Hooks (`async: true`)

- hook 設定で `async: true` フラグを立てると、hook の完了を Claude が待たない
- Slack 通知 / 画像生成 / 外部 CI 起動 / 通知系の重い処理がノンブロッキング化
- v2.1.141 で追加された `terminalSequence` 出力（hook から desktop notification を出す）と組合せると **「重い処理は async で投げ、軽い通知は sync で UI に出す」** の二段構成が組める

### HTTP Hooks

- hook の中から **外部 web server に直接 POST** できる
- 社内 CI / Issue Tracker / Slack incoming webhook / Discord webhook など、これまで `bash:` 経由で curl していたものが宣言的に書ける
- 認証情報の渡し方は環境変数経由が前提で、settings.json に直書きしないこと

### 実運用上の留意点

1. **async hook 失敗の無視化防止**: async は完了を待たないため、失敗が silent になりがち。失敗をログ＋通知する仕組みを別途持つ必要がある
2. **認証情報の秘匿化**: HTTP hooks では URL / Header に API キーが混じりやすい。`${env.WEBHOOK_TOKEN}` 形式の参照に統一
3. **Pre/Post hooks の責務明確化**: PreToolUse は同期で必要（v2.1.139 の `continueOnBlock` も同期前提）。async は通知系・副作用系の PostToolUse に絞るのが安全

## 試すなら

1. 自分の skill ファイルすべての frontmatter `description` を読み返し、Skills 検索でヒットしやすい固有名詞・動詞を含むよう書き直す
2. `.claude/hooks.json` または settings.json に PostToolUse の async hook を試作（catch-up 完了時に Discord webhook を叩く PoC）
3. v2.1.141 の `terminalSequence` 出力と async hook を組合せ、catch-up の最後でデスクトップ通知 + Discord 通知の二段構成を試す
4. ai-agent-knowledge 配下の `.claude/skills/` の skill 群に、新規 entry-template-blender, entry-template-unity 等を追加して **Skills 検索の効果を体感**
5. 元記事の実運用上の留意点（async 失敗の通知・認証情報秘匿化）を CLAUDE.md または hooks.json コメントに反映

## ソース

- [Claude Code 5 月アップデート総括 — skills 検索 / async hooks / HTTP hooks を個人開発パイプラインへ組み込む (Qiita, creolab_dev)](https://qiita.com/creolab_dev/items/5f058d93b1f88c43f339)

---

## 感想・考察

### Skills 検索

- 実機で確認したところ、**スラッシュ以降に日本語の単語を入力しても description にマッチして該当 skill がヒットする**
- 自分の自作 Skill（catch-up / digest / try / fewer-permission-prompts 等）の description は日本語で書いているので、追加コストゼロで恩恵を受けられる
- 今後 skill が 15〜20 に増えても運用に耐える前提条件が揃った。「試すなら 1番」（description 書き直し）の優先度は、現状すでに日本語で書き込んでいるので一旦保留で良さそう

### async hooks

- 現状 **hook 自体をほぼ活用していない** ため、いきなり async hooks に飛びつく動機は薄い
- 導入順序としては「sync hook で何か自動化したい用途を見つける」→「Claude を待たせる重さになってきたら async 化する」が自然
- 自分のパイプライン文脈での候補（catch-up 完了時の Discord 通知 / try Skill での experiment 完了通知 等）は「無くても困らないが、あると便利」枠で、現時点で優先度を上げる必然性は無い
- HTTP hooks も同様。Discord は既に MCP 経由で繋がっており、わざわざ webhook を別経路で組む必然性は薄い

### 結論

- **Skills 検索**: 自動で恩恵を受ける。description の書き方を多少意識する程度
- **async hooks / HTTP hooks**: hook 利用の必然性が出てきたタイミングで再評価する。エントリ自体は read で完了扱い、03_todo には回さない
