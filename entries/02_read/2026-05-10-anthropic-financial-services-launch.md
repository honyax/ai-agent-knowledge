---
date: 2026-05-10
status: read
relevance: C
tags: [anthropic, financial-services, agents, microsoft-365, moodys, enterprise, jamie-dimon, business]
source_urls:
  - https://www.anthropic.com/news/finance-agents
  - https://fortune.com/2026/05/05/anthropic-wall-street-financial-services-agents-jamie-dimon/
  - https://www.axios.com/2026/05/05/anthropic-wall-street-dimon-amodei
  - https://winbuzzer.com/2026/05/06/anthropic-ships-ten-ai-agents-for-finance-as-both-xcxwbn/
  - https://www.bloomberg.com/news/articles/2026-05-05/anthropic-unveils-ai-agents-to-field-financial-services-tasks
experiment_dir: null
---

# Anthropic、金融サービス向け Claude を本格展開 — 10エージェント＋Microsoft 365 全統合＋Moody's MCP連携

## 3行要約

- 2026-05-05、Anthropic がニューヨークの招待制金融サービスブリーフィングで **金融機関向け 10 種のプリビルト AI エージェント** を発表。pitchbook 作成・KYC スクリーニング・月次決算クローズなど時間消費の大きい作業をテンプレ化。Opus 4.7 を金融用主力モデルに据える
- **Microsoft 365 全統合**: Excel・PowerPoint・Word の Add-in が GA、Outlook はベータ。**Claude が4つのアプリ間でコンテキストを保持して横断する単一エージェントとして動作**。Excel 内のセル参照を PPT スライドへ持ち越せる
- **Moody's の MCP ネイティブアプリ**: Moody's の信用格付けデータベース（公開・非公開600M社）を Claude 内から直接照会可能。CEO 級では JPMorgan の Jamie Dimon が公の場で Anthropic を支持発言

## 自分への関連度: C

直接の業務影響はなし（自分は金融業界外）。ただし Anthropic の方針（関心領域 8）として:

1. **「金融特化エージェント・テンプレ」というスタイルが Anthropic の標準商品形態に**: Code w/ Claude 2026 で発表された Managed Agents の Outcomes・Multi-agent orchestration を業界別エージェントの量産フレームに使う方針が透ける。今後ゲーム業界・他業界向けにも同形式が出る可能性
2. **MCP がエンタープライズ統合の標準プロトコルに**: Moody's が「ネイティブアプリ」として MCP サーバを提供する形は、これまでの「サードパーティが MCP を作る」段階から「データプロバイダ自身が MCP を一級市民として提供する」段階への移行を示唆
3. **Microsoft 365 統合は Claude.ai と Office の橋渡し**: 自分は Office を業務で使わないが、企業向け Claude が Excel/PPT 内に深く入り込む方針は、Cursor/Copilot との競合構造を理解する上で重要

## 詳細

### 10 種の金融エージェント（テンプレ）

公式発表時点で具体名が出ているもの:

- **Pitchbook 作成**: 投資銀行向けプレゼン資料の自動生成
- **KYC スクリーニング**: 顧客本人確認資料の照合・リスク評価
- **月次クローズ**: 帳簿締め・調整の自動実行
- 他、信用分析・コンプライアンス・取引監視・リサーチ要約・規制対応・社内ナレッジ検索・顧客レポート作成（同種の業務カテゴリ）

すべて **Opus 4.7** を主力モデルに、Managed Agents 上で動作。

### Microsoft 365 全統合

| アプリ | 状態 | 機能 |
|--------|------|------|
| Excel | GA Add-in | セル参照・関数生成・データ分析・PPT への持ち越し |
| PowerPoint | GA Add-in | スライド草稿・図表生成・文体統一 |
| Word | GA Add-in | 文書作成・要約・編集 |
| Outlook | ベータ | メール起草・要約・スケジュール調整 |

注目点: **Claude が 4 アプリ横断で同じコンテキストを保持する単一エージェント** として動く。Excel の数値を選択 → PPT で「これを表にして」と指示する流れがネイティブで成立。

### Moody's の MCP ネイティブアプリ

- **対象**: 公開・非公開合わせて 600M 社の信用格付け・財務データ
- **形式**: Moody's 自身が MCP サーバを提供（Anthropic 経由のサードパーティラッパーではない）
- **ユースケース**: 信用分析・コンプライアンス・新規取引先評価・営業

### 業界の受け止め

- JPMorgan の Jamie Dimon CEO がカンファレンスでステージ登壇し Anthropic を支持
- Wall Street Tech 系メディアの論調は「Anthropic は金融向け OS layer を狙っている」「OpenAI との金融サービス争奪戦が本格化」
- Bloomberg は「pre-built agent templates が銀行の AI 導入摩擦を下げる」と評価

