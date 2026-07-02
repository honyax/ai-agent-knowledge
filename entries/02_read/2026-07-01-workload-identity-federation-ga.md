---
date: 2026-07-01
status: read
relevance: B
tags: [wif, oidc, security, api-key, enterprise, claude-api, credential-rotation]
source_urls:
  - https://claude.com/blog/workload-identity-federation
  - https://platform.claude.com/docs/en/manage-claude/workload-identity-federation
  - https://imisofts.com/blog/anthropic-workload-identity-federation-no-api-keys-news-june-19-2026/
  - https://aembit.io/blog/anthropic-workload-identity-federation-what-it-gets-right-and-what-it-still-doesnt-solve/
experiment_dir: null
---

# Workload Identity Federation (WIF) GA: 静的 API キーを短期 OIDC トークンで置き換え

## 3行要約

- 6/17、Anthropic は **Workload Identity Federation (WIF)** を Claude Platform で GA。静的な `sk-ant-...` API キーの代わりに、リクエスト時に発行される **短命 OIDC トークン**で認証可能に。
- 対応 IdP: AWS IAM ロール、GCP / Kubernetes サービスアカウント、Azure managed identity、GitHub Actions token、Okta、その他 OIDC 準拠 IdP 全般。Anthropic 側に static credential を「作らない・回転しない・漏らさない」運用が可能。
- 既存 API キーとの共存もサポート。1 automation ずつ移行できる。**リクエストごとに named service account がひもづく**ため、per-automation 監査ログが取れるようになった（従来「共有 1 キー」ではどのスクリプトが叩いたか分からなかった）。

## 自分への関連度: B

自分は個人開発でローカル + 個人 API キーの運用なので即座に WIF に置き換える強い動機はない。だが CLAUDE.md の関心領域 3（AI ツールのセキュリティリスクと対策）に該当し、[[2026-05-16-claude-code-v21140-v21143]] で `ANTHROPIC_WORKSPACE_ID` として一度出ていた仕組みが GA した続報。企業導入相談時のリファレンス、および GitHub Actions ワークフローに Claude を組み込む場合に価値が出る。

## 詳細

### 何が変わったか

- **旧**: `sk-ant-...` の長期キーを 1 つ発行、環境変数 / secret に保存、必要時に手で回転。
- **新 (WIF)**: 各ワークロードが既に持っている ID（AWS IAM ロール、GCP SA 等）から **短命 OIDC トークン**を取り、Claude API に提示。Anthropic 側は Federation trust に基づき承認。static credential は Anthropic 側に不要。

### 対応 IdP

- **AWS**: IAM ロール（EC2 / EKS / Lambda / GitHub Actions OIDC）
- **GCP**: サービスアカウント（GKE / Cloud Run / Cloud Functions）
- **Azure**: Managed Identity（AKS / Functions / Container Apps）
- **Kubernetes**: サービスアカウント（Projected token）
- **GitHub Actions**: OIDC token（`id-token: write`）
- **Okta / Auth0 / その他**: OIDC-compliant なら基本 OK

### セキュリティ効果

- **回転不要**: 短命トークン（デフォルト 1h 程度）なので、通常運用でキーローテーションが不要。
- **漏洩リスク低下**: 漏れても短時間で失効。
- **監査可能**: 各リクエストに named service account がひもづくため、per-automation の audit trail が取れる。旧 API キーは「共有 1 キー」で誰が叩いたか区別不能だった。
- **DevSecOps**: Vault / KMS でキー管理していた手間が消える。

### 移行

- 既存 API キーはそのまま動く（共存可）。
- 新規 automation から key-free で開始、旧スクリプトは移行タイミングで置き換え。
- 1 automation ずつ段階移行可能。

### 「まだ解決していないこと」（Aembit の分析）

- **Anthropic 以外のプロバイダに横展開できない**: Claude 用の WIF は Anthropic 専用。同じワークロードが Claude と OpenAI と Bedrock 全てを叩く場合、それぞれ別の federation trust 設定が必要。
- **細粒度の scope 制御**: モデル別 / rate limit 別の scope はまだ大まかで、fine-grained な RBAC は今後の課題。
- **秘密ゼロ**にはならない: IdP 側の trust 設定は Anthropic に登録する必要があり、そこは注意深く管理。

### Claude Apps Gateway との関係

- [[2026-07-01-claude-apps-gateway-bedrock-vertex]] は self-hosted 制御プレーンで、内部で WIF を使えば「gateway 内部の Anthropic 認証も key-free」にできる。相性◎。

## 試すなら

1. まず自分の運用（個人 API キー 1 本）では WIF 導入コスト >メリット なので、当面は monitoring のみ。
2. GitHub Actions で Claude を動かすワークフローを組む予定があれば、GH Actions OIDC → Anthropic WIF 経路を試す（キーを secrets に置かない設計）。
3. `ANTHROPIC_WORKSPACE_ID` ([[2026-05-16-claude-code-v21140-v21143]]) を使った workspace scoping と WIF の service account を組み合わせ、Claude Code から発行されたリクエストを workspace 単位で識別できるか確認。
4. 学習用に AWS IAM ロール → WIF の最小構成をローカル / ダミーアカウントで組み、動作を体験（IdP 側 trust 設定の実感を得る）。
5. 企業導入相談時に「WIF あります、キーローテーション不要です」を提示できるよう公式 doc のブックマーク。

## ソース

- [Workload Identity Federation (WIF) is now generally available (Anthropic 公式)](https://claude.com/blog/workload-identity-federation)
- [Workload Identity Federation - Claude Platform Docs](https://platform.claude.com/docs/en/manage-claude/workload-identity-federation)
- [Anthropic Makes Static API Keys Optional With WIF (Imisofts)](https://imisofts.com/blog/anthropic-workload-identity-federation-no-api-keys-news-june-19-2026/)
- [Anthropic WIF: What It Gets Right and What It Still Doesn't Solve (Aembit)](https://aembit.io/blog/anthropic-workload-identity-federation-what-it-gets-right-and-what-it-still-doesnt-solve/)

---

## 感想・考察

### 「よく分からないけど関係なさそう」の判断は妥当（2026-07-03）

一言で言うと「**API キーを使い捨ての短命トークンに置き換える企業向けの認証強化機能**」。

**解決している問題**: 従来は `sk-ant-...` の長期キーを 1 つ発行し、環境変数/設定ファイルに保存して使い回す運用だった。漏洩リスクや、企業でのキーローテーションの手間があった。

**WIF の仕組み**: 「AWS や GCP に既にログインしている状態」を使い、Claude API 用の短時間（~1h）だけ有効なトークンをその場で自動発行。漏れても被害が小さく、「どの自動化スクリプトが呼んだか」の監査ログも取れる。

**関係なさそうという判断が妥当な理由**:

- 企業のクラウドインフラ運用（AWS/GCP で複数サービスが Claude API を叩く、チームで共有）向けの機能
- 個人開発でローカル環境から Claude Code を 1 人で使う分にはキー 1 本の運用で十分。ローテーションの手間もほぼない
- 唯一の接点は GitHub Actions での自動化だが、今すぐの計画ではない

### 結論

当面は monitoring のみで良い。GitHub Actions で Claude を組み込むワークフローを実際に作る計画が出てきたタイミングで再訪する。企業導入相談を受けた際の「WIF あります」という引き出しとして記憶に留める程度。

<!-- /try 実行時に自動生成 -->
