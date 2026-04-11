---
date: 2026-04-10
status: read
relevance: S
tags: [claude-code, update, security, vertex-ai, mcp, bash]
source_urls:
  - https://github.com/anthropics/claude-code/releases/tag/v2.1.98
  - https://github.com/anthropics/claude-code/releases
  - https://code.claude.com/docs/en/changelog
experiment_dir: null
---

# Claude Code v2.1.98 — Bash セキュリティ修正・Vertex AI ウィザード・Monitor ツール

## 3行要約

- **Bash ツールのセキュリティ脆弱性を複数修正**: バックスラッシュエスケープによる権限バイパス・`/dev/tcp` リダイレクト自動許可・複合コマンドの強制許可プロンプト回避など
- **Google Vertex AI 対話型セットアップウィザード追加**: ログイン画面の「3rd-party platform」からGCP認証・プロジェクト設定・認証情報検証を対話的に完結できる
- **Monitor ツール追加**: バックグラウンドスクリプトからのイベントをストリーミング受信できる新ツール

## 自分への関連度: S

Bash ツールのセキュリティ修正は今すぐ更新すべきレベルの脆弱性修正。特にバックスラッシュ回避でread-only許可から任意コード実行に繋がるバグは重大。Vertex AI ウィザードはGCP環境でのセットアップを大幅に簡略化する。

## 詳細

### Bash ツール セキュリティ修正（重要）

以下の脆弱性が修正された:

- **バックスラッシュエスケープによる権限バイパス**: `\grep -f FILE` のようにバックスラッシュ付きフラグがread-onlyとして自動許可され、任意コード実行に繋がる問題
- **`/dev/tcp/...` や `/dev/udp/...` へのリダイレクト自動許可**: ネットワーク接続が無条件で許可されていた問題を修正。プロンプト表示に変更
- **複合コマンドが強制許可プロンプトをバイパス**: `auto` / `bypass-permissions` モードのsafety checkを回避できた問題
- **`grep -f FILE` / `rg -f FILE` が作業ディレクトリ外のパターンファイル読み込みを許可**: プロンプトなしで外部ファイル参照されていた問題

### Linux サブプロセスのサンドボックス強化

- `CLAUDE_CODE_SUBPROCESS_ENV_SCRUB` 設定時、Linux で PID namespace 分離によるサブプロセスサンドボックスを追加
- `CLAUDE_CODE_SCRIPT_CAPS` 環境変数でセッションあたりのスクリプト起動回数を制限可能

### Google Vertex AI セットアップウィザード

ログイン画面から「3rd-party platform」を選択すると対話的ウィザードが起動:
1. GCP 認証
2. プロジェクト・リージョン設定
3. 認証情報検証
4. モデルのピン留め

### Monitor ツール

バックグラウンドで動作するスクリプトからイベントをストリーミング受信できる新ツール。長時間バックグラウンドタスクの監視に活用可能。

### その他の修正

- W3C TRACEPARENT 環境変数を Bash ツールのサブプロセスに追加（OTELトレーシング時に親スパン正常化）
- Write ツールのdiff計算速度60%向上（タブ・`$` を含む大ファイル向け）
- MCP ツールの `_meta["anthropic/maxResultSizeChars"]` がトークンベースのpersistレイヤーをバイパスしない問題を修正
- VSCode でネイティブ MCP サーバー管理ダイアログを追加（`/mcp` でOAuth認証・サーバー有効化・再接続が可能）

## 試すなら

1. `claude --version` で現在のバージョンを確認
2. `npm update -g @anthropic-ai/claude-code` で v2.1.98 以降に更新
3. Bash ツールを多用しているプロジェクトで `\grep` 等のエスケープコマンドが正しくプロンプト表示されるか確認
4. Vertex AI ユーザーはログイン画面の「3rd-party platform」から新ウィザードを試す
5. バックグラウンドスクリプトがある場合は Monitor ツールの動作を確認

## ソース

- [Release v2.1.98 (GitHub)](https://github.com/anthropics/claude-code/releases/tag/v2.1.98)
- [Claude Code Releases](https://github.com/anthropics/claude-code/releases)
- [Claude Code Changelog](https://code.claude.com/docs/en/changelog)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
