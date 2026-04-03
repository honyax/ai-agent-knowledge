---
date: 2026-03-24
status: tried
relevance: S
tags: [claude-code, channels, mcp, telegram, discord, remote]
source_urls:
  - https://code.claude.com/docs/en/channels
  - https://venturebeat.com/orchestration/anthropic-just-shipped-an-openclaw-killer-called-claude-code-channels
experiment_dir: experiments/2026-03-24-claude-code-channels
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

詳細は [experiments/2026-03-24-claude-code-channels/2026-03-24-claude-code-channels.md](../experiments/2026-03-24-claude-code-channels/2026-03-24-claude-code-channels.md) 参照。

**Chat bridge としての評価: 期待外れ**
スマホから Claude Code を操作したいという動機で試したが、Discord を経由するのが面倒で、出力も見えず Allow / Deny しか選択できない。Remote Control（claude.ai モバイルアプリ）で同じことができるため、Chat bridge としての Channels に独自のメリットはほぼない。

**Webhook receiver が本命**
CI/CD 失敗通知を Claude Code セッションに直接届けて自律修正させるユースケースが Channels の真価。GitHub Actions が Discord に投稿 → ローカルの Discord プラグインが受信 → 同一セッション（文脈あり）で Claude が修正対応、というループが実現できる。ローカルマシンに inbound ポートを開けずに済む点も重要。

**制約は合理的な設計意図あり**
`--channels` の明示的オプトインは「常時起動デーモンにしない」安全設計。Discord 経由はローカルを外部に晒さないアーキテクチャ上の必然。不便さではなく意図的な制約。

**理想との差分**
CI ループを人間不在で完全自動化したいが、Channels は「人間が許可した上で自律実行」という設計思想。完全自動ループは Claude Code on the Web や将来の GitHub Actions 直接統合が担う方向性。
