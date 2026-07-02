---
date: 2026-07-01
status: read
relevance: A
tags: [claude-science, 医療AI, 生命科学, 長寿研究, workbench, beta, funding]
source_urls:
  - https://www.anthropic.com/news/claude-science-ai-workbench
  - https://techcrunch.com/2026/06/30/anthropics-claude-science-bets-on-workflow-not-a-new-model-to-win-over-scientists/
  - https://endpoints.news/anthropic-debuts-claude-science-an-ai-product-for-bioscience/
  - https://www.forbes.com/sites/johndrake/2026/06/30/anthropics-new-ai-workbench-mapped-my-field-for-26-now-imagine-it-aimed-at-the-rest-of-science/
  - https://news.northeastern.edu/2026/06/30/anthropic-claude-science-launch/
experiment_dir: null
---

# Claude Science: 60+ 科学データベース統合の AI workbench、$30k 助成 50 件も

## 3行要約

- 6/30、Anthropic は科学研究者向けの AI workbench **Claude Science** をベータリリース。UniProt / PDB / Ensembl / Reactome / ClinVar / ChEMBL / GEO など **60+ の科学データベース** を統合し、ゲノミクス / 単細胞 / プロテオミクス / ケミンフォマティクス向けのプリビルドツールキットを提供。監査可能な artifact と柔軟な計算資源アクセスも組み込み。
- 「新モデルではなくワークフローで勝負」がスタンス（TechCrunch）。Pro / Max / Team / Enterprise で利用可能、学術・非営利研究機関は割引プログラムあり。
- **助成プログラム**: 最大 50 件の研究プロジェクトに、それぞれ最大 $30,000 のクレジットを付与。応募 7/15 締切、期間 9/1〜12/1。Forbes 記者は「自分の分野を $26 でマップした」と報告。創薬領域では既に注目。

## 自分への関連度: A

[[user_longevity_goal]] で 50 歳・目標 150 歳・医療 AI/生命科学/長寿研究を第 2 の関心軸としているので、これは直撃案件。ゲノミクスとプロテオミクスに強い workbench は、老化研究論文の読解・データベース横断調査に使える可能性。Forbes 記者の「自分の分野を $26 でマップした」は「1 個人が特定分野を安価に俯瞰できる時代」の実例で、自分でも試したい。$30k 助成は個人には非対象だが、学術/非営利の枠でこれから公開されるプロジェクト成果に注目。

## 詳細

### 対象分野（プリビルドツールキット）

- **ゲノミクス**: DNA/RNA シーケンス解析
- **単細胞（Single-cell）**: scRNA-seq などのマルチオミクス
- **プロテオミクス**: タンパク質構造・機能
- **ケミンフォマティクス**: 化合物・分子

### 統合データベース（60+）

例: UniProt（タンパク質）、PDB（構造）、Ensembl（ゲノム）、Reactome（経路）、ClinVar（臨床変異）、ChEMBL（生理活性化合物）、GEO（発現データ）ほか。

各データベースは独自のスキーマ・クエリ言語を持つが、Claude Science が抽象化・横断検索・自動 join を担う。ジャーナル・プレプリントサーバー・ドメイン特化オープンモデルも統合。

### 特徴

- **監査可能な artifact**: 実行結果は再現可能なフォーマットで保存。査読者が追跡できる。
- **柔軟な compute**: 軽い解析はブラウザ、重い解析は AWS/GCP のバックエンドに委譲。
- **既存モデルベース**: 独自の科学特化モデルではなく、Claude（Opus 4.8 / Sonnet 5 / Fable 5）を workflow で拡張する設計。「新モデルより先にワークフローを固めた」のがポイント。

### 提供プラン

- ベータ: Claude **Pro / Max / Team / Enterprise** ユーザーで利用可能。
- 学術/非営利: 割引 Claude Science プログラム（個別問い合わせ）。
- 個別価格は非公開。

### 助成プログラム

- 最大 **50 件の研究プロジェクト**に、それぞれ最大 **$30,000 のクレジット**。
- 応募締切: 7/15
- 実施期間: 9/1〜12/1
- 対象: 学術機関・非営利研究機関の active laboratory

