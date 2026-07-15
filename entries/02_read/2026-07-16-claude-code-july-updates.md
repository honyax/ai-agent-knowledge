---
date: 2026-07-16
status: read
relevance: A
tags: [claude-code, changelog, memory-leak, accessibility, vim, auto-mode, 安定性]
source_urls:
  - https://code.claude.com/docs/en/changelog
  - https://github.com/anthropics/claude-code/releases
  - https://releasebot.io/updates/anthropic/claude-code
experiment_dir: null
---

# Claude Code 7月上旬〜中旬の更新まとめ: メモリリーク修正群、経過時間カウンター、スクリーンリーダーモード

## 3行要約

- **長時間セッションのメモリリークを複数修正**: MCP stdio サーバーの stderr 蓄積、LSP ドキュメントの開きっぱなし、async hook 出力の保持、headless/SDK セッションでの大きな tool-result ペイロードによる無制限成長。長時間・background 運用の安定性が向上。
- **UX 改善**: 折りたたまれたツールサマリー行に**経過時間のライブカウンター**追加（長いツール呼び出しが「固まってる」ように見えない）。`vimInsertModeRemaps` 設定で `jj` → Escape などの 2 キーリマップ。fullscreen メニューのマウスクリック対応拡大。permission / timeout 警告の明確化。
- **スクリーンリーダーモード**追加（`claude --ax-screen-reader` / `CLAUDE_AX_SCREEN_READER=1` / settings の `axScreenReader: true`）。auto モードが Bedrock / Vertex AI / Foundry で**オプトイン不要のデフォルト有効**に（`disableAutoMode` で無効化）。

## 自分への関連度: A

破壊的変更や大型新機能はないが、**メモリリーク修正群は catch-up のような長セッション・並列 MCP 運用の安定性に直結**する（自分は Unity/Blender/Godot MCP を stdio で使うので stderr 蓄積の修正は該当し得る）。経過時間カウンターは「長い検索やビルドで止まって見える」不安の解消に地味に効く。vim リマップは vim mode を使うなら嬉しい小改善。[[2026-07-04-claude-code-v21200-manual-mode]] 以降の差分として記録。

## 詳細

### メモリリーク修正（長時間セッションの安定性）

| リーク箇所 | 影響していた運用 |
|-----------|----------------|
| MCP stdio サーバーの stderr 蓄積 | Unity / Blender / Godot MCP など stdio サーバー常用時 |
| LSP ドキュメントが無期限に開いたまま | LSP プラグイン（csharp-lsp 等）利用時 |
| async hook 出力の保持 | hooks 利用時 |
| headless / SDK セッションでの大きな tool-result 無制限成長 | `claude -p` / SDK / background agents |

background agents（[[2026-07-01-claude-code-v21198-background-agents-auto-pr]]）や夜間ループ運用の土台がまた一段固くなった。

### UX / アクセシビリティ

- **経過時間ライブカウンター**: 折りたたみ表示のツールサマリー行で、実行中のツール呼び出しの経過時間がリアルタイムに進む。
- **`vimInsertModeRemaps`**: vim mode の insert モードで `jj` → Escape のような 2 キーシーケンスのリマップを設定可能。
- **スクリーンリーダーモード**: プレーンテキストレンダリングにオプトイン切替。起動フラグ / 環境変数 / settings の 3 通り。
- **fullscreen マウス対応拡大**: multi-select メニューと「Other」入力行のクリック対応。
- **permission / timeout 警告の文言明確化**。

### auto モード関連

- **Bedrock / Vertex AI / Foundry でデフォルト有効化**: `CLAUDE_CODE_ENABLE_AUTO_MODE` のオプトインが不要に。無効にしたい場合は settings の `disableAutoMode`。
- Bedrock のデフォルトモデルが Opus 4.8 に更新。
- 企業のクラウド経由利用でも auto モード + guardrails（[[2026-07-01-claude-code-v21180-v21193]]）の体験が個人版と揃う方向。

### セッション / エージェント信頼性

- attach、background workers、worktrees、MCP サーバー、approvals まわりの安定性改善が広範に入った（詳細は公式 changelog 参照）。
- SDK MCP サーバーの初回接続が次ターンまで遅延するバグ、`/mcp` が設定編集後に placeholder サーバーを再分類しないバグも修正。

## 試すなら

1. `claude --version` で最新化し、長めのタスク実行中にツールサマリー行の経過時間カウンターを確認。
2. 長時間セッション（catch-up 数回分）を回した後のメモリ使用量を、更新前の体感と比較（特に MCP サーバー併用時）。
3. vim mode を使っているなら `vimInsertModeRemaps` で `jj` → Escape を設定してみる。
4. VSCode 拡張版（[[user_environment]]）への反映を確認（CLI 先行パターンに注意）。

## ソース

- [Claude Code changelog (公式)](https://code.claude.com/docs/en/changelog)
- [Releases · anthropics/claude-code](https://github.com/anthropics/claude-code/releases)
- [Claude Code Updates by Anthropic - July 2026 (Releasebot)](https://releasebot.io/updates/anthropic/claude-code)

---

## 感想・考察

読了時に「UX 改善・スクリーンリーダーモードは VSCode ネイティブ拡張版に関係するか」を整理した（2026-07-16）。

### VSCode ネイティブ拡張版（自分の環境）への影響整理

UX 改善とスクリーンリーダーモードは、ほぼ CLI（ターミナル TUI）側の機能で、拡張版には基本的に関係しない。

| 項目 | 拡張版への影響 |
|------|--------------|
| 経過時間ライブカウンター | 対象外。TUI のツールサマリー行の描画要素。拡張版は独自 GUI でツール表示しており描画レイヤーが別 |
| `vimInsertModeRemaps` | 対象外。CLI vim mode（TUI 入力欄）向け。VSCode で vim 操作するなら VSCode の Vim 拡張の領分 |
| fullscreen マウス対応拡大 | 対象外。拡張版には fullscreen renderer 自体がない（[[user_environment]] で確認済みのパターン） |
| スクリーンリーダーモード | 対象外。TUI をプレーンテキスト描画に切り替えるもの。拡張版のアクセシビリティは VSCode 本体のスクリーンリーダー統合に乗る |
| permission / timeout 警告の文言 | コア側の共有文字列なら反映される可能性あり。実利は小さい |

### 拡張版ユーザーとして実利があるもの

- **メモリリーク修正群**（共有コアの修正）。特に MCP stdio サーバーの stderr 蓄積（Unity / Blender / Godot MCP 常用で該当）と headless / SDK セッションの tool-result 無制限成長の修正。
- auto モード関連とセッション / エージェント信頼性改善。

「試すなら」4 番の「VSCode 拡張版への反映を確認」は、UX 系項目については「反映を待つ」ではなく「そもそも対象外」が正確。CLI 先行かどうかではなく、描画サーフェスが違うという話。
