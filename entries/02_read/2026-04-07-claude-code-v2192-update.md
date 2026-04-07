---
date: 2026-04-07
status: read
relevance: S
tags: [claude-code, update, mcp, bedrock, windows]
source_urls:
  - https://code.claude.com/docs/en/changelog
  - https://github.com/anthropics/claude-code/releases
---

# Claude Code v2.1.92 アップデート: Bedrock ウィザード・/cost 詳細表示・MCP 500K 永続化

## 3行要約

- `/cost` コマンドにモデル別・キャッシュヒット率の内訳が追加され、どのモデルにコストがかかっているか可視化できるようになった
- MCP ツール結果永続化が最大500Kまで拡張（`_meta["anthropic/maxResultSizeChars"]` アノテーション）、DBスキーマ等の大きなデータが切り捨てられなくなった
- AWS Bedrock のインタラクティブセットアップウィザードが追加、ログイン画面から認証・リージョン・モデル固定まで一括設定可能

## 自分への関連度: S

日常的に使う Claude Code の実務直結アップデート。コスト可視化と MCP 大容量化は即座にワークフローに影響する。

## 詳細

**v2.1.92（2026年4月4日）の主要変更:**

- `/cost` コマンドにモデル別・キャッシュヒット率の内訳を追加（サブスクリプションユーザー向け）
- `/release-notes` がインタラクティブなバージョン選択ピッカーに対応
- Remote Control セッション名のデフォルトがホスト名プレフィックス形式に（例: `myhost-graceful-unicorn`）
- `forceRemoteSettingsRefresh` ポリシー設定の追加（起動時にリモート管理設定を強制再取得）
- Write ツールの差分計算速度が約60%向上（タブ・`&`・`$` を含む大ファイル）
- `/tag` コマンドと `/vim` コマンドが削除（vim モードは `/config` → Editor mode から設定）
- Linux サンドボックスで `apply-seccomp` ヘルパーを npm/ネイティブ両ビルドに配備

**MCP 関連:**
- MCP ツール結果永続化サイズ上限を最大500Kに設定可能（`_meta["anthropic/maxResultSizeChars"]` アノテーション）
- プラグイン提供の MCP サーバーで手動設定と同一のサーバーが重複する場合は自動スキップ
- VSCode で `/mcp` コマンドによる MCP サーバー管理ダイアログが追加（ターミナル不要）

## 試すなら

1. `/cost` を実行してモデル別コスト内訳を確認する
2. MCP サーバーで大きなデータを扱う場合、`_meta["anthropic/maxResultSizeChars"]` を設定してみる
3. VSCode の Claude Code 拡張で `/mcp` コマンドを実行してサーバー管理 UI を確認

## ソース

- [Changelog - Claude Code Docs](https://code.claude.com/docs/en/changelog)
- [Releases · anthropics/claude-code](https://github.com/anthropics/claude-code/releases)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
