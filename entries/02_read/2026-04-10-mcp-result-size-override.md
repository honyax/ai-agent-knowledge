---
date: 2026-04-10
status: read
relevance: A
tags: [mcp, claude-code, api, context]
source_urls:
  - https://github.com/anthropics/claude-code/releases
  - https://code.claude.com/docs/en/changelog
experiment_dir: null
---

# MCP ツール結果サイズ上限の拡張 — `_meta["anthropic/maxResultSizeChars"]` で最大500K

## 3行要約

- MCP ツール結果に `_meta["anthropic/maxResultSizeChars"]` アノテーションを付けることで、最大500K文字まで結果を渡せるようになった
- DBスキーマ・大型ファイルツリー等の大容量レスポンスがトークンベースの永続化レイヤーで切り捨てられずに通過できる
- Claude Code の MCP ツール呼び出し結果の「context pollution」問題への対策として活用可能

## 自分への関連度: A

MCP サーバーを自作・カスタマイズする際に使える機能。DBスキーマ取得やファイルツリー全体をMCPで渡す場合に実用的。既存のMCPサーバーに追加するだけで機能する。

## 詳細

### 概要

Claude Code の MCP ツール結果はデフォルトでサイズ制限があり、大きな結果は切り捨てられる場合がある。今回の更新で、ツール側から「このレスポンスは大きいので上限を拡張してほしい」と明示できるようになった。

### 使い方

MCP ツールのレスポンスの `_meta` フィールドに以下を追加:

```json
{
  "_meta": {
    "anthropic/maxResultSizeChars": 500000
  },
  "content": "...大量のテキスト..."
}
```

上限は最大 500,000 文字（500K）。

### 活用例

- **DBスキーマ取得**: テーブル定義・カラム型・インデックス情報など大量のスキーマをまとめてClaude Codeに渡す
- **ファイルツリー**: プロジェクト全体の構造を一括で提供
- **コードベース索引**: 検索結果・シンボル一覧など大きなコンテキスト

### 注意

- バグ修正として「`_meta["anthropic/maxResultSizeChars"]` がトークンベースのpersistレイヤーをバイパスしない問題」も同時修正済み → 正しく動作するようになった

## 試すなら

1. 自作MCPサーバーまたは設定中のMCPサーバーのレスポンス部分を確認
2. 大きなレスポンスが想定される場所に `_meta["anthropic/maxResultSizeChars"]` を追加（値: 最大500000）
3. Claude Code でそのMCPツールを呼び出し、結果が切り捨てられずに届くか確認
4. DBスキーマ取得系のMCPサーバーに適用して実用性を検証

## ソース

- [Claude Code Releases](https://github.com/anthropics/claude-code/releases)
- [Claude Code Changelog](https://code.claude.com/docs/en/changelog)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
