---
date: 2026-03-29
entry: entries/2026-03-24-claude-code-channels.md
status: done
---

# 実験: Claude Code Channels（Discord）

## 目的

外出先のスマホ（Discord）から Claude Code セッションに指示を出し、プッシュ通知で結果を受け取れるか検証する。

## 環境

- OS: Windows 11
- Claude Code: v2.1.x（要確認）
- Bun: インストール中
- チャンネル: Discord

## セットアップ手順

### 1. Bun インストール（ユーザー実施）

```powershell
# Windows（公式インストーラ）
powershell -c "irm bun.sh/install.ps1 | iex"
```

確認:
```
bun --version
```

### 2. Discord Bot 作成

1. https://discord.com/developers/applications → **New Application**
2. **Bot** セクション → **Reset Token** → トークンをコピー
3. **Privileged Gateway Intents** → **Message Content Intent** を ON
4. **OAuth2 > URL Generator** → `bot` スコープ + 権限設定:
   - View Channels, Send Messages, Send Messages in Threads
   - Read Message History, Attach Files, Add Reactions
5. 生成 URL でサーバーに Bot を招待

### 3. Claude Code 内でプラグイン設定

```
# マーケットプレイス未登録の場合（初回のみ）
/plugin marketplace add anthropics/claude-plugins-official

# プラグインインストール
/plugin install discord@claude-plugins-official
/reload-plugins

# トークン設定（~/.claude/channels/discord/.env に保存される）
/discord:configure <BOT_TOKEN>
```

### 4. Channels 起動 & ペアリング

```bash
claude --channels plugin:discord@claude-plugins-official
```

Discord でボットに DM → ペアリングコードが返信される

```
/discord:access pair <code>
/discord:access policy allowlist
```

## 実行ログ

### セットアップ

- Bun インストール: ユーザーが実施
- Discord Developer Portal で `ClaudeCode` アプリを作成、Bot トークンを発行
- トークンを `~/.claude/channels/discord/.env` に保存（`/discord:configure` と同等）
- `claude --channels plugin:discord@claude-plugins-official` で起動
- Discord でボットに DM → ペアリングコード取得 → `/discord:access pair <code>` で登録
- 権限リクエスト（Allow / Deny）が Discord に通知されることを確認

### 発生した問題

Discord から `/status` コマンドを実行した際にエラーが発生し、MCP サーバーがクラッシュした。

**原因**: `fetchAllowedChannel` 関数（server.ts:407）で、DM チャンネルを REST API 経由でフェッチした際に `ch.recipientId` が null になるケースがある。Discord.js は Gateway（WebSocket）経由で受信したチャンネルオブジェクトにはレシピエント情報を含むが、REST でフェッチした場合は欠落することがある。

**対処**: `access.json` の `groups` に DM チャンネル ID を追加（フォールバックチェックが server.ts:408-409 に実装済み）。セッション再起動で解消。

```json
// access.json の最終状態
{
  "dmPolicy": "pairing",
  "allowFrom": ["824986346879713324"],
  "groups": {
    "1487676309122584686": {
      "requireMention": false,
      "allowFrom": ["824986346879713324"]
    }
  },
  "pending": {}
}
```

## 観察・結果

- ペアリング自体は問題なく動作した
- 権限リクエストが Discord に通知され、スマホから Allow / Deny を選択できることを確認
- `/status` コマンドはプラグインバグにより今回は動作確認できず（Research Preview 段階のバグ）
- Claude の返信テキストは Discord に表示されるが、ターミナルの詳細出力は見えない

## 注意事項

- Research Preview 段階。`--channels` フラグの構文は変更される可能性あり
- claude.ai ログイン必須（Console/API キー認証は不可）
- セッションが開いている間のみメッセージを受信（常時稼働は tmux 推奨）
- パーミッションプロンプトが出るとセッションが一時停止する
- `ch.recipientId` null バグあり（2026-03-29 時点）。`groups` にDMチャンネルIDを追加することで回避可能

## 考察

### Channels の用途の整理

試してみて、公式の比較表に書かれている「Chat bridge」と「Webhook receiver」の位置づけが実感として理解できた。

**Chat bridge（Discord/Telegram経由でスマホ操作）について**

最初の動機は「外出先からスマホで Claude Code を操作したい」だったが、試してみると以下の不満があった：

- Discord を経由するのが面倒。Claude Code の出力が見えず、Allow / Deny しか選択できない。
- そもそも Remote Control（claude.ai モバイルアプリからローカルセッションを操作）で同じことができるのではないか。

この感想は正しく、Chat bridge としての Channels は Remote Control と機能が重複する。Discord/Telegram 経由のメリットは「Claude アプリを使わなくても既存のチャットアプリで操作できる」程度で、利便性の差でしかない。

**Webhook receiver が本命**

Channels が既存技術では代替困難なユースケースは Webhook receiver だと理解した。

たとえば以下のワークフローが実現できる：

```
Claude Code で実装 → PR 作成 → CI/CD 実行
→ 失敗時に GitHub Actions が Discord に通知
→ ローカルの Discord プラグインが WebSocket で受信
→ Claude Code セッションにイベントとして届く
→ Claude Code がエラーを確認して修正・再プッシュ
```

CI → Claude Code への通知は Discord が仲介するため、ローカルマシンに inbound ポートを開ける必要がない点が重要。また、同一セッションにイベントが届くため、Claude がどのブランチで何を作業していたかの文脈を持ったまま対応できる。

**制約の設計意図**

「`--channels` でセッションが起動したままである必要がある」「Discord を経由する」という制約について、不便さの原因はセキュリティ担保のための設計かと思ったが、実際は：

- `--channels` の明示的オプトイン → Claude Code を常時起動デーモンにしない安全設計
- Discord 経由 → ローカルマシンを外部に晒さずに外部イベントを受け取るアーキテクチャ上の必然

どちらも合理的な理由があった。

**理想のワークフローと現状のギャップ**

CI 失敗を受けて修正し、再度 CI を回すループを人間の介入なしに自律的に回してほしいが、現状の Channels はそこまでの完全自動化を想定していない。「人間が許可した上で、あとは自律的に進める」という設計思想がある。

完全自動 CI ループに近いのは Claude Code on the Web や GitHub Actions との直接統合の方向性で、Channels はその手前の「外出中でも人間が介在できる」レイヤーとして機能する。
