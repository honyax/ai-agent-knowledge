---
date: 2026-04-17
status: read
relevance: S
tags: [claude-code, parallel, workflow, skills, productivity]
source_urls:
  - https://zenn.dev/pepabo/articles/claude-code-stop-watching-parallel-work
experiment_dir: null
---

# Claude Codeの並列作業で「画面に張り付く」をやめるためにやったこと（Zenn: pepabo）

## 3行要約

- 複数の Claude Code を並列起動しても「張り付き」があると実質1本分の生産性しか出ない。ボトルネックは人間側。
- 「投げて放置、終わったら回収」の非同期スタイルに移行するには、**完了まで自走できる Skill 設計**が鍵。
- 5ペインに一斉に指示を投げて別作業に移り、完了通知が来たペインから順に確認するフローに変えた。

## 自分への関連度: S

Claude Code でゲーム開発を加速させる際に直接応用できる。複数タスクを並列実行するワークフロー設計のノウハウとして即実践可能。

## 詳細

### 問題の構造
3〜5本の Claude Code を並行起動しても、権限プロンプトやエラーが出るたびに目が向き、経過が気になって張り付いてしまう。結果として「並列数 × 効率」ではなく「1本分の効率」になる。

### 解決策: 自走できる Skill 設計
- Skill には「判断が必要な分岐」を含めない
- 「このスキルを呼んだら完了まで自走する」設計にする
- 例: `/review-fix-loop`、`/triage-issues`、`/e2e-test` など投げたら放置できる Skill

### 非同期ワークフロー
以前: 1つの Claude Code に指示 → 完了待ち → 次の指示（同期）
現在: 5ペインに一斉に指示 → 別作業へ → 完了通知が来たペインから回収（非同期）

### 適切な Auto Mode との組み合わせ
Auto Mode（今回追加された機能）と組み合わせることで、権限プロンプトによる中断もさらに削減できる可能性がある。

## 試すなら

1. 自分の作業を「投げたら放置できるタスク」に分解してみる
2. 既存の Skill を「完了まで自走する」設計に見直す
3. ターミナル5ペインで異なるタスクを並列投入して試す
4. 通知フック（Hooks: stop イベント）でタスク完了を検知する設定を追加

## ソース

- [Claude Codeの並列作業で「画面に張り付く」をやめるためにやったこと — Zenn (pepabo)](https://zenn.dev/pepabo/articles/claude-code-stop-watching-parallel-work)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