### Anthropic の業界別展開パターン

これまで:
- Healthcare（2026-01: Claude for Healthcare）
- Creative（2026-04-29: Creative Connectors 9種）
- **Financial（2026-05-05: 本エントリ）**

「業界別 pre-built エージェント＋業界データ MCP＋既存 SaaS 統合」の三点セット型展開が固まりつつある。次は Legal・Manufacturing あたりが想定される。

## 試すなら

（金融業務外のため実践要素なし。**ただし「業界別エージェントテンプレ」「業界データの MCP ネイティブアプリ」というモデルは今後ゲーム業界向けにも適用される可能性があり、Unity × AI の展開で類似発表があったら追跡する**）

## ソース

- [Agents for financial services（Anthropic 公式）](https://www.anthropic.com/news/finance-agents)
- [Anthropic deepens push into Wall Street with new AI agents, full Microsoft 365 integration, Moody's data partnership（Fortune）](https://fortune.com/2026/05/05/anthropic-wall-street-financial-services-agents-jamie-dimon/)
- [Anthropic deepens its ties to Wall Street with new partnerships, tools（Axios）](https://www.axios.com/2026/05/05/anthropic-wall-street-dimon-amodei)
- [Anthropic Expands Claude With 10 Finance Workflow Agents（WinBuzzer）](https://winbuzzer.com/2026/05/06/anthropic-ships-ten-ai-agents-for-finance-as-both-xcxwbn/)
- [Anthropic Unveils AI Agents to Field Financial Services Tasks（Bloomberg）](https://www.bloomberg.com/news/articles/2026-05-05/anthropic-unveils-ai-agents-to-field-financial-services-tasks)

---

## 感想・考察

### Mythos との関係 — 直接ではなく間接の経路

最初は「Mythos で金融業界に食い込んだ結果として今回の進出があるのでは」と考えたが、Project Glasswing のパートナーリスト（Amazon・Apple・Microsoft・Cisco・CrowdStrike・Palo Alto・Linux Foundation 等）を見直すと **テック/セキュリティ企業中心で銀行は入っていない**。JPMorgan や Moody's との関係は Glasswing 経由ではなく、別ルートのエンタープライズ営業で開拓されたものと見るのが妥当。

ただし **「責任ある展開」のナラティブ強化という間接効果**は確実にある。「危険すぎるから一般公開しない」という Mythos の打ち出し方は、規制業界（金融・医療）が最も気にする"安全性のシグナリング"として機能し、コンプラ部門の決裁を通す材料になる。Jamie Dimon が公の場で支持表明できたのも、「Anthropic = 暴走しない AI 開発者」というブランドが固まった後だから。

整理すると: Mythos → 金融業界の信頼を直接獲得、ではなく、Mythos → Anthropic 全体のブランドを"安全志向の AI 企業"として固定 → 金融営業の追い風、という間接経路。

### 業態としての位置付け — "安定化"より"プラットフォーム化への変態"

ここ最近のエントリを並べると、安定化どころか enterprise platform 企業への変態が起きていると見える:

- 業界別商用展開: Healthcare → Creative → **Financial** の三点セット型量産
- Compute マルチサプライヤ: Google TPU 3.5GW＋AWS Trainium＋SpaceX/xAI 300MW＋軌道上検討（[[spacex-colossus-deal]]）
- エンタープライズ SaaS 深部統合: M365 4 アプリ横断＋Moody's MCP ネイティブ
- トップ顧客の公の場での支持: JPMorgan の Jamie Dimon
- 競合との取引すら成立: マスク陣営の Colossus 1 を借り切る

特にイーロン・マスク（Anthropic と対立的）の SpaceX/xAI から **データセンター丸ごと借りる**取引が成立する点は、業界の力学が「Anthropic を無視できない」段階に来た象徴。

### ただし"安定"とは言い切れない要注意点

- **Pentagon AI 7 vendor 契約から Anthropic は除外**（[[pentagon-ai-7vendors-anthropic-excluded]]）— 防衛セクターでは敗北。「安全志向」のブランドが軍事向けでは逆風になっている可能性
- **Compute 支出規模が桁違い** — 3.5GW + 300MW + 軌道上構想の資本投入を回収するには、金融・医療・クリエイティブの売上だけでは厳しい計算で、**売上拡大に強制的にコミットしている状態**とも読める
- **OpenAI との金融サービス争奪戦が本格化**（Bloomberg 論調）— 先行者利益はあるが防御戦になる

「安定化」というより **「もう後戻りできない規模に投資して、エンタープライズ AI プラットフォーム勝者になるしかない位置に自分を追い込んだ」段階** と捉えるのが実態に近い。Mythos のブランド効果はその走り出しを後押ししたピースの一つ。
