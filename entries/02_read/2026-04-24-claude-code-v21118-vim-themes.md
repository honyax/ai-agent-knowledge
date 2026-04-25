---
date: 2026-04-24
status: read
relevance: A
tags: [claude-code, release, vim, theme, hooks, mcp]
source_urls:
  - https://github.com/anthropics/claude-code/releases/tag/v2.1.118
  - https://changelogs.directory/tools/claude-code/releases/2.1.118
experiment_dir: null
---

# Claude Code v2.1.118 — Vim visualモード・カスタムテーマ・MCP直接呼び出しHooks

## 3行要約

- 2026年4月23日リリースのv2.1.118でVim visualモード（v/V）追加、選択・オペレータ・ビジュアルフィードバック対応。Vim派の編集体験が大幅向上
- `/theme` でカスタムテーマを作成・切り替え可能に（`~/.claude/themes/` のJSON編集、プラグインからもテーマ配布可能）
- `/cost` と `/stats` を `/usage` に統合、Hooksから `type: "mcp_tool"` でMCPツールを直接呼び出し可能、`DISABLE_UPDATES` 環境変数で完全に更新を無効化できるように

## 自分への関連度: A

Vim visualモードは日常の編集作業に直接影響。カスタムテーマはコード色分けの最適化に使える。Hooks→MCP直接呼び出しは自作Hooksの設計自由度を大きく広げる。`/usage` 統合はコマンド体系の簡素化で歓迎。

## 詳細

### 主要な新機能

**Vim visualモード**
- `v` でビジュアルモード開始、`V` でビジュアル行モード
- hjkl移動、d/c/y によるテキストオブジェクト操作
- 選択範囲のビジュアルフィードバック表示

**カスタムテーマ**
- `/theme` コマンドで作成・切り替え
- `~/.claude/themes/` にJSONファイルを直接編集可能
- プラグインから `themes/` ディレクトリ経由で配布可能

**Hooks→MCP直接呼び出し**
- Hooksの設定で `type: "mcp_tool"` を指定するとMCPツールを直接呼べる
- これまではshellコマンド経由でラップする必要があった

**コマンド統合**
- `/cost` と `/stats` が `/usage` に統合（両方ともショートカットとして残る）

**Auto modeコントロール拡張**
- Auto modeのオン/オフを条件設定で細かく制御可能

**DISABLE_UPDATES環境変数**
- 設定すると全ての自動更新経路を完全遮断（企業環境での固定バージョン運用に有用）

### バグ修正

NO_FLICKERモードのテキストレンダリング不具合・クラッシュ・メモリリーク・キーボードショートカット問題など多数修正。

## 試すなら

1. `claude update` で v2.1.118 に更新
2. `/theme` でカスタムテーマを1つ作成してみる
3. Vim visualモード（`v` キー）で選択→操作を試す
4. 既存のHookを1つ `type: "mcp_tool"` に書き換えてシンプル化
5. `/usage` で統合されたUIを確認

## ソース

- [Release v2.1.118 (GitHub)](https://github.com/anthropics/claude-code/releases/tag/v2.1.118)
- [Claude Code v2.1.118 Changelog (changelogs.directory)](https://changelogs.directory/tools/claude-code/releases/2.1.118)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
