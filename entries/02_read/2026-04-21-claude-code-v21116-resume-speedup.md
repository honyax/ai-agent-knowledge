---
date: 2026-04-21
status: read
relevance: S
tags: [claude-code, changelog, resume, mcp, performance, security]
source_urls:
  - https://github.com/anthropics/claude-code/releases
  - https://code.claude.com/docs/en/changelog
  - https://releasebot.io/updates/anthropic/claude-code
experiment_dir: null
---

# Claude Code v2.1.115-116: /resume 最大67%高速化・インライン思考スピナー・サンドボックス修正

## 3行要約

- `/resume` が 40MB 超の大規模セッションで最大 67% 高速化。デッドフォークエントリが多いセッションの処理効率も改善。
- 思考中の進捗表示が独立行からインライン表示（"still thinking" → "thinking more" → "almost done thinking"）に変わり、UI がスッキリした。
- サンドボックスセキュリティ修正のほか、MCP stdio サーバーの起動高速化（resources/templates/list を初回 @-mention まで遅延評価）、`--resume` でスケジュールタスクが復元されるようになった。

## 自分への関連度: S

長いセッションの resume が遅い・重いと感じていたユーザーに直撃する改善。複数セッション並列運用時に resume 速度はボトルネックになりやすい。即アップデートして恩恵を受けられる。

## 詳細

**v2.1.116（2026-04-21）主な変更点:**

- `/resume` が 40MB+ のセッションで最大 67% 高速化、デッドフォーク多数でも安定
- 思考進捗スピナーがインライン表示に変更（独立した hint 行を廃止）
- サンドボックスセキュリティ修正
- MCP stdio 複数サーバー設定時の起動高速化（resources/templates/list を遅延）
- `/doctor` が複数 config スコープで同一 MCP サーバーが異なるエンドポイントで定義されている場合に警告表示
- `--resume`/`--continue` が期限切れでないスケジュールタスクを復元するようになった
- SSE/HTTP トランスポートでサーバー接続がレスポンス途中で切れた際に MCP ツール呼び出しが無限ハングする問題を修正
- v2.1.85 以前のセッションで `--resume` が "tool_use ids were found without tool_result blocks" で失敗するバグを修正
- プロジェクトルート外のファイルへの Write/Edit/Read が conditional skills/rules 設定時に失敗する問題を修正
- Windows での不要な config ディスク書き込み（パフォーマンス低下・破損の原因）を修正
- フルスクリーンモードで DEC 2026 対応端末における重複メッセージを修正
- `/clear` が現在のコンテキストサイズではなく累積トークンを表示していたバグを修正

## 試すなら

1. `npm update -g @anthropic-ai/claude-code` で v2.1.116 に更新
2. 40MB 超の古いセッションで `/resume` を実行して速度を確認
3. `/doctor` を実行して MCP サーバーの設定競合がないか確認
4. 思考スピナーのインライン表示を確認（長めのタスクで）

## ソース

- [Claude Code by Anthropic - Release Notes - April 2026](https://releasebot.io/updates/anthropic/claude-code)
- [Releases · anthropics/claude-code](https://github.com/anthropics/claude-code/releases)
- [Changelog - Claude Code Docs](https://code.claude.com/docs/en/changelog)

---

## 感想・考察

3行要約の範囲では直接関係する項目は少ないが、詳細を見ると以下が関係する可能性あり：

- **conditional skills/rules 設定時にプロジェクトルート外ファイルへの Write/Edit/Read が失敗するバグを修正**（37行目）— カスタムスキルを使っているなら直接関係するバグ修正。
- **`/clear` のトークン表示バグ修正** — 現在のコンテキストサイズではなく累積トークンを表示していた問題が解消。

**`/resume` と VSCode 拡張の関係について：**

VSCode 拡張で履歴からセッションを再開する操作は、内部で `claude --resume <session-id>` に相当する処理が走っていると思われる。拡張が CLI のラッパーである以上、UI 上で透過的に処理されるだけで仕組みは同じ。

CLI で明示的に `--resume` を使う場面は「ターミナルを閉じた後に別のターミナルから同じセッションを拾い直す」ような状況が主で、VSCode 拡張ユーザーは意識する機会がほとんどない。
