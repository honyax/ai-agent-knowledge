---
date: 2026-05-12
status: read
relevance: B
tags: [anthropic, aws, claude-platform, bedrock, sigv4, iam, managed-agents, ga, enterprise]
source_urls:
  - https://dev.classmethod.jp/articles/claude-platform-on-aws-ga-setup/
  - https://claude.com/blog/claude-platform-on-aws
  - https://aws.amazon.com/blogs/machine-learning/introducing-claude-platform-on-aws-anthropics-native-platform-through-your-aws-account/
  - https://aws.amazon.com/about-aws/whats-new/2026/05/claude-platform-aws/
  - https://thenewstack.io/anthropics-claude-platform-comes-to-aws/
experiment_dir: null
---

# Claude Platform on AWS GA（2026-05-11） — AWS が「Bedrock とは別の Anthropic ネイティブ Platform」窓口になった

## 3行要約

- 2026-05-11、Anthropic と AWS が **Claude Platform on AWS** の GA を発表。**AWS は Anthropic の Claude Platform を直接提供する最初のクラウド事業者**。15 リージョン（北米3・カナダ1・南米1・欧州6・APAC4）で同時提供開始
- 仕組み: AWS 経由でサブスクライブするが、**サービス自体は Anthropic 運営。顧客データは AWS セキュリティ境界の外で処理**。Base URL は `aws-external-anthropic.{region}.api.aws`、認証は **API key（12時間／365日）と SigV4（IAM 連携）** の2方式。モデル ID は Anthropic 標準（`claude-sonnet-4-6` 等、Bedrock の `anthropic.` プレフィックスなし）。料金は **Claude API 直接と同単価**、AWS 統合請求
- Bedrock との違い: Bedrock は **AWS 運用** で IAM・SageMaker と深く統合、データは AWS 境界内、リージョン residency 重視。Claude Platform on AWS は **Anthropic 運用** で Managed Agents・Web Search・Files API・MCP コネクタ等の **「Claude Platform 限定機能」をフル装備**。HIPAA・Fast mode 非対応、ワークスペースはリージョンあたり1つ

## 自分への関連度: B

自分は AWS で本番運用していないため即実践性は低い。ただし関心領域 5（Claude API 変更・新モデル）と 8（Anthropic の方針）として把握すべき重要な戦略動向:

- **「Bedrock 経由 vs Anthropic 直接」の二択が「Bedrock vs Claude Platform on AWS vs Anthropic API 直接」の三択に**: 企業向け選択肢が増え、料金・機能・データ境界のトレードオフ表が必要になった
- **Managed Agents / Web Search / Files API / MCP コネクタが AWS 内で使える**: これまで「Bedrock では使えない上位機能」だったものが AWS 経由でアクセス可能に。エンタープライズ採用障壁が下がる
- **SigV4 認証対応**: API key を長期保管したくない本番運用シナリオで、AWS IAM ロールベースの細粒度制御が可能。CVE-2026-21852（`ANTHROPIC_BASE_URL` 改ざんによる API キー漏洩、2026-05-04 Agent SDK エントリ）対策としても有効

将来的に自分のサイドプロジェクト（Unity + AI 等）をクラウド展開する時、AWS スタックを使うなら **Claude Platform on AWS が第一候補**になりうる。Anthropic 関連エントリ（compute サプライヤ戦略：2026-04-08 Google TPU、2026-05-10 SpaceX Colossus）とも整合する。

## 詳細

### 提供開始

- **発表日**: 2026-05-11
- **対象モデル**: Claude Opus 4.7・Sonnet 4.6・Haiku 4.5（以降の新モデルも同時提供予定）
- **対象リージョン**: 15 リージョン（北米3・カナダ1・南米1・ヨーロッパ6・アジア太平洋4）

### セットアップ手順（classmethod 検証、15〜20分）

1. AWS コンソールから Claude Platform サービスを開始
2. AWS Marketplace サブスクリプションが自動化される
3. Claude Console（Anthropic 提供）に組織情報を入力
4. メール確認 → Workspace 作成
5. API key を生成（または IAM ロールで SigV4 認証を設定）

