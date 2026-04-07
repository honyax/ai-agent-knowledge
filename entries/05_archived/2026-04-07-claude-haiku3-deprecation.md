---
date: 2026-04-07
status: archived
relevance: A
tags: [claude-api, deprecation, model, haiku]
source_urls:
  - https://platform.claude.com/docs/en/about-claude/model-deprecations
  - https://releasebot.io/updates/anthropic
---

# Claude Haiku 3 が 2026-04-19 に廃止・Claude Sonnet 4.5/4 の1Mコンテキストが 2026-04-30 に終了

## 3行要約

- `claude-3-haiku-20240307`（Claude Haiku 3）が 2026年4月19日に廃止。期日後のリクエストはエラーになる
- Claude Sonnet 4.5 および Sonnet 4 の 1M トークンコンテキストウィンドウ beta が 2026年4月30日に終了
- Haiku 3 は Claude Haiku 4.5 へ、1M コンテキストは Sonnet 4.6 / Opus 4.6 へ移行が必要

## 自分への関連度: A

Claude API をゲーム内AI統合で使う可能性がある場合、Haiku 3 を使っているコードはすぐに確認・移行が必要。4/19 まで残り約2週間。

## 詳細

**廃止スケジュール:**

| モデル | 廃止日 | 移行先 |
|--------|--------|--------|
| `claude-3-haiku-20240307` | 2026-04-19 | `claude-haiku-4-5-20251001` |
| Sonnet 4.5/4 の 1M context beta | 2026-04-30 | Claude Sonnet 4.6 / Opus 4.6 |

**その他の API 変更（2026年4月）:**
- Message Batches API で Claude Opus 4.6 / Sonnet 4.6 の `max_tokens` が300Kに拡張（`output-300k-2026-03-24` ベータヘッダーが必要）
- コード実行 API が Web Search / Web Fetch と組み合わせた場合は無料に
- データレジデンシー制御を追加（`inference_geo` パラメーター、US限定は1.1x料金）
- 構造化出力が Sonnet 4.5 / Opus 4.5 / Haiku 4.5 で GA

## 試すなら

1. 自分のプロジェクトで `claude-3-haiku` を使っているコードを検索する
2. `claude-haiku-4-5` に置き換えてテストする
3. 1M コンテキストを使っている場合は `claude-sonnet-4-6` への切り替えを確認する

## ソース

- [Model deprecations - Claude API Docs](https://platform.claude.com/docs/en/about-claude/model-deprecations)
- [Anthropic Release Notes - April 2026](https://releasebot.io/updates/anthropic)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
