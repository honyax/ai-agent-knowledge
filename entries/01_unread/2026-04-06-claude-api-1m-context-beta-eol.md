---
date: 2026-04-06
status: unread
relevance: B
tags: [claude-api, 1m-context, 廃止, モデル移行, sonnet-4-6]
source_urls:
  - https://platform.claude.com/docs/en/release-notes/overview
experiment_dir: null
---

# Claude API: Sonnet 4.5/4 の1Mトークンβコンテキスト窓が2026-04-30に廃止

## 3行要約

- `context-1m-2025-08-07` ベータヘッダーがClaude Sonnet 4.5・Sonnet 4で**2026年4月30日**に廃止される
- 廃止後は200kを超えるリクエストがエラーになるため、Sonnet 4.6またはOpus 4.6への移行が必要
- Sonnet 4.6・Opus 4.6では1Mコンテキスト窓が**ベータヘッダー不要・標準価格**で利用可能

## 自分への関連度: B

Claude APIを使ったゲーム内AI統合を検討している立場として、モデル選定の際に1Mコンテキストが必要になるケースを考慮すると知っておくべき情報。現時点では直接影響はないが、古いモデルを使い続けている場合は要注意。

## 詳細

**廃止対象と日程:**
- `claude-sonnet-4-5-*` + `context-1m-2025-08-07` ベータヘッダー → 2026-04-30廃止
- `claude-sonnet-4-*` + `context-1m-2025-08-07` ベータヘッダー → 2026-04-30廃止

**移行先（ヘッダー不要で1M標準対応）:**
- `claude-sonnet-4-6`
- `claude-opus-4-6`

**あわせて覚えておくこと:**
- Message Batches APIのmax_tokensが300kに引き上げ（Opus 4.6/Sonnet 4.6、`output-300k-2026-03-24` ヘッダー）
- Claude Haiku 3（`claude-3-haiku-20240307`）は2026-04-19に廃止済み（移行先: Haiku 4.5）

## 試すなら

1. 現在のプロジェクトで `context-1m-2025-08-07` ヘッダーを使っているか確認
2. 使っている場合、`claude-sonnet-4-6` または `claude-opus-4-6` に切り替え
3. 1Mコンテキストが不要なら200k以内のモデルで最適化を検討
4. 価格影響を確認（Sonnet 4.6での1M使用は標準価格）

## ソース

- [Claude Platform Release Notes - March 30, 2026](https://platform.claude.com/docs/en/release-notes/overview)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
