---
date: 2026-04-03
status: tried
relevance: A
tags: [claude-code, chrome, browser-automation, workflow]
source_urls:
  - https://claude.com/ja-jp/claude-for-chrome
  - https://code.claude.com/docs/en/chrome
  - https://support.claude.com/en/articles/12012173-get-started-with-claude-in-chrome
experiment_dir: experiments/2026-04-03-claude-for-chrome
---

# Claude for Chrome — Claude CodeからChromeブラウザを直接操作するChrome拡張機能（ベータ）

## 3行要約

- Chrome拡張機能「Claude in Chrome」をインストールして `claude --chrome` で起動するとClaude Codeがブラウザを操作できる。ナビゲーション、クリック、フォーム入力、コンソールログ読取、スクリーンショット、GIF録画が可能
- ログイン済みサイト（Google Docs、Gmail、Notion等）にも直接アクセスできるためAPIコネクタ不要で自動化できる
- 現在ベータ版。Chrome/Edge対応（Brave・Arc・WSL非対応）。Pro/Max/Team/Enterpriseプランが必要（Bedrock/Vertex AI経由では利用不可）

## 自分への関連度: A

WebアプリのテストやUIデバッグをClaude Codeと連携して行える点はゲーム開発でも応用が効く（WebGLビルドの動作確認、ブラウザ上のゲームUIテストなど）。Claude Codeユーザーとして即セットアップできる実用的な機能。

## 詳細

**セットアップ**:
1. Chrome Web StoreからClaude拡張機能（v1.0.36以上）をインストール
2. Claude Code v2.0.73以上であることを確認
3. `claude --chrome` で起動、または既存セッション中に `/chrome` を実行

**できること**:
- **ライブデバッグ**: コンソールエラー・DOM状態を直接読み取り、コードを修正
- **Webアプリテスト**: フォームバリデーション検証、ビジュアルリグレッション確認、ユーザーフロー確認
- **ログイン済みアプリ操作**: Google Docs・Gmail・Notionなどに直接書き込み
- **データ抽出**: Webページから構造化データを取得してCSV保存
- **反復タスク自動化**: フォーム入力・複数サイト横断ワークフロー
- **セッション録画**: ブラウザ操作をGIFとして保存・共有

**利用例（CLIから）**:
```bash
# Chrome有効で起動
claude --chrome

# セッション内で有効化
/chrome

# デフォルト有効化（毎回フラグ不要）
/chrome → "Enabled by default"を選択
```

**操作例**:
```
# WebアプリのUIテスト
I just updated the login form. Open localhost:3000, try submitting
with invalid data, and check if error messages appear correctly.

# コンソールデバッグ
Open the dashboard and check the console for any errors on page load.

# データ抽出
Go to the product listings and extract name, price, availability.
Save as CSV.
```

**注意点**:
- ログインが必要なページやCAPTCHAに遭遇すると一時停止して手動対応を要求
- `--chrome` をデフォルト有効にするとブラウザツールが常時読み込まれてコンテキスト消費が増加
- 長時間セッションでサービスワーカーがアイドル化して接続が切れることがある（`/chrome` → "Reconnect"で回復）
- WindowsではWSL非対応。レジストリにNative Messagingホスト設定が必要

**Computer Useとの違い**:
- Claude for Chrome: ブラウザ内のタスクに特化。Claude Codeと連携してコーディング+ブラウザ操作をシームレスに実行
- Computer Use: macOSネイティブアプリを含む画面全体の操作が可能

## 試すなら

1. [Chrome Web Store](https://chromewebstore.google.com/detail/claude/fcoeoabgfenejglbffodgkkbkckhcgfn) からClaude拡張機能をインストール
2. `claude --version` でv2.0.73以上を確認（古ければ `npm update -g @anthropic-ai/claude-code`）
3. `claude --chrome` でClaude Codeを起動
4. `Go to localhost:PORT and check the page title` など簡単なブラウザ操作を指示して動作確認
5. 問題があれば `/chrome` でステータスを確認・再接続

## ソース

- [Claude for Chrome（公式ページ）](https://claude.com/ja-jp/claude-for-chrome)
- [Use Claude Code with Chrome (beta) - 公式ドキュメント](https://code.claude.com/docs/en/chrome)
- [Get started with Claude in Chrome - ヘルプセンター](https://support.claude.com/en/articles/12012173-get-started-with-claude-in-chrome)

---

## 感想・考察

実施日: 2026-04-05  
実験ファイル: [experiments/2026-04-03-claude-for-chrome/](../../experiments/2026-04-03-claude-for-chrome/2026-04-03-claude-for-chrome.md)

**良かった点:**
- ログイン済みセッションをそのまま使えるのが本当に便利で、Unity Asset Store の購入済みアセット87件を API トークン不要で全件取得できた。
- `javascript_tool` でDOM操作・データ抽出・ボタンクリックまで全部できるので、SPA 相手でも柔軟に対応できた。

**微妙だった点・制限:**
- SPA（React）サイトでは `get_page_text` が不安定で、ページ遷移後に前ページのコンテンツが混在することがある。データ抽出は `javascript_tool` + `innerText` の正規表現パターンが安定していた。
- public リポジトリの GitHub PR 取得はClaude for Chrome の強みが活かせず、`gh` コマンドで十分だと後から気づいた。ログイン必須・API 非公開のサイトに使うべき。

**自分のワークフローへの適用:**
- Unity Asset Store でのアセット管理（購入済み一覧の定期エクスポート、特定カテゴリの棚卸し）に使えそう。
- localhost で動いている React/TypeScript の開発サーバーに対して、フォームバリデーションやUIの動作確認を自動化するのが最も直接的な活用先。

**次のアクション:**
- ローカル開発サーバー（React）への UI テスト自動化を実際に試す。
- GraphQL エンドポイント（`/api/graphql/batch`）を直接叩いて全アセットを1リクエストで取得できるか検証する。
