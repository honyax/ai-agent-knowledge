---
date: 2026-03-24
status: unread
relevance: S
tags: [claude-code, channels, mcp, telegram, discord, remote]
source_urls:
  - https://code.claude.com/docs/en/channels
  - https://venturebeat.com/orchestration/anthropic-just-shipped-an-openclaw-killer-called-claude-code-channels
experiment_dir: null
---

# Claude Code Channels — Telegram/DiscordからClaude Codeセッションを操作可能に

## 3行要約

- Claude Codeに `--channels` フラグが追加され、MCPサーバー経由でTelegram/Discordからリアルタイムにセッションにメッセージを送受信できるようになった
- 従来の「ターミナルに座って操作する」モデルから、外出中でもスマホからタスク指示・結果受信が可能なプッシュベースの非同期開発モデルに移行
- Research Preview段階。Bun必須、公式プラグインのみ使用可。tmux/screenでの常時起動推奨

## 自分への関連度: S

ゲーム開発中にビルド待ちや長時間のClaude Codeタスクを走らせている間、デスクを離れても進捗を確認・指示できる。カードゲームのNetcode実装やBlender MCP連携のような長時間タスクで特に有用。また、CI結果やWebhookをチャンネル経由でClaudeに流す拡張も将来的に可能。

## 試すなら

1. `bun --version` でBunの有無を確認、なければ `curl -fsSL https://bun.sh/install | bash` でインストール
2. Claude Code内で `/plugin install fakechat@claude-plugins-official` → `/reload-plugins`
3. `claude --channels plugin:fakechat@claude-plugins-official` で起動し、`http://localhost:8787` でテスト
4. 動作確認後、Telegram BotFatherでbot作成 → `/plugin install telegram@claude-plugins-official` → `/telegram:configure` でトークン設定
5. `claude --channels plugin:telegram@claude-plugins-official` で起動し、スマホからDM送信して動作確認

## ソース

- [Push events into a running session with channels - Claude Code Docs](https://code.claude.com/docs/en/channels)
- [Anthropic just shipped an OpenClaw killer called Claude Code Channels | VentureBeat](https://venturebeat.com/orchestration/anthropic-just-shipped-an-openclaw-killer-called-claude-code-channels)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->

