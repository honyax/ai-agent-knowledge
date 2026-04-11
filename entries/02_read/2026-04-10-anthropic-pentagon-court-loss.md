---
date: 2026-04-10
status: read
relevance: C
tags: [anthropic, legal, policy]
source_urls:
  - https://www.axios.com/2026/04/08/anthropic-loses-bid-to-block-pentagon-blacklisting
experiment_dir: null
---

# Anthropic、Pentagon ブラックリスト阻止の訴訟で敗訴

## 3行要約

- Anthropic が米国防総省（Pentagon）のブラックリスト指定に異議を申し立てた訴訟で、DC連邦裁判所が Anthropic の申し立てを棄却
- Anthropic は Claude の軍事利用に関する倫理的立場を理由に Pentagon との契約締結を拒否→制裁措置に発展した経緯
- 法的手段での撤回は現時点では不成功。Anthropic の政府・軍事分野との関係が今後どうなるかが注目点

## 自分への関連度: C

直接の開発ワークフローへの影響はない。ただし Anthropic の政策・存続リスクの文脈として知っておく程度の情報。

## 詳細

4月8日、DC連邦地裁は Anthropic の申し立てを棄却。Pentagon によるブラックリスト指定は維持される。

**背景（既存エントリ `2026-04-04-anthropic-pentagon-trump-appeal.md` 参照）:**
- Trump 政権が Anthropic の Claude を軍事利用しようとしたが、Anthropic はセーフガード条件なしの利用を拒否
- Pentagon が Anthropic をブラックリスト指定
- Anthropic が Trump 政権の控訴に応じて訴訟

今回の判決で法的手段による撤回は一時失敗。今後の行政上の争い・交渉が焦点となる。

**補足：裁判所間で判断が分裂**

一方、サンフランシスコ連邦地裁（別訴訟）では2026年3月26日に Rita Lin 判事が Anthropic の仮差し止めを認め、「Claudeの使用禁止令の執行停止」を命じた。DC巡回裁判所とサンフランシスコ地裁で判断が分裂した結果、現状は以下の通り：

- 国防総省（Pentagon）との契約からは排除（DC巡回裁判所の判決）
- その他の政府機関とは引き続き取引可能（サンフランシスコ地裁の仮差し止め）

DC巡回裁判所は口頭弁論を2026年5月19日に設定している。

## 試すなら

特になし（情報把握のみ）

## ソース

- [Anthropic loses bid to block Pentagon blacklisting in DC court (Axios)](https://www.axios.com/2026/04/08/anthropic-loses-bid-to-block-pentagon-blacklisting)

---

## 感想・考察

Anthropic の立場は「軍事利用全般の拒否」ではなく、「完全自律型兵器」と「国内大規模監視」という2点に限った使用制限の明記を求めただけ。Pentagon 側の「法律で既に禁じている」という反論も一理あるが、「信頼できるなら契約書に書けるはず」というAnthropicの主張も筋は通っている。対立の本質は技術的・安全保障的な問題ではなく、「民間企業が政府の利用に条件をつけることを認めるか否か」という権限・慣例の問題。

サプライチェーンリスク指定は制度の明らかな悪用。この指定は本来、Huawei・ZTE・Kasperskyのような外国政府と繋がりのある企業向けに設計されたもので、米国企業への適用は史上初。セキュリティ上の問題ではなく「契約条件を拒否した」という理由での指定は、法律の文言上は辛うじて可能でも、趣旨からは大きく逸脱している。

一連の動きはトランプ政権特有の「最大圧力→ディール」手法と一致する。ブラックリスト指定と同日に競合のOpenAIと契約、翌日には裏で「ほぼ合意に近い」とメール、というのは示威行為と交渉を同時並行で進める典型的なパターン。意思決定の構造は「大方針はTrump（Truth Socialで宣言）、法的手続きはHegseth、実務交渉はEmil Michael」という分業体制。

ただしHegsethがトランプへの情報をフィルタリングしている可能性もあり、「Pentagon＝トランプの完全な意思通り動いている」とも言い切れない。Hegsethは軍内部でも評価が低く、イラン紛争の戦況を楽観的に報告しているという内部告発も出ており、組織として一枚岩ではない。

MIT Technology Reviewが「文化戦争的な戦術が裏目に出た」と評した通り、Anthropicのブランドはむしろ向上し、英国などが歓迎姿勢を示した。圧力の副作用として、「倫理的なAI企業を政府が弾圧した」という国際的な印象が定着しつつある点はトランプ政権にとっても誤算かもしれない。

5月19日の口頭弁論が次の焦点。
