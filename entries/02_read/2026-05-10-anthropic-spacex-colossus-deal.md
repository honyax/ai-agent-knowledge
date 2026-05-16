---
date: 2026-05-10
status: read
relevance: B
tags: [anthropic, spacex, xai, colossus, infrastructure, gpu, compute, capacity, max, pro]
source_urls:
  - https://www.cnbc.com/2026/05/06/anthropic-spacex-data-center-capacity.html
  - https://x.ai/news/anthropic-compute-partnership
  - https://www.datacenterdynamics.com/en/news/anthropic-to-use-all-of-spacex-xais-colossus-1-data-center-compute/
  - https://capacityglobal.com/news/anthropic-secures-full-capacity-of-spacex-data-centre/
  - https://lifeboat.com/blog/2026/05/anthropic-to-consider-using-spacex-orbital-data-center-satellites
experiment_dir: null
---

# Anthropic、SpaceX × xAI の Colossus 1（Memphis）全容量を確保 — 220K GPU・300MW・「軌道上データセンター」も検討

## 3行要約

- 2026-05-06、Anthropic が SpaceX と提携し、Memphis にある xAI/SpaceX 共同運営の **Colossus 1 データセンター（300MW、220,000 GPU 相当）** の全コンピュート容量をレンタルする契約を発表。GPU は H100・H200・次世代 GB200 のミックスで、今月中に利用開始
- 同契約に **「複数 GW 規模の orbital（軌道上）AI compute」を SpaceX と共同で開発検討する」** 条項も付随。Anthropic は Code w/ Claude 2026（同日）でこの契約を Pro/Max のキャパ改善に直結すると説明、レート制限2倍と整合
- 注目点: イーロン・マスク率いる SpaceX/xAI のデータセンターを、Anthropic（マスクと長く対立的）が借りるという業界アライメントの異例さ。xAI は「自社モデル学習にはまだ余剰がある」とし、Anthropic への提供は **Colossus 1 が xAI の Colossus 2（建設中）への移行で空くキャパ** を活用する形

## 自分への関連度: B

Max プラン利用者として、Pro/Max のキャパ改善が今月中に効いてくる点は短期的にメリット（Code w/ Claude エントリと連動）。一方で技術的な実務影響は限定的で、知識・トレンド理解として把握しておくべき情報。関心領域 8（Anthropic の方針）、特に同社が「自社・Google TPU・AWS Trainium・SpaceX/xAI」と複数の compute サプライヤを並列確保する戦略を取っていることが分かる。Anthropic の compute スタック関連の既存エントリ（2026-04-08-anthropic-30b-revenue-google-tpu.md）と並べると全体像が見える。

## 詳細

### 契約の主要数字

| 項目 | 内容 |
|------|------|
| 対象施設 | Memphis 旧 Electrolux 工場跡地（Boxtown 地区）の Colossus 1 データセンター |
| 容量 | 300MW（Anthropic がフルキャパ確保） |
| GPU 数 | 220,000 相当（H100・H200・GB200 ミックス） |
| 運用元 | SpaceX（土地・施設）× xAI（運用・電源契約） |
| 利用開始 | 2026年5月中 |
| 直接効果 | Pro/Max サブスクライバのキャパ改善 |

### GPU 内訳（2025年12月時点）

| GPU | 数量 | 割合 | 世代 |
|-----|------|------|------|
| H100 | 約 150,000 | 約 65% | Hopper |
| H200 | 約 50,000  | 約 22% | Hopper |
| GB200 | 約 30,000 | 約 13% | Blackwell |
| **合計** | **約 230,000** | 100% | — |

- 報道の「220,000 GPU 相当」は上記合計のラフな丸め、または GB200 を H100 換算した数字と思われる
- **Hopper 世代（H100+H200）が約 87%** で、Colossus 1 の主力は依然 Hopper。Blackwell（GB200）の大規模投入は Colossus 2 側に振り分け
- Anthropic 側の使い方としては、フラッグシップ学習よりも **Pro/Max の inference キャパ補填**寄りと推測（Code w/ Claude 2026 のレート制限2倍と整合）

### 「軌道上 AI コンピュート」条項

契約には Anthropic が SpaceX と「複数 GW 規模の orbital AI compute」を共同で開発検討する旨を含む。地上データセンターの電源・冷却ボトルネックを長期的に回避する選択肢として位置づけ。報道時点では具体的な launch スケジュールや capacity 数字は未公開。

