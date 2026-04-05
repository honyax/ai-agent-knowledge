# MCP Elicitation 実験ログ

実施日: 2026-04-05
環境: Windows 11, Python 3.10.11, fastmcp 3.2.0, Claude Code (要 v2.1.76+)

## 実験内容

FastMCP を使って Elicitation 対応の MCPサーバーを作成し、Claude Code から呼び出して
インタラクティブダイアログの動作を確認する。

## セットアップ手順

### 1. fastmcp インストール

```bash
pip install fastmcp
```

### 2. MCP サーバーをプロジェクト設定に登録

プロジェクトルートに `.claude/settings.json` を作成（または既存ファイルに追記）:

```json
{
  "mcpServers": {
    "elicitation-demo": {
      "command": "python",
      "args": ["D:/var/git/ai-agent-knowledge/experiments/2026-03-25-mcp-elicitation/demo_server.py"]
    }
  }
}
```

### 3. Claude Code を再起動

MCPサーバーの登録を反映するため再起動する。
`/mcp` コマンドで `elicitation-demo` が表示されれば登録成功。

## デモの呼び出し方

Claude Code のチャットで以下を入力:

- **Demo 1（最小構成）**: 「echo_with_input ツールを使って何か入力してもらって」
- **Demo 2（構造化入力）**: 「create_task ツールでタスクを作って」

## 期待される動作

Demo 1 実行時:
1. Claude が `echo_with_input` ツールを呼び出す
2. Claude Code がテキスト入力フォームをダイアログ表示
3. テキストを入力して送信
4. Claude が「あなたが入力したのは: 〇〇」と返す

Demo 2 実行時:
1. Claude が `create_task` ツールを呼び出す
2. Claude Code が3フィールドのフォームを表示:
   - title（テキスト）
   - priority（low / medium / high のドロップダウン）
   - confirmed（チェックボックス）
3. フォーム送信後、タスク作成完了メッセージを返す

## 実行結果

実施日: 2026-04-05（CLI版 Claude Code で確認）

- [ ] Demo 1: echo_with_input 動作確認
- [x] Demo 2: create_task 動作確認 — タイトル（テキスト）・優先度（ドロップダウン）・確認（チェックボックス）の専用UIが表示され、入力・送信が正常に動作した
- [ ] Elicitation Hook の動作確認

### 補足: 登録方法について

プロジェクトレベルの `.claude/settings.json` への手書き追加では Claude Code に認識されなかった。
以下のコマンドでユーザーレベルに登録することで動作確認できた:

```bash
claude mcp add elicitation-demo \
  "C:/Users/honya/AppData/Local/Programs/Python/Python310/python.exe" \
  "D:/var/git/ai-agent-knowledge/experiments/2026-03-25-mcp-elicitation/demo_server.py"
```

プロジェクトレベルで登録する場合は `--scope project` を明示する必要がある。

### 補足: VS Code 拡張について

VS Code 拡張版の Claude Code セッションでは `/mcp` コマンドが動作せず、MCP サーバーのツールも利用不可だった。Elicitation の動作確認には CLI 版が必要。

## Elicitation Hook のテスト（オプション）

`.claude/settings.json` に以下を追加するとリクエストをログできる:

```json
{
  "hooks": {
    "Elicitation": [{
      "matcher": "",
      "hooks": [{
        "type": "command",
        "command": "node -e \"let d=''; process.stdin.on('data',c=>d+=c).on('end',()=>{ console.error('ELICITATION:', d); process.stdout.write(d); })\""
      }]
    }]
  }
}
```

## 観察メモ

- `ctx.elicit()` の `response_type` に dataclass を渡すと自動的にフォームフィールドが生成される
- `Literal["low", "medium", "high"]` が enum として扱われドロップダウンになる想定
- `bool` フィールドはチェックボックスになる想定
- action が "accept" / "decline" / "cancel" の3種類あり、それぞれハンドリングが必要
- タイムアウトに注意: ユーザーが入力に時間をかけるとリクエスト全体がタイムアウトする可能性がある
