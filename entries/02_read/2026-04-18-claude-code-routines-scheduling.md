---
date: 2026-04-18
status: read
relevance: A
tags: [claude-code, routines, scheduling, loop, automation, desktop]
source_urls:
  - https://zenn.dev/aria3/articles/claude-code-routines-scheduling
  - https://venturebeat.com/orchestration/we-tested-anthropics-redesigned-claude-code-desktop-app-and-routines-heres-what-enterprises-should-know
  - https://thenewstack.io/claude-code-desktop-redesign/
experiment_dir: null
---

# Claude Code 定期実行を整理する: Routines・/loop・Desktopタスクの使い分け

## 3行要約

- Claude Codeの定期実行には「Routines（クラウド自律実行）」「Desktopスケジュールタスク（ローカル実行）」「/loop（セッション内ポーリング）」の3種類がある
- Claude Code Desktopアプリが並列セッション対応にリニューアルされ、タスクの管理UIが大きく改善された
- Routinesはクラウド側でClaude自身がトリガーを判断して実行するため、完全自律型のエージェントワークフロー向け

## 自分への関連度: A

現在のAI-agent-knowledgeリポジトリでのcatch-upスケジューリングや、Unityプロジェクトでの定期テスト実行など、実際に使えるかを検証したい。3種類の明確な使い分け基準が得られる。

## 詳細

**3種の比較**:
| 種類 | 実行場所 | トリガー | 用途 |
|------|---------|---------|------|
| Routines | Anthropicクラウド | Claude自身が判断 | 完全自律タスク |
| Desktopスケジュール | ローカル | 時刻ベースcron | ローカルリソースが必要なタスク |
| /loop | ローカルセッション内 | 手動起動+間隔指定 | セッション内ポーリング・監視 |

**Claude Code Desktop リニューアル**:
- 並列セッション（複数ワーカー）の管理UIを搭載
- タスクの進捗・ステータス一覧が確認しやすくなった
- エンタープライズ向けに統制・ガバナンス機能が強化

## 試すなら

1. Zenn記事でRoutines・/loop・Desktopタスクの詳細仕様を読む
2. このリポジトリのcatch-upをDesktopスケジュールタスクで自動化してみる
3. `/loop` でgit statusを定期監視する小実験をする

## ソース

- [Claude Code の定期実行まわりを整理する（Routines・/loop・Desktop タスク）- Zenn](https://zenn.dev/aria3/articles/claude-code-routines-scheduling)
- [We tested Anthropic's redesigned Claude Code desktop app and Routines - VentureBeat](https://venturebeat.com/orchestration/we-tested-anthropics-redesigned-claude-code-desktop-app-and-routines-heres-what-enterprises-should-know)
- [Anthropic's redesigned Claude Code desktop app - The New Stack](https://thenewstack.io/claude-code-desktop-redesign/)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
