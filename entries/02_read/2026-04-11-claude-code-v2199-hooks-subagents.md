---
date: 2026-04-11
status: read
relevance: A
tags: [claude-code, hooks, subagents, mcp, permissions]
source_urls:
  - https://github.com/anthropics/claude-code/releases
  - https://code.claude.com/docs/en/changelog
  - https://releasebot.io/updates/anthropic/claude-code
experiment_dir: null
---

# Claude Code v2.1.99 — PreToolUse defer フック・PermissionDenied フック・サブエージェント補完

## 3行要約

- **PreToolUse フックに "defer" 判定を追加**: ヘッドレスセッション中にツール呼び出しを一時停止し、`-p --resume` で再開時にフックを再評価できる（承認フローの外部化が可能）
- **PermissionDenied フックを新設**: オートモード分類器がツールを拒否した後に発火する新しいフックイベント。拒否ログの記録や通知に活用できる
- **`--mcp-config` のサーバー接続を最大5秒で打ち切り**: 最も遅いサーバーにブロックされる問題を解消し、名前付きサブエージェントが `@` メンション補完候補に表示されるようになった

## 自分への関連度: A

（当初 S と評価したが、手動の VSCode 実行メインのため A に修正）

`defer` は `claude -p` ヘッドレス・CI/CD 実行向けの機能で、手動インタラクティブ実行では出番がほぼない。`PermissionDenied` フックは手動実行でも使えて、auto モード使い始めの時期に一時的に仕掛けて「意図せず拒否されている操作がないか」確認するのに使える。MCP の5秒タイムアウトはハードコード固定値（設定不可）で、複数 MCP サーバーがある場合のハング防止の安全装置。

## 詳細

### PreToolUse フックの "defer" 判定

```json
// hooks の exit code で制御
// 0: allow, 1: deny, 2: defer (新規追加)
```

`defer` を返すとセッションが一時停止し、`claude -p --resume <session-id>` での再開時にフックが再評価される。これにより：
- ヘッドレスバッチ処理中に特定のツール（Bash の rm 系など）を人間承認待ちにできる
- CI/CD パイプラインで危険な操作だけを手動確認するフローが組める

インタラクティブ実行では Claude がその場で確認を求めてくるため、`defer` の恩恵はない。

### PermissionDenied フック

オートモード分類器がツール実行を拒否した後に発火する新フックイベント。
- 拒否されたツール名・引数をログに残す
- Slack や Discord への通知トリガーに使える
- フック設定例: `settings.json` の `hooks.PermissionDenied` に追加

auto モード使い始めのタイミングで一時的に有効にし、パターンを把握したら外す使い方が実用的。

### サブエージェント補完と MCP 改善

- 名前付きサブエージェントが `@` メンション補完に表示される（エージェント間の参照が素早くなる）
- `--mcp-config` で起動時に最大5秒でサーバー接続を打ち切る（タイムアウト値は固定・設定不可。上限なしから5秒固定への変更で、遅いサーバーが原因のハング防止が目的）

## 試すなら

1. `npm update -g @anthropic-ai/claude-code` でv2.1.99以降に更新
2. `settings.json` に `PermissionDenied` フックを追加してみる（まずは echo でログ出力するだけでOK）
3. ヘッドレス実行 (`claude -p`) で `defer` を返すフックを試し、`--resume` で再開する流れを確認
4. 複数MCPサーバーを使っている場合、起動時間が短縮されているか確認
5. エージェント定義がある場合、`@` 補完にサブエージェント名が表示されるか確認

## ソース

- [Claude Code Releases (GitHub)](https://github.com/anthropics/claude-code/releases)
- [Claude Code Changelog](https://code.claude.com/docs/en/changelog)
- [Anthropic Claude Code Release Notes - April 2026](https://releasebot.io/updates/anthropic/claude-code)

---

## 感想・考察

- `defer`: CI/CD 等のヘッドレス実行向け。手動実行では不要
- `PermissionDenied`: auto モード導入初期に一時的に使って、意図せず拒否されている操作を洗い出すのに有用
- MCP 5秒タイムアウト: 設定変更ではなくハードコード固定。複数 MCP サーバーを使っていない場合は影響なし
