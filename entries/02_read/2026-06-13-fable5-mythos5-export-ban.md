---
date: 2026-06-13
status: read
relevance: A
tags: [anthropic, fable5, mythos5, export-control, security, regulation]
source_urls:
  - https://www.anthropic.com/news/fable-mythos-access
  - https://simonwillison.net/2026/Jun/13/us-government-directive-to-suspend-access/
  - https://thehackernews.com/2026/06/us-orders-anthropic-to-suspend-fable-5.html
  - https://snyk.io/blog/fable-mythos-suspension-security-takeaways/
experiment_dir: null
---

# 米政府がAnthropicにFable 5 / Mythos 5の外国人アクセス停止を指示

## 3行要約

- 米政府は2026/06/13、輸出管理権限を根拠にFable 5とMythos 5への外国人（米国内外問わず、Anthropicの外国籍従業員も含む）によるアクセス停止を指示した。
- 政府側の理由は「特定のコードベースを読ませてソフトウェア欠陥を修正させる」狭いjailbreakが存在する可能性で、現状口頭での非公開情報のみ。Anthropic側は「誤解」として早期復旧を目指す方針。
- 他Anthropicモデル（Opus/Sonnet/Haiku等）は影響なし。ただし最新フラッグシップ2モデルが急に使えなくなる事態は、AI開発ツール依存度の高い開発者にとってサプライチェーンリスクの顕在化例。

## 自分への関連度: A

Fable 5は普段使い候補のモデル。直接影響はないが、特定モデルへの政治的・規制的シャットダウンが現実に起きうるという事実は、Claude Code/Claude APIに業務を依存する人間として継続監視すべき。ゲーム内AIにClaude APIを使う場合、モデル選定の冗長化戦略にも影響する。

## 詳細

- 指示が来た時刻: 2026/06/13 17:21 ET
- 対象範囲: 「Fable 5とMythos 5」の2モデルのみ。米国市民向けは影響なし。
- 「foreign national」の定義は米国籍以外すべて。海外法人の従業員だけでなくAnthropic社内の外国籍従業員も含む。
- Anthropic公式声明: "We believe there has been a misunderstanding" として早期復旧に向けて政府と対話中。
- Snykの分析記事は「セキュリティ部門への教訓」として、(1) モデル供給の集中リスク、(2) jailbreakがコード修正という"良性"の文脈でも規制対象になりうる、(3) ベンダー切替計画の必要性 を指摘。

## 試すなら

1. 自分の利用しているClaude Codeでデフォルトモデルを確認（Opus 4.8 / Sonnet 4.6 / Haiku 4.5は影響なし）。
2. APIを使うコードがあれば、Fable 5/Mythos 5依存箇所を検索（モデルIDの文字列 `claude-fable-5`, `claude-mythos-5` など）。
3. 影響がある場合、Opus 4.8等への一時切替で動作確認。
4. 長期的には、複数モデルプロバイダ（Anthropic + OpenAI等）への切替容易性を担保する抽象化レイヤを検討。

## ソース

- [Statement on the US government directive to suspend access to Fable 5 and Mythos 5 \\ Anthropic](https://www.anthropic.com/news/fable-mythos-access)
- [Simon Willison's notes on the directive](https://simonwillison.net/2026/Jun/13/us-government-directive-to-suspend-access/)
- [U.S. Orders Anthropic to Suspend Fable 5 and Mythos 5 Access for Foreign Nationals - The Hacker News](https://thehackernews.com/2026/06/us-orders-anthropic-to-suspend-fable-5.html)
- [Fable Mythos Suspension Security Takeaways - Snyk](https://snyk.io/blog/fable-mythos-suspension-security-takeaways/)

---

## 感想・考察

### その後の状況（2026-07-01 時点メモ）

- 2026-06-30 時点でも Fable 5 / Mythos 5 の外国人向けアクセスは **正式には再開されていない**。
- ただし「一部ユーザー / 一部リージョンで徐々に利用できるようになっている」との噂あり（公式アナウンスなし）。Anthropic は引き続き「誤解」として政府との対話継続を表明している。
- 続報待ちのトピックとして関連情報を継続ウォッチ予定。米政府ディレクティブの完全解除、または恒久化の声明が出たら別エントリ化する。
