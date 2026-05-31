---
date: 2026-05-22
status: read
relevance: B
tags: [claude-managed-agents, mcp, sandbox, enterprise, security]
source_urls:
  - https://thenewstack.io/anthropic-mcp-tunnels-sandboxes/
  - https://releasebot.io/updates/anthropic
experiment_dir: null
---

# Claude Managed Agents、自前サンドボックス＋プライベート MCP トンネルに対応 — エージェントの実行環境と接続先を企業境界内に閉じ込める

## 3行要約

- Claude Managed Agents が、ユーザー側が管理するサンドボックス内で動き、社内のプライベート MCP サーバに接続できるように。ツールを実行する環境も、到達するサービスも、企業の既定境界内で動かせる。
- 自前サンドボックスは public beta。ツール実行を自社インフラや Cloudflare / Daytona / Modal / Vercel 等のマネージド環境へ移せる。MCP トンネルは research preview で、エージェントループ自体は Anthropic 側に置いたまま社内ネットワークの MCP に接続する。
- 狙いは企業がエージェントの実行とネットワークアクセスを統制下に置けるようにすること。AI エージェントのインフラをロックダウンする方向の動き。

## 自分への関連度: B

MCP（関心領域6）と Managed Agents（[[2026-04-09-claude-managed-agents-ant-cli]]、[[2026-04-24-claude-managed-agents-memory]]）の延長。個人開発では直接使わないが、「エージェントの実行環境とツール接続先を境界内に閉じる」という設計は、ローカルの MCP（Blender 連携など）やセキュリティ（関心領域3）を考えるときの参考になる。知識として押さえる。

## 詳細

- 自前サンドボックス＝コード実行の隔離先を選べる。MCP トンネル＝Anthropic 側のエージェントから社内 MCP へ安全に到達する経路。両者は別機能で成熟度も異なる（beta vs research preview）。
- エンタープライズ向けの統制機能だが、サプライチェーン／権限分離の観点は個人環境のセキュリティ自己点検（[[2026-04-06-claude-code-security-self-check]]）にも通じる。

## ソース

- [Anthropic debuts MCP tunnels and self-hosted sandboxes (The New Stack)](https://thenewstack.io/anthropic-mcp-tunnels-sandboxes/)
- [Anthropic Release Notes - May 2026 (Releasebot)](https://releasebot.io/updates/anthropic)

---

## 感想・考察

### 「自前サンドボックス」のメリットを巡る考察

自前のサンドボックス環境があるなら、その上で普通に Claude Code を回せばよいのでは？という疑問が出発点。

- 両者の違いは「自前環境に何を置くか」。Claude Code ローカル実行はエージェントループ（推論・オーケストレーション・メモリ・コンテキスト圧縮・リトライ）も実行も全部自前。Managed Agents + 自前サンドボックスは、ループは Anthropic に外注し、コード実行と MCP 接続先だけ自社境界に引き込む。
- なぜ後者か。危険なのも統制したいのも「実行とデータ接触面」の方（任意コード実行・社内ネットワークアクセス・機密データ接触）。頭脳（ループの保守、モデル追従）は自前で持っても旨味がないので外注。統制ポイントを「実行」と「MCP 接続」の2点に絞れるのが設計の狙い。
- 結論: 個人〜小規模なら Claude Code ローカル実行で十分。Managed Agents が効くのは、ヘッドレスで大量の自律エージェントを企業として運用しつつ実行環境とネットワーク到達先を社内境界に閉じたい「フリート運用」の場面。

### 「大量の自律エージェント」が必要な環境とは

現時点ではかなり限定的、という認識は妥当。需要は2系統。

1. 社内のエンジニアリング/業務自動化（今すでにある）: 大規模コードベースの保守（依存更新・脆弱性パッチ・PR レビューの並列実行）、チケット/サポート処理、SOC のアラートトリアージ、大量ドキュメント処理。ただし大企業の規模がないと「大量」にならず、中小では数体で足りるので旨味が薄い。
2. SaaS プロダクトへの組み込み（これから伸びる本命）: 自社プロダクトにエージェント機能を載せ、エンドユーザーごとにエージェントが起動するケース。ユーザー数 = 同時稼働エージェント数になり桁が変わる。各テナントのデータに触れる部分だけ統制したいので、前述の「ループは外・実行と接続先は内」の分業がちょうど効く。

Managed Agents は「今すでに需要がある所」より「SaaS にエージェントが標準搭載されていく流れの先」を狙った先行投資的インフラ。個人〜小規模開発では現状出番なしで正解。企業がエージェントをプロダクトに組み込み始めたら一気にスケールする領域として知識で押さえておく。

<!-- /try 実行時に自動生成 -->
