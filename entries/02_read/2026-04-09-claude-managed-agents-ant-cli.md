---
date: 2026-04-09
status: read
relevance: C
tags: [claude-api, managed-agents, cli, ant, agentic]
source_urls:
  - https://platform.claude.com/docs/en/release-notes/overview
  - https://platform.claude.com/docs/en/managed-agents/overview
experiment_dir: null
---

# Claude Managed Agents (パブリックベータ) と ant CLI のリリース

## 3行要約

- **Claude Managed Agents** がパブリックベータ開始：安全なサンドボックス・組み込みツール・SSEストリーミングを備えたフルマネージドのエージェントハーネス
- **`ant` CLI** がリリース：Claude API へのコマンドライン接続ツール。Claude Code とのネイティブ統合・YAML リソースバージョニング対応
- Amazon Bedrock への Messages API 提供開始（us-east-1）、`/anthropic/v1/messages` エンドポイントで同一リクエスト形式を使用可能

## 自分への関連度: C

**Claude Code でゲーム実装をしている現在のワークフローへの直接の影響はない。**
Managed Agents は「APIを直接呼び出して自前のエージェント自動化ツールを作る」場面で選択肢になる。
ant CLI は API を素早く試したいときに使える。

## 詳細

### Claude Managed Agents

Messages API の上位に位置するサービスで、自律エージェント実行に必要なインフラ一式をAnthropicが提供する。

**4つのコアコンセプト：**

| 概念 | 内容 |
|------|------|
| Agent | モデル・システムプロンプト・ツール・MCPサーバーの定義 |
| Environment | Python/Node.js等をインストール済みのコンテナテンプレート |
| Session | Agentを実際に動かすインスタンス（タスク単位） |
| Events | アプリとエージェント間のメッセージ（SSEストリーミング） |

**組み込みツール：**
- Bash（コンテナ内でシェルコマンド実行）
- ファイル操作（読み書き・検索）
- Web検索・取得
- MCPサーバー接続

**Messages API との違い：**

| | Messages API | Claude Managed Agents |
|---|---|---|
| 向いている用途 | 細かく制御したいカスタム実装 | 長時間・非同期タスク |
| 自分で作るもの | エージェントループ・ツール実行・サンドボックス | ほぼなし |

**自分のワークフローへの影響：**
- Claude Code（CLIツール）をそのまま使っている分には今まで通り、影響なし
- APIを直接呼び出して「コードを書く→テスト→結果を見てまた書く」のような長時間の自動化パイプラインを組みたい場合に有力な選択肢
- 現時点ではパブリックベータ。`outcomes`・`multiagent`・`memory` はリサーチプレビュー（別途アクセス申請が必要）

- Claude Code の `/claude-api` スキルも Managed Agents 対応に更新済み

### ant CLI

- Claude API へのコマンドラインクライアント
- より速いインタラクション
- Claude Code とのネイティブ統合
- YAML リソースバージョニング対応

### Amazon Bedrock 対応

- `/anthropic/v1/messages` エンドポイントが Bedrock で利用可能（リサーチプレビュー）
- us-east-1 で提供開始
- ファーストパーティ API と同一リクエスト形式

## 試すなら

1. [Claude Platform Release Notes](https://platform.claude.com/docs/en/release-notes/overview) で Managed Agents のドキュメントを確認
2. `ant` CLI のインストール方法を調べる
3. 既存の Claude API コードで Managed Agents が使えるか検討する
4. Bedrock 利用者は `/anthropic/v1/messages` への移行可否を確認

## ソース

- [Claude Platform Release Notes](https://platform.claude.com/docs/en/release-notes/overview)
- [Claude Managed Agents Overview](https://platform.claude.com/docs/en/managed-agents/overview)

---

## 感想・考察

公式ドキュメントを確認した上で、自分のワークフローへの影響を整理した。

Managed Agents は「Anthropic API を直接呼び出してエージェントアプリを自前で作る人」向けのサービスであり、Claude Code CLI の上で Skills を使っている自分には直接関係しない。レイヤーが根本的に違う。

```
Claude Code CLI（Skills・Hooks等）  ←── 自分のワークフロー
        ↕
  Anthropic API
        ↕
  Messages API   /   Claude Managed Agents  ←── 別レイヤー
```

長時間の実装パイプラインも現在は Skills で実現できており、Managed Agents を検討する動機がない。「知識として知っておく」程度の情報（関連度 C）として整理。

将来的に Claude API を直接呼び出す自動化ツールを自作したくなった場合に、このエントリを再確認する価値がある。
