---
date: 2026-06-12
status: read
relevance: B
tags: [anthropic, public-opinion, ai-policy, survey, regulation]
source_urls:
  - https://www.anthropic.com/news/anthropic-public-record
experiment_dir: null
---

# Anthropic Public Record 初回結果: 米国民約5.2万人のAI観調査

## 3行要約

- Anthropicが世論データを継続的に公開する新シリーズ「Anthropic Public Record」を開始。第1回は2025年11月〜12月に米国民約52,000人を対象に実施。
- 期待トップは「がん/アルツハイマー等の治療」(48%)、次いで障がい者支援(36%)、技術進歩と生活向上(各23%)。
- 不安トップは「AIによる失業」(64%、全州で1位)、「認知依存」(56%)、「誤情報」(52%)。70%超が政府によるAI規制を支持（超党派）。

## 自分への関連度: B

直接ワークフローに影響しないが、Anthropicが「政府規制を市民が望んでいる」というシグナルを公的記録として固定化するのは政治戦略として注目に値する。今後のAI規制議論やAnthropicの自社ポジショニング（responsible scaling、coordinated pause等の [[anthropic-coordinated-pause-proposal]] と地続き）の文脈で参照される一次資料になりそう。

## 詳細

- 調査名: Anthropic Public Record（公開記録の意。世論データを定期発表する継続企画）
- 第1回調査時期: 2025/11〜2025/12
- 対象: 米国民 約52,000人
- 期待 (top 3 で複数選択):
  - 病気治療(がん、アルツハイマー等): 48%
  - 障がい者支援: 36%
  - 技術進歩 / 生活向上: 各23%
- 不安:
  - 雇用喪失: 64%（全州で最多）
  - 認知依存(cognitive dependency): 56%
  - 誤情報: 52%
- 政府規制への支持: 70%超、超党派で支持。

文脈: 直近のIPO申請([[anthropic-ipo-confidential-filing]])、政府との対立([[anthropic-coordinated-pause-proposal]], [[fable5-mythos5-export-ban]])が重なる中、「我々は市民の声を代弁している」という立場を示すための地ならし的発表と読める。

## 試すなら

1. 原文をざっと読み、データの開示粒度（個別質問の単純集計が出ているか、クロス集計まであるか）を確認。
2. AIプロダクトを社内提案する際の「世論的バックグラウンド」資料としてストック。
3. 同種の調査（Pew Research等）と比較して、AnthropicのバイアスやN数の妥当性を批判的に検証。

## ソース

- [Results from first Anthropic Public Record \\ Anthropic](https://www.anthropic.com/news/anthropic-public-record)

---

## 感想・考察

### 「期待」と「不安」のギャップが示すもの（2026-07-01 議論）

- **「医療(48%) > 技術進歩(23%) > 生活向上(23%)」のギャップ**: AIに期待するのは「具体的な苦痛の回避」であって、「便利になる」「進歩する」のような漠然としたものは半分以下の支持率。
- **不安は抽象的・社会構造寄り**: 失業(64%)、認知依存(56%)、誤情報(52%)。心理構造としては「具体的な命を救ってほしい、抽象的な社会の地盤は揺らさないでほしい」。
- **Anthropic の戦略と符合**: Project Glasswing で生命科学者向けに Mythos 5 を出した動き（[[2026-06-06-project-glasswing-expansion-claude-security]]）、輸出規制でも医療貢献を「正当性の盾」として持つ構造（[[2026-06-13-fable5-mythos5-export-ban]]）と綺麗に重なる。
- **開発者目線の応用**: AI製品を社内提案するときは「業務効率化」より「健康/医療/苦痛回避」「アクセシビリティ」を含めた方が世論的説得力が高い。ゲーム開発でもアクセシビリティ機能(障がい者支援36%)への AI 活用は社会的正当性で勝負しやすい。

### 個人的な長期目標との接続

自分は現在50歳、個人的な目標は150歳まで生きること。これまで「100年後の医療水準」を予想できなかったが、AI による医療技術の進歩を見ていると意外と現実的に思えるようになった。

- 医療系AI（Project Glasswing、Mythos 5、生命科学研究者向けモデル）の動向は **個人的に長期ウォッチ対象** に格上げ
- Anthropic への投資判断（[[anthropic-coordinated-pause-proposal]] の議論で触れた IPO 応募検討）も、医療AI領域への期待が後押し材料になる
- 関連度判定でも医療AI・長寿関連の話題は B→A に格上げしてよさそう
