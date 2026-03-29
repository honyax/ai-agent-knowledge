# MCP Tool Search 実践ログ

実施日: 2026-03-29
環境: Claude Code v2.1.87 / Windows 11

## 確認方法

コードなしの検証。以下の観点で確認した。

### 1. バージョン確認

```
$ claude --version
2.1.87 (Claude Code)
```

導入済みのバージョン（v2.1.76以降）を十分に超えている。

### 2. このセッション自体が証拠

この会話のシステムプロンプトに以下が含まれている：

```
The following deferred tools are now available via ToolSearch:
AskUserQuestion, CronCreate, CronDelete, CronList, EnterPlanMode,
EnterWorktree, ExitPlanMode, ExitWorktree, NotebookEdit, RemoteTrigger,
TaskOutput, TaskStop, TodoWrite, WebFetch, WebSearch
```

これがまさに MCP Tool Search の動作そのもの。ツール定義は「deferred（遅延）」状態で、
必要になった時点で `ToolSearch` ツールを呼び出してスキーマを取得する仕組みになっている。
スキルの `try` を起動した際も、内部的にこの遅延ロードが機能していた。

### 3. 設定確認

~/.claude/settings.json にはMCP固有の設定なし。デフォルト有効なので設定変更不要。

## 観察結果

- **動作確認: OK** — 現在のセッションで ToolSearch が機能していることをシステムプロンプトで直接確認できた
- **体感できる変化**: 以前はセッション開始時に全ツール定義が一括でコンテキストに入っていたはずが、
  現在は「名前だけ」が入り、使用時にオンデマンドで取得される
- MCP サーバーを多数持つ環境（Blender MCP + discord + カスタム等）では、
  セッション冒頭のコンテキスト使用量が大幅に改善されているはず
