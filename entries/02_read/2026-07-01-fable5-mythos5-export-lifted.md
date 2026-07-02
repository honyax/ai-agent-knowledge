---
date: 2026-07-01
status: read
relevance: A
tags: [claude-fable-5, claude-mythos-5, export-controls, 復活, anthropic-news]
source_urls:
  - https://www.cnbc.com/amp/2026/06/30/anthropic-says-trump-admin-has-lifted-export-controls-on-claude-fable-5-and-mythos-5.html
  - https://www.coindesk.com/tech/2026/07/01/anthropic-restores-ai-models-fable-mythos-after-the-u-s-lifts-export-controls
  - https://9to5mac.com/2026/06/30/claude-fable-5-cleared-to-return-as-us-lifts-anthropics-export-control-restriction/
  - https://www.marketscale.com/industries/software-and-technology/fable-5-and-mythos-5-are-back-what-the-19-day-shutdown-taught-every-enterprise-about-ai-as-infrastructure
experiment_dir: null
---

# Claude Fable 5 / Mythos 5 の輸出規制解除、Fable 5 は 7/1 グローバル復活

## 3行要約

- 6/30、米商務省が Fable 5 / Mythos 5 の輸出規制を解除。**Fable 5 は 7/1 から全世界で利用可能に復活**（Claude Platform, Claude.ai, Claude Code, Claude Cowork）。19 日間の停止を経ての正式復帰。
- 発端は Amazon が発見した「Fable 5 が exploit code を生成する jailbreak」で 6/13 に export ban ([[2026-06-13-fable5-mythos5-export-ban]]) が発動していた案件。政府レビューで解除。
- Pro / Max / Team / 一部 Enterprise ユーザーは 7/7 まで通常 usage limit の 50% 制限付きで利用開始、以降は usage-based クレジットへ。**Mythos 5 は「承認済み米国組織のみ」に段階的復帰**、Fable 5 と扱いが異なる。

## 自分への関連度: A

[[2026-06-11-claude-fable-5-mythos-5-release]] / [[2026-06-13-fable5-mythos5-export-ban]] からの続報として重要。自分は Opus 4.7 / Sonnet 4.6 系を主に使っており Fable 5 は直近ワークフローの主力ではないが、Anthropic の政府対応・供給リスクの実例として企業導入判断に効く材料。Anthropic IPO ([[2026-06-02-anthropic-ipo-confidential-filing]]) 前の 19 日間停止が「AI をインフラとして扱う際のリスク」を突きつけた点は重い。

## 詳細

### タイムライン

| 日付 | 出来事 |
|------|--------|
| 6/11 | Fable 5 / Mythos 5 リリース ([[2026-06-11-claude-fable-5-mythos-5-release]]) |
| 6/13 頃 | Amazon が jailbreak → exploit code 生成を発見 |
| 6/13〜 | 米商務省が輸出規制、非米国ユーザーへの提供停止 ([[2026-06-13-fable5-mythos5-export-ban]]) |
| 6/30 | 商務省が輸出規制解除を発表 |
| 7/1 | **Fable 5 が全世界で復活**（Pro/Max/Team/一部 Enterprise は 7/7 まで 50% cap） |
| 7/7 以降 | 通常の usage-based クレジット運用へ |

### Fable 5 と Mythos 5 の扱いの違い

- **Fable 5**: 7/1 に全世界復活。Claude Platform / Claude.ai / Claude Code / Claude Cowork 全面。
- **Mythos 5**: 「Less restricted variant」として、承認済み米国組織のみに段階復帰。
- 「同じ underlying model のバリアント」だが、Mythos 5 側はより高リスクとみなされて扱いが厳格。

### 「19 日間の教訓」

- MarketScale の分析: 「AI をインフラとして扱う企業に、供給停止リスクの現実を突きつけた 19 日間」
- 単一プロバイダ・単一モデル依存の危険性。マルチプロバイダ設計（Bedrock / Vertex / Foundry など）や、モデル切替可能な抽象化の重要性が改めて議論に。
- Claude Apps Gateway ([[2026-07-01-claude-apps-gateway-bedrock-vertex]]) の「provider 間 failover」機能はまさにこの文脈で活きる。

### Anthropic 側の対応

- 政府レビュー期間中に jailbreak を修正・再監査。
- CEO Dario Amodei が復活を公式声明（各種メディアに引用）。
- Opus 4.8 / Sonnet 4.6 / Sonnet 5 は影響なく提供継続だった（規制対象は Fable 5 / Mythos 5 のみ）。

