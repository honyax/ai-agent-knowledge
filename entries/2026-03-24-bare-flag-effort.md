---
date: 2026-03-24
status: unread
relevance: S
tags: [claude-code, bare-flag, scripting, automation, effort]
source_urls:
  - https://github.com/anthropics/claude-code/releases
  - https://releasebot.io/updates/anthropic/claude-code
experiment_dir: null
---

# Claude Code `--bare` フラグ & スキルの `effort` フロントマター

## 3行要約

- `--bare` フラグが追加：スクリプトからの `-p` 呼び出し時にhooks/LSP/プラグイン同期/スキルディレクトリ走査をスキップし、軽量に実行できる
- スキルとスラッシュコマンドの YAML フロントマターに `effort` を指定して、モデルのeffortレベルを上書き可能に
- `ANTHROPIC_CUSTOM_MODEL_OPTION` 環境変数で `/model` ピッカーにカスタムエントリを追加可能に

## 自分への関連度: S

`--bare` フラグは、まさにこのナレッジベースの自動収集（GitHub Actionsでの定期実行）に使える。余計なプラグイン読み込みをスキップして高速にClaude Codeを回せる。`effort` フロントマターは、簡単なタスク（status確認等）は低effortで高速に、重いタスク（コードレビュー等）は高effortで丁寧に、とスキルごとに使い分けられる。

## 試すなら

1. `claude --bare -p "Hello"` で `--bare` の動作を確認（hooks等がスキップされることを確認）
2. 既存のスラッシュコマンド（例: `/status`）のフロントマターに `effort: low` を追加して速度差を体感
3. `ANTHROPIC_CUSTOM_MODEL_OPTION` を設定して `/model` ピッカーにカスタムモデルが出るか確認

## ソース

- [Claude Code Releases - GitHub](https://github.com/anthropics/claude-code/releases)
- [Claude Code Release Notes - Releasebot](https://releasebot.io/updates/anthropic/claude-code)

---

## 感想・考察

<!-- /review コマンドで自動生成、または手動で記入 -->

