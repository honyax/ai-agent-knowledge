---
date: 2026-07-01
status: read
relevance: B
tags: [claude-tag, slack, enterprise, multiplayer-ai, anthropic-news]
source_urls:
  - https://www.anthropic.com/news/introducing-claude-tag
  - https://techcrunch.com/2026/06/23/anthropics-claude-tag-is-learning-your-company-one-slack-message-at-a-time/
  - https://venturebeat.com/technology/anthropic-launches-claude-tag-replacing-its-slack-app-with-a-persistent-ai-teammate-that-learns-monitors-and-works-autonomously
  - https://thenewstack.io/anthropic-claude-tag-slack/
experiment_dir: null
---

# Claude Tag in Slack: 「個人会話」から「チームチャンネル」へ単位を移す常駐 AI 同僚

## 3行要約

- 6/23、Anthropic は Slack 用 Claude アプリを Claude Tag に刷新。「個人会話の AI」ではなく「チャンネル単位で 1 体の常駐 Claude」を提供し、誰でも @Claude をタグ付けして対話・タスク委任が可能。前任者が途中まで進めた作業を別メンバーが引き継げる。
- Claude Tag はチャンネルを継続フォローし、文脈と仕事の進め方を学習する。管理者の許可で他チャンネルからの情報自動収集も可能。Team / Enterprise プランでベータ提供。
- Anthropic 社内では Claude Tag をタグ付けすることが「仕事の主流動線」になっており、プロダクトチームのコードの 65% は社内版 Claude Tag が生成。管理者は per-channel でツール/情報/メモリを制限できる。

## 自分への関連度: B

[[user_environment]] は個人開発中心、Slack を業務で使っていない（Discord 主体）ため直接利用機会は薄い。ただし「@Claude をチャンネル常駐させる」モデルは Discord 用カスタム Skill ([[project_custom_skills]] / `/discord:access` 等) との発想が共通しており、将来 Discord 統合が増えた際の参考になる。Anthropic 自身が「PR は Claude Code で 100%、プロダクトコード 65% は Claude Tag」と明かしているのは、彼らの内部ドッグフーディングの強度を示す指標として知識として有用。

## 詳細

### 何が変わったか

- **旧 Slack 用 Claude**: 個人ベースの DM ボット風。会話単位。
- **Claude Tag**: チャンネル単位。@Claude メンションで全員と協働、「multiplayer」モデル。誰が見ても同じ Claude が同じ状態を持つ。
- **継続学習**: チャンネル滞在中に話題・進行中の仕事・固有名詞を学習。

### 主要機能

- **タスク委任**: メンションでタスクを投げて、Claude が裏で進捗。完了時にチャンネルへ報告。
- **クロスチャンネル情報収集**: 管理者が許可した他チャンネル/コネクター（Google Drive, GitHub, Linear など）からの情報自動収集。
- **メモリと境界**: per-channel でアクセス可能なツール・情報・メモリを管理者が指定。誤って他チームのメモリにアクセスしない。
- **企業ガバナンス**: Enterprise の SSO / 監査ログ / DLP と統合。

### Anthropic 社内の使い方

- TechCrunch / VentureBeat の報道に「@Claude タグ付けが Anthropic の主要な進め方になった」
- **プロダクトチームのコードの 65%** は内部版 Claude Tag が生成。
- 既存の [[2026-04-04-anthropic-pentagon-trump-appeal]] 系で見えていた「Claude Code で PR の 100%」の主張と合わせ、社内のAI依存度が極端に高い。

### 提供範囲

- ベータ。Claude Team / Claude Enterprise プラン限定。
- Pro プランや個人プラン（[[user_claude_plan]]）では使えない。

### 競合・文脈

- Slack の自前 AI 機能、Microsoft Copilot for Teams との競合関係。
- Anthropic の「AI 同僚」シリーズ（Claude Cowork, Claude Managed Agents, Claude Tag）が連結する形。
- [[2026-06-12-anthropic-public-record-survey]] や [[2026-06-11-claude-fable-5-mythos-5-release]] と同じく、6 月の集中的なエンタープライズ向け攻勢の一環。

## 試すなら

1. 自分は個人プランで使えないので、Anthropic 公式の Demo / 動画で Claude Tag の UX を視聴。
2. Discord 連携の自作 Skill（`/discord:access` / `/discord:configure`）との設計対比を整理。チャンネル常駐型 AI のメモリ境界設計を Discord にも横展開できないか検討。
3. 「per-channel でメモリ/ツール制限」のモデルが将来 Claude Code 個人版にも降ってくるか観察（プロジェクト境界とコンテキスト分離の設計参考）。
4. Anthropic の社内利用統計（65% など）を継続ウォッチし、Anthropic 内部の「AI 依存度」が次の機能リリースにどう反映されるか追う。

## ソース

- [Introducing Claude Tag (Anthropic 公式)](https://www.anthropic.com/news/introducing-claude-tag)
- [Anthropic's Claude Tag is learning your company, one Slack message at a time (TechCrunch)](https://techcrunch.com/2026/06/23/anthropics-claude-tag-is-learning-your-company-one-slack-message-at-a-time/)
- [Anthropic launches Claude Tag, replacing its Slack app (VentureBeat)](https://venturebeat.com/technology/anthropic-launches-claude-tag-replacing-its-slack-app-with-a-persistent-ai-teammate-that-learns-monitors-and-works-autonomously)
- [Anthropic gives @Claude a permanent seat in your Slack channels (The New Stack)](https://thenewstack.io/anthropic-claude-tag-slack/)

---

## 感想・考察

### 「チャンネルに 1 ユーザーとして常駐する Claude」への期待（2026-07-03）

「Claude が Slack 内に 1 ユーザーとして常駐する」という理解で合っている。従来の「個人が DM で呼び出す AI」から「チャンネル全員が共有する 1 体の Claude」へのモデル転換が本質。

**まだ使っていない**が、以下の点にワクワクしている:

- 誰か1人が途中まで進めた作業を、別メンバーが「Claude、続きお願い」で引き継げる継続性
- チャンネルの文脈・固有名詞・進行中の仕事を Claude 自身が学習し続ける点（1回限りの会話ではなく「そのチームの一員」に育っていく感覚）
- per-channel でメモリ/ツールを分離できる設計（誤って他チームの情報にアクセスしない安全性と、チャンネルごとに育つ「専門性」の両立）

### 現状は Discord 主体、Slack は未導入

[[user_environment]] の通り業務で Slack を使っておらず、個人プランでは Claude Tag 自体使えない（Team/Enterprise 限定）。実際に触る機会は当面なさそうだが、「チャンネル常駐型 AI」というコンセプト自体は自作 Discord Skill（`/discord:access` 等、[[project_custom_skills]]）の設計にも応用できそうな発想として記憶しておきたい。

### 次に見たいもの

- Anthropic 公式のデモ動画・UX 紹介
- 個人プランやより小規模なチーム向けに降りてくる可能性があるか（Discord 版の類似機能を自分で作る際の参考にもなる）

<!-- /try 実行時に自動生成 -->
