---
date: 2026-03-25
status: unread
relevance: A
tags: [claude-code, mcp, elicitation, hooks, interactive-dialog]
source_urls:
  - https://code.claude.com/docs/en/changelog
  - https://qiita.com/AI-SKILL-LAB/items/aa6d96f7d18644ff95a1
experiment_dir: null
---

# MCP Elicitation — MCPサーバーがセッション中にユーザー入力を要求可能に

## 3行要約

- MCPサーバーがタスク実行中にユーザーへ構造化入力を要求できる「Elicitation」機能が追加。フォームフィールドやブラウザURL表示による対話的なワークフローが可能に
- 新しいフック `Elicitation` と `ElicitationResult` で、Elicitationリクエストの傍受・応答の上書きが可能。自動化パイプラインとの統合に使える
- Claude Code v2.1.76以降で利用可能。MCPサーバー側の対応も必要

## 自分への関連度: A

MCPサーバー開発時に、ユーザー確認や追加情報の入力をセッション中に求められるようになる。Blender MCPやUnity MCPでの利用シーン（例: マテリアル選択の確認ダイアログ）が考えられる。Hooksと組み合わせることで、CI/CDパイプラインでの承認フローにも応用可能。

## 試すなら

1. Claude Code を最新版に更新
2. Elicitation対応のMCPサーバーのサンプルを探す or 自作する
3. `Elicitation` フックを `.claude/settings.json` に設定して動作を確認
4. Blender MCP等で実際にElicitationが発火するケースを検証

## ソース

- [Claude Code Changelog](https://code.claude.com/docs/en/changelog)
- [Claude Code v2.1.76 完全ガイド 2026年3月版](https://qiita.com/AI-SKILL-LAB/items/aa6d96f7d18644ff95a1)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
