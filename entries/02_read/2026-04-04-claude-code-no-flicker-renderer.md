---
date: 2026-04-04
status: read
relevance: S
tags: [claude-code, terminal, rendering, performance, v2.1.89, v2.1.90]
source_urls:
  - https://code.claude.com/docs/en/fullscreen
  - https://x.com/bcherny/status/2039421575422980329
  - https://piunikaweb.com/2026/04/02/anthropic-no-flicker-mode-claude-code/
  - https://dev.to/raxxostudios/claude-code-just-fixed-terminal-flickering-how-to-enable-noflicker-mode-apf
experiment_dir: null
---

# Claude Code v2.1.89/v2.1.90 — NO_FLICKER ターミナルレンダラー & 新フック

## 3行要約

- `CLAUDE_CODE_NO_FLICKER=1` でオプトインできる新ターミナルレンダラーが追加（v2.1.89）。差分描画・仮想ビューポート・マウスサポートを実現しちらつきを約85%削減。
- Alt-screen バッファ（vim/htop方式）で動作し、表示中のメッセージのみレンダリングするため長セッションでもメモリが増えない。
- v2.1.90 で `PermissionDenied` フック（Auto Modeのclassifier拒否時に発火）、auto modeが「push しないで」などの明示的境界を尊重する改善も追加。

## 自分への関連度: S

長時間セッションでのちらつきと重さは常に気になっていた。`CLAUDE_CODE_NO_FLICKER=1` をセットするだけで試せるため即実践候補。マウス操作対応も魅力的。

## 詳細

### NO_FLICKER の仕組み

従来の「全画面クリア→再描画」サイクルをやめ、**仮想ビューポート**を保持して変更箇所だけを差分更新する。viやhtopと同様のalt-screen bufferで動作し、スクロールバックも仮想化されている。

主な改善点:
- ちらつき約85%削減
- メモリ使用量が会話長さに依存しなくなる
- マウスクリック・スクロール対応
- 入力ボックスが画面下部に固定される

### 注意事項

- v2.1.89以降が必要
- オプトイン（env var）のリサーチプレビュー
- スクロールバックを壊すという報告もあり（GitHub Issue #41965）

### v2.1.90 その他の主な変更

- `PermissionDenied` フック: Auto modeのclassifierが拒否した後に発火する新フックイベント
- Auto modeが「don't push」「wait for X before Y」等の明示的境界を尊重するようになった
- Named subagentsが `@` typeaheadサジェストに表示される
- /powerup にアニメーションデモ付きインタラクティブレッスンが追加

## 試すなら

1. `npm update -g @anthropic-ai/claude-code` で v2.1.89+ にアップデート
2. `CLAUDE_CODE_NO_FLICKER=1 claude` で起動して動作確認
3. マウスでスクロール・クリックできることを確認
4. 長セッションでのメモリ・ちらつき改善を体感
5. 問題があれば env var なしに戻す（スクロールバック壊れる場合あり）

## ソース

- [Fullscreen rendering - Claude Code Docs](https://code.claude.com/docs/en/fullscreen)
- [Boris Cherny on X (NO_FLICKER発表)](https://x.com/bcherny/status/2039421575422980329)
- [Anthropic Fixes Claude Code Terminal Flickering with NO_FLICKER](https://piunikaweb.com/2026/04/02/anthropic-no-flicker-mode-claude-code/)
- [Claude Code Just Fixed Terminal Flickering - DEV Community](https://dev.to/raxxostudios/claude-code-just-fixed-terminal-flickering-how-to-enable-noflicker-mode-apf)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
