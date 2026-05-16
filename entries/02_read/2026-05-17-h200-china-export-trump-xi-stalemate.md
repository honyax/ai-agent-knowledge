---
date: 2026-05-17
status: read
relevance: B
tags: [nvidia, h200, china, us-china, export-control, trump, xi, geopolitics, gpu, supply-chain]
source_urls:
  - https://www.cnbc.com/2026/05/14/us-clears-h200-chip-sales-to-10-china-firms-as-nvidia-ceo-looks-for-breakthrough.html
  - https://www.tomshardware.com/tech-industry/trump-says-china-is-blocking-h200-purchases
  - https://www.techtimes.com/articles/316674/20260515/trump-xi-close-beijing-summit-warm-rhetoric-nvidia-h200-deliveries-remain-stalled-rare-earth.htm
  - https://jbpress.ismedia.jp/articles/-/92622
  - https://gigazine.net/news/20260515-nvidia-h200-chip-sales-to-china-firms/
  - https://www.jetro.go.jp/biznews/2026/01/9cd6adfab702bd46.html
experiment_dir: null
---

# NVIDIA H200 の対中輸出、トランプ・習近平会談でも納入実現せず — 「25% 上納金」と中国の国産優遇で膠着

## 3行要約

- 2026-05-14〜15 の北京での米中首脳会談で H200（Hopper 世代、一世代前）の中国販売が話題に。商務省は2026年1月に Alibaba・Tencent・ByteDance・JD.com など中国大手 **約10社への H200 販売を許可済み**だが、**会談時点で 1 枚も納入実現せず**
- 膠着の主因は (1) トランプが NVIDIA に飲ませた「販売収益の **25% を米国政府に上納**」する異例スキーム、(2) チップが一度米国領を経由する流通条件、(3) **中国側の国産チップ（Huawei Ascend など）優遇政策とバックドア懸念**による買い渋り
- アメリカの線引きは「最新 Blackwell（B100/B200/GB200）は禁止／一世代前の H200 までは交渉カードとして許可」。NVIDIA としては中国市場と CUDA エコシステム維持が至上命題で、Jensen Huang CEO がトランプの北京訪問に同行

## 自分への関連度: B

直接の業務影響はないが、関心領域 5（Claude API・モデル提供基盤）と関心領域 8（Anthropic の方針）に間接的に関わる。Anthropic は H100/H200 を含む Hopper 世代 GPU の調達を Colossus 1 経由でも増やしており（[[2026-05-10-anthropic-spacex-colossus-deal]]）、**H200 の最大需要先がアメリカ国内＋同盟国の AI ラボに集中する構図**が、米中分断の進行で固定化される。長期的には「中国は中国系チップで自前のモデルを学習、欧米は NVIDIA で学習」という二極化が加速し、グローバルな AI モデル供給網の前提が変わる可能性があるため、地政学背景として押さえておきたい。

## 詳細

### 経緯のタイムライン

| 時期 | 出来事 |
|------|--------|
| 2025-12-08 | トランプが Truth Social で「H200 を中国の承認顧客に売ることを習主席に伝えた」と発表 |
| 2026-01-13 | 米商務省 BIS が H200・AMD MI325X の対中輸出管理緩和の最終規則を発表 |
| 2026-01-15 | 規則発効。Alibaba・Tencent・ByteDance・JD.com など約10社が承認リスト入り |
| 2026-05-13 | Jensen Huang（NVIDIA CEO）がトランプの北京訪問に急遽同行 |
| 2026-05-14〜15 | 北京で米中首脳会談。会談時点で H200 の納入実績ゼロ |
| 2026-05-15 | トランプが「中国は H200 購入を選ばなかった」と認める発言 |

### 「25% 上納金」スキーム

トランプは輸出許可の見返りに、**NVIDIA が H200 を中国に販売した収益の 25% を米国政府に納める**ことを条件化。

- チップは一度米国領を経由してから中国へ出荷する建付け
- 議会からは「実質的な輸出税で違憲では」という批判
- 中国側の購入価格に転嫁されている疑念があり、国産代替のコスト競争力を相対的に高める結果に

