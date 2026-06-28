---
date: 2026-06-11
status: read
relevance: S
tags: [claude, model-release, fable-5, mythos-5, anthropic]
source_urls:
  - https://www.anthropic.com/news/claude-fable-5-mythos-5
  - https://techcrunch.com/2026/06/09/anthropic-released-claude-fable-5-its-most-powerful-model-publicly-days-after-warning-ai-is-getting-too-dangerous/
  - https://www.cnbc.com/2026/06/09/anthropic-mythos-claude-fable-5.html
  - https://simonwillison.net/2026/Jun/9/claude-fable-5/
experiment_dir: null
---

# Claude Fable 5 / Mythos 5 リリース — Mythosクラスが一般提供開始、Proプランは6/22まで無料

## 3行要約

- 2026-06-09、AnthropicがClaude Fable 5（Mythosクラスを一般利用向けに安全化した初の公開モデル）とClaude Mythos 5（Project Glasswing参加組織・選別された生命科学研究者向けの制限解除版）をリリース。ソフトウェアエンジニアリング・知識労働・ビジョンでほぼ全ベンチマークのstate-of-the-artを更新。Stripeのテストでは「5,000万行のコードベースで2ヶ月要する作業を1日で完了」。
- 安全設計はフォールバック方式: サイバーセキュリティ・生物化学・蒸留攻撃の3分類器が高リスク出力を検知するとOpus 4.8に切り替えて応答を継続（拒否ではない）。95%以上のセッションはフォールバックなし。API価格は入力$10/M・出力$50/M（Mythos Previewの半分以下）、モデルIDは `claude-fable-5`。
- **Pro/Max/Team/Enterpriseプランには6/9〜6/22の期間限定で追加費用なしで含まれる**。6/23以降は使用クレジット購入が必要になり、容量が確保でき次第標準プランへ復帰予定。Claude Code v2.1.170でFable 5対応済み。

## 自分への関連度: S

Proプランで6/22まで無料で使える期間限定ウィンドウが今まさに開いている。Claude Codeでの体感差（特に大規模コードベース・長時間自律作業）を今のうちに確認しておかないと、6/23以降は追加コストが発生する。

## 詳細

- 5/28のOpus 4.8リリース時に予告されていた「Mythosクラスを数週間以内に全顧客へ」（[[2026-06-06-project-glasswing-expansion-claude-security]]）が実現した形。
- TechCrunchは「AIが危険になりすぎていると警告した数日後に最強モデルを公開」という構図（6/5の協調的一時停止提言 → [[2026-06-11-anthropic-coordinated-pause-proposal]]）を指摘。
- 長文コンテキストは数百万トークン対応をうたい、メモリ活用による性能向上も実証と発表。
- Mythosクラスのトラフィックは30日間保持義務あり（新規モデル学習には不使用）。
- 同時期の6/5にOpus 4.1のリタイア通知も出ている（移行先はOpus 4.8推奨）。

## 試すなら

1. Claude Code を v2.1.170 以上に更新する
2. `/model` で Fable 5 を選択（Proプランは6/22まで追加費用なし）
3. 普段のタスク（Unity/C#のリファクタや調査系タスク）をOpus 4.8と同条件で投げて体感差を比較
4. 長コンテキストの調査タスク（大きめのリポジトリ全体読解）で差が出るか確認
5. 6/22までに「クレジットを払ってでも使い続けたいか」を判断する

## ソース

- [Claude Fable 5 and Claude Mythos 5 (Anthropic)](https://www.anthropic.com/news/claude-fable-5-mythos-5)
- [Anthropic releases Claude Fable, a version of Mythos, days after warning AI is becoming too dangerous (TechCrunch)](https://techcrunch.com/2026/06/09/anthropic-released-claude-fable-5-its-most-powerful-model-publicly-days-after-warning-ai-is-getting-too-dangerous/)
- [Anthropic releases Mythos-like AI model to the public, Claude Fable 5 (CNBC)](https://www.cnbc.com/2026/06/09/anthropic-mythos-claude-fable-5.html)
- [Initial impressions of Claude Fable 5 (Simon Willison)](https://simonwillison.net/2026/Jun/9/claude-fable-5/)

---

## 感想・考察

### 性能以外の論点 — 「規制・監視前提のリリース設計」（2026-06-24 議論）

性能ベンチマーク以外で押さえるべき要素を3つ整理。

**1. フォールバック型の安全機構（新規性あり）**
- 3分類器（サイバーセキュリティ / 生物化学 / 蒸留攻撃）が高リスク出力を検知すると Opus 4.8 に切り替えて応答継続する設計。「拒否」ではない。
- ユーザー体験を壊さずに安全側へ落とす方式。後の輸出規制（[[2026-06-13-fable5-mythos5-export-ban]]）で議論された「jailbreakがコード修正という良性文脈で起きうる」問題と地続き。
- 95%以上のセッションはフォールバック発動なしと公表。

**2. 階層化されたモデル提供**
- 一般公開は安全化版の Fable 5、フル版 Mythos 5 は Project Glasswing 参加組織と選別された生命科学研究者に限定。
- 「同じクラスのモデルを能力レベルでセグメント配布」する形は、後の輸出規制（外国人アクセス停止）と同じ思想の延長。
- 最初から「誰に何を出すか」を細かく制御する前提のリリース。

**3. 警告→リリースの矛盾構図とトラフィック保持義務**
- 6/5の協調一時停止提言（[[2026-06-11-anthropic-coordinated-pause-proposal]]）の4日後に最強モデル公開、というポジショントーク的側面。
- Mythosクラスはトラフィック30日間保持義務つき。これは後の規制対応で「政府への証跡提示能力」を担保する伏線とも読める。

**総括**: 性能ニュースの裏で「Anthropicは最初からこのモデルを規制・監視前提で出していた」ことが読み取れる。リリース構造そのものが後の輸出規制への伏線・対応準備になっていた、と振り返ると整合的。
