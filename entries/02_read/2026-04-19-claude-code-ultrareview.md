---
date: 2026-04-19
status: read
relevance: A
tags: [claude-code, code-review, multi-agent, premium]
source_urls:
  - https://code.claude.com/docs/en/ultrareview
  - https://claude.com/blog/code-review
  - https://thenewstack.io/anthropic-launches-a-multi-agent-code-review-tool-for-claude-code/
  - https://www.infoq.com/news/2026/04/claude-code-review/
  - https://www.tech2geek.net/claude-code-ultrareview-deep-multi-agent-code-reviews-explained/
experiment_dir: null
---

# Claude Code /ultrareview: クラウドマルチエージェントによる深層コードレビュー

## 3行要約

- `/ultrareview` コマンドがリモートクラウドサンドボックスで複数AIエージェントを並列起動し、深層コードレビューを実行
- 各エージェントが異なる種類のバグを独立してスキャンし、検証ステップで偽陽性を除去してから報告
- Pro/Maxプランで3回まで無料、Team/Enterpriseではリサーチプレビューとして利用可能

## 自分への関連度: A

単体エージェントでは見落としがちなバグを複数視点で並列レビューする仕組みは、品質担保に直接使える。既存のコードレビューワークフローに組み込めるか検証したい。無料3回分で試せるのは大きい。

## 詳細

**仕組み**
- `/ultrareview` 実行時、Anthropicインフラのリモートサンドボックスが起動
- 複数のAIレビュアーエージェントがdiffと周辺コードを並列スキャン
- 各エージェントは異なる種類の問題（セキュリティ、ロジック、パフォーマンス等）を担当
- 候補バグを実際のコード動作と照合して検証するステップがあり、偽陽性を除去
- レビュー完了まで平均20分

**使い方**
```
/ultrareview          # 現在のブランチをレビュー
/ultrareview 123      # GitHubのPR番号を指定してレビュー（GitHub remote必須）
```

**利用条件・コスト**
- Pro/Maxプラン: 3回無料、以降は従量課金
- Team/Enterprise: リサーチプレビューとして利用可能
- PR規模・複雑さに応じてコストが変動

**既知の問題**
- 大規模リポジトリでは30分タイムアウトが発生し、findings無しで終了するケースがある
- 大きいブランチスコープで空のfindingsが返るバグが報告済み（無料クレジットは消費される）
- 対象ブランチの指定が意図しないものになるケースも報告あり

## 試すなら

1. Claude Code（Pro/Max）で `/ultrareview` を実行し、現在ブランチをレビュー
2. 小〜中規模のPRでまず動作確認（30分タイムアウト回避のため）
3. GitHubリモートを設定した上で `/ultrareview <PR番号>` を試す
4. findingsの精度と偽陽性率を手動レビューと比較
5. 通常の `/review` コマンドとの使い分けポイントを確認

## ソース

- [Find bugs with ultrareview - Claude Code Docs](https://code.claude.com/docs/en/ultrareview)
- [Code Review - Claude Code Blog](https://claude.com/blog/code-review)
- [Anthropic Launches Multi-Agent Code Review Tool - The New Stack](https://thenewstack.io/anthropic-launches-a-multi-agent-code-review-tool-for-claude-code/)
- [Anthropic Introduces Agent-Based Code Review for Claude Code - InfoQ](https://www.infoq.com/news/2026/04/claude-code-review/)
- [Claude Code Ultrareview: Deep Multi-Agent Code Reviews Explained - Tech2Geek](https://www.tech2geek.net/claude-code-ultrareview-deep-multi-agent-code-reviews-explained/)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
