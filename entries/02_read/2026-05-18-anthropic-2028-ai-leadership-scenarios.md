---
date: 2026-05-18
status: read
relevance: B
tags: [anthropic, policy, geopolitics, china, export-controls, agi-2028, vision, distillation-attacks]
source_urls:
  - https://www.anthropic.com/research/2028-ai-leadership
  - https://www.rswebsols.com/news/anthropic-claims-agi-could-be-achieved-by-2028-urges-us-to-prevent-china-from-dominating-ai-competition/
  - https://www.eweek.com/news/anthropic-us-china-ai-leadership-2028-apac/
  - https://interestingengineering.com/ai-robotics/anthropic-china-us-ai-race-2028
---

# Anthropic、政策論文「2028: Two Scenarios for Global AI Leadership」公開 — AGI 到達時期を 2028 に据え置き、対中輸出規制と蒸留攻撃対策を強硬要請

## 3行要約

- 2026-05-14、Anthropic が研究ブログに **「2028: Two Scenarios for Global AI Leadership」** を公開。AGI 級モデルの到達時期を 2028 と置き、（A）米国・民主主義陣営が 12〜24 ヶ月のリード維持、（B）中国が事実上のフロンティア並走、という **2 つのシナリオ** を提示。「民主主義がAIの規範を決めるか、権威主義が大規模抑圧をAIで自動化するか」という枠組みで論じる
- 政策提言は 3 本柱: ① **輸出規制の穴埋め**（密輸・海外データセンター経由の H200/Blackwell 入手をブロック、SME 規模の小口販売も対象化）、② **蒸留攻撃の阻止**（米国モデルへの未許可アクセスを立法で抑止、API レート規制やフィンガープリンティング強化）、③ **米国 AI インフラの世界輸出**（民主主義側にレールを敷く）
- 注目点は「中国のフロンティアラボは才能・研究力ではなく **compute access** だけで縛られている」という現状認識。これを前提に、過去数ヶ月の H200 輸出停止（[[h200-china-export-trump-xi-stalemate]]）や Pentagon 契約の動向と整合する**「Anthropic は政策面で対中強硬派」**というポジションを再確認

## 自分への関連度: B

CLAUDE.md の関心領域 8 番（Anthropicの方針・ビジョン）に直結。直接の業務影響はないが、Anthropic が「2028 AGI / 対中強硬 / 民主主義陣営の輸出促進」という政策スタンスを公式に固めたという意味で、Claude / Claude Code 製品の中長期方向を読む材料になる。Pentagon 契約問題（[[pentagon-ai-7vendors-anthropic-excluded]]、[[anthropic-pentagon-court-loss]]）と合わせて読むと、「政府向けにより踏み込みつつ、対中輸出には強硬」という二面的な戦略が見える。

## 詳細

### 2 シナリオの定義
- **シナリオA（好転）**: 米国の compute 優位が維持され、輸出規制強化・蒸留対策・民主主義圏のAI採用加速が成功。米国側が 12〜24 ヶ月のフロンティアリードを保つ
- **シナリオB（破局）**: 抜け穴が放置され、中国がほぼフロンティア並走。AI 規範を権威主義が主導し、自動化された監視・抑圧が世界に拡散

### 政策提言の3本柱
1. **輸出規制の穴埋め**
   - 第三国経由の密輸防止、海外データセンター（東南アジア等）でのリモート利用制限
   - SME（small/medium enterprise）規模の販売チャネルも規制対象化
2. **蒸留攻撃の阻止**
   - 米国モデル API への大量アクセスによる蒸留を立法・契約双方で抑止
   - 「distillation attacks は systematic industrial espionage に当たる」と表現
3. **米国 AI の世界輸出**
   - 民主主義圏の同盟国に米国製モデル・インフラを優先展開
   - Sovereign AI イニシアチブとも整合

### Anthropic が打ち出すスタンスの変化
- 過去（2024〜2025）の Anthropic は安全性・解釈可能性の研究主導の印象が強かったが、2026 に入ってから **「Pentagon 契約申請」「対中強硬政策提言」「Salesforce/PwC/Gates との大型提携」** など、政府・大企業のレールに乗る動きが急加速
- これと並行して Mythos モデル（防衛・諜報用）の存在が報じられており、「研究」「商業」「安全保障」の3軸を同時に動かしている

## 試すなら

1. 原文 PDF を流し読み、特に「12〜24 month lead」の根拠となる compute 試算と「distillation attack」の定義を確認
2. 既存エントリ [[h200-china-export-trump-xi-stalemate]] と組み合わせて、Anthropic 提言と現実の輸出政策のズレ（25%上納金スキーム等）を整理
3. ニュースアラート（"anthropic policy china"）を仕掛け、今後の議会証言・補助金提案を追跡

## ソース

