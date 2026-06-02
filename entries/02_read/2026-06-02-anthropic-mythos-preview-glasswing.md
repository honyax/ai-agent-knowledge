---
date: 2026-06-02
status: read
relevance: A
tags: [anthropic, mythos, security, cyber, zero-day, glasswing, model-release]
source_urls:
  - https://red.anthropic.com/2026/mythos-preview/
  - https://fortune.com/2026/05/29/anthropic-raises-65-billion-at-record-965-billion-valuation-promises-mythos-ai-model-in-wide-release-in-coming-weeks-releases-claude-opus-4-8/
  - https://www.theregister.com/security/2026/05/25/anthropic-to-release-mythos-class-models-to-the-public/5245596
  - https://www.radware.com/blog/anthropic-claude-mythos-and-the-2026-cybersecurity-landscape/
  - https://www.eweek.com/news/anthropic-mythos-cyber-ai-public-release/
experiment_dir: null
---

# Claude Mythos Preview と Project Glasswing — ゼロデイを発見・連鎖させる「サイバー特化」モデルの段階公開

## 3行要約

- Anthropic が Claude Mythos Preview を公開。汎用言語モデルだがとりわけコンピュータセキュリティが突出して強く、ユーザー指示下で主要OS・主要ブラウザのゼロデイ脆弱性を「発見し、さらにエクスプロイト化」でき、クローズドソース製品のリバースエンジニアリングにも長けるとされる。
- この能力への対応として Anthropic は Project Glasswing を立ち上げ、Mythos Preview を使って世界の重要ソフトウェアの防御を固め、攻撃者に先んじる運用慣行を業界に準備させる方針。Mythos Preview 自体は一般提供しない（GAなし）が、Mythos クラスのモデルは「coming weeks」で全顧客に展開予定（Series H と同時告知 → [[2026-05-28-anthropic-series-h-965b-valuation]]）。
- 安全に大規模展開するには「最も危険な出力を検知・ブロックするサイバーセキュリティ・セーフガード」の開発が前提、というのが Anthropic の整理。攻撃にも防御にも効く明確なデュアルユース・モデル。

## 自分への関連度: A

CLAUDE.md の関心領域#3（AI開発ツールのセキュリティリスクと対策）に直結。コーディングエージェントが「脆弱性の発見＋エクスプロイト連鎖」まで踏み込む段階に入ったことは、サプライチェーン攻撃や偽インストーラ（[[2026-05-16-fake-claude-code-installer-cookie-stealer]]）といった既存の脅威の温床がさらに高度化することを意味する。防御側として `security-guidance` プラグインや自作のセキュリティポリシー（[[2026-05-18-aigis-claude-code-security-policy]]）の重要度が一段上がる。即実践ではないが、近い将来のリスク評価の前提として押さえておくべき。

## 詳細

- **能力**: 「performs strongly across the board, but strikingly capable at computer security tasks」。ユーザーの指示があれば、あらゆる主要OS・主要ブラウザのゼロデイを特定→エクスプロイト化。リバースエンジニアリング能力が極めて高く、クローズドソースのブラウザ・OSの脆弱性発見にも使われた。
- **Project Glasswing**: 攻撃能力の裏返しとして、Mythos Preview を「守りに使う」取り組み。重要ソフトウェアの防御強化と、業界の防御運用の底上げが目的。初出は 2026/04 の発表時で、当時は「危険すぎて非公開」・Glasswing パートナー40社超の限定展開という整理だった（[[2026-04-08-project-glasswing-mythos]]）。今回はそこから「Mythos クラスを数週間以内に全顧客展開」へと方針が一歩進んだ続報にあたる。
- **提供方針**: Mythos Preview は GA しない。最終目標は Mythos クラスを安全に大規模展開できる状態にすることで、そのために危険出力の検知・ブロック機構が必要。Mythos クラス自体は数週間以内に全顧客へ。
- **文脈**: Opus 4.8 リリース・Series H（$965B 評価・$65B 調達）と同タイミングの告知。Mythos は「step change in capabilities」と評され、初出はデータ漏洩で存在が露見した 2026/03 時点に遡る。

## 試すなら

1. （試用対象ではない）Mythos Preview は一般提供されないため、まずは red.anthropic.com の preview ノートで能力範囲とセーフガード方針を読む。
2. 自分の防御スタックを点検: `security-guidance` プラグインの有効化状況、依存パッケージ・MCP サーバの出所確認フローを再確認。
3. Mythos クラスの一般展開アナウンスを `/catch-up` でウォッチし、API/Claude Code 側のアクセス制御がどう変わるか追う。

## ソース

- [Claude Mythos Preview（red.anthropic.com）](https://red.anthropic.com/2026/mythos-preview/)
- [Anthropic leapfrogs OpenAI with $965B valuation, promises Mythos（Fortune）](https://fortune.com/2026/05/29/anthropic-raises-65-billion-at-record-965-billion-valuation-promises-mythos-ai-model-in-wide-release-in-coming-weeks-releases-claude-opus-4-8/)
- [Anthropic to release Mythos-class models to the public（The Register）](https://www.theregister.com/security/2026/05/25/anthropic-to-release-mythos-class-models-to-the-public/5245596)
- [Anthropic Claude Mythos and the 2026 Cybersecurity Landscape（Radware）](https://www.radware.com/blog/anthropic-claude-mythos-and-the-2026-cybersecurity-landscape/)
- [Anthropic May Open Mythos Cyber AI to the Public Within Weeks（eWeek）](https://www.eweek.com/news/anthropic-mythos-cyber-ai-public-release/)

---

## 感想・考察

### 「Mythos を近々公開するのか？」の整理（2026-06-02 のやり取り）

「非公開だった Mythos がそのまま公開される」という理解は不正確。2つの別物を区別する必要がある。

- **Mythos Preview（当該プレビュー版モデル）**: ゼロデイの発見＋エクスプロイト化までできる、今回 red.anthropic.com で能力が公表されたモデル本体。これは **一般提供しない（GAなし）**。危険な能力を持つため、そのまま配布はしない。
- **Mythos クラス（Mythos 世代のモデル群）**: Preview と同系統で、おそらくセーフガードを組み込んだ展開可能な版。これが **「coming weeks（数週間以内）」に全顧客へ展開予定**。

つまり段階公開の構図:
1. 危険な Mythos Preview で能力と防御策を検証
2. 最終目標は「Mythos クラスを**安全に**大規模展開できる状態にすること」
3. そのために危険出力を検知・ブロックするセーフガードの開発が前提
4. 並行して Project Glasswing で、その能力を防御側（重要ソフトの防御強化・業界の運用底上げ）に先回りで活用

→「公開されないもの＝能力を実証した Preview 本体」「近々展開されるもの＝セーフガードを固めた Mythos クラス」。Mythos の存在自体は 2026/03 のデータ漏洩で先に露見しており、今回が正式な能力公表のタイミング。数週間以内の展開アナウンスは `/catch-up` でウォッチ対象。
