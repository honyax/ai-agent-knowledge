---
date: 2026-03-31
status: archived
relevance: B
tags: [claude-api, model, deprecation, migration]
source_urls:
  - https://platform.claude.com/docs/en/about-claude/model-deprecations
  - https://platform.claude.com/docs/en/release-notes/overview
experiment_dir: null
---

# Claude Haiku 3 モデル廃止（2026-04-19）— Haiku 4.5 移行を

## 3行要約

- `claude-3-haiku-20240307`（Claude Haiku 3）が 2026年4月19日に廃止予定で、以降のリクエストはエラーになる
- 移行先は `claude-haiku-4-5-20251001`（Claude Haiku 4.5）が推奨
- 既存コードで claude-3-haiku を使っている場合は4月19日までに更新が必要

## 自分への関連度: B

Haiku 3.5（`claude-3-5-haiku-20241022`）は既に廃止済みで対応済みのはず。
Haiku 3（`claude-3-haiku-20240307`）を直接使っているコードがあれば要確認。

## 詳細

### 廃止スケジュール

| モデルID | 廃止日 | 移行先 |
|---------|--------|--------|
| `claude-3-haiku-20240307` | 2026-04-19 | `claude-haiku-4-5-20251001` |

- 廃止後のリクエストは HTTP エラーで失敗する
- Anthropic は退役日の60日前に通知を送付する方針

### 注意点

- Haiku 3.5（`claude-3-5-haiku-20241022`）は既に廃止済み（今回の発表とは別）
- Haiku 4.5 はHaiku 3 より高性能かつコスト効率が良い

## 試すなら

1. コードベースで `claude-3-haiku` を検索して使用箇所を確認
2. 見つかった箇所を `claude-haiku-4-5-20251001` に更新
3. 動作テストを実施して品質に問題がないか確認

## ソース

- [Model deprecations（Claude API Docs）](https://platform.claude.com/docs/en/about-claude/model-deprecations)
- [Claude Platform Release Notes](https://platform.claude.com/docs/en/release-notes/overview)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