### なぜ「難航」しているのか — 売り渋りではなく買い渋り

ニュースの見出しだけ見ると「アメリカが売り渋り」のように読めるが、実態は逆で**中国が買い渋っている**。

| 中国側の買い渋り要因 | 内容 |
|---------------------|------|
| 国産チップの台頭 | Huawei Ascend（昇騰）、寒武紀（Cambricon）が性能で追いつきつつある |
| 政府の国産優遇 | 「セキュリティ審査」を理由に国産品調達を事実上強制 |
| 価格上乗せ | 25% 上納金分が販売価格に転嫁されている疑念 |
| バックドア懸念 | 「アメリカが許可した時点で監視機能があるのでは」という不信感 |

### アメリカの対中 GPU 規制の全体構造

| GPU | 世代 | 対中輸出 | 備考 |
|-----|------|---------|------|
| H100 | Hopper | 禁止 | 性能ダウン版 H20 で代替 |
| **H200** | **Hopper** | **2026-01 許可、納入ゼロ** | 本エントリの主役 |
| B100/B200 | Blackwell | 禁止 | 最新単体 GPU |
| **GB200** | **Blackwell** | **禁止** | Colossus 2 で使用中 |

「最新世代は絶対渡さない／一世代前は交渉カード」という線引きが定着。

### NVIDIA 側の動機

- 中国は元々 NVIDIA 売上の 20% 超を占めていた巨大市場
- 完全に閉ざされると Huawei Ascend にシェアを奪われ、**CUDA エコシステムが中国で「離脱」する**リスク
- 一度離脱が起きると、欧米の AI 規制が緩んでも中国側が NVIDIA に戻ってこない可能性が高い
- Jensen Huang が北京訪問に同行したのは「最後の市場防衛戦」の意味合い

## 試すなら

（地政学・サプライチェーン情報のため実践要素なし。以下の観点で関連エントリと並べると理解が深まる）

1. [[2026-05-10-anthropic-spacex-colossus-deal]] — Anthropic が Hopper 中心の Colossus 1 を確保した話と並べると、「H200 の主需要が欧米 AI ラボに固定化」する流れが見える
2. Huawei Ascend や中国国産 LLM の進展ニュースを横並びで追うと、米中 AI 二極化の進行度合いが測れる
3. Blackwell（GB200）の中国向け輸出が議論される段階になったら、それは「H200 と同じカードがもう使えなくなった」サインとして注視

## ソース

