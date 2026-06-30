---
date: 2026-06-11
status: read
relevance: S
tags: [claude-code, bug, opus-4.8, windows, japanese, tool-use]
source_urls:
  - https://zenn.dev/edhiblemeer/articles/claude-code-opus48-tool-corruption
  - https://github.com/anthropics/claude-code/issues/63875
experiment_dir: null
---

# Claude Code (Opus 4.8) で全ツール呼び出しが壊れる — Windows×日本語環境で踏みやすい未修正バグと回避策

## 3行要約

- 2026-06-03公開のZenn記事。Opus 4.8で長時間セッションを続けると、関数呼び出しの開始タグが「count」「court」など無意味なトークンに化け、引数が生テキストとして漏洩する。一度発生するとモデルが誤った形式を「正しい」と学習する自己強化ループに陥り、自然回復しない。
- 発生条件は「Opus 4.8 × Windows × 非ASCII文字（特に日本語）× 長時間・大規模コンテキスト」の組み合わせ。GitHub issue #63875で報告されているがOPENのまま未修正。Opus 4.7では同種バグが修正済みで未報告。
- 回避策: 発生したら `/rewind` か `/compact` で壊れたターンを消去（即時復旧）。予防は早期の `/compact`、タスク分解、重要情報のCLAUDE.md記載、必要ならOpus 4.7へのモデル変更。

## 自分への関連度: S

自分の環境（Windows 11 × 日本語）はまさにこのバグの最頻発条件。長時間セッションでツール呼び出しが急に壊れたら、本体の不調ではなくこのバグを疑って `/rewind` する、という対処を知っているだけで復旧時間が大きく変わる。

## 詳細

- 症状の本質は「ツール呼び出しの特殊トークンが壊れる」こと。モデル出力のfunction call開始タグが類似トークンに置き換わり、以降のツール実行がすべて失敗する。
- 自己強化ループが厄介な点: 壊れた出力がコンテキストに残るため、モデルはそれを参照して同じ壊れ方を繰り返す。だからこそ「壊れたターンをコンテキストから消す」`/rewind`/`/compact` が効く。
- Fable 5リリース（[[2026-06-11-claude-fable-5-mythos-5-release]]）後はFable 5側で同症状が出るかも観察ポイント。
- 記事は回避策として「重要情報はCLAUDE.mdに書いておく」ことも挙げている。rewind/compactで消えても文脈を再構築しやすくするため。

## 試すなら

1. 長時間セッション中にツール呼び出しが連続失敗したら、まずエラー内容に意味不明トークン（count/court等）がないか確認
2. 該当したら `/rewind` で壊れたターンの直前まで戻す
3. 戻れない場合は `/compact` でコンテキストを再圧縮
4. 頻発するなら作業をタスク分解して早めの `/compact` を習慣化
5. GitHub issue #63875 の修正状況を定期確認

## ソース

- [Claude Code (Opus 4.8) で全ツール呼び出しが壊れる — 日本語環境で踏みやすい未修正バグと回避策 (Zenn)](https://zenn.dev/edhiblemeer/articles/claude-code-opus48-tool-corruption)
- [GitHub issue #63875](https://github.com/anthropics/claude-code/issues/63875)

---

## 感想・考察

### Opus 4.8 への風当たりと "Anthropic内部はMythos/Fableを使っているのでは" 説（2026-07-01 議論）

このツール呼び出し崩壊バグ以外にも、Opus 4.8 には色々と不評が集まっている。コミュニティの一部では「Opus は 4.6 が一番良かった」という声も出ている。リリース時点では性能向上が謳われた（[[2026-05-28-claude-opus-48-release]]）にもかかわらず、実運用での体感はネガティブ寄り。

注目すべき推測として「Anthropic の社員はみんな Mythos / Fable を使っていて、Opus はもうメインのドッグフード対象になっていないのではないか」という見方がある。これが本当だとすれば、Opus 4.8 のバグが見逃され続けている構造的な理由が説明できる：

- 社内で日常使いされていないモデルはエッジケースの報告が上がりにくい
- 特に Windows × 日本語のような「米国本社では踏まれにくい」条件は、社内ドッグフードでカバーされない
- Fable 5 / Mythos 5（[[2026-06-11-claude-fable-5-mythos-5-release]]）への注力が始まった時期と、Opus 4.8 不評の時期が重なる

実用判断としては、自分の Windows × 日本語環境（[[user_environment]]）では:
- 当面 Opus 4.8 ではなく Opus 4.7 をデフォルトにするのが安全策
- Fable 5 は無料期間が終わり追加クレジットが必要なため、頻繁な切替はコスト的に厳しい
- Opus 4.7 で運用しつつ、Opus 4.8 の修正アップデートと社内ドッグフード復活を待つ

[[fable5-mythos5-export-ban]] で Fable 5 が一時的に使えなくなった件と合わせると、「Anthropic は内部的に Mythos/Fable に依存しすぎて、Opus 系の品質維持が手薄になっている」という構造的リスクが浮かび上がる。
