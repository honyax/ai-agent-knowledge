---
date: 2026-07-04
status: read
relevance: S
tags: [claude-code, changelog, permission-mode, askuserquestion, plugin, accessibility]
source_urls:
  - https://code.claude.com/docs/en/permission-modes
  - https://github.com/anthropics/claude-code/releases
  - https://zenn.dev/ytkdm/articles/claude-code-askuserquestion-timeout
  - https://releasebot.io/updates/anthropic/claude-code
experiment_dir: null
---

# Claude Code v2.1.200 前後: permission mode「Manual」改名、AskUserQuestion の自動タイムアウト廃止

## 3行要約

- **v2.1.200** で「default」permission mode の表示名が **Manual** に改名（CLI / `--help` / VS Code / JetBrains すべて）。設定値としての `default` は互換維持され、hooks や SDK 連携は従来のまま。`manual` エイリアスも追加。
- **AskUserQuestion ダイアログの auto-continue（60 秒アイドルで勝手に既定選択肢へ進む挙動）がデフォルト無効に**。放置してもダイアログが待ち続けるようになり、タイムアウトさせたい場合は `/config` の `askUserQuestionTimeout` でオプトイン。
- その他: `claude plugin prune`（孤児化した自動インストール済みプラグイン依存の削除）、git worktree からのプロジェクトスコーププラグイン読み込み修正、tmux 3.4+ のレンダリングちらつき修正（synchronized output）、スクリーンリーダー出力改善、voice dictation の誤エラー表示修正。

## 自分への関連度: S

自分の CLAUDE.md は「ユーザーに選択や判断を求める場合は AskUserQuestion ツールを使うこと」と定めており、AskUserQuestion は日常的に使う導線。**60 秒放置で勝手に進む挙動は「選択を求めたのに聞いていない」ことになり危険だった**ので、デフォルト無効化は歓迎すべき変更（Zenn に無効化手順の記事が出るほど不満のあった挙動）。permission mode の「Manual」改名は実害のない表示変更だが、[[feedback_changelog_rename_caveat]] の教訓どおり「設定値は `default` のまま」という点を押さえておく。

## 詳細

### permission mode「Manual」改名（v2.1.200）

- **表示名**: 全アクションを確認する mode が CLI / `claude --help` / VS Code / JetBrains 拡張で「Manual」と表示されるように。
- **設定値**: config 上の値は従来どおり `default`。hooks や SDK 連携で `default` を参照している既存設定はそのまま動く。
- **エイリアス**: `manual` も v2.1.200 以降で使用可能。
- 名前と実体の対応: Manual（=default、全確認）/ acceptEdits / plan / bypassPermissions / auto などのモード体系の中で、「default という名前が何も説明していなかった」問題への対処。

### AskUserQuestion の auto-continue 廃止（v2.1.200）

- **旧挙動**: ダイアログを 60 秒放置すると自動でタイムアウトし、既定の選択肢で続行してしまう。
- **新挙動**: デフォルトでは無期限に待つ（auto-continue しない）。
- **オプトイン**: `/config` の `askUserQuestionTimeout` でアイドルタイムアウトを設定すれば旧挙動に近づけられる。
- 背景: 離席中に勝手に選択が進む問題は Zenn 記事（「質問ダイアログが勝手にタイムアウトするのを無効化する」）が書かれるほどユーザーの不満があった。安全側（人間の判断を待つ）へのデフォルト変更。

### プラグイン関連

- **`claude plugin prune`**: 自動インストールされたが依存元を失った（orphaned）プラグイン依存を削除する新コマンド。
- **worktree 修正**: git worktree からプロジェクトスコープのプラグインが正しく読み込まれないバグを修正。[[2026-07-01-claude-code-v21198-background-agents-auto-pr]] の worktree ベース運用と関わる。
- **`claude agents --plugin-dir`**: フラグを `agents` の後に置いたときにプラグインの agents / skills が agent view に出ないバグを修正。

### 端末・アクセシビリティ

- **tmux 3.4+ ちらつき修正**: synchronized terminal output を有効化してレンダリングのちらつきを解消。
- **スクリーンリーダー改善**: 装飾グリフを非表示にし、transcript 記号を短いラベルとして読み上げ。
- **voice dictation**: 無音録音時に「Voice connection failed」という誤解を招くエラーが出ていたのを修正。

## 試すなら

1. `claude --version` で v2.1.200 以降であることを確認し、permission mode の表示が「Manual」になっているか見る。
2. AskUserQuestion が発生するタスク（自分の catch-up / try skill でも発生する）を実行し、放置してもダイアログが待ち続けることを確認。
3. `/config` で `askUserQuestionTimeout` の設定項目を確認（自分は「待ち続ける」デフォルトのままが合っているはず）。
4. `claude plugin prune` を dry-run 的に実行し、孤児化プラグインが溜まっていないか掃除する。
5. VSCode 拡張版（[[user_environment]]）でも「Manual」表記と AskUserQuestion 挙動が反映されているか、CLI 版との差を確認する。

## ソース

- [Choose a permission mode (Claude Code Docs)](https://code.claude.com/docs/en/permission-modes)
- [Releases · anthropics/claude-code](https://github.com/anthropics/claude-code/releases)
- [Claude Code の質問ダイアログが勝手にタイムアウトするのを無効化する (Zenn)](https://zenn.dev/ytkdm/articles/claude-code-askuserquestion-timeout)
- [Claude Code Updates by Anthropic - July 2026 (Releasebot)](https://releasebot.io/updates/anthropic/claude-code)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->

### 会話メモ（2026-07-08）

- 「Manual」への改名は新モード追加ではなく、既存 `default` モードの**表示名変更**（設定値は `default` のまま、`manual` エイリアス追加）である点を確認。誤解しやすいので注意。
- AskUserQuestion の自動タイムアウト（60秒放置で既定選択肢へ進む挙動）自体を認識していなかった。振り返ると、これまで放置してタイムアウトした自覚もなく、実害・リスクを感じたことはない。とはいえ「聞いたつもりが実は聞けていなかった」というのは気づきにくい失敗モードなので、デフォルト無効化（無期限に待つ）自体は歓迎できる変更。