- [U.S. clears H200 chip sales to 10 China firms as Nvidia CEO looks for breakthrough（CNBC）](https://www.cnbc.com/2026/05/14/us-clears-h200-chip-sales-to-10-china-firms-as-nvidia-ceo-looks-for-breakthrough.html)
- [Trump says China is blocking Nvidia H200 purchases despite US approval（Tom's Hardware）](https://www.tomshardware.com/tech-industry/trump-says-china-is-blocking-h200-purchases)
- [Trump and Xi Close Beijing Summit: Warm Rhetoric, Nvidia H200 Deliveries Remain Stalled（TechTimes）](https://www.techtimes.com/articles/316674/20260515/trump-xi-close-beijing-summit-warm-rhetoric-nvidia-h200-deliveries-remain-stalled-rare-earth.htm)
- [トランプ氏のAI半導体「H200」対中輸出容認が招いた米中の葛藤（JBpress）](https://jbpress.ismedia.jp/articles/-/92622)
- [NVIDIAによるAIチップ「H200」の中国企業への販売をアメリカ政府が承認、中国政府の承認待ちへ（GIGAZINE）](https://gigazine.net/news/20260515-nvidia-h200-chip-sales-to-china-firms/)
- [トランプ米政権、エヌビディア製半導体「H200」などの対中輸出管理を緩和（JETRO）](https://www.jetro.go.jp/biznews/2026/01/9cd6adfab702bd46.html)

---

## 感想・考察

### 「H200 OK / H100 NG」のねじれ

ニュースだけ見ると「H200 は許可、H100 は禁止のまま」という線引きが**世代順で自然**に見えるが、実際は性能比較が逆転している。

| 項目 | H100 | H200 |
|------|------|------|
| アーキテクチャ | Hopper | Hopper（同世代） |
| メモリ | HBM3 80GB | **HBM3e 141GB** |
| メモリ帯域 | 3.35 TB/s | **4.8 TB/s（+40%）** |
| LLM 推論性能 | 基準 | **約 2 倍** |

H200 は H100 のメモリ強化版であり、特に LLM 推論ワークロードではむしろ上位。**性能だけで見れば「H200 許可・H100 禁止」は技術的に説明がつかない**。

### 公式に語られていること／いないこと

公式（BIS Final Rule 2026-01-15、トランプ発言、商務省プレスリリース）が述べているのは**「H200 を解禁する動機」だけ**:

- BIS Final Rule: 「H200 およびそれと同等以下を case-by-case 審査」「米国内供給を脅かさない」「セキュリティ手続き」「第三者検証」などの**条件**
- トランプ発言: 「H200 は最高レベルじゃない（Blackwell や Rubin が上）」「規制が米国企業に nobody wants な低性能品を作らせた」という**経済合理性のフレーミング**

**公式に語られていないのは「H100 を禁止し続ける比較論的な根拠」**。各所で様々な憶測（規制の象徴性、流通管理のしやすさ、ディール演出など）が飛び交っているが、ホワイトハウスや BIS の公式根拠を持つ説明は見当たらないため、本エントリでは深入りしない。CFR は新政策全体を "strategically incoherent and unenforceable" と評しており、**論理的整合性より政治的・運用的な都合で形成された政策**と捉えるのが妥当そう。

### トランプ「nobody wants な低性能品」発言が指すもの

トランプの 2026 年首脳会談前後の発言に出てくる「nobody wants な低性能品」は具体的には **H20 GPU** を指す。

- 2022 年の H100 規制を受けて、NVIDIA は **規制適合のための大幅ダウングレード版 H20** を中国向けに開発（H100 の約 1/6 性能）
- 数十億ドル規模の開発投資をしたが、中国側は「性能不足」で Huawei Ascend に流れ、米国では需要なし
- トランプの含意: バイデン規制を「**産業政策として失敗**」と位置づけ、H200 解禁を「**経済合理性に基づく軌道修正**」として正当化する。同時に、NVIDIA の中国売上＋米政府の 25% 上納金で「米国の利益になるディール」とフレーミング

ただしこの主張は安全保障コミュニティ（CFR 等）からは「**中国の AI 学習能力を底上げするだけ**」と批判されている。

### 用語整理: BIS

本エントリで頻出する **BIS** は **Bureau of Industry and Security（米商務省産業安全保障局）** のこと。

- 米国商務省（Department of Commerce）の下部機関
- **輸出管理規則（EAR: Export Administration Regulations）の策定・執行**を担う政府機関
- **Entity List**（取引禁止企業リスト）の管理 — Huawei・SMIC・寒武紀などが指定済み
- **Foreign Direct Product Rule**（外国製でも米国技術を含む製品は規制対象）も BIS が運用
- 日本で言えば**経済産業省の安全保障貿易管理部**（外為法運用部署）に近いポジション

本件の重要な動きはすべて BIS が発出:

| 時期 | BIS の動き |
|------|-----------|
| 2022-10 | A100/H100 を対中規制対象に追加 |
| 2023-10 | A800/H800（NVIDIA の中国向けダウン版）の抜け穴を塞ぐ規則改正 |
| 2026-01-13 | **H200 の Final Rule 発表**（presumption of denial → case-by-case review） |
| 2026-01-15 | 同 Rule 発効 |

つまり「ホワイトハウスが政治的方針を示し、BIS が技術的・法的に落とし込んで運用する」という関係。米国対中半導体規制の実務を仕切っている役所として、今後も Blackwell の扱いなどで動向を追うべきポイント。
