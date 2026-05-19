---
date: 2026-05-18
status: read
relevance: B
tags: [salesforce, anthropic, benioff, token-spend, coding-agents, slack, intermediary-layer, model-routing]
source_urls:
  - https://thenextweb.com/news/salesforce-benioff-300-million-anthropic-tokens-slack-coding
  - https://letsdatascience.com/news/marc-benioff-announces-300m-anthropic-token-use-90d52de1
  - https://www.benzinga.com/markets/tech/26/05/52622251/salesforce-ceo-marc-benioff-goes-all-in-on-awesome-anthropic-with-300-million-spend-hails-coding-agents-ive-never-been
  - https://www.aol.com/articles/salesforce-ceo-marc-benioff-said-030807000.html
---

# Salesforce、Anthropic に 2026 年だけで $300M トークン消費へ — Benioff「ほぼ全てコーディング」「Slack の中で書ける未来」「intermediary layer が必要」

## 3行要約

- Marc Benioff（Salesforce CEO）が All-In ポッドキャスト（2026-05-16 公開）で、2026 年に Salesforce が **Anthropic トークンを約 $300M 消費する見込み** と発言。「**ほぼ全てコーディング用途**」と明言し、コーディングエージェントを「インフラ」として扱う段階に入ったことを表明
- 同時に Slack 内でのコーディング統合を予告: 「Slack とコードでクールなことをやる、まだ話せないが」。今夏以降、Salesforce の新規顧客には **Slack が AI 有効化済みで自動プロビジョン** される予定で、社内コラボ × AI エージェントの境界を消す方針
- 注目発言は **「intermediary layer」** 構想 — 「全トークンをフロンティアモデルに送る必要はない、ルーティング層で複雑タスクだけ Claude に、簡易タスクは安価モデルに」。$300M スケールでは年間数千万ドルの差になり、コスト最適化が経営イシュー化している

## 自分への関連度: B

業界トップクラスのソフトウェア企業 CEO が「token spend を年間 $300M、ほぼコーディング」と公言したのは、AI コーディングエージェントが **R&D 予算から OpEx へ移った** ことの象徴。Pro / Max プラン以上に「企業がどれだけ Anthropic に依存するか」を示すマクロ指標として観察価値が高い。「intermediary layer」 構想は **モデルルーティング層の標準化** が次の競争点になる可能性を示唆しており、Claude Code 側でも `model: haiku|sonnet|opus` の使い分けが標準パターンになっていきそう。

## 詳細

### 発言内容の要点
| 項目 | 内容 |
|------|------|
| 年間トークン支出 | 約 $300M（2026 年見込み） |
| 用途比率 | 「ほぼ全てコーディング」 |
| Slack 統合 | 「コーディングを Slack の中でできる未来を仕込み中」 |
| 新規顧客への展開 | 今夏以降、Slack を AI 有効化済みで自動プロビジョン |
| コスト最適化 | intermediary layer でモデルルーティング |
| 既存出資 | Salesforce は Anthropic に $300M+ 出資、約 1% 保有 |

### intermediary layer 構想の意味
- 「フロンティアモデルに全部送らない」: 簡易タスクは小型・安価モデル、複雑タスクのみ Claude Opus 4.7
- これは **Claude Code 内の `model:` フィールド・subagent モデル指定・Haiku 4.5 のポジショニング** とそのまま対応する
- 既存エントリ [[ai-agent-comparison-apr2026]] の評価軸とも合致

### 業界全体への含意
- AI コーディングエージェントが **企業の構造コスト** になった（数十億〜数百億円規模が経営判断の対象）
- ベンダー側（Anthropic）は「個別ユーザ単価」より「大企業契約のスケール」が収益柱になる流れ
- 既存エントリ [[anthropic-30b-revenue-google-tpu]]、[[pwc-anthropic-alliance-expansion|2026-05-18 PwC エントリ]] と合わせると、Anthropic の収益構造が「個人 Pro/Max + クラウド販路 + 大型エンタープライズ」の3層で固まりつつある

## 試すなら

1. Salesforce / Anthropic の今後の Slack 統合発表（Dreamforce 2026 等）をウォッチ
2. 自分の環境でも「複雑タスク=Opus 4.7、調査=Haiku 4.5」の subagent ルーティングを整え、intermediary layer のミニ版を体感
3. [[agent-sdk-separate-credit-pool]] と組み合わせ、企業契約と個人サブスクのコスト構造差を整理

## ソース

