# Claude Code プラグインマーケットプレイス & レート制限表示 — 実験ノート

> ローカル実行なし。機能把握・考察のみ（2026-03-30、Claude Code Web）
> プラグインCLIコマンドはローカルClaude Codeが必要なため、Web環境では実行不可

## 機能整理

### プラグインマーケットプレイス
```bash
# プラグインソースの追加
/plugin marketplace add anthropics/claude-plugins-official

# インストール済み一覧
/plugin list

# インストール
/plugin install <name>
```

settings.json からインラインで宣言も可能:
```json
{
  "plugins": ["anthropics/claude-plugins-official/fakechat"]
}
```

### レート制限表示（statusline）
`rate_limits` フィールドが statusline スクリプトに追加:
- 5時間ウィンドウ / 7日間ウィンドウの使用率
- リセット時刻のリアルタイム表示

### CLIツール使用検知によるTips
- ファイルパターンに加えてツール使用パターンでもプラグイン提案が出る

## 検討メモ

- このナレッジベースの `/catch-up`, `/try`, `/status` スキルをプラグイン化すれば他プロジェクトでも使い回せる可能性がある
- レート制限表示は statusline カスタマイズ設定で追加できる → ローカル環境で要確認

## ローカルで試すステップ（メモ）

1. `claude-code-web` で動作確認後、ローカル環境で試す
2. `/plugin marketplace add anthropics/claude-plugins-official`
3. `/plugin list` で確認
4. statusline に `rate_limits` フィールドを追加
