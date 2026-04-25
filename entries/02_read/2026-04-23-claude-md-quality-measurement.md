---
date: 2026-04-23
status: read
relevance: A
tags: [claude-code, claude-md, measurement, optimization, workflow]
source_urls:
  - https://zenn.dev/progate/articles/cb3018bbfc5aad
experiment_dir: null
---

# CLAUDE.md の品質を Claude Code で計測・定量検証する手法（Progate）

## 3行要約

- Progateエンジニアが329行あったCLAUDE.mdを131行に削減し、変更前後の効果をClaude Code自身を使って定量比較した実践記事
- `duration_ms` と `total_cost_usd` を計測指標にして同じ質問を複数回実行し、探索系質問ではコスト減少、実装計画系はブレが大きいという結論を得た
- 「良さそうに見えるCLAUDE.md」が本当に良いかを感覚ではなくデータで判断できる手法を公開

## 自分への関連度: A

CLAUDE.md の改善は感覚でやりがちだが、コスト・速度での検証は再現性があり自分でも適用できる。現在のグローバルCLAUDE.mdの最適化に使える具体的な計測方法。

## 詳細

**計測手法の概要**

1. 変更前のCLAUDE.mdでClaudeに同じ質問を複数回実行
2. Claude Code の出力する `duration_ms`・`total_cost_usd` を記録
3. CLAUDE.md を改修（329行→131行、約60%削減）
4. 同じ質問を再度実行して比較

**結果のポイント**

- 探索系の質問（「〇〇はどこに書いてある？」等）：平均コスト減少
- 実装計画系の質問（「〇〇を実装する手順を教えて」等）：ブレが大きく優劣をつけにくい
- 削減の副作用として「指示が抜けていないか不安」という心理コストが発生 → 定量計測で解消できる

**なぜ重要か**

CLAUDE.mdが長すぎると重要な指示がノイズに埋もれる（April 19のモジュール分割エントリと同問題）が、削減しすぎると指示漏れリスクがある。計測でその均衡点を探れる。

## 試すなら

1. 現在のCLAUDE.mdのトークン数を確認する
2. 「よく使う質問パターン」を3種類選ぶ（探索系・実装計画系・コードレビュー系）
3. 変更前に各質問を3回実行し、コストと時間を記録
4. CLAUDE.md を削減・整理してから同じ計測を繰り返す
5. 探索系クエリでコスト改善があれば削減成功と判断する

## ソース

- [いい CLAUDE.md なのか、Claude Code と計測・分析してみた (Zenn / Progate)](https://zenn.dev/progate/articles/cb3018bbfc5aad)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
