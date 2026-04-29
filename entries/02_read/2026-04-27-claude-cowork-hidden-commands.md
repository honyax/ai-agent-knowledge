---
date: 2026-04-27
status: read
relevance: B
tags: [claude-cowork, slash-commands, automation, schedule, productivity]
source_urls:
  - https://note.com/tolove/n/n46113b20f104
experiment_dir: null
---

# Claude Cowork 隠しコマンド40選 — 「便利なAI」から「業務OS」へ

## 3行要約

- Claude Cowork を「便利な AI アシスタント」から「自律業務 OS」へ進化させるための 40 個のコマンドとワークフローをまとめた note 記事
- 構成は5カテゴリ: スラッシュコマンド10選・ファイル操作8選・コネクタ連携8選（Gmail/Calendar/Drive/Slack）・ドキュメント作成8選・スケジュール自動化6選
- 主要コマンド: `/schedule`（定期タスク）・`/plan`（段階的プラン承認）・`/compact`（履歴圧縮）・`/memory`（コンテキスト確認）

## 自分への関連度: B

Cowork は日常の情報収集・分析に使用しているため知識として有用。ただしコーディングワークフローではないため即実践度は低め。`/plan` と `/schedule` は既に Claude Code 側で類似機能を使っており、Cowork 側でも同じ思想で使えるかを確認する程度。

## 詳細

### 主要コマンド

| コマンド | 機能 |
|---------|------|
| `/schedule` | 定期タスク自動化（毎週月曜7:30に実行など） |
| `/plan` | 実行前に段階的プラン作成・承認 |
| `/compact` | 会話履歴の圧縮 |
| `/memory` | 読み込まれたコンテキスト確認 |

### 推奨される開始ポイント（初心者向け3点）

1. `/plan` コマンド
2. 会議録の構造化
3. 週次プランニング自動化

### 実運用シーン例

- 毎週月曜7:30にカレンダー要約 + 未処理タスク + 資料収集を自動実行
- Gmail/Calendar/Drive/Slack コネクタで部署横断の状況把握

### Claude Code との関連性

`/plan`・`/schedule`・`/compact` は Claude Code 側にも対応する機能があり、Cowork も同じ思想で運用できる可能性がある。ただし Cowork はコーディング向けではなく情報処理・スケジューリング向け。

## 試すなら

1. Claude Cowork で `/memory` を実行し、現在のコンテキスト状態を把握
2. `/schedule` で週次の情報収集タスクを1つだけ登録（例: 月曜朝の AI 業界ニュース要約）
3. `/plan` を実走前に1度試して、Claude Code 側の同コマンドとの差分を比較
4. コネクタ連携は Calendar 連携から最小構成で試す
5. ドキュメント作成ワークフローは1ヶ月運用してみて自分のフローに合うか判断

## ソース

- [9割が知らない、Claude Cowork『40の隠しコマンド』完全攻略 (note)](https://note.com/tolove/n/n46113b20f104)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