### 認証方式

| 方式 | 用途 | 特徴 |
|------|------|------|
| API key（12時間） | 短期セッション | 自動失効でセキュア |
| API key（365日） | 長期保管用 | アプリ組込み |
| **SigV4 (IAM)** | 本番運用推奨 | API key を持たず、IAM ロールで細粒度制御 |

### 主要 API・機能

- **Messages API**（Claude API 標準）— Bedrock の専用形式ではなくそのまま使える
- **Files API**
- **Message Batches API**
- **Claude Managed Agents**（Code w/ Claude 2026 で Dreaming・Outcomes・Multi-agent orchestration 拡張）
- **Agent Skills**
- **Code execution（コード実行ツール）**
- **Tool use** 全般
- **Claude Console**: prompt improver / prompt generator / evaluation tools

### Claude Code / Claude Cowork からの利用

公式 AWS ML ブログに「**Claude Code, Claude Cowork, or any other API client** を workspace に向けられる」と明記。環境変数 2 つで切り替え可能:

```bash
export ANTHROPIC_BASE_URL=https://aws-external-anthropic.<region>.api.aws
export ANTHROPIC_CUSTOM_HEADERS='{"anthropic-workspace-id":"<workspace-id>"}'
```

既存の Claude Code on Bedrock（`CLAUDE_CODE_USE_BEDROCK=1`）との差:

| 観点 | Bedrock 経由 | Claude Platform on AWS 経由 |
|---|---|---|
| 機能ラグ | Bedrock 側対応待ち（Files API・Skills・Web Search 等が制限） | Anthropic 本家と同等、新機能即時 |
| 設定 | `CLAUDE_CODE_USE_BEDROCK=1` | `ANTHROPIC_BASE_URL` + `ANTHROPIC_CUSTOM_HEADERS` |
| 請求 | AWS | AWS 統合請求 |

→ **Anthropic と別契約せず AWS アカウントだけで Claude Code をフル機能で導入可能**。エンタープライズの調達障壁が大幅に下がる。

### 制限事項（classmethod 検証）

- **HIPAA 非対応**（医療用途は Bedrock 推奨）
- **Fast mode 非対応**
- **ワークスペース: リージョンあたり1つ**
- **推論ジオ**: `inference_geo` パラメータが `global`・`us` のみ対応、`jp` は 400 エラー（GA 開始時点）

### 料金

- Claude API 直接と同単価
- AWS 統合請求にロールアップ
- AWS のコミットメント（Savings Plan 等）の対象になるかは公式記載なし（要 ASD ガイダンス）

### Bedrock との明確な違い

| 項目 | Bedrock | Claude Platform on AWS | Claude API 直接 |
|------|---------|------------------------|-----------------|
| 運用元 | AWS | Anthropic | Anthropic |
| データ処理 | AWS 境界内 | AWS 境界外 | Anthropic 境界 |
| 認証 | IAM | IAM (SigV4) + API key | API key |
| 課金 | AWS | AWS（統合請求） | Anthropic |
| HIPAA | 対応 | 非対応 | 対応プランあり |
| Managed Agents | 一部のみ | フル装備 | フル装備 |
| Web Search / Files API / MCP コネクタ | 限定 | フル装備 | フル装備 |
| モデル ID | `anthropic.claude-...` | `claude-sonnet-4-6` 等 | `claude-sonnet-4-6` 等 |
| リージョン残留要件 | あり | なし（global/us） | なし |

### 業界文脈

- **AWS が "first cloud" として Anthropic Platform 窓口を取った** ことは、Microsoft Azure OpenAI Service 並みの戦略ポジション
- 2026-05-10 の SpaceX Colossus 提携・2026-04-08 の Google TPU 契約と並行して、Anthropic は **「単独クラウド依存しない compute スタック」＋「複数クラウドでの platform 展開」** という両建てを進めている

## 試すなら

（自分は AWS 本番運用なしのため実践予定なし。将来 AWS スタック導入時に第一候補として検討）

