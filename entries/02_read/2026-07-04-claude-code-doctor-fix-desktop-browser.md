---
date: 2026-07-04
status: read
relevance: A
tags: [claude-code, /doctor, desktop, browser, auto-mode, context-cost, トークン削減]
source_urls:
  - https://code.claude.com/docs/en/whats-new
  - https://code.claude.com/docs/en/debug-your-config
  - https://releasebot.io/updates/anthropic/claude-code
experiment_dir: null
---

# Claude Code: `/doctor` が「診断して直す」コマンドに進化、desktop 版に内蔵ブラウザ

## 3行要約

- **`/doctor` の修復機能化**（v2.1.205 で read-only 画面から転換とドキュメントに記載）: インストール健全性チェックに加え、**未使用の skills / MCP サーバー / プラグインをコンテキストコストと突き合わせて検出**、ローカル CLAUDE.md と checked-in 版の重複排除、コードベースから導出可能な CLAUDE.md 記述のトリミング提案、遅い hooks の検出。報告 → 確認 → 修正の順で、勝手には変更しない。エイリアス `/checkup`。
- **desktop 版に内蔵ブラウザ**: ドキュメント・デザイン・任意のサイトを Claude が開いて読み・クリック・操作できる。サンドボックス化されており、ブラウジングセッションの永続化は選択可能、外部サイトでのアクションは safety classifier がレビュー。
- **auto モードの追加ガードレール**: セッション transcript ファイルの改ざんをブロック、コンテキストから解決できない変数への `rm -rf` は実行前に確認。`/cd` のパス入力補完も追加。

## 自分への関連度: A

`/doctor` の「**未使用 skills / MCP / プラグイン vs コンテキストコスト**」分析は、RTK ([[2026-07-01-rtk-rust-token-killer]]) や Headroom ([[2026-06-02-project-headroom-token-compression]]) と同じトークン最適化の文脈で、**harness 標準機能として**入ったのが重要。自分は skills / プラグインを多数入れているので棚卸しに即使える。内蔵ブラウザは desktop 版限定のため VSCode 拡張環境（[[user_environment]]）の自分には当面関係ないが、Claude in Chrome ([[2026-07-03-claude-in-chrome-ga]]) との役割分担は把握しておく。transcript 改ざんブロックは MCP サプライチェーン攻撃（[[2026-07-03-mcp-supply-chain-token-theft]]）と同方向の防御強化。

## 詳細

### `/doctor` の修復機能化

- **従来**: read-only の診断レポートを表示するだけ（`f` キーでレポートを Claude に送って修正依頼する形）。
- **新**: 診断 + 修正提案 + 確認後に実行、まで一体化。
- **チェック項目**:
  - インストール健全性
  - **未使用の skills / MCP サーバー / プラグインの検出**（それぞれのコンテキストコストと突き合わせ）
  - ローカル CLAUDE.md と リポジトリ checked-in 版の重複排除
  - コードベースから導出できる CLAUDE.md 記述のトリミング提案
  - 遅い hooks のフラグ
- **安全設計**: findings を先に報告し、変更前に必ず確認を取る。
- **エイリアス**: `/checkup`
- ドキュメント上は v2.1.205 が境界とされるが、正確な導入バージョンは changelog 要確認。

### desktop 版の内蔵ブラウザ

- Claude Code desktop アプリ内にブラウザを内蔵。docs / デザイン / 任意サイトを開き、ローカル dev サーバーのプレビューと同じ要領で**読む・クリックする・操作する**。
- **サンドボックス化**: ブラウジングセッションを永続化するかは選択制。外部サイトでのアクションは safety classifier がレビュー。
- Claude in Chrome ([[2026-07-03-claude-in-chrome-ga]]) が「ユーザーの Chrome を操作する拡張」なのに対し、こちらは「Claude Code 専用の内蔵ブラウザ」。開発検証用途は内蔵ブラウザ、ユーザーの実ブラウザ連携は Chrome 拡張、という住み分け。

### auto モードのガードレール追加

- **transcript 改ざんブロック**: セッション transcript ファイルへの改変をブロック。エージェント自身（またはプロンプトインジェクション経由の指示）が作業履歴を書き換えて痕跡を消す攻撃パスを塞ぐ。
- **`rm -rf $VAR` の確認**: コンテキストから解決できない変数への `rm -rf` は実行前に確認を要求（変数が空で `/` を消す類の事故防止）。
- v2.1.183 の destructive block ([[2026-07-01-claude-code-v21180-v21193]]) からの継続強化。

