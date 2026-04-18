---
date: 2026-04-17
status: read
relevance: A
tags: [mcp, security, vulnerability, supply-chain]
source_urls:
  - https://www.theregister.com/2026/04/16/anthropic_mcp_design_flaw/
  - https://www.cyberkendra.com/2026/04/anthropics-mcp-design-flaw-enables.html
  - https://www.techradar.com/pro/security/this-is-not-a-traditional-coding-error-experts-flag-potentially-critical-security-issues-at-the-heart-of-anthropics-mcp-exposes-150-million-downloads-and-thousands-of-servers-to-complete-takeover
experiment_dir: null
---

# MCPの設計上の脆弱性が200K+サーバーを危険にさらす — Anthropicは修正を拒否

## 3行要約

- OX Security が MCP の STDIO トランスポート層に設計上の根本的欠陥を発見。200,000以上のサーバーで任意OSコマンド実行が可能になりうる。
- LangFlow / LiteLLM / GPT Researcher など多数の OSS に波及し、10件超の High/Critical CVE が発行済み。
- Anthropicは「仕様通りの動作」と回答し、プロトコルのアーキテクチャ変更を拒否。セキュリティドキュメントの注意書き更新のみで対応。

## 自分への関連度: A

MCP を使った Blender 連携や今後のツール統合に直接関わるセキュリティリスク。特にサードパーティ製 MCP サーバーを使う場合は信頼性確認が必須。

## 詳細

### 脆弱性の仕組み
MCP の STDIO トランスポートは、AIアプリがMCPサーバーをサブプロセスとして起動する仕組み。しかしこのメカニズムは任意のOSコマンドを実行でき、コマンドが STDIO サーバーを正常に起動すればハンドルを返し、失敗すればエラーを返すが、**コマンド自体は実行されてしまう**。

### 影響範囲
- Shodan で確認された公開脆弱サーバー: 7,374台
- 推定被影響サーバー: 200,000+
- MCP SDK ダウンロード数: 月間 9700万（2026年3月時点）
- 影響OSS: LangFlow, LiteLLM, GPT Researcher, IBM low-code AI framework, など

### Anthropicの対応
- 2026年1月7日に脆弱性通知を受領
- 9日後に「expected behavior（仕様通り）」と回答
- プロトコルのアーキテクチャ変更なし
- セキュリティドキュメントに「STDIOアダプタの慎重な使用」を追記するのみ

### 関連CVE
- CVE-2025-59536, CVE-2026-21852 (Claude Code プロジェクトファイル経由のRCEとAPIトークン漏洩、Check Point Research)
- CVE-2025-65720 (GPT Researcher)
- CVE-2025-49596 (MCP Inspector RCE)

## 試すなら

1. 使用中の MCP サーバーのバージョンを確認し、CVE の影響対象かチェック
2. 信頼できないソースからの MCP サーバーはローカル実行を避ける
3. MCP STDIO 使用時は最小権限原則（サンドボックス、コンテナ化）を適用
4. Blender MCP 連携のセキュリティ設定を見直す

## ソース

- [Anthropic won't own MCP 'design flaw' putting 200K servers at risk — The Register](https://www.theregister.com/2026/04/16/anthropic_mcp_design_flaw/)
- [Anthropic's MCP Design Flaw Enables RCE Across 200K+ Servers — Cyber Kendra](https://www.cyberkendra.com/2026/04/anthropics-mcp-design-flaw-enables.html)
- [Critical security issues at the heart of Anthropic's MCP — TechRadar](https://www.techradar.com/pro/security/this-is-not-a-traditional-coding-error-experts-flag-potentially-critical-security-issues-at-the-heart-of-anthropics-mcp-exposes-150-million-downloads-and-thousands-of-servers-to-complete-takeover)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
