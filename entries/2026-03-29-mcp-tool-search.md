---
date: 2026-03-29
status: unread
relevance: S
tags: [claude-code, mcp, tool-search, lazy-loading, token-optimization, context-window]
source_urls:
  - https://www.atcyrus.com/stories/mcp-tool-search-claude-code-context-pollution-guide
  - https://medium.com/@joe.njenga/claude-code-just-cut-mcp-context-bloat-by-46-9-51k-tokens-down-to-8-5k-with-new-tool-search-ddf9e905f734
  - https://venturebeat.com/orchestration/claude-code-just-got-updated-with-one-of-the-most-requested-user-features
experiment_dir: null
---

# MCP Tool Search: ツール定義の遅延読み込みでトークン使用量85%削減

## 3行要約

- Claude CodeにMCP Tool Searchが導入され、全ツール定義を事前読み込みする代わりにオンデマンドで必要なツールだけを取得する仕組みになった
- 内部テストではトークン使用量が134kから5kへ約85%削減。実運用でも51kから8.5kへ46.9%の削減が確認されている
- ツール定義がコンテキストの10%を超えると自動的にTool Searchモードに切り替わり、キーワード検索で3-5個の関連ツール(約3Kトークン)だけが読み込まれる

## 自分への関連度: S

MCP サーバーを複数使っている環境ではコンテキスト圧迫が実際の課題。Blender MCP等を併用するワークフローで即効果が出る。全ユーザーにデフォルト有効なので設定変更不要。

## 詳細

### 仕組み
- Claude Codeがコンテキスト使用量を監視し、ツール記述がコンテキストウィンドウの10%を超えた場合に軽量な検索インデックスに自動切り替え
- Claudeがツールを必要とする際、キーワードで検索し、関連する3-5個のツール定義(約3Kトークン)だけがロードされる
- 全ユーザーにデフォルトで有効化済み、オプトイン不要

### 効果
- Anthropic内部テスト: 134k -> 5k トークン(85%削減)
- 実運用事例: 51k -> 8.5k トークン(46.9%削減)
- MCP サーバーを多数接続している環境ほど効果が大きい

## 試すなら

1. Claude Code を最新版にアップデートする
2. 複数のMCPサーバーを接続した状態でセッションを開始する
3. ToolSearchツールが自動的に使われていることを確認する(ツール呼び出しログで確認可能)
4. 以前の同等タスクとトークン使用量を比較する

## ソース

- [MCP Tool Search Guide](https://www.atcyrus.com/stories/mcp-tool-search-claude-code-context-pollution-guide)
- [Claude Code Just Cut MCP Context Bloat by 46.9%](https://medium.com/@joe.njenga/claude-code-just-cut-mcp-context-bloat-by-46-9-51k-tokens-down-to-8-5k-with-new-tool-search-ddf9e905f734)
- [Claude Code updated with most-requested feature - VentureBeat](https://venturebeat.com/orchestration/claude-code-just-got-updated-with-one-of-the-most-requested-user-features)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
