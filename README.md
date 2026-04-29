# AI Agent Knowledge Base

AIコーディングエージェント・AI開発ツール関連の情報キャッチアップを半自動化するナレッジリポジトリ。
現在のメイン対象: Claude（Claude Code, API, Claude.ai）。今後 Cursor, Copilot 等にも拡張可能。

## セットアップ

```bash
# 1. リポジトリをクローン
git clone git@github.com:[YOUR_USERNAME]/ai-agent-knowledge.git
cd ai-agent-knowledge

# 2. Claude Code で開く
claude
```

## 使い方

Claude Code を起動して以下のコマンドを実行:

| コマンド | 何をする |
|---------|---------|
| `/catch-up` | 最新のAIエージェント関連情報を収集・要約してエントリ生成（デフォルト: Claude） |
| `/catch-up MCP` | 特定トピックに絞って情報収集 |
| `/try entries/2026-03-24.md` | エントリの内容を実践・実験し、感想・考察まで自動生成 |
| `/digest` | ナレッジベースの状態を確認（Claude Code ビルトインの `/status` とは別） |

## 推奨ワークフロー

### 週1回（10-15分）

```
1. /digest          → 状態確認
2. /catch-up        → 情報収集（自動）
3. 気になるものを /try → 実践・感想まで自動生成
4. git add -A && git commit → 保存
```

### 気になるニュースを見かけた時

```
1. /catch-up [キーワード]  → そのトピックを検索・要約
2. /try [エントリ]          → すぐ試す
```

## カスタマイズ

- `CLAUDE.md` の「関心領域」を更新すると、情報フィルタリングの精度が変わる
- `templates/entry-template.md` を編集するとエントリの構造を変えられる
- コマンドの `.md` ファイルを直接編集してプロンプトを調整可能

## 段階的拡張（将来）

- [ ] GitHub Actions で週次自動収集（`claude --bare -p` + APIキー）
- [ ] 月次サマリー自動生成
- [ ] RSS/Atomフィードからの自動取得
- [ ] 実践結果をブログ記事に変換するコマンド
- [ ] Cursor / Copilot / Codex 等の情報収集コマンド追加（`/catch-up cursor` 等）
