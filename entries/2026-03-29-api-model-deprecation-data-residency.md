---
date: 2026-03-29
status: unread
relevance: B
tags: [claude-api, model-deprecation, sonnet-37, haiku-35, data-residency, inference-geo]
source_urls:
  - https://platform.claude.com/docs/en/about-claude/models/overview
  - https://platform.claude.com/docs/en/release-notes/overview
experiment_dir: null
---

# Claude API: Sonnet 3.7/Haiku 3.5廃止 & Data Residency導入

## 3行要約

- Claude Sonnet 3.7とClaude Haiku 3.5が正式に廃止(リタイア)され、リクエストはエラーを返すようになった。後継はSonnet 4.6とHaiku 4.5
- Data Residency(データ所在地制御)が導入され、`inference_geo`パラメータでモデル推論の実行場所を指定可能に
- US限定推論は2026年2月1日以降のモデルで利用可能、通常料金の1.1倍

## 自分への関連度: B

現時点でClaude APIを本番利用していないが、カードバトルゲームへの統合を検討中。旧モデルのAPI呼び出しが失敗するため、既存コードのモデルID更新が必要。Data Residencyは将来的にグローバル展開する場合に関連。

## 詳細

### モデル廃止
- Claude Sonnet 3.7 -> Claude Sonnet 4.6 (`claude-sonnet-4-6`)へ移行推奨
- Claude Haiku 3.5 -> Claude Haiku 4.5 (`claude-haiku-4-5-20251001`)へ移行推奨
- 旧モデルIDでのリクエストはエラーを返す

### Data Residency
- `inference_geo`パラメータでモデル推論の地理的制約を指定可能
- US限定推論: 通常料金の1.1倍
- 2026年2月1日以降にリリースされたモデルが対象
- コンプライアンス要件がある企業向け機能

## 試すなら

1. 既存のClaude APIコードでモデルIDを確認し、旧モデルを使用している場合は更新
2. `claude-sonnet-4-6` または `claude-haiku-4-5-20251001` に切り替え
3. Data Residencyが必要な場合は `inference_geo` パラメータを追加

## ソース

- [Models overview - Claude API Docs](https://platform.claude.com/docs/en/about-claude/models/overview)
- [Claude Platform Release Notes](https://platform.claude.com/docs/en/release-notes/overview)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
