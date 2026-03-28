---
allowed-tools: WebSearch, Read, Write, Bash
description: AIエージェント関連の最新情報を収集・要約し、ナレッジエントリを自動生成する（デフォルト: Claude）
---

# catch-up: 情報収集・要約コマンド

## やること

1. **情報収集**: Web検索で直近のAIエージェント関連ニュースを収集する
2. **フィルタリング**: CLAUDE.md の「関心領域」と「評価基準」を読み、自分に関係あるものを選別する
3. **要約エントリ生成**: `entries/` に日付付きMarkdownファイルを生成する

## 引数の解釈

- 引数なし (`/catch-up`) → デフォルトのClaude関連検索クエリを実行
- トピック指定 (`/catch-up MCP`) → そのキーワードで追加検索
- エージェント指定 (`/catch-up cursor`, `/catch-up copilot`) → 該当エージェントの検索クエリに切り替え

追加の引数: $ARGUMENTS

## デフォルト検索クエリ（Claude）

- `Anthropic Claude release changelog 最近1週間`
- `Claude Code new features update`
- `Anthropic MCP update`
- `Claude API changes new model`
- `Anthropic blog announcement`

## エージェント別検索クエリ（引数に応じて切り替え）

### cursor
- `Cursor IDE update changelog`
- `Cursor AI new features`

### copilot
- `GitHub Copilot update changelog`
- `GitHub Copilot new features`

### codex
- `OpenAI Codex update`
- `OpenAI coding agent news`

### all（全エージェント横断）
- 上記の全クエリを実行し、横断的にまとめる

## エントリ生成ルール

`templates/entry-template.md` を読み、そのフォーマットに従って `entries/YYYY-MM-DD.md` を生成する。
同日のファイルが既に存在する場合は `entries/YYYY-MM-DD-2.md` のように連番をつける。

### 各項目の書き方

- **3行要約**: 技術的に正確に、ただし平易な日本語で。箇条書き3行以内。
- **自分への関連度**: CLAUDE.md のコンテキストに基づき、S/A/B/C で判定
  - S: 今すぐワークフローに影響する
  - A: 近い将来使いそう
  - B: 知識として有用
  - C: 現時点では関係薄い
- **試すなら**: 実践する場合の最小ステップ（5ステップ以内）
- **ソース**: 元記事のURLを必ず記載

## 出力後のアクション

エントリ生成後、以下を表示:
1. 生成したエントリのサマリー（関連度S/Aのものをハイライト）
2. 「試してみますか？」の確認（関連度S/Aのものについて）
3. 確認されたら `/try` コマンドの使い方を案内