## 試すなら

1. Fable 5 を使う予定がある場合、7/1 復活直後は 50% cap を意識してヘビータスクは 7/7 以降に回す。
2. 自分の Claude Code / Claude API 設定で、モデル指定が Fable 5 系にハードコードされていないか確認（あれば `claude-sonnet-5` などへのフォールバックを準備）。
3. 中長期のリスク対策として、[[2026-07-01-claude-apps-gateway-bedrock-vertex]] のような provider failover モデルを念頭に、Bedrock / Vertex アクセス経路を整理。
4. Anthropic の「今後の類似停止」に備え、Sonnet 5 ([[2026-07-01-claude-sonnet-5-release]]) や Opus 4.8 の代替運用パスを常時 1 本用意しておく。
5. 企業導入検討時、19 日停止事案を「単一 AI 依存リスク」の実例として提示できるよう整理。

## ソース

- [Anthropic says Trump admin has lifted export controls on Fable 5 and Mythos 5 (CNBC)](https://www.cnbc.com/amp/2026/06/30/anthropic-says-trump-admin-has-lifted-export-controls-on-claude-fable-5-and-mythos-5.html)
- [Anthropic restores AI models Fable, Mythos after US lifts export controls (CoinDesk)](https://www.coindesk.com/tech/2026/07/01/anthropic-restores-ai-models-fable-mythos-after-the-u-s-lifts-export-controls)
- [Claude Fable 5 cleared to return (9to5Mac)](https://9to5mac.com/2026/06/30/claude-fable-5-cleared-to-return-as-us-lifts-anthropics-export-control-restriction/)
- [Fable 5 and Mythos 5 Are Back. What the 19-Day Shutdown Taught Every Enterprise (MarketScale)](https://www.marketscale.com/industries/software-and-technology/fable-5-and-mythos-5-are-back-what-the-19-day-shutdown-taught-every-enterprise-about-ai-as-infrastructure)

---

## 感想・考察

### 実地確認: Claude アプリでは復活済み、Claude Code はまだ（2026-07-03）

自分の環境で確認したところ:

- **Claude アプリ（Claude.ai）**: Fable 5 が選択可能になっている ✅
- **Claude Code（VSCode 拡張版）**: **まだ Fable 5 が使えない** ❌

報道記事の「Claude Platform / Claude.ai / Claude Code / Claude Cowork 全面復活」という記述は、実態としては **プラットフォームごとに展開タイミングがずれている** 可能性が高い。よくあるパターンとして、Claude.ai（Web/アプリ）が先行し、Claude Code や API 経由の展開は数日遅れることがある。

### 追加確認: ターミナル CLI では選択できた（2026-07-03）

その後、ターミナルで `claude` コマンドを直接実行したところ **Fable 5 を選択できた**。つまり問題は「Claude Code 全体」ではなく、**VSCode 拡張版（[[user_environment]] で常用している実行環境）側の対応が遅れている**、というのが正確な切り分け。

- **ターミナル CLI**: Fable 5 選択可能 ✅
- **VSCode 拡張版**: Fable 5 選択不可 ❌

[[user_environment]] の「VSCode ネイティブ拡張版は fullscreen renderer 依存機能が使えない」という既知の制約と同系統で、**新機能ロールアウトが CLI 版 → VSCode 拡張版の順で遅延するパターン**が今回も再現した形。モデル追加のような基本機能でもタイムラグが起きることが確認できた。

### この件の教訓

- 報道の「全面対応」は必ずしも「同時に」を意味しない。特に段階的ロールアウトをする Anthropic の場合、リリースアナウンスの文言だけでなく実地確認が必要。
- 「Claude Code で使えない」と思ったら、**VSCode 拡張版と CLI 版を切り分けて確認する**のが定石になりそう。CLI 版の方が新機能が先行する傾向。
- 自分は現状 [[feedback_model_preference]] の通り Sonnet 5 を常用しているため、Fable 5 の VSCode 拡張版対応待ちによる実害は今のところない。

### 次に見たいこと

- VSCode 拡張版で Fable 5 が選択可能になるタイミング（次回 catch-up で追跡）
- Mythos 5 は元々「承認済み米国組織のみ」なので、そもそも自分の環境では対象外の可能性が高い（要確認だが優先度低）

<!-- /try 実行時に自動生成 -->
