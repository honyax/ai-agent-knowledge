---
date: 2026-07-01
status: read
relevance: B
tags: [claude-code, gateway, enterprise, sso, bedrock, google-cloud, foundry, self-hosted]
source_urls:
  - https://claude.com/blog/introducing-the-claude-apps-gateway
  - https://code.claude.com/docs/en/claude-apps-gateway
  - https://devops.com/anthropic-adds-enterprise-gateway-to-simplify-claude-code-access-on-aws-and-google-cloud/
experiment_dir: null
---

# Claude Apps Gateway: Claude Code の self-hosted 制御プレーン (Bedrock / GCP / Foundry 対応)

## 3行要約

- Anthropic が Claude Code 向けの **self-hosted 制御プレーン** をリリース。SSO（OIDC）認証、RBAC、per-user コスト帰属、日/週/月のスペンド上限、provider failover（Claude API / Amazon Bedrock / Google Cloud / Microsoft Foundry の切替）を一元管理。
- アーキ: 単一の **stateless コンテナ**（Linux）+ **PostgreSQL** バックエンド。上流認証情報を保持し、開発者を IdP で認証、managed settings を配信、per-user usage を OTLP で自前 collector に送信。
- **推論トラフィックと usage データは Anthropic に送られない**（Claude API を使う設定にしない限り）。OSS 版 Docker、`gateway.yaml` に OIDC issuer と upstream credential を書き、IdP に OIDC アプリを 1 つ登録するだけで運用開始。

## 自分への関連度: B

自分は個人開発中心で SSO や RBAC を必要としないため直接利用機会は薄い。ただし [[2026-07-01-fable5-mythos5-export-lifted]] の「単一 provider 依存リスク」対応の実例として、および [[2026-05-22-claude-managed-agents-sandbox-mcp-tunnels]] の企業向けガバナンス整備の続編として知識として有用。将来 Anthropic Economic Index の技術職ユーザーが企業導入で対峙する形なので、質問が来た時のリファレンス。

## 詳細

### 何を解決するか

- 従来「Claude Code を Bedrock / GCP で使う」には、各 CSP の IAM / KMS / VPC 設定を個別に組む必要があった。
- Gateway はそれを **1 つのコンテナで抽象化**し、企業の IdP と直結。
- Bedrock で使うか GCP で使うか Anthropic 直接 API にするかを **ランタイムで切替**（failover）できる。

### アーキテクチャ

- **単一 stateless コンテナ**（Linux, x86_64/arm64）+ PostgreSQL
- 開発者は Claude Code CLI をそのまま使用、`ANTHROPIC_BASE_URL` を gateway に向けるだけ。
- gateway が (1) OIDC で認証 (2) upstream credential で AWS/GCP/Anthropic に proxy (3) OTLP で usage を collector に送信。
- **推論トラフィックは Anthropic に送信されない**（Bedrock/GCP 経由の場合）。企業のデータ境界内で完結。

### 主要機能

| カテゴリ | 内容 |
|---------|------|
| 認証 | OIDC（Okta, Entra ID, Auth0, Google, GitHub 等） |
| 認可 | RBAC。組織/グループ/ユーザー単位の権限 |
| モデルアクセス | per-group で使えるモデルを制限（例: 特定チームのみ Opus 4.8） |
| コスト管理 | 日/週/月 のスペンド上限。組織/グループ/ユーザー粒度 |
| Provider failover | Claude API ↔ Bedrock ↔ GCP ↔ Foundry の自動切替 |
| Telemetry | OTLP で自前 collector（Datadog, Grafana, Splunk 等）に送信 |
| Managed settings | Claude Code の settings.json を中央配信 |

### セットアップ

```yaml
# gateway.yaml (概要)
auth:
  oidc:
    issuer: https://your-idp.example.com
upstream:
  provider: bedrock  # or google_cloud, foundry, anthropic
  credential: aws-role-arn:...
spend_limits:
  default:
    daily_usd: 50
    monthly_usd: 500
```

- CLI: `docker run anthropic/claude-apps-gateway`
- IdP 側: OIDC アプリを 1 つ登録、gateway の redirect URL を指定
- Claude Code 側: `claude config set gateway <url>`

### 位置づけ

- **Enterprise 向け**が明確。個人・小規模開発では過剰。
- Anthropic の Vertex/Bedrock ネイティブ課金（[[2026-04-08-anthropic-30b-revenue-google-tpu]] 系）を尊重しつつ、SSO とガバナンスだけ Anthropic 側で提供する形。
- Microsoft Foundry ([[2026-04-08-anthropic-30b-revenue-google-tpu]]) 対応も含まれ、3 大クラウド全てに対応。

## 試すなら

1. 個人利用では過剰なので、企業導入相談があった時のリファレンスとして [claude.com/blog/introducing-the-claude-apps-gateway] を参照できるようブックマーク。
2. 自分の Unity 案件で複数の Claude Code を並列運用する場合、gateway で「テスト用モデル」「本番用モデル」の分離ができるか実験（ローカル PostgreSQL で気軽に立てられるはず）。
3. Failover 機能を activated し、意図的に Anthropic API を落として Bedrock / GCP にフェイルオーバーする挙動を検証（[[2026-07-01-fable5-mythos5-export-lifted]] のリスク対策デモとして）。
4. OTLP → Grafana でトークン消費量の per-project 可視化ができるか試す。RTK/Headroom の効果測定を集約する土台に。

## ソース

- [Introducing the Claude apps gateway (Anthropic 公式)](https://claude.com/blog/introducing-the-claude-apps-gateway)
- [Claude apps gateway - Claude Code Docs](https://code.claude.com/docs/en/claude-apps-gateway)
- [Anthropic Adds Enterprise Gateway to Simplify Claude Code Access (DevOps.com)](https://devops.com/anthropic-adds-enterprise-gateway-to-simplify-claude-code-access-on-aws-and-google-cloud/)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
