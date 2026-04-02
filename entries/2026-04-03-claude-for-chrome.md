---
date: 2026-04-03
status: unread
relevance: A
tags: [claude-code, chrome, browser-automation, workflow]
source_urls:
  - https://claude.com/ja-jp/claude-for-chrome
  - https://code.claude.com/docs/en/chrome
  - https://support.claude.com/en/articles/12012173-get-started-with-claude-in-chrome
experiment_dir: null
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

<!-- /try 実行時に自動生成 -->

