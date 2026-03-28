---
date: 2026-03-25
status: unread
relevance: A
tags: [opus-4.6, api, adaptive-thinking, compaction, fast-mode, model-retirement]
source_urls:
  - https://platform.claude.com/docs/en/about-claude/models/whats-new-claude-4-6
  - https://platform.claude.com/docs/en/release-notes/overview
experiment_dir: null
---

# Claude Opus 4.6 & API主要アップデート（Compaction, Fast Mode, モデル廃止）

## 3行要約

- Opus 4.6リリース（2/5）。128k出力トークン、1Mコンテキスト。Adaptive Thinking（`thinking: {type: "adaptive"}`）でモデルが思考の深さを動的に判断。effortパラメータがGA化（betaヘッダー不要）
- Context Compaction（サーバーサイド自動要約）で実質無限の会話が可能に。Fast Mode（`speed: "fast"`）でOpusの出力速度が最大2.5倍（プレミアム価格 $30/$150 per MTok）
- Sonnet 3.7（`claude-3-7-sonnet-20250219`）とHaiku 3.5（`claude-3-5-haiku-20241022`）が廃止済み。Sonnet 4.6 / Haiku 4.5への移行が必要

## 自分への関連度: A

Adaptive Thinkingはゲーム内AIの応答品質とコストのバランス調整に有用。Compactionは長時間のゲームセッションでのAI対話に使える。旧モデル廃止は既存のAPI呼び出しコードに影響するため、使用中なら即対応が必要。1MコンテキストのGA化と長文コンテキストプレミアム廃止（標準価格化）はコスト面で朗報。

## 試すなら

1. Opus 4.6でAdaptive Thinkingを試す: `thinking: {type: "adaptive"}` をリクエストに追加
2. effortパラメータをbetaヘッダーなしで送信できることを確認
3. 既存コードでSonnet 3.7/Haiku 3.5を使っている箇所がないか確認し、4.6/4.5に移行
4. Fast Modeの速度差を体感: `speed: "fast"` をOpusリクエストに追加

## ソース

- [What's new in Claude 4.6](https://platform.claude.com/docs/en/about-claude/models/whats-new-claude-4-6)
- [Claude Platform Release Notes](https://platform.claude.com/docs/en/release-notes/overview)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
