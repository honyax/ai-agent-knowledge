---
date: 2026-07-04
status: read
relevance: A
tags: [claude-cowork, cloud, mobile, web, beta, anthropic-news]
source_urls:
  - https://www.nbcnews.com/tech/tech-news/anthropic-will-make-claude-cowork-available-users-cloud-rcna353218
  - https://woyable.com/en/posts/claude-cowork-web-mobile-expansion
  - https://support.claude.com/en/articles/12138966-release-notes
experiment_dir: null
---

# Claude Cowork がクラウド化: Web / モバイルからアクセス、デバイスオフラインでもタスク継続

## 3行要約

- Anthropic が **Claude Cowork のクラウド移行**を発表。従来のデスクトップ常駐から、**セッションがクラウド側で実行**される形になり、Web / モバイル（iOS / Android）からアクセス可能に。ロールアウトは 7/7 予定。
- **デバイスがオフラインでもタスクが走り続ける**: デスクで開始 → スマホで進捗確認 → ラップトップを閉じても作業継続。ファイルとセッション状態は Claude アカウントに保存され、デバイス間で引き継がれる。スケジュールタスクはデバイス 0 台オンラインでも完走する。
- ベータは **Max プランから**、数週間かけて他プランへ拡大予定。記念として Cowork の**倍増 usage limit を 8/5 まで延長**。Claude chat と Cowork を**同一スペースに統合**し、同じファイルにアクセスできるようにする計画も同時発表。

## 自分への関連度: A

CLAUDE.md 関心領域 7（Claude.ai の新機能 — Cowork、日常の情報収集・分析に使用）に直結。自分は Pro プラン（[[user_claude_plan]]）なので初期ベータ（Max 限定）の対象外だが、「数週間で他プランへ拡大」とあるため近く使えるようになる見込み。「タスクを投げてデバイスを閉じても続く」のは、[[2026-03-30-cloud-scheduled-tasks]]（Routines）や [[2026-07-01-claude-code-v21198-background-agents-auto-pr]]（background agents）と同じ「非同期 AI 作業」の Cowork 版で、日常の情報収集を仕込んでおく用途が広がる。

## 詳細

### 何が変わるか

| 項目 | 従来（デスクトップ Cowork） | クラウド Cowork |
|------|---------------------------|----------------|
| 実行場所 | ローカルデバイス上 | クラウド（リモートセッション） |
| アクセス | デスクトップアプリのみ | Web + iOS / Android + デスクトップ |
| デバイスオフライン時 | タスク停止 | **継続実行** |
| セッション状態 | デバイスローカル | Claude アカウントに保存、デバイス間で共有 |
| スケジュールタスク | デバイス起動が前提 | デバイス 0 台でも完走 |

### ロールアウト

- **7/7**: Web / モバイルへの展開開始
- **ベータ順序**: Max プラン → 数週間で他プラン拡大
- **プロモ**: Cowork の倍増 usage limit を 8/5 まで延長

### Claude chat との統合計画

- Claude chat と Cowork を同一スペースに置き、**同じファイルへのアクセス**を共有させる計画。
- 「チャットで相談 → そのまま Cowork にタスク委任」の導線が滑らかになる見込み。

### 文脈

- [[2026-04-09-claude-cowork-ga-enterprise]] で GA + Enterprise 展開 → 今回でデバイス非依存のクラウドサービスへ。
- Claude Tag（[[2026-07-01-claude-tag-slack-multiplayer]]）が「チームの Slack 常駐」、クラウド Cowork が「個人のマルチデバイス常駐」という住み分けに見える。
- 「デバイスから独立して動く AI エージェント」は Claude Managed Agents（[[2026-05-22-claude-managed-agents-sandbox-mcp-tunnels]]）の個人版とも言える方向性。

## 試すなら

1. Pro プランへのベータ拡大を待つ（数週間とのこと）。次回 catch-up で拡大状況を確認。
2. 拡大されたら、普段デスクトップ Cowork でやっている情報収集タスクをクラウド版で走らせ、モバイルからの進捗確認を試す。
3. スケジュールタスク（デバイスオフライン完走）を 1 つ仕込み、Routines（[[2026-03-30-cloud-scheduled-tasks]]）との使い分けを整理する。
4. Claude chat との統合が来たら、「チャット → Cowork 委任」の導線を試す。

## ソース

- [Anthropic will make Claude Cowork available to users via the cloud (NBC News)](https://www.nbcnews.com/tech/tech-news/anthropic-will-make-claude-cowork-available-users-cloud-rcna353218)
- [Claude Cowork Expands to Web and Mobile: What Changed (Woyable)](https://woyable.com/en/posts/claude-cowork-web-mobile-expansion)
- [Release notes (Claude Help Center)](https://support.claude.com/en/articles/12138966-release-notes)

---

## 感想・考察

### クラウド化で何が出来るようになるか

- Cowork の仕事(Web調査、レポート・スプレッドシート作成、ファイル分析)の大半は、実は「ローカルPCである必要」がなかった。本質的に必要なのは計算資源 + ブラウジング + 成果物の置き場で、クラウド化はその置き場をローカルディスクから Claude アカウント上のストレージに移した変化。
- 出来るようになること: (1) デバイスを閉じてもタスク継続、(2) スケジュールタスクの無人完走(デバイス0台でも)、(3) モバイル・Web からの進捗確認と追加指示、(4) セッション状態のデバイス間引き継ぎ。
- 逆に「ローカルPC上のファイル作成・編集」はクラウドセッションから直接は届かない。置き換えではなく、ローカル密着タスクはデスクトップ、非同期・調査・生成系はクラウド、という使い分けになる。

### Claude Code ローカル利用者にとってのローカル Cowork の価値

- 機能的な優位は薄い(ファイル操作・スクリプト・Web調査は Claude Code で全部でき、制御の細かさは Claude Code が上)。
- それでも残るメリット: (1) Office 系成果物(Excel/Word/PPT)の生成・編集・プレビューが標準装備で「見た目確認して微調整」の往復が速い、(2) git でもプロジェクトでもない雑多なタスクの置き場として、開発(Claude Code)と生活・事務(Cowork)の関心事分離、(3) Dispatch(スマホ起票)の受け皿、(4) 当面は Cowork 倍増 usage limit(8/5まで)で Pro プランの Claude Code 枠を開発に温存できる実利。
- クラウド化で情報収集・調査系はクラウド Cowork に移っていくため、ローカル Cowork の固有領分は「ローカルファイルを触る非開発タスク」に絞られていく見立て。

### クラウド版 Claude Code に対するクラウド版 Cowork の利点

- 実行環境はほぼ同じ(Anthropic のクラウドサンドボックス)で、違いは作業モデルと成果物の形。クラウド版 Claude Code は「リポジトリ in、PR out」のパイプライン、クラウド版 Cowork は「個人のファイルワークスペースごとクラウド常駐」。
- Cowork の利点: (1) リポジトリ不要でファイルがアカウントに永続、(2) 成果物を PR の diff ではなく文書(プレビュー付き)としてそのまま受け取れる、(3) Claude chat との同一スペース統合計画(チャット相談 → タスク委任 → 成果物が同じ場所に溜まる導線)、(4) Gmail/Calendar/Drive コネクタや Dispatch など個人コンテキストとの接続。
- 自分のこのナレッジベース運用は「Cowork 的な仕事を Claude Code の作法(git + skills)に載せた」構成。バージョン管理・ステータス管理・/catch-up 等が効いているので Claude Code 側に置く合理性が明確にある。線引きは「成果物を git で管理したいか、文書として受け取りたいか」。
