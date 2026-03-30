---
date: 2026-03-31
status: unread
relevance: B
tags: [claude-code, scheduled-tasks, architecture, cloud, oauth]
source_urls:
  - https://zenn.dev/iineineno03k/articles/20260325-claude-code-scheduled-task-architecture
experiment_dir: null
---

# Claude Code 定期実行アーキテクチャ — サーバーとローカルで役割を分ける

## 3行要約

- Cloud Scheduled Tasks は OAuth トークンの有効期限（約24時間）とネットワーク制限（git操作のみ）という2つの制約がある
- **AI処理はクラウド（Anthropic）、非AI処理（メール送信等）はローカル**に分割する非同期2段階アーキテクチャで回避できる
- 中間成果物を git push で受け渡すことで、異なる実行環境をシンプルに連携させる

## 自分への関連度: B

Cloud Scheduled Tasks を実際に運用した際の落とし穴と設計パターン。
週次ビルドチェックや依存関係監査など、定期実行を本番運用する場合の参考になる。

## 詳細

### 2つの制約

| 制約 | 内容 | 影響 |
|------|------|------|
| **OAuth トークン失効** | 約24時間で期限切れ → 無人運用で認証エラー | 毎朝コケる |
| **ネットワーク制限** | サーバー側サンドボックスで git 以外の外部接続がブロック | メール・Slack 送信不可 |

### 解決アーキテクチャ：非同期2段階

```
[毎朝 8:00] サーバー（Cloud Scheduled Tasks）
  ├─ 5ソースからフィード取得
  ├─ AI 要約生成
  └─ git push → claude/ブランチ

[毎朝 8:30] ローカル Mac（cron）
  ├─ git pull --rebase
  ├─ feed.md の存在確認
  └─ Gmail 送信（curl SMTP）
```

**設計原則**: 「AI が必要な処理」と「AI が不要な処理」を分離する。

- AI 処理 → Anthropic クラウドで完結（OAuth 不要）
- メール・通知等 → ローカルに残す（環境固有の制約を回避）

### 他の非AI処理をローカルに逃がす例

- Slack 通知
- 外部 API 呼び出し（分析ツール等）
- ファイルシステム操作（ローカルパスへのアクセス）

## 試すなら

1. Cloud Scheduled Tasks で行いたいタスクを「AI が必要な部分」と「そうでない部分」に分解する
2. AI 処理部分を Cloud Scheduled Tasks として設定し、結果を git push で出力する
3. ローカル側の cron で git pull → 後処理（通知・メール等）を実行するスクリプトを書く
4. 30分の時間差（8:00 → 8:30）でタイミングを合わせる

## ソース

- [Claude Codeの定期実行が毎朝コケるので、サーバーとローカルで役割を分けた話（Zenn）](https://zenn.dev/iineineno03k/articles/20260325-claude-code-scheduled-task-architecture)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
