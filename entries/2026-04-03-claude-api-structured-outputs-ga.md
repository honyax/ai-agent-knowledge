---
date: 2026-04-03
status: unread
relevance: B
tags: [claude-api, structured-outputs, message-batches, models-api]
source_urls:
  - https://platform.claude.com/docs/en/release-notes/overview
  - https://releasebot.io/updates/anthropic
experiment_dir: null
---

# Claude API — Structured Outputs GA、Message Batches 300k、Models API 拡張

## 3行要約

- Structured Outputs（構造化出力）がベータヘッダー不要でGA化。Claude Sonnet 4.5 / Opus 4.5 / Haiku 4.5で利用可能になり、スキーマサポート拡充とlatency改善が行われた
- Message Batches API の max_tokens 上限が 300k に引き上げられた（Opus 4.6 / Sonnet 4.6 対象、`output-300k-2026-03-24` ベータヘッダーで有効）
- Models API（GET /v1/models）にモデル能力フィールド（`max_input_tokens`、`max_tokens`、`capabilities`）が追加され、プログラムからモデルの仕様を取得できる

## 自分への関連度: B

Structured Outputs GAはゲーム内AIのレスポンス設計（JSON形式で状態管理など）に直結する。Message Batches 300kは大量処理ユースケースで有用。Models APIの拡張は動的モデル選択の実装に使える。

## 詳細

**Structured Outputs GA**:
- ベータヘッダー（`structured-output-2024-xx-xx`）不要になった
- JSON Schemaのサポートが拡充。文法コンパイルのlatencyも改善
- 対象: Claude Sonnet 4.5, Opus 4.5, Haiku 4.5

**Message Batches API 300k**:
- 従来のmax_tokens上限を大幅に引き上げ
- 長文コンテンツ生成・大規模コード生成・大量構造化データ処理に対応
- `output-300k-2026-03-24` ベータヘッダーをリクエストに付与して利用

**Models API 拡張**:
```
GET /v1/models → capabilitiesオブジェクト + max_input_tokens + max_tokens を返すように
```

**その他**:
- コード実行はWeb SearchまたはWeb Fetchと組み合わせると無料
- 1Mトークンコンテキストウィンドウベータ（Claude Sonnet 4.5 / Sonnet 4）は2026-04-30に廃止予定

## 試すなら

1. Structured Outputs: ベータヘッダーなしでJSON Schema指定のリクエストを送信してみる
2. Models API: `GET /v1/models` のレスポンスを確認してcapabilitiesオブジェクトの構造を把握
3. 1Mトークンベータ利用中の場合は移行計画を立てる（4月30日期限）

## ソース

- [Claude Platform Release Notes](https://platform.claude.com/docs/en/release-notes/overview)
- [Anthropic Release Notes - April 2026](https://releasebot.io/updates/anthropic)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->

