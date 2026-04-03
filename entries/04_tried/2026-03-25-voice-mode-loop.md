---
date: 2026-03-25
status: tried
relevance: S
tags: [claude-code, voice-mode, loop, cron, background-worker, productivity]
source_urls:
  - https://code.claude.com/docs/en/changelog
  - https://releasebot.io/updates/anthropic/claude-code
  - https://help.apiyi.com/en/claude-code-2026-new-features-loop-computer-use-remote-control-guide-en.html
experiment_dir: experiments/2026-03-25-voice-mode-loop
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

実施日: 2026-03-29 / 詳細: [実験ログ](../experiments/2026-03-25-voice-mode-loop/2026-03-25-voice-mode-loop.md)

**良かった点**: `/loop` が `CronCreate/CronList/CronDelete` ツールで実装されていることが判明。`CronList` を実際に呼んで動作確認できた。`/loop 6h /catch-up` はこのリポジトリで今すぐ使える形になっている。

**微妙な点**: `/loop` は「セッション中のみ有効」なので PC をシャットダウンすると止まる。常時稼働の定期収集には `schedule` スキル（RemoteTrigger）の方が向いている。

**`/voice` 実機検証結果（2026-03-29）**: Windows ターミナルで実際に試したが、音声認識バックエンドが未動作で文字起こしされなかった。`claude install`（npm版→ネイティブバイナリ移行）でマイク権限は解消されたが、認識自体は動かず。段階的ロールアウト中のため現時点では実用不可。代替として `Win+H`（Windows標準音声入力）がターミナル入力欄でそのまま使える。

**次のアクション**: `/loop 6h /catch-up` を実際のセッション中に試す。`/voice` はロールアウト完了後に再確認。
