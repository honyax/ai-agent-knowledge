---
date: 2026-04-19
status: unread
relevance: S
tags: [claude-code, changelog, cli, security, keyboard-shortcut]
source_urls:
  - https://www.claudeupdates.dev/version/2.1.113
  - https://code.claude.com/docs/en/changelog
  - https://github.com/anthropics/claude-code/releases
  - https://github.com/anthropics/claude-code/issues/50270
experiment_dir: null
---

# Claude Code v2.1.113-114: ネイティブバイナリ起動・ネットワーク制御強化

## 3行要約

- CLIがJSバンドルではなくプラットフォーム別ネイティブバイナリを起動する構造に変更された（v2.1.113）
- `sandbox.network.deniedDomains` 設定でワイルドカード許可がある場合でも特定ドメインをブロック可能に
- `Shift+↑/↓` でフルスクリーン時のビューポートスクロール、`Ctrl+A/E` で論理行の先頭/末尾移動が追加

## 自分への関連度: S

ネイティブバイナリ化はアップデート後の挙動変化（起動速度・環境依存バグ）に注意が必要。Termux/Android等glibc非対応環境では既に問題報告あり。deniedDomainsはMCPサーバーのネットワーク制御に直接使える。

## 詳細

**v2.1.113 主な変更点（2026-04-17リリース）**

- `claude` コマンドが各プラットフォーム向けオプショナル依存パッケージ経由のネイティブバイナリを起動するよう変更
  - 従来のJS実行からバイナリ実行へ移行
  - glibc依存のため Termux/Android では動作しない問題が報告済み（Issue #50270）
- `sandbox.network.deniedDomains` 設定の追加
  - `allowedDomains` にワイルドカードがある状態でも特定ドメインを明示的にブロック可能

**キーボードショートカット改善**
- `Shift+↑/↓`: フルスクリーンモードでの選択範囲延長時にビューポートをスクロール
- `Ctrl+A` / `Ctrl+E`: 複数行入力時に現在の論理行の先頭/末尾へ移動
- Windows: `Ctrl+Backspace` で前の単語を削除
- 折り返した長いURLもクリック可能なまま維持

**その他の改善**
- `/extra-usage` コマンドがRemote Control（モバイル/Webクライアント）から使用可能に
- Writeツールのdiff計算速度が60%高速化（タブ・`&`・`$`を含む大きなファイル）
- `/loop` 改善: `Esc` で保留中のウェイクアップをキャンセル可能

**v2.1.114 変更点（2026-04-18リリース）**
- Agent Teamsのチームメイトがツール権限を要求した際のpermission dialogクラッシュを修正

## 試すなら

1. `claude --version` でv2.1.113以上であることを確認
2. `.claude/settings.json` に `sandbox.network.deniedDomains` を追加してMCPサーバーのネットワーク制限を検証
3. フルスクリーンモード（`/tui fullscreen`）で `Shift+↑/↓` スクロールを確認
4. 大きなファイルへのWriteツール速度変化を体感

## ソース

- [Claude Code v2.1.113 Release Notes - Claude Updates](https://www.claudeupdates.dev/version/2.1.113)
- [Changelog - Claude Code Docs](https://code.claude.com/docs/en/changelog)
- [Releases · anthropics/claude-code](https://github.com/anthropics/claude-code/releases)
- [v2.1.113+ broken on Termux/Android (Issue #50270)](https://github.com/anthropics/claude-code/issues/50270)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
