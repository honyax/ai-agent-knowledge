---
date: 2026-03-29
status: unread
relevance: A
tags: [claude-api, compaction, context-management, infinite-conversation, opus-46]
source_urls:
  - https://platform.claude.com/docs/en/build-with-claude/compaction
  - https://platform.claude.com/cookbook/tool-use-automatic-context-compaction
  - https://dev.to/ayyazzafar/claudes-context-compaction-api-infinite-conversations-with-one-parameter-515f
experiment_dir: null
---

# Claude API Compaction: サーバーサイド自動要約で実質無限の会話を実現

## 3行要約

- Claude APIに「Compaction」機能がベータ導入され、コンテキストウィンドウの上限に近づくと自動的に古い会話内容を要約・圧縮する
- 1パラメータの追加(compact_20260112 strategy)で有効化でき、要約前のメッセージブロックは自動的に削除される
- 現時点ではClaude Opus 4.6のみ対応。OpenAIやGeminiに同等機能はなく、Claude独自の優位性

## 自分への関連度: A

カードバトルゲームにClaude APIを統合する場合、長時間のゲームセッションでコンテキスト管理が課題になる。Compactionはこの問題を自動的に解決できる可能性がある。また、Claude Agent SDKでの長時間タスクにも有用。

## 詳細

### 有効化方法
- ベータヘッダー `compact-2026-01-12` をAPIリクエストに含める
- `context_management.edits` に `compact_20260112` strategyを設定
- トリガー条件(トークン閾値)のカスタマイズが可能

### 動作
- コンテキストが設定した閾値に近づくと、Claudeが自動的に古い会話部分を要約
- 要約ブロック以前のメッセージは全て削除され、要約から会話が継続
- 圧縮後に手動介入のためのポーズを設定するオプションもあり

### 制限
- ベータ版(2026年1月12日リリース、3月時点でも継続中)
- Claude Opus 4.6のみサポート
- 要約の品質はモデル依存、重要な詳細が失われるリスクあり

## 試すなら

1. Claude API のベータヘッダー `compact-2026-01-12` を追加してリクエストを送信
2. `context_management.edits` に `compact_20260112` を設定
3. 長い会話を模擬して圧縮が発生するタイミングを確認
4. 圧縮前後のレスポンス品質を比較する

## ソース

- [Compaction - Claude API Docs](https://platform.claude.com/docs/en/build-with-claude/compaction)
- [Automatic context compaction - Cookbook](https://platform.claude.com/cookbook/tool-use-automatic-context-compaction)
- [Claude's Context Compaction API - DEV Community](https://dev.to/ayyazzafar/claudes-context-compaction-api-infinite-conversations-with-one-parameter-515f)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