- [2028: Two scenarios for global AI leadership（Anthropic Research、2026-05-14）](https://www.anthropic.com/research/2028-ai-leadership)
- [Anthropic Claims AGI Could Be Achieved by 2028, Urges US to Prevent China From Dominating（RSWebSols）](https://www.rswebsols.com/news/anthropic-claims-agi-could-be-achieved-by-2028-urges-us-to-prevent-china-from-dominating-ai-competition/)
- [Anthropic Predicts US-China AI Race Could Be Decided by 2028（eWeek）](https://www.eweek.com/news/anthropic-us-china-ai-leadership-2028-apac/)
- [Anthropic warns China could overtake the US in global AI race by 2028（Interesting Engineering）](https://interestingengineering.com/ai-robotics/anthropic-china-us-ai-race-2028)

---

## 感想・考察

### Anthropic の政策スタンスは「悩みの可視化」として読む

公表時期と内容から見て、本論文は単独の研究発表ではなく **Anthropic が政府・議会向けに自社のポジションを再定義する政策文書** と捉えるのが妥当。注目点は以下。

### 「民主主義 vs 権威主義」フレーミングのロジック

シナリオB（破局）の「自動化された監視・抑圧」という表現は、中国の compute access を絞らなければ → 中国フロンティアラボがAGI級に追いつく → 中国政府が国内監視・対外影響工作に転用 → 世界に権威主義モデルが輸出される、というロジックで対中規制を正当化する建付け。Anthropic の本音というより、**米政府・議会向けに通用する政策提言の言語** として一貫して打ち出している点を割り引いて読む必要がある。

### トランプ H200 解禁路線とのねじれ

提言3本柱を [[h200-china-export-trump-xi-stalemate]] と照らすと、現政権の H200 解禁＋25% 上納金スキームと真っ向から矛盾する:

| Anthropic 提言（2026-05-14） | トランプ政権の実態 |
|---|---|
| 輸出規制の穴埋め（密輸・第三国経由） | H200 解禁で穴を広げる方向 |
| SME 規模の小口販売も規制対象化 | Alibaba/Tencent 等大口に直接販売許可 |
| 蒸留攻撃を立法で抑止 | 規制ではなく税収化（25% 上納金） |

ただし現実は中国側が買い渋って納入実績ゼロ。**結果的には Anthropic の懸念する compute 流出は起きていない／しかし政策ロジックとしては真逆**、という捻れが現状。

### 「必要悪としての政府接近」3つの読み方

Anthropic は過去軍事用途を明確に拒否してきたが、2024 の Usage Policy 改定以降、Pentagon 契約申請（[[pentagon-ai-7vendors-anthropic-excluded]]、[[anthropic-pentagon-court-loss]]）・Mythos モデル・本論文と、政府接近を加速。これをどう読むか。

- **読み方A（必要悪説）**: 中国脅威への対抗策。信念ではなく戦略的妥協。Amodei のエッセイ "Machines of Loving Grace" 等で繰り返される「民主主義側AIの decisive advantage が安全保障上必須」というロジックと整合
- **読み方B（コマーシャル必然説）**: OpenAI が国防・国務省に深く食い込み、Palantir・Scale AI が国防 AI 市場を取り込む中、Anthropic だけ純粋路線では数十億ドル市場から閉め出される。Colossus 等の compute 調達規模を商業売上だけで賄うのは構造的に困難
- **読み方C（政治的キャプチャ説）**: 当初は必要悪のつもりでも、Pentagon・議会と関係を深めるほど、安全保障コミュニティの世界観に同化していく。「民主主義 vs 権威主義」二項対立フレーム自体がワシントン側の世界観の輸入かもしれない

おそらく A・B・C は **同時に成立** している。個別判断はすべて正当化できる一方、**累積的なドリフトが見えにくい** のが必要悪フレーミングの最大のリスク。Google の Project Maven 騒動（2018）からの軌跡を見ても、テック企業の「一線」は脅威認識のスケールに比例して後退するのが歴史的パターン。

### 影響力と倫理の構造的逆説

Anthropic の苦悩が興味深いのは、彼らが古典的な逆説を意識的に引き受けている点。

- 影響力なし → 信念は守れるが、世界は変えられない
- 影響力あり → 世界は変えられるが、信念は妥協を強いられる

Anthropic は元々 OpenAI の安全性軽視に反対して飛び出した人々が作った会社。その彼らが今、当時の OpenAI に似たポジションに立たされている。これは個人の意志の弱さではなく、**「最先端 AI を作る企業」というポジション自体に内在するベクトル** が個人の信念を上回った構造と読める。

逆に言えば、彼らの当初の信念が無力だったわけでもなく、**Anthropic がやらなければもっと早く・もっと深い妥協が業界標準になっていた** 可能性も高い。「安全性最重視」を掲げる主要プレイヤーが存在することで、業界全体の倫理的下限が引き上げられている側面はある。

多くのテック企業が「我々はビジネスだ」と開き直るか「中立技術だ」と責任回避する中、Anthropic は Amodei のエッセイや今回の 2028 論文のように **苦悩を文書化して晒す** 点で異質。これが誠実さの表れか、悩んでいる姿そのものがブランディングなのかは見る人次第だが、少なくとも完全にキャプチャされていない証拠ではある。

**問題は、何年その状態を維持できるか**。観測ポイントは「Anthropic がいつ、何を理由に NO と言ったか」を継続的に追うこと。今のところ Mythos モデルの用途制限がどこに引かれているかは公表されておらず、ここが今後の判断材料になりそう。
