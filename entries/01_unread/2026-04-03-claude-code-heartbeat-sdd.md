---
date: 2026-04-03
status: unread
relevance: B
tags: [claude-code, automation, specification-driven-development, hooks, queue]
source_urls:
  - https://zenn.dev/yoshiakist/articles/d26f69195f25ac
experiment_dir: null
---

# Claude Code にハートビート自動起動で仕様駆動開発してもらうには

## 3行要約

- `queue.md` にタスクを書いておき、バッチスクリプトで定期的にClaudeにハートビートプロンプトを送り続けることで、非インタラクティブな自律開発を実現
- セッションをまたいで記憶を持てないため、`queue.md` と作業ノートのみがセッション間の情報伝達手段となる
- `--permission-mode bypassPermissions` と `--add-dir` でゲームプロジェクト複数ディレクトリを対象に、ほぼ無人で仕様駆動開発を回す構成

## 自分への関連度: B

自律開発の長時間実行パターンとして参考になる。queue.mdによる状態管理はClaude Codeの自律化において重要な設計パターン。ゲーム開発文脈での適用例もあり。

## 詳細

**構成の核心**:
- ハートビートスクリプトが定期的にClaudeに「次のタスクを実行してください」と送り続ける
- `queue.md` = 実行待ちタスクのキュー
- 作業ノート = セッション間の唯一の記憶媒体

**CLIオプション**:
- `--model opus` : Opus 4.6を指定
- `--add-dir [paths]` : 複数のゲームプロジェクトディレクトリを追加
- `--permission-mode bypassPermissions` : 非インタラクティブ実行

**著者の背景**:
- 双極性障害のムードサイクル対策として、鬱期もClaudeに開発を継続させるための仕組みとして開発

## 試すなら

1. `queue.md` にタスクリストを記述する形式を設計する
2. ハートビートプロンプト（「queue.mdの次のタスクを実行」）を設計する
3. バッチスクリプトまたはcronで定期実行を設定
4. `--permission-mode bypassPermissions` の安全な適用範囲を確認する

## ソース

- [Claude Code にハートビート自動起動で仕様駆動開発してもらうには - Zenn](https://zenn.dev/yoshiakist/articles/d26f69195f25ac)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
