---
date: 2026-04-03
status: unread
relevance: A
tags: [claude-code, agent-teams, multi-agent, parallel, refactoring, experimental]
source_urls:
  - https://code.claude.com/docs/ja/agent-teams
  - https://uravation.com/media/claude-code-agent-teams-guide-2026/
  - https://claudefa.st/blog/guide/agents/agent-teams
experiment_dir: null
---

# Claude Code Agent Teams — 複数インスタンスがチームとして連携する実験的マルチエージェント機能

## 3行要約

- 複数のClaude Codeインスタンスがチームリーダー＋チームメンバー構成で連携する実験的機能（v2.1.32以降）
- チームメンバーは独自のコンテキストウィンドウを持ち、互いに直接通信・共有タスクリストで自己調整する
- 大規模リファクタリング・並列コードレビュー・競合仮説でのデバッグに特に効果的

## 自分への関連度: A

ゲーム開発でのリファクタリング（例: Unityプロジェクトのモジュール分割）や並列PR作成に直結する。Subagentsとの使い分けの判断基準が明確になるため、マルチエージェントワークフロー設計に役立つ。実験的機能なのでコストと制限を把握した上で試す価値あり。

## 詳細

### 有効化

```json
// .claude/settings.json
{
  "env": {
    "CLAUDE_CODE_EXPERIMENTAL_AGENT_TEAMS": "1"
  }
}
```

### Subagents との使い分け

| | Subagents | Agent Teams |
|---|---|---|
| 通信 | リーダーにのみ結果を報告 | メンバー同士で直接通信 |
| 調整 | メインエージェントが全管理 | 共有タスクリストで自己調整 |
| 最適な用途 | 結果だけ必要な単純タスク | 議論・協調が必要な複雑作業 |
| トークンコスト | 低い | 高い（メンバー数×コンテキスト） |

### 大規模リファクタリングでの使い方

```
Create a team with 4 teammates to refactor these modules in parallel.
Use Sonnet for each teammate.
```

**重要なルール：**
- 各メンバーに異なるファイル/モジュールを担当させる（同じファイルの同時編集は上書きが発生）
- `Require plan approval before they make any changes.` でプラン承認ステップを挟める
- 3〜5人が適正規模（メンバーあたり5〜6タスクが目安）

**並列コードレビュー例：**
```
Create an agent team to review PR #142. Spawn three reviewers:
- One focused on security implications
- One checking performance impact
- One validating test coverage
```

**競合仮説でのデバッグ例：**
```
Spawn 5 agent teammates to investigate different hypotheses.
Have them talk to each other to try to disprove each other's theories.
```

### Subagent定義との組み合わせ

`.claude/agents/` に定義したSubagent（security-reviewer等）をチームメンバーとして指定可能：

```
Spawn a teammate using the security-reviewer agent type to audit the auth module.
```

チームメンバーは isolation: worktree を指定するとWorktree分離で並列作業できる。

### 既知の制限

- `/resume` `/rewind` でin-processチームメンバーは復元されない
- セッションあたり1チームのみ（ネスト不可）
- 分割ペイン表示にはtmuxまたはiTerm2が必要

## 試すなら

1. `settings.json` に `CLAUDE_CODE_EXPERIMENTAL_AGENT_TEAMS: "1"` を追加
2. 小規模なPR並列レビューから試す（リスクが低くAgent Teamsの効果が出やすい）
3. Unityプロジェクトのモジュール単位リファクタリングで3メンバー構成を試す
4. トークンコストを記録して単一セッションと比較する

## ソース

- [Claude Code エージェントチームドキュメント（公式・日本語）](https://code.claude.com/docs/ja/agent-teams)
- [Claude Code Agent Teams完全ガイド - Uravation](https://uravation.com/media/claude-code-agent-teams-guide-2026/)
- [Claude Code Agent Teams: Setup & Usage Guide 2026](https://claudefa.st/blog/guide/agents/agent-teams)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
