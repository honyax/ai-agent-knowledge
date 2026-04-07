---
date: 2026-04-07
status: archived
relevance: B
tags: [mcp, security, enterprise, aaif, anthropic]
source_urls:
  - https://thenewstack.io/mcp-maintainers-enterprise-roadmap/
  - https://startupnews.fyi/2026/04/07/mcp-maintainers-from-anthropic-aws-microsoft-and-openai-lay-out-enterprise-security-roadmap-at-dev-summit/
  - https://aaif.io/
---

# MCP Dev Summit: Anthropic・AWS・Microsoft・OpenAI がエンタープライズセキュリティロードマップを発表

## 3行要約

- MCP Dev Summit NY で Anthropic・AWS・Microsoft・OpenAI のメンテナーが MCP のエンタープライズセキュリティロードマップを公表した
- MCP は Linux Foundation 傘下の Agentic AI Foundation（AAIF）に寄贈済みで、現在170メンバー・月9700万 SDK ダウンロード規模に成長
- セキュリティ研究では「認証の複雑さ」「リソース増幅ループ」「脆弱クライアント設定」が主要リスクとして挙げられている

## 自分への関連度: B

MCP をツール連携で使っている場合の将来的なエンタープライズ対応・セキュリティ動向として知識として押さえておく価値がある。直接の実装変更は不要。

## 詳細

**AAIF の現状:**
- MCP・goose・AGENTS.md が貢献プロジェクトとして発足（2025年12月）
- 2026年4月時点で170メンバー
- MCP は月間9700万 SDK ダウンロード（2025年11月ローンチ時は約200万）

**Dev Summit のフォーカスエリア:**
- プロトコル進化・適合テスト・セキュリティ研究
- 本番デプロイの知見共有・スケーラブルなエージェントシステム設計
- 95件以上のセッション

**現在のセキュリティ課題（2026年4月時点の研究より）:**
- ステルスなリソース増幅ループ
- リモートデプロイでの複雑な認証問題
- 脆弱なクライアント設定

## 試すなら

1. AAIF のサイト（aaif.io）で MCP のガバナンス方針を確認する
2. 自作 MCP サーバーのセキュリティ設定（認証・レート制限）を見直す

## ソース

- [MCP maintainers lay out enterprise security roadmap - The New Stack](https://thenewstack.io/mcp-maintainers-enterprise-roadmap/)
- [Agentic AI Foundation (AAIF)](https://aaif.io/)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
