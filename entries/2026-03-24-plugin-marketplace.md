---
date: 2026-03-24
status: read
relevance: A
tags: [claude-code, plugin, marketplace, rate-limit, statusline]
source_urls:
  - https://releasebot.io/updates/anthropic/claude-code
  - https://claude.com/plugins
experiment_dir: experiments/2026-03-24-plugin-marketplace
---

# Claude Code プラグインマーケットプレイス & レート制限表示

## 3行要約

- Claude Codeにプラグインマーケットプレイスが正式導入。`/plugin marketplace add` でプラグインソースを追加、`/plugin install` でインストール。settings.jsonからインラインで宣言も可能
- ステータスラインスクリプトに `rate_limits` フィールド追加。Claude.aiのレート制限使用状況（5時間/7日間ウィンドウ、使用率、リセット時刻）をリアルタイム表示可能に
- CLIツール使用検知によるプラグインTips表示も追加。ファイルパターンに加えてツール使用パターンでもプラグイン提案が出る

## 自分への関連度: A

プラグインマーケットプレイスは、このナレッジベースのコマンドをプラグイン化して他プロジェクトでも使い回せる可能性がある。レート制限表示は実用的で、Pro/Maxプラン利用時に残量を気にしながら作業する必要がなくなる。ゲーム開発の長時間セッションで特に助かる。

## 試すなら

1. Claude Code内で `/plugin marketplace add anthropics/claude-plugins-official` を実行
2. `/plugin list` で利用可能なプラグイン一覧を確認
3. 何か1つインストールして動作確認（例: fakechat）
4. レート制限表示は statusline カスタマイズを要確認

## ソース

- [Claude Code Release Notes - Releasebot](https://releasebot.io/updates/anthropic/claude-code)
- [Plugins for Claude Code and Cowork | Anthropic](https://claude.com/plugins)

---

## 感想・考察

**良かった点**: プラグイン化の仕組みが整備されたことで、このナレッジベースのスキルを他プロジェクトで横展開できる可能性がある。レート制限表示は長時間ゲーム開発セッションで残量を気にしなくて済む実用的な機能。

**微妙な点**: CLI専用機能なのでWeb環境では試せない。まずローカルで動作確認が必要。

**ワークフローへの適用**: ローカルに戻ったら `rate_limits` を statusline に追加する。プラグインマーケットプレイスも一度探索して有用なものがないか確認したい。

**次のアクション**: ローカル環境で `/plugin marketplace add` + statuslineへの `rate_limits` 追加を試す。

→ [実験ノート](../experiments/2026-03-24-plugin-marketplace/2026-03-24-plugin-marketplace.md)