### 業界アライメント上の異例さ

- イーロン・マスクは Anthropic と対立的なポジション（OpenAI 創設に関与、xAI で競合）
- それでも Colossus 1 を Anthropic に丸ごと貸す形になった理由として、xAI 側は **「Colossus 2 への移行で Colossus 1 のキャパが余剰になる」** と説明
- Anthropic 側は短期キャパ確保＋ペーパーには出ていない「電源契約の継承」が魅力と推測される

### Anthropic の compute マルチサプライヤ戦略

| プロバイダ | 規模 | 関連エントリ |
|-----------|------|--------------|
| Google / Broadcom（TPU） | 3.5GW（中長期） | 2026-04-08-anthropic-30b-revenue-google-tpu.md |
| AWS（Trainium） | 既存契約 | — |
| Microsoft / OpenAI と並ぶ大規模 GPU | 既存 | — |
| **SpaceX × xAI（H100/H200/GB200）** | 300MW・220k GPU | 本エントリ |
| Orbital（軌道上） | 数 GW（検討段階） | 本エントリ |

「単一サプライヤに依存しない・GW スケールで複数並列」がパターン化している。

## 試すなら

（インフラ提携情報のため実践要素なし。Pro/Max のキャパ改善体感を5月後半以降に観察する程度）

## ソース

- [Anthropic, SpaceX announce compute deal that includes space development（CNBC）](https://www.cnbc.com/2026/05/06/anthropic-spacex-data-center-capacity.html)
- [New Compute Partnership with Anthropic（xAI 公式）](https://x.ai/news/anthropic-compute-partnership)
- [Anthropic to use all of SpaceX-xAI's Colossus 1 data center compute（DataCenter Dynamics）](https://www.datacenterdynamics.com/en/news/anthropic-to-use-all-of-spacex-xais-colossus-1-data-center-compute/)
- [Anthropic secures full capacity of SpaceX's Colossus 1 data centre in Memphis compute deal（Capacity）](https://capacityglobal.com/news/anthropic-secures-full-capacity-of-spacex-data-centre/)
- [Anthropic to consider using SpaceX orbital data center satellites（Lifeboat News）](https://lifeboat.com/blog/2026/05/anthropic-to-consider-using-spacex-orbital-data-center-satellites)

---

## 感想・考察

「Anthropic とマスクは対立的」という記述について整理しておく。

- **OpenAI ほど激しい個人攻撃はない**: マスクが訴訟まで起こしているのは Altman / OpenAI に対してで、Anthropic への直接的な名指し批判は比較的少ない。「OpenAI から分派した安全派」程度の認識
- **対立軸は主に規制スタンス**: Anthropic は California SB 1047 など AI 規制に賛成寄り。マスクもかつては規制賛成派だったが、xAI 立ち上げ後は「規制で競合を縛りつつ自社は加速」と批判されるポジションに移行
- **xAI として直接競合**: Grok と Claude は同じ汎用 LLM 市場で衝突

つまり「個人的に犬猿」というより**業界の構図上の対立サイド**という関係。

### マスクと「仲が良い」AI 業界の相手は基本インフラ側のみ

主要 AI ラボとは軒並み距離があるなか、関係良好なのは以下：

| 相手 | 関係 |
|------|------|
| NVIDIA / Jensen Huang | xAI が GPU 爆買い、Jensen を高く評価する発言 |
| Oracle / Larry Ellison | 個人的親友、Tesla 取締役歴あり、Oracle Cloud が xAI に提供 |
| Dell / Supermicro | Colossus のサーバー供給で密接 |

逆に OpenAI（訴訟）/ Google DeepMind（Page と AI で決裂）/ Meta（Zuckerberg と物理的ケンカ寸前）/ Anthropic（構造的競合）と主要 AI ラボすべてと距離あり、というのが現状。

### 今回の取引が示すもの

そのマスクの xAI が「ぼっち」状態の Anthropic に Colossus 1 を丸ごと貸すというのは、**経済合理性（Colossus 2 移行で余るキャパの収益化）が政治的・感情的アライメントを上回った**というケース。AI 業界の「敵味方マップ」が、GW スケールの compute 需要の前では意外と流動的になり得ることを示すサインとして覚えておく。
