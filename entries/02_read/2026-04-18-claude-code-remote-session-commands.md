---
date: 2026-04-18
status: read
relevance: A
tags: [claude-code, remote-session, slash-commands, cloud-execution]
source_urls:
  - https://qiita.com/imk1t/items/5b22654dc3d6b3b1b7bc
  - https://help.apiyi.com/en/claude-code-changelog-2026-april-updates-en.html
experiment_dir: null
---

# Claude Code リモートセッション系 5 コマンド: /autopilot, /bugfix, /dashboard, /docs, /investigate

## 3行要約

- Anthropicのクラウドインフラ上でClaude Codeセッションを起動する5つのリモートセッションコマンドが追加された
- /autopilot（自律開発）/bugfix（バグ修正）/dashboard（ダッシュボード生成）/docs（ドキュメント生成）/investigate（問題調査）のタスクに特化
- ローカルマシンのリソースを消費せずにバックグラウンドで重いタスクを実行できる

## 自分への関連度: A

並列実行・自律開発ワークフローの関心領域に直結。ローカル実行とクラウド実行の使い分け指針になる。Unityプロジェクトへの適用可能性も含めて把握しておきたい。

## 詳細

追加された5コマンド:
- `/autopilot`: 自律的な開発タスクをクラウド実行
- `/bugfix`: バグ修正をリモートセッションで実行
- `/dashboard`: ダッシュボードやレポートの生成
- `/docs`: ドキュメント自動生成
- `/investigate`: 問題の調査・分析

これらはAnthropic提供のクラウドインフラ上で動作するため、ローカルのClaude Codeセッションとは独立して実行される。Remote Controlとの連携でモバイルから進捗確認も可能。

## 試すなら

1. Claude Code最新版に更新
2. `/autopilot` または `/investigate` を小さなタスクで実行してみる
3. ローカル実行との速度・コスト比較をする
4. 長時間タスク（ドキュメント生成等）にクラウド実行を使う

## ソース

- [Claude Code に追加されたリモートセッション系 5 コマンド徹底解説 - Qiita](https://qiita.com/imk1t/items/5b22654dc3d6b3b1b7bc)
- [Decoding the Claude Code April 2026 Changelog - Apiyi Blog](https://help.apiyi.com/en/claude-code-changelog-2026-april-updates-en.html)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
