---
date: 2026-03-30
status: tried
relevance: S
tags: [claude-code, permissions, safety, workflow]
source_urls:
  - https://claude.com/blog/auto-mode
  - https://code.claude.com/docs/en/permission-modes
  - https://techcrunch.com/2026/03/24/anthropic-hands-claude-code-more-control-but-keeps-it-on-a-leash/
experiment_dir: null
---

# Claude Code Auto Mode: --dangerously-skip-permissions の安全な代替

## 3行要約

- Auto Mode は Claude Code の新しい権限モードで、安全な操作は自動承認し、リスクの高い操作だけをブロックする
- Sonnet 4.6 ベースのセーフティ分類器が各ツール呼び出しを事前にチェックし、大量ファイル削除・本番デプロイ・force push などを防ぐ
- ユーザーの 93% が権限プロンプトを承認しているという実態を踏まえ、その承認作業を自動化しつつ安全性を担保する設計

## 自分への関連度: S

`--dangerously-skip-permissions` を使っていた場面で代替として即使える。特に自律的な長時間タスクを走らせるときのリスク低減に直結する。Team プラン以上が必要な点は注意。

## 詳細

Auto Mode は以下の仕組みで動作する:

1. **Input Layer**: ツール呼び出し実行前に Sonnet 4.6 分類器がアクション内容をチェック
2. **Output Layer**: トランスクリプト分類器でセッション全体の挙動を監視
3. 安全と判定されたアクションは自動実行、リスクありと判定されたアクションはブロックして別アプローチを促す

**ブロックされる主なアクション:**
- ダウンロードしたコードの実行
- 本番環境へのデプロイ
- main ブランチへの force push
- 大量ファイル削除
- 機密データの外部送信

**注意点:**
- Research Preview 段階（完全な安全保証はない）
- Team/Enterprise/API プランが必要
- Claude Sonnet 4.6 または Opus 4.6 が必要

## 試すなら

1. Team プランを確認する（Pro では現時点で利用不可）
2. `claude --auto` または設定から Auto Mode を有効化
3. 通常のタスクを流してみて、どの操作がブロックされるかを確認
4. ブロックされる操作のパターンを把握して、プロンプトを調整

## ソース

- [Auto mode for Claude Code（Anthropic公式ブログ）](https://claude.com/blog/auto-mode)
- [Choose a permission mode（Claude Code Docs）](https://code.claude.com/docs/en/permission-modes)
- [Anthropic hands Claude Code more control, but keeps it on a leash（TechCrunch）](https://techcrunch.com/2026/03/24/anthropic-hands-claude-code-more-control-but-keeps-it-on-a-leash/)

---

## 感想・考察

実施日: 2026-03-31 / 実験ファイル: [experiments/2026-03-30-claude-code-auto-mode/](../experiments/2026-03-30-claude-code-auto-mode/2026-03-30-claude-code-auto-mode.md)

**良かった点:**
- v2.1.87 では `--permission-mode auto` として既に利用可能で、`claude auto-mode` サブコマンドでルール内容を事前確認できるのが便利。
- `soft_deny` の25ルールを見ると「エージェントが自分の設定を書き換えて制約を回避する（Self-Modification）」や「データ外部送信（Data Exfiltration）」など、長時間自律タスクで実際に怖いシナリオを網羅している。

**微妙だった点・制限:**
- `--auto` フラグは存在せず `--permission-mode auto` が正しい呼び出し方。エントリ元情報が古かった。
- Teamプラン必須かどうかは `--help` だけでは判断できず、実際に動かしてみるまで不明。

**自分のワークフローへの適用:**
- `--dangerously-skip-permissions` を使っていた長時間ビルド・自動コミット系タスクで代替として試せる。ゲーム開発でのアセット一括生成・Unity スクリプト自動修正などに使えそう。
- `claude auto-mode config` で現在の設定を確認する習慣をつけると、何がブロックされるか事前に把握できる。

**次のアクション:**
- 実際に `--permission-mode auto` で簡単なタスク（ファイル編集→git commit）を流して soft_deny が動くか確認する。