- [Salesforce expects to spend $300 million on Anthropic tokens this year（TNW）](https://thenextweb.com/news/salesforce-benioff-300-million-anthropic-tokens-slack-coding)
- [Marc Benioff Announces $300M Anthropic Token Use（Let's Data Science）](https://letsdatascience.com/news/marc-benioff-announces-300m-anthropic-token-use-90d52de1)
- [Salesforce CEO Marc Benioff Goes All-In on 'Awesome' Anthropic With $300 Million Spend（Benzinga）](https://www.benzinga.com/markets/tech/26/05/52622251/salesforce-ceo-marc-benioff-goes-all-in-on-awesome-anthropic-with-300-million-spend-hails-coding-agents-ive-never-been)
- [Salesforce CEO Marc Benioff said his company will likely spend $300M on Anthropic tokens（AOL/Yahoo Finance）](https://www.aol.com/articles/salesforce-ceo-marc-benioff-said-030807000.html)

---

## 感想・考察

### Salesforce = Slack の親会社という前提

2021 年に Salesforce が Slack を約 $277 億で買収済み。今回の Benioff 発言「Slack の中でコーディングできる未来」は、自社プロダクトだからこそ言える統合戦略。新規 Salesforce 顧客への自動プロビジョンも、Slack を **Salesforce プラットフォームの UI レイヤー** として再定義する流れの一環。

### 業務 Slack 利用者への想定インパクト

自分の職場も Slack を使っているので、これは「他社事例」ではなく直接効いてくる話。

- バグ報告スレッドで Claude が再現コード・原因仮説を即提示
- コードレビュー依頼の差分プレビューに自動レビューコメント
- 過去の仕様議論スレッドを Claude が要約 → 新人キャッチアップに使える
- データ送信先の懸念: 機密コード・未発表情報の扱いは会社ポリシー要確認

### 「スレッドで議論 → Claude にツール作らせる」フロー

経費精算の愚痴スレッドから、そのまま自動化ツールが生まれる、みたいな流れが現実的になる。**議論スレッドそのものが要件定義書になる** のがインパクトとして大きい。ステークホルダー（スレッド参加者）が自動で紐付き、元発言にリンクバックして合意確認できる構造になる。

### 規模による棲み分け

| 規模 | アウトプット |
|------|-------------|
| 軽量ツール（数百行・単一目的） | スレッド内で完結、動くスクリプト |
| 中規模機能（既存システム改修） | 実装計画書・タスク分割 |
| 大規模機能（設計判断あり） | RFC・ADR・複数 PR への分解 |

判断軸: **影響範囲 / 可逆性 / ステークホルダー数 / データ・権限**。

軽量ツールでも雑な要件のまま走らせると手戻りが多いので、**Claude にまず計画書を書かせてスレッドに貼り戻す** ワンクッションを挟むのが鉄則。これは [[user_planning_workflow|普段の plan mode を使わない計画書ワークフロー]] と同じ発想で、Slack 統合でも活きる。

### Slack 起点が VSCode 起点に勝る点 — マルチモーダル入力

これが今回の議論で一番の発見だった。

| 要素 | VSCode + Claude Code | Slack + Claude |
|------|---------------------|----------------|
| マルチモーダル入力 | 主にテキスト・ファイル | 画像・動画・音声・リンク・絵文字リアクション |
| Canvas | なし | スレッドに紐付けて直接編集可 |
| 参加者の多様性 | エンジニア中心 | デザイナー・PdM・QA・経営層も自然に参加 |
| 議論の非同期性 | 単一セッション | スレッドで時間差・並行議論 |

ゲーム開発の例: 企画が攻撃パターンの動画、アートが参考画像、プランナーがスプレッドシート、エンジニアがコードスニペットを貼った状態で「@Claude これらをまとめて Canvas に実装計画書を作って」と頼める。VSCode 単体だと自分で全部テキスト化して渡す必要がある。**Slack だと元の形のまま Claude のコンテキストに入る** のが本質的な差。

### フェーズ別ツール分担の最終形

| フェーズ | ツール | 役割 |
|---------|--------|------|
| 発想・議論・素材収集 | Slack スレッド | マルチモーダル入力の集約 |
| 要件整理・実装計画 | Slack Canvas | 構造化ドキュメント |
| 詳細実装・デバッグ | VSCode + Claude Code | コードに密着した深い作業 |
| レビュー・進捗共有 | Slack スレッド再開 | 結果のフィードバックループ |

「**Slack でまとめて、VSCode で深掘る**」が AI 駆動開発のメインストリームになりそう。

### ウォッチポイント

- Dreamforce 2026（例年 9 月）で具体的なロードマップ発表があるか
- Slack 統合機能の UI が「軽量ツール／実装計画書」の棲み分けを支援する作りになっているか
- intermediary layer が Slack 統合にどう組み込まれるか（マルチモーダル入力はトークン消費が跳ねるので、ルーティング設計が経営イシューになる）