1. AWS アカウント上で Claude Platform サブスクライブ（無料枠なしのため評価用 IAM 分離推奨）
2. SigV4 認証で `aws-external-anthropic.{region}.api.aws` を叩く最小コードを書く
3. Claude Console から Managed Agents をひとつ試作し、AWS 統合請求への計上を確認
4. Bedrock 既存統合と比較してデータフロー・レイテンシを測る
5. CloudTrail で API 呼び出しが記録されることを確認（監査要件の検証）

## ソース

- [Claude Platform on AWS がGA。セットアップとAPI呼び出しを試してみた（DevelopersIO / classmethod）](https://dev.classmethod.jp/articles/claude-platform-on-aws-ga-setup/)
- [Introducing the Claude Platform on AWS（Claude 公式ブログ）](https://claude.com/blog/claude-platform-on-aws)
- [Introducing Claude Platform on AWS（AWS ML ブログ）](https://aws.amazon.com/blogs/machine-learning/introducing-claude-platform-on-aws-anthropics-native-platform-through-your-aws-account/)
- [Claude Platform on AWS is now generally available（AWS What's new）](https://aws.amazon.com/about-aws/whats-new/2026/05/claude-platform-aws/)
- [Anthropic's Claude Platform comes to AWS（The New Stack）](https://thenewstack.io/anthropics-claude-platform-comes-to-aws/)

---

## 感想・考察

### 「AWS だけで Claude が使える」の実体

Anthropic と直接契約せず、**AWS Marketplace 経由のサブスクリプションだけ**で Claude Code・Cowork・Messages API・Managed Agents・Skills・Files API などを本家とほぼ同等に使える。エンタープライズの調達では「Anthropic との個別契約交渉」が消えるだけで導入障壁が一段下がる。

### 組織にとってのシナジー（大きい）

- **IAM 権限統合**: 「Junior は Haiku のみ」「本番ロールは Opus 不可」を宣言的に管理。API key を Secrets Manager に詰めて配る運用が不要に
- **CloudTrail 監査証跡**: SOC2・ISO27001 監査で「誰がいつどのモデルを叩いたか」を即出せる
- **データパイプライン**: S3 → Lambda → Files API → Step Functions のような AWS 完結のエージェントワークフローが組める
- **コスト管理統合**: Cost Explorer・部門タグ・チャージバックが既存 AWS 仕組みに乗る
- **Bedrock 併用**: HIPAA 要件は Bedrock、最新機能は Claude Platform、と同一アカウント内で棲み分け

### 個人（特に自分）にとってのシナジー（ほぼなし）

- 料金は Anthropic 直と同単価 → 金銭メリットなし
- **Fast mode 非対応**が痛い。Claude Code 日常使いだと `/fast`（Opus 4.6）が使えないのは実用上の制約
- **`inference_geo: jp` 非対応**（GA 時点） → 日本リージョン最適化不可
- API key を `~/.claude.json` に貼るだけの Anthropic 直契約に対して、AWS Marketplace サブスク + Workspace 作成 + IAM 設定（15-20分）は明確に重い

個人でメリットが出るのは **「既に AWS をヘビーに使っている」「AWS クレジット保有」「副業 SaaS を AWS Lambda 本番運用していて IAM で Claude を呼びたい」** のいずれか。自分は該当しないので、**Anthropic 直契約のままが正解**。

将来、Unity サイドプロジェクトを AWS Lambda + Managed Agents でデプロイする構成になった時に初めて検討対象になる。それまでは「組織導入時の選択肢」「Anthropic のマルチクラウド戦略の進展」として把握しておく位置づけ。

### Anthropic の戦略文脈

2026-04-08 Google TPU・2026-05-10 SpaceX Colossus・今回の AWS first-cloud Platform 提供と並べると、Anthropic は **「compute は複数サプライヤ」かつ「platform 配布も複数クラウド」** という両建てを徹底している。Azure OpenAI Service が OpenAI を抱え込む構図に対する、**「ベンダーロックインを作らない」アプローチでのエンタープライズ攻め**。Bedrock のような「クラウド事業者ラッパー」ではなく「Anthropic 本家を AWS が再販」という形を取ったのは、機能ラグを許容しないという強い意志の表れ。
