---
date: 2026-05-28
status: read
relevance: A
tags: [anthropic, claude-opus, model-release, api, claude-ai, fast-mode]
source_urls:
  - https://www.anthropic.com/news/claude-opus-4-8
  - https://9to5mac.com/2026/05/28/anthropic-upgrades-claude-with-new-opus-4-8-model-heres-whats-new/
  - https://www.axios.com/2026/05/28/anthropic-opus-release-mythos
  - https://gizmodo.com/anthropic-debuts-claude-opus-4-8-teases-upcoming-launch-of-mythos-class-models-2000764742
experiment_dir: null
---

# Claude Opus 4.8 リリース — コーディング・エージェント性能と「正直さ」を強化、Fast mode は大幅値下げ

## 3行要約

- Anthropic が 5/28 に Claude Opus 4.8 を公開。Opus 4.7 比でエージェンティックコーディング 64.3%→69.2%、ツール併用の多分野推論 54.7%→57.9%、ナレッジワークスコア 1753→1890 と各ベンチで改善。価格は Opus 4.7 と据え置き。
- 目玉は「正直さ・信頼性」の向上。自分が書いたコードの欠陥を見逃さず指摘する確率が Opus 4.7 比で約4倍に。長時間の自律作業や進捗の自己申告の正確さも改善。
- 同時に claude.ai に effort コントロール（タスクへの注力度を選べる）、Claude Code に dynamic workflows、より安い Fast mode（Opus 4.8 では従来の約1/3コスト・2.5倍速）を追加。API も当日から利用可能。次世代「Mythos クラス」モデルも予告。

## 自分への関連度: A

自分が日常で使う Claude Code / claude.ai の基盤モデルがそのまま Opus 4.8 に更新されるため、実際の作業品質に直結する（特にコード欠陥の自己指摘強化は信頼性向上として効く）。モデル自体の話なので分類は A だが、Claude Code 側の使い勝手への影響は別エントリ [[2026-05-28-claude-code-dynamic-workflows]] と [[2026-05-30-claude-code-v21147-v21158]] を参照。ゲーム内 AI 統合（関心領域5）の観点でも、価格据え置きで性能が上がるのは API 採用判断にプラス。

## 詳細

- ベンチ改善はあくまで Anthropic 公表値。実利用での体感は要検証。
- Fast mode の値下げは、Fast mode を使う自分の運用に直接効く（[[user_environment]] / Fast mode 利用）。Opus 4.8 では「標準レートの2倍で2.5倍速」と案内されており、従来より割安。
- 「Mythos クラス」は将来の上位モデル系列の予告で、現時点ではプレビュー扱い。過去の mythos 関連リーク（[[2026-04-08-project-glasswing-mythos]]）の流れと符合。

## 試すなら

1. Claude Code で `/model` を開き Opus 4.8 が選択されているか確認する
2. `/effort` で xhigh（高注力）と通常を切り替え、難しめのタスクで品質差を見る
3. Fast mode をオンにして応答速度とコスト感を確認する
4. claude.ai 側の effort コントロール UI を触ってみる

## ソース

- [Introducing Claude Opus 4.8 (Anthropic)](https://www.anthropic.com/news/claude-opus-4-8)
- [Anthropic upgrades Claude with new Opus 4.8 model (9to5Mac)](https://9to5mac.com/2026/05/28/anthropic-upgrades-claude-with-new-opus-4-8-model-heres-whats-new/)
- [Anthropic releases new model, Opus 4.8 (Axios)](https://www.axios.com/2026/05/28/anthropic-opus-release-mythos)
- [Anthropic Debuts Claude Opus 4.8, Teases Mythos-Class Models (Gizmodo)](https://gizmodo.com/anthropic-debuts-claude-opus-4-8-teases-upcoming-launch-of-mythos-class-models-2000764742)

---

## 感想・考察

### 4.7 → 4.8 の本質（読んだ上での整理）

- ベンチ数値（コーディング 64.3%→69.2% 等）は派手だが、価格据え置きでの底上げという点が地味に大きい。性能は上がってコストは変わらないので、API 採用判断（関心領域5）にも素直にプラス。
- 今回の核心は「正直さ・信頼性」の強化のほう。**自分が書いたコードの欠陥を指摘する確率が 4.7 比で約4倍**、自律作業中の進捗の自己申告も正確に。Claude Code で自律的にコードを書かせる自分の使い方だと、「できたと言っているが実は壊れている」が減る効果に直結する。ベンチの賢さより、この自己レビュー精度の改善のほうが日常の体感に効くと判断。
- effort コントロール / dynamic workflows / Mythos クラス予告はモデル本体とは別レイヤの話。詳細は [[2026-05-28-claude-code-dynamic-workflows]] と [[2026-05-30-claude-code-v21147-v21158]] 側で追う。

### Fast mode について（このやり取りで理解した点）

- Fast mode は「廉価モデルへのダウングレード」ではなく、**Opus のまま出力を高速化**するモード（Opus 4.8 で約2.5倍速、対応は 4.8/4.7/4.6）。賢さを犠牲にせず速さだけ得られるのがポイント。
- 使い方は `/fast` でトグル。VSCode ネイティブ拡張版（[[user_environment]]）でも利用可。
- コスト/レートの注意: 標準レートより消費が速い（従来「標準レートの2倍で2.5倍速」）。ただし Opus 4.8 では Fast mode コストが従来比約1/3に値下げ。自分は Pro プラン（[[user_claude_plan]]）で5時間枠運用なので、「重い作業をサクサク進めたいとき=Fast on / 長時間コツコツで枠を持たせたいとき=通常」の使い分けが現実的。
- 次アクション: 実際に `/fast` を切り替えて、難しめのタスクで体感差と Pro 枠の消費ペースを比べてみる（このエントリ「試すなら」3番に対応）。
