---
date: 2026-05-04
status: read
relevance: B
tags: [anthropic, pentagon, policy, mythos, government, blacklist, supply-chain]
source_urls:
  - https://www.cnbc.com/2026/05/01/pentagon-anthropic-blacklist-mythos-michael.html
  - https://www.theregister.com/2026/05/01/mythos_complicates_anthropic_us_gov_breakup/
  - https://www.roborhythms.com/pentagon-ai-contracts-anthropic-excluded-may-2026/
  - https://orbitaltoday.com/2026/05/03/8-ai-companies-win-pentagon-classified-contracts-while-anthropic-remains-blacklisted/
  - https://www.militarytimes.com/news/pentagon-congress/2026/05/01/pentagon-freezes-out-anthropic-as-it-signs-deals-with-ai-rivals/
experiment_dir: null
---

# Pentagon、AI機密ネットワーク契約7社にAnthropicを含めず — Mythosは「別件」と切り分け

## 3行要約

- 2026-05-01、Pentagon が機密ネットワーク向け AI 契約を **OpenAI・Google・Microsoft・AWS・Nvidia・SpaceX・Reflection AI（スタートアップ）の7社** に発注。フロンティアラボでただ一社、Anthropic だけが除外された
- Pentagon CTO Emil Michael 氏は CNBC 取材で **「Anthropic は依然サプライチェーンリスク」「ただし Mythos は別件で、国家安全保障の moment」** と発言。NSA は Pentagon のブラックリストにかかわらず Mythos Preview を既に利用中
- 対立の根は「Anthropic が Claude をすべての合法用途に開放することを拒み、特に大規模国内監視と自律兵器開発を walling off している」点。Anthropic は3月に Trump 政権を提訴して反論中

## 自分への関連度: B

直接の業務影響はゼロだが、Anthropic の方針（関心領域 8: Anthropic のビジョン）を読む上で重要なシグナル。「Anthropic は Pentagon との契約を失ってでも Acceptable Use Policy を曲げない」という事実は、長期的に同社のプロダクト方針（Claude の倫理ガードレール・モデル提供範囲）を予測する材料になる。関連エントリ: 2026-04-04 Pentagon trump appeal、2026-04-10 Pentagon court loss、2026-04-08 Project Glasswing/Mythos と一連のタイムラインを成す。

## 詳細

### 5/1 Pentagon AI 機密契約: 受注7社

- OpenAI
- Google
- Microsoft
- AWS（Amazon Web Services）
- Nvidia
- SpaceX
- Reflection AI（スタートアップ、フロンティアラボ系で初）

Anthropic はフロンティアラボ4社のうち唯一含まれず。

### Pentagon CTO Emil Michael 氏の発言要旨（CNBC）

- **「Anthropic はまだサプライチェーンリスク」**: 同社のテクノロジーは米国国家安全保障に脅威となりうる、という DoD 公式見解は変わっていない
- **「Mythos は別の national security moment」**: サイバー脆弱性の発見・修正に特化した Mythos の能力は、政府ネットワークの hardening に必要。ブラックリストとは切り分けて評価
- 並行して Nvidia・SpaceX 等との別 AI 契約も同日発表

### 対立の構造

DoD は Claude を「すべての合法用途に開放せよ」と要求。Anthropic は **(1) 大規模国内監視、(2) 自律兵器開発** での利用を Acceptable Use Policy で禁止し続け、両者の合意は不成立。Anthropic は3月に Trump 政権を提訴し（先述エントリ参照）、Pentagon の決定を覆そうとしている。

### Mythos の二重ステータス

NSA は Pentagon ブラックリストの存在にかかわらず Mythos Preview を業務で利用中（2026-04-19 Axios 報道）。サイバーセキュリティ用途では「Anthropic を排除すると国益を損なう」という認識が政府内にも併存しており、ブラックリストは一枚岩ではない。

### Anthropic 側の動き

- Mythos を「too dangerous to release」として一般公開を見送りつつ Preview だけ NSA・選別された機関に提供（2026-04-08 Project Glasswing 発表と整合）
- Pentagon との契約喪失は短期的な収益機会ロスだが、「倫理 walling off を維持」という長期方針の方を優先

### 関連時系列（このリポジトリの既存エントリと接続）

- 2026-02 ごろ: DoD が Anthropic をサプライチェーンリスクに指定
- 2026-03: Anthropic が Trump 政権を提訴（既存: 2026-04-04 entry）
- 2026-04-02: Trump 政権が裁定不服を控訴（既存: 2026-04-04 entry）
- 2026-04-08: Project Glasswing 公開、Mythos Preview 発表（既存エントリ）
- 2026-04-10: Pentagon 控訴審で連邦判事が Anthropic 側勝訴判決（既存エントリ）
- 2026-04-19: NSA が Mythos Preview を利用中と Axios 報道
- **2026-05-01: 機密AI契約7社発注、Anthropic 除外。Pentagon CTO「Anthropic blacklist 維持／Mythos は別」と発言（本エントリ）**

## 試すなら

（このエントリは情報追跡用で実践要素なし。Anthropic の方針が今後の Claude プロダクト（特に Acceptable Use Policy・gov't 向けエディションの有無）にどう反映されるかを継続観察）

## ソース

- [Pentagon tech chief says Anthropic is still blacklisted, but Mythos is a separate issue（CNBC）](https://www.cnbc.com/2026/05/01/pentagon-anthropic-blacklist-mythos-michael.html)
- [Pentagon keeps Anthropic barred despite Mythos interest（The Register）](https://www.theregister.com/2026/05/01/mythos_complicates_anthropic_us_gov_breakup/)
- [The Pentagon Just Picked Seven AI Vendors and Anthropic Wasn't One（Roborhythms）](https://www.roborhythms.com/pentagon-ai-contracts-anthropic-excluded-may-2026/)
- [8 AI Companies Win Pentagon Classified Contracts While Anthropic Remains Blacklisted（Orbital Today）](https://orbitaltoday.com/2026/05/03/8-ai-companies-win-pentagon-classified-contracts-while-anthropic-remains-blacklisted/)
- [Pentagon freezes out Anthropic as it signs deals with AI rivals（Military Times）](https://www.militarytimes.com/news/pentagon-congress/2026/05/01/pentagon-freezes-out-anthropic-as-it-signs-deals-with-ai-rivals/)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
