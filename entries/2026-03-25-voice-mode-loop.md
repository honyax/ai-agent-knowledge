---
date: 2026-03-25
status: unread
relevance: S
tags: [claude-code, voice-mode, loop, cron, background-worker, productivity]
source_urls:
  - https://code.claude.com/docs/en/changelog
  - https://releasebot.io/updates/anthropic/claude-code
  - https://help.apiyi.com/en/claude-code-2026-new-features-loop-computer-use-remote-control-guide-en.html
experiment_dir: null
---

# Claude Code Voice Mode & /loop コマンド — 2026年3月の目玉機能

## 3行要約

- `/voice` コマンドでボイスモードが利用可能に。Push-to-talk方式（スペースバー長押し→離して送信）で20言語対応。常時リスニングではなく制御された入力方式
- `/loop` コマンドでCron的な定期タスク実行が可能に。PRレビュー、デプロイ監視、テスト実行などをバックグラウンドワーカーとして自動化できる
- いずれもClaude Code v2.1.76以降で段階的にロールアウト中。Opus 4.6がデフォルトモデル、1Mトークンコンテキストウィンドウ対応

## 自分への関連度: S

`/loop` はこのナレッジベースの定期情報収集に直接使える可能性がある（GitHub Actionsの代替）。例えば `/loop 6h /catch-up` のような定期実行が考えられる。Voice Modeはハンズフリーでのコード指示に使えるが、ゲーム開発での実用性は要検証。

## 試すなら

1. Claude Code を最新版に更新（`claude update`）
2. `/voice` を実行し、スペースバー長押しで音声入力を試す
3. `/loop 10m "git status"` のような簡単なコマンドで /loop の動作を確認
4. `/loop` の定期実行間隔やキャンセル方法を確認
5. このナレッジベースの `/catch-up` を `/loop` で定期実行するテスト

## ソース

- [Claude Code Changelog](https://code.claude.com/docs/en/changelog)
- [Claude Code Release Notes - Releasebot](https://releasebot.io/updates/anthropic/claude-code)
- [Claude Code March 2026 Full Capability Interpretation](https://help.apiyi.com/en/claude-code-2026-new-features-loop-computer-use-remote-control-guide-en.html)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
