---
date: 2026-04-17
status: read
relevance: A
tags: [claude-api, model, opus, task-budgets, reasoning]
source_urls:
  - https://venturebeat.com/technology/anthropic-releases-claude-opus-4-7-narrowly-retaking-lead-for-most-powerful-generally-available-llm
  - https://platform.claude.com/docs/en/about-claude/models/whats-new-claude-4-7
  - https://github.blog/changelog/2026-04-16-claude-opus-4-7-is-generally-available/
experiment_dir: null
---

# Claude Opus 4.7 正式リリース — task budgets・xhigh推論・視覚3倍向上

## 3行要約

- Anthropic が 2026-04-16 に Claude Opus 4.7 を GA リリース。コーディングベンチマークで Opus 4.6 比 +13%。
- 新機能：**task budgets**（エージェントループのトークン上限設定）、**xhigh 推論レベル**（high と max の中間）、視覚解像度 3x 向上。
- 価格は Opus 4.6 と同じ $5/$25 per M tokens。4/23 以降、Enterprise/API デフォルトモデルが Opus 4.7 に変更予定。

## 自分への関連度: A

ゲーム内AI統合で Claude API を使う際に直接影響。task budgets はエージェントループのコスト管理に実用的。Claude Code もデフォルトで xhigh を使用するため、既存ワークフローへの影響確認が必要。

## 詳細

### task budgets
エージェントループ全体（thinking + tool calls + tool results + final output）のトークン上限を事前に指定できる。モデルは残りトークンを見ながら作業を優先順位付けし、予算切れ前にグレースフルに完了しようとする。無限ループや予算超過のリスクを制御しやすくなる。

### xhigh 推論レベル
thinking の深さを `high` と `max` の間で制御できる新レベル。Claude Code は全プランで xhigh をデフォルト採用。コーディングタスクで xhigh ≈ 75% の達成率、max はそれ以上だがトークン消費が大幅増。

### 破壊的変更
Opus 4.6 → 4.7 にはAPIの破壊的変更あり。移行前にマイグレーションガイドを確認推奨。

### Claude Sonnet 4 / Opus 4 の非推奨
- claude-sonnet-4-20250514 / claude-opus-4-20250514: 2026-06-15 に廃止予定
- claude-3-haiku-20240307: 2026-04-19 に廃止予定（直近！）

## 試すなら

1. API migration guide を確認し、Opus 4.6 → 4.7 の破壊的変更をチェック
2. task budgets パラメータを既存エージェントループに追加してみる
3. xhigh レベルで既存タスクの品質 vs コストを比較
4. Haiku 3 を使っているコードがあれば 2026-04-19 廃止前に更新

## ソース

- [Anthropic releases Claude Opus 4.7 — VentureBeat](https://venturebeat.com/technology/anthropic-releases-claude-opus-4-7-narrowly-retaking-lead-for-most-powerful-generally-available-llm)
- [What's new in Claude Opus 4.7 — Anthropic Docs](https://platform.claude.com/docs/en/about-claude/models/whats-new-claude-4-7)
- [Claude Opus 4.7 is generally available — GitHub Changelog](https://github.blog/changelog/2026-04-16-claude-opus-4-7-is-generally-available/)

---

## 感想・考察

破壊的変更は **Messages API 直接呼び出しのみ**に影響する。Claude Code や Agent SDK 経由で使っている分には影響なし。自分で `anthropic` SDK を呼び出すコードがある場合のみ、`temperature` / `top_p` / `top_k` の設定削除と `thinking` パラメーターの `adaptive` への移行が必要。

<!-- /try 実行時に自動生成 -->