### その他

- **`/cd` のパス補完**: `/add-dir` と同様に、入力中にディレクトリパスを提案。
- **background agent の事前アップデート**: Claude Code 更新直後にバックグラウンドで新バージョンへ更新され、attach 時の遅い stale-session アップグレードが不要に。
- **Bedrock / Vertex / Foundry で auto モードがデフォルト有効化**、Bedrock は Opus 4.8 に更新。

## 試すなら

1. Claude Code を最新化し、`/doctor`（または `/checkup`）を実行。未使用の skills / MCP サーバー / プラグインとそのコンテキストコストのレポートを確認する。
2. レポートに基づき、使っていないプラグイン・skills を削除してコンテキスト削減効果を体感（`claude plugin prune` [[2026-07-04-claude-code-v21200-manual-mode]] と併用）。
3. CLAUDE.md のトリミング提案を確認し、「コードから導出できる記述」の判定精度を評価（提案どおり削るかは自分で判断）。
4. desktop 版を使う機会があれば内蔵ブラウザを試し、VSCode 拡張 + Claude in Chrome 構成との使用感を比較。
5. VSCode 拡張版（[[user_environment]]）で `/doctor` の新機能が使えるか確認（CLI 先行パターンの可能性に注意）。

## ソース

- [What's new (Claude Code Docs)](https://code.claude.com/docs/en/whats-new)
- [Debug your configuration (Claude Code Docs)](https://code.claude.com/docs/en/debug-your-config)
- [Claude Code Updates by Anthropic - July 2026 (Releasebot)](https://releasebot.io/updates/anthropic/claude-code)

---

## 感想・考察

### `/doctor` について

- `/doctor` 自体は以前からあった read-only の診断コマンド（インストール健全性のレポート表示のみ、`f` キーでレポートを Claude に送って修正依頼する二段構え）。今回の変化は「診断のみ」から「診断 + 修正提案 + 確認後に実行」への進化。
- チェック対象もインストール健全性だけでなく、未使用 skills / MCP / プラグインのコンテキストコスト分析や CLAUDE.md のトリミング提案まで拡大した点が新しい。

### 内蔵ブラウザの位置づけ

- 「ユーザーが表示している内容をエージェントに渡す」機能ではなく、「Claude が自分でブラウザを開いて読み・クリック・操作する」機能。方向が逆。
- Web アプリ開発が主要ユースケース。dev サーバー起動 → localhost を Claude が開く → レンダリング確認・クリック・フォーム入力 → 問題を見て修正、という「確認とフィードバック」の往復がエージェント側で閉じる。動的 Web アプリの「実際に操作しないと分からないバグ」に効く。
- VSCode 拡張版では使えないため、同等のことをやるなら Playwright MCP / Chrome DevTools MCP が現行の定番。内蔵ブラウザは「それを MCP セットアップなしで、サンドボックス付きで標準搭載した」ものと捉えられる。

### desktop 版 vs VSCode 拡張版の整理（公式ドキュメントで確認）

- 「desktop 版はファイル名補完がない」は誤解で、`@` メンションによるファイル参照・補完はある（ローカルセッションのみ。クラウド/WSL セッションでは不可）。
- desktop 版の本当のデメリットはエディタ統合の欠如: 開いているファイル・選択範囲の自動コンテキスト共有、LSP diagnostics の連携がない。ファイルペインはスポット編集用で本格的なエディタではない。
- 一方 desktop 限定機能は多い: 内蔵ブラウザ + auto-verify（編集のたびにスクリーンショット・DOM 検査で自己検証）、並列セッション（worktree 自動隔離）、computer use（Pro プランで利用可・research preview）、side chat（/btw）、PR モニタリング（CI auto-fix / auto-merge）、Dispatch 連携。
- 「コードを書く道具の延長」なら VSCode 拡張、「エージェントを回す管制塔」なら desktop 版、という設計思想の分化が明確になってきた。自分の使い方では VSCode 拡張が主軸のままで妥当だが、Web アプリ開発での自己検証や並列タスクでは desktop 版併用の価値が出てきている。