### 「$26 で自分の分野をマップ」の実例

- Forbes 記事の John Drake が「自分の学術分野の主要文献・データ・プレイヤーを $26 分の Claude Science 実行で俯瞰した」と報告。
- 個人研究者が「新分野の landscape 把握」にかかるコストが激減する示唆。

### 医療 AI 業界の反応

- Endpoints News（バイオファーマ専門紙）が独立記事で扱う。
- Northeastern の記事は「創薬を加速する」と評価。
- Anthropic の中の医療・科学領域への本格参入。[[user_longevity_goal]] の関心軸に直結。

## 試すなら

1. 自分の Claude Pro プラン（[[user_claude_plan]]）で Claude Science ベータの利用可否を確認（Team/Enterprise 限定の可能性あり）。Pro で不可の場合は、公開情報での動向追跡に切替。
2. 個人利用可能なら、老化・長寿研究の主要データベース（例: GTEx / GEO / UniProt）を横断させ、興味あるトピック（例: senolytics, autophagy）を $30 以下でマップしてみる。
3. Forbes 記事の「$26 マップ」の手順を追体験できるレポートが出るのを待つ（他ユーザーの実測 blog を追う）。
4. 助成プログラムに応募する研究者が知り合いにいれば紹介、7/15 締切に注意。
5. Claude Code から Claude Science の MCP や API 連携が今後出るか監視（コーディング作業と科学ワークフローの橋渡し）。

## ソース

- [Claude Science, an AI workbench for scientists (Anthropic 公式)](https://www.anthropic.com/news/claude-science-ai-workbench)
- [Anthropic's Claude Science bets on workflow, not a new model (TechCrunch)](https://techcrunch.com/2026/06/30/anthropics-claude-science-bets-on-workflow-not-a-new-model-to-win-over-scientists/)
- [Anthropic debuts Claude Science, an AI product for bioscience (Endpoints News)](https://endpoints.news/anthropic-debuts-claude-science-an-ai-product-for-bioscience/)
- [Anthropic's New AI Workbench Mapped My Field For $26 (Forbes)](https://www.forbes.com/sites/johndrake/2026/06/30/anthropics-new-ai-workbench-mapped-my-field-for-26-now-imagine-it-aimed-at-the-rest-of-science/)
- [Anthropic's Claude Science will boost drug discovery (Northeastern)](https://news.northeastern.edu/2026/06/30/anthropic-claude-science-launch/)

---

## 感想・考察

### 「助成金の話」だけではないという確認（2026-07-03）

初読では「Anthropic が科学系組織に助成金を出す話」と受け取ったが、実は **2 つの別モノが同時発表** されている:

1. **Claude Science 本体（プロダクト）** — 60+ 科学 DB 統合の AI workbench。Pro / Max / Team / Enterprise で利用可能とされる。Forbes 記者の「$26 で自分の分野をマップ」は助成金ではなく **本体を使った実例**。
2. **$30k 助成プログラム** — 学術機関・非営利ラボ限定（自分は対象外）。50 件、7/15 締切、9/1〜12/1 実施。

つまり本体そのものは Pro プラン（[[user_claude_plan]]）で触れる可能性があり、[[user_longevity_goal]] の第 2 関心軸（医療 AI / 長寿研究）に直接使える道具になり得る。

### 現時点の判断: 追わない

ただし今すぐ試すには以下が引っかかるため、当面は追跡のみ:

- Pro で本当に開けるかは要確認（Team/Enterprise 限定の可能性）
- 老化・長寿系トピックを深追いする「今」の余裕は特にない
- 他ユーザーの実測レポート（Forbes 型の「$26 マップ」再現）を待って判断する方が効率的

### いつ復活させるか

- Pro で開けることが判明した場合
- senolytics / autophagy / mTOR 系の論文をまとめて読み込みたいタイミングが来たとき
- Claude Code から Claude Science への API / MCP 連携が出て、コーディングと科学ワークフローが橋渡しされたとき

<!-- /try 実行時に自動生成 -->
