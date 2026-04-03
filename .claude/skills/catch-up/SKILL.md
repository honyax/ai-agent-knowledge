---
name: catch-up
allowed-tools: WebSearch, Read, Write, Bash
description: AIエージェント関連の最新情報を収集・要約し、ナレッジエントリを自動生成する（デフォルト: Claude）
---

# catch-up: 情報収集・要約コマンド

## やること

1. **前回実行日の取得**: `entries/` 配下の全サブフォルダ内の最新ファイルの日付を前回実行日として取得する
2. **重複チェック用データ収集**: 全サブフォルダの既存エントリの `source_urls` とタイトルを収集しておく
3. **情報収集**: Web検索で前回実行日以降のAIエージェント関連ニュースを収集する
4. **フィルタリング**: CLAUDE.md の「関心領域」と「評価基準」を読み、自分に関係あるものを選別する
5. **重複排除**: 既存エントリと同一の source_url またはほぼ同じタイトルの記事はスキップする
6. **要約エントリ生成**: `entries/` に日付付きMarkdownファイルを生成する

## 引数の解釈

- 引数なし (`/catch-up`) → デフォルトのClaude関連検索クエリを実行
- トピック指定 (`/catch-up MCP`) → そのキーワードで追加検索
- エージェント指定 (`/catch-up cursor`, `/catch-up copilot`) → 該当エージェントの検索クエリに切り替え

追加の引数: $ARGUMENTS

## 前回実行日の取得方法

`entries/` 配下の全サブフォルダのファイル名を確認し、最新の日付（YYYY-MM-DD形式）を取得する。

```bash
# 最新エントリの日付を取得するイメージ
ls entries/*/ | grep -oP '^\d{4}-\d{2}-\d{2}' | sort | tail -1
```

取得した日付の **1日前** を `SEARCH_FROM` として、検索クエリに `after:SEARCH_FROM` を付与する。

例: 最新エントリが `2026-03-30-xxx.md` → `SEARCH_FROM = 2026-03-29` → `after:2026-03-29`

こうすることで `after:` が「その日より後（当日を含まない）」と解釈される検索エンジンでも、
最新エントリの日付当日のコンテンツが確実に取得対象に含まれる。
当日以前の重複記事は後述の重複排除ルールで除去する。

`entries/` 配下のサブフォルダが全て空の場合は「直近1週間」をデフォルトとする。

## デフォルト検索クエリ（Claude）

以下のクエリに `after:LAST_DATE` を付与して実行する:

### 英語（公式・海外メディア）

- `Anthropic Claude release changelog after:LAST_DATE`
- `Claude Code new features update after:LAST_DATE`
- `Anthropic MCP update after:LAST_DATE`
- `Claude API changes new model after:LAST_DATE`
- `Anthropic blog announcement after:LAST_DATE`

### 日本語（Qiita / Zenn / Note）

- `Claude Code site:qiita.com after:LAST_DATE`
- `Claude Code site:zenn.dev after:LAST_DATE`
- `Anthropic Claude site:note.com after:LAST_DATE`
- `Claude Code 使い方 実践 after:LAST_DATE`

## エージェント別検索クエリ（引数に応じて切り替え）

### cursor
- `Cursor IDE update changelog after:LAST_DATE`
- `Cursor AI new features after:LAST_DATE`

### copilot
- `GitHub Copilot update changelog after:LAST_DATE`
- `GitHub Copilot new features after:LAST_DATE`

### codex
- `OpenAI Codex update after:LAST_DATE`
- `OpenAI coding agent news after:LAST_DATE`

### all（全エージェント横断）
- 上記の全クエリを実行し、横断的にまとめる

## 重複排除ルール

エントリ生成前に以下を確認し、該当する記事はスキップする:

1. **URL重複**: 記事の URL が既存エントリのいずれかの `source_urls` に含まれている
2. **タイトル類似**: 記事のタイトルと既存エントリのタイトルが本質的に同じ内容を指している（同一トピックの別記事も含む）

重複と判断した場合は生成をスキップし、スキップした旨を出力後のサマリーに記載する。

## エントリ生成ルール

`templates/entry-template.md` を読み、そのフォーマットに従って `entries/01_unread/YYYY-MM-DD-slug.md` を生成する。
同日のファイルが既に存在する場合は `entries/01_unread/YYYY-MM-DD-slug-2.md` のように連番をつける。

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
1. 検索期間（前回実行日 〜 今日）
2. 生成したエントリのサマリー（関連度S/Aのものをハイライト）
3. スキップした重複記事の件数（あれば）
4. 「試してみますか？」の確認（関連度S/Aのものについて）
5. 確認されたら `/try` コマンドの使い方を案内
