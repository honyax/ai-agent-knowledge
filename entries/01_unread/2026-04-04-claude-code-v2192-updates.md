---
date: 2026-04-04
status: unread
relevance: A
tags: [claude-code, mcp, bedrock, changelog, security, vscode]
source_urls:
  - https://github.com/anthropics/claude-code/blob/main/CHANGELOG.md
  - https://releasebot.io/updates/anthropic/claude-code
  - https://help.apiyi.com/en/claude-code-v2-1-92-mcp-persistence-powerup-tutorial-en.html
experiment_dir: null
---

# Claude Code v2.1.90〜v2.1.92 — MCP大容量結果・Bedrockウィザード・トランスクリプト検索など

## 3行要約

- MCP ツール結果のサイズ上限を `_meta["anthropic/maxResultSizeChars"]` アノテーションで最大500Kまで拡張可能に（DBスキーマ等の大容量データ向け）
- AWS Bedrock のセットアップウィザードをログイン画面から対話的に実行できるようになった（認証・リージョン・モデル選択を順番に案内）
- `/cost` コマンドにサブスクリプションユーザー向けのモデル別・キャッシュヒット別の内訳表示が追加された

## 自分への関連度: A

MCPの大容量結果対応はUnity MCPや大きなスキーマを扱う場面で直接役立つ。Bedrockウィザードや`/cost`の詳細化も実運用に便利な改善。

## 詳細

**MCP result persistence override**:
- `_meta["anthropic/maxResultSizeChars"]` アノテーションを付けることで500Kまでの結果をトランケートせず通過させられる
- DBスキーマ・大規模ログ・コード全体などを一度に渡せる

**Bedrock セットアップウィザード**:
- ログイン画面の「3rd-party platform」選択時に対話型ウィザードが起動
- AWS認証・リージョン設定・認証情報確認・モデル固定を順番に案内

**その他の改善**:
- トランスクリプトモード中に `/` キーで検索可能になった
- `CLAUDE_CODE_SUBPROCESS_ENV_SCRUB=1` でサブプロセス環境からAnthropicおよびクラウドプロバイダーの認証情報を除去（セキュリティ強化）
- VSCodeでネイティブMCPサーバー管理ダイアログを `/mcp` コマンドから利用可能に（有効化/無効化・再接続・OAuth認証管理）
- Writeツールのdiff計算速度が大きなファイル（タブ含む）で60%高速化
- `--resume` 時にdeferredツール・MCPサーバーがある場合のプロンプトキャッシュミスを修正

## 試すなら

1. `npm update -g @anthropic-ai/claude-code` でv2.1.92以上に更新
2. MCPサーバーで大きなデータを返す場面で `_meta["anthropic/maxResultSizeChars"]` を設定してみる
3. `/cost` コマンドを実行してモデル別コスト内訳を確認
4. VSCodeで `/mcp` を実行してMCPサーバー管理ダイアログを確認

## ソース

- [Claude Code Changelog (GitHub)](https://github.com/anthropics/claude-code/blob/main/CHANGELOG.md)
- [Claude Code April 2026 Release Notes - Releasebot](https://releasebot.io/updates/anthropic/claude-code)
- [Master 5 New Features of Claude Code v2.1.92 - Apiyi](https://help.apiyi.com/en/claude-code-v2-1-92-mcp-persistence-powerup-tutorial-en.html)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
