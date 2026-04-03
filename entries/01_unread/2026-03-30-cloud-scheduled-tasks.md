---
date: 2026-03-30
status: unread
relevance: A
tags: [claude-code, scheduled-tasks, automation, cloud]
source_urls:
  - https://code.claude.com/docs/en/web-scheduled-tasks
  - https://medium.com/coding-nexus/claude-code-just-got-a-scheduler-and-its-kind-of-wild-1c529900b015
experiment_dir: null
---

# Claude Code Cloud Scheduled Tasks: PCを起動しなくてもタスクを定期実行

## 3行要約

- Claude Code の Cloud Scheduled Tasks は Anthropic のクラウドインフラ上でタスクを定期実行する機能で、PC が OFF でも動作する
- GitHub リポジトリを指定してクローン→実行→`claude/`プレフィックスのブランチにプッシュ、という自律的なワークフローが組める
- `/schedule` コマンドまたは Web UI から設定でき、MCP コネクター（Slack、Linear 等）との連携もサポート

## 自分への関連度: A

ゲーム開発での定期的なビルドチェック、依存関係の監査、PR レビューの自動化などに活用できる。
ただし現時点ではローカルファイルへのアクセスは不可（GitHub リポジトリのクローンのみ）なので、用途が限られる。

## 詳細

### 3種類のスケジューリング方式の比較

| 方式 | 実行場所 | PC OFF でも動作 | ローカルファイル |
|------|---------|----------------|----------------|
| Cloud Scheduled Tasks | Anthropic クラウド | ○ | × (GitHub のみ) |
| Desktop Scheduled Tasks | 自分の PC | × | ○ |
| `/loop` コマンド | 自分の PC | × | ○ (セッション中のみ) |

### Cloud Scheduled Tasks の主な仕様

- **最小実行間隔**: 1時間
- **スケジュール**: 毎時、毎日、平日のみ、毎週から選択
- **ブランチ操作**: デフォルトは `claude/` プレフィックスのブランチのみ（保護ブランチへの誤操作を防止）
- **権限プロンプト**: なし（自律実行）
- **MCP コネクター**: 設定済みのコネクターを選択的に付与可能

### ユースケース例

- 毎朝オープン PR をレビュー
- 夜間の CI 失敗を分析してサマリーを作成
- PR マージ後にドキュメントを同期
- 週次の依存関係監査

### 設定方法

```bash
# CLI から設定（会話形式でガイド）
/schedule

# 直接記述でも設定可能
/schedule daily PR review at 9am

# タスク一覧確認
/schedule list

# 既存タスクの更新（カスタムスケジュール設定も可能）
/schedule update
```

## 試すなら

1. Web UI で `claude.ai/code/scheduled` を開く
2. **New scheduled task** をクリック
3. タスク名とプロンプト（自己完結した指示）を記述
4. 対象 GitHub リポジトリを選択
5. スケジュール（毎日9時など）を設定して作成

## ソース

- [Schedule tasks on the web（Claude Code Docs）](https://code.claude.com/docs/en/web-scheduled-tasks)
- [Claude Code Just Got a Scheduler, and It's Kind of Wild（Medium）](https://medium.com/coding-nexus/claude-code-just-got-a-scheduler-and-its-kind-of-wild-1c529900b015)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
