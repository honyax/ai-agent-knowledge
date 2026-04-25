---
date: 2026-04-23
status: read
relevance: A
tags: [claude-code, workflow, claude-md, skills, hooks, agents, autonomous]
source_urls:
  - https://qiita.com/teppei19980914/items/8da88b33ffa8cf88dfa2
experiment_dir: null
---

# Claude Code を Level 5 まで育てたら、開発が「指示と確認だけ」になった

## 3行要約

- CLAUDE.md → Skills → Hooks → Agents の4段階で Claude Code を育てると、人間の仕事は「何を作るか決める」と「動作確認する」だけになる
- 同じ指示を繰り返したらCLAUDE.mdに書き、手順説明が増えたらSkillsに切り出し、確認忘れが増えたらHooksで自動化という育て方のサイクルを解説
- 実際のファイル構成（CLAUDE.md・Skills定義・Hooks設定・Agents連携）を公開した実践記事

## 自分への関連度: A

自分はすでにCLAUDE.md・Skills・Hooksを活用しているが、Level 5のAgents（並列レビュー自動化）まで到達しているかは不明。記事の「Level判定基準」で自分の現在地を確認できる。

## 詳細

**5レベルの定義**

| Level | 何を使う | 効果 |
|-------|----------|------|
| 1 | プロンプトだけ | 毎回同じことを説明 |
| 2 | CLAUDE.md | プロジェクトルールを記述、指示が減る |
| 3 | Skills | 手順系の指示を切り出し、呼び出すだけ |
| 4 | Hooks | Prettier・テスト等のチェックを自動化 |
| 5 | Agents | 並列レビューなどを自動化、指示と確認だけに |

**育て方のサイクル（記事より）**

- 「同じことを何度も言った」→ CLAUDE.mdに追記
- 「手順を毎回説明している」→ Skillsに切り出す
- 「確認するのを忘れがち」→ Hooksで自動化
- Level 5では `Agent` ツールを使ってClaude同士が協調

## 試すなら

1. 記事を読んで自分の現在Levelを判定する
2. 直近1週間で「同じことをClaudeに言った回数」を数える
3. 3回以上言った指示をCLAUDE.mdに追加する
4. 手順系のスキル（まだSkills化していないもの）を1つ切り出す
5. 保存時自動実行のHookでよく忘れるチェックを自動化する

## ソース

- [Claude Code を Level 5 まで育てたら、開発が「指示と確認だけ」になった — 実ファイル構成で解説 (Qiita)](https://qiita.com/teppei19980914/items/8da88b33ffa8cf88dfa2)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
