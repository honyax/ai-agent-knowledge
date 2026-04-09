---
date: 2026-04-08
status: read
relevance: A
tags: [mcp, code-execution, claude-code, engineering]
source_urls:
  - https://www.anthropic.com/engineering/code-execution-with-mcp
experiment_dir: null
---

# MCP を使ったコード実行：より効率的なAIエージェントの構築（Anthropic Engineering）

## 3行要約

- AnthropicエンジニアリングブログでMCPを使ったコード実行アーキテクチャの詳細解説が公開された
- サンドボックス内コード実行をMCPツールとして提供することで、エージェントの実行能力を安全に拡張する手法
- Claude Code での実際の MCP × コード実行パターンが実装例付きで解説されている模様

## 自分への関連度: A

MCPサーバー構築とコード実行の組み合わせは、Blender MCP連携やUnity MCP拡張の実装パターンとして直接参考になる。Claude Codeのエージェント能力を安全に拡張する公式ガイドとして価値が高い。

## 詳細

Anthropic Engineering ブログに新たに掲載されたエントリ。MCP（Model Context Protocol）を通じてAIエージェントにコード実行能力を付与する際のアーキテクチャ設計・セキュリティ考慮事項・実装パターンを解説。

主なポイント（要フェッチ確認）：
- サンドボックス化されたコード実行環境の設計
- MCPツールとして安全にコード実行を公開する方法
- エラーハンドリングとタイムアウト管理
- 実際の活用事例とコードサンプル

## 試すなら

1. 記事を読んでアーキテクチャ全体を把握する
2. サンドボックス付きコード実行 MCP サーバーのサンプルコードを確認
3. Blender MCP または Unity MCP での応用可能性を検討
4. 小さなプロトタイプで実装パターンを試す（`experiments/` に記録）

## ソース

- [Code execution with MCP: building more efficient AI agents (Anthropic Engineering)](https://www.anthropic.com/engineering/code-execution-with-mcp)

---

## 感想・考察

### アーキテクチャの本質

このアプローチの核心は「ツール定義の爆発をランタイム設計で吸収する」こと。

従来のMCPでは、N個のサーバー × M個のツール分のJSON Schema定義をコンテキストに詰め込む必要があった。このアプローチでは各サーバーが `search_tools` と `exec_code` の2ツールだけを公開し、内部の機能はランタイム環境（ファイルシステム上のコード）として持つ。結果として接続サーバー数 × 2 のツール定義だけで全機能を賄える。

実際に150,000トークン → 2,000トークン（98.7%削減）の事例が紹介されている。

### コンテキスト効率化の2つの軸

1. **ツール定義の選択的ロード**：最初からすべてのAPI定義をコンテキストに入れるのではなく、`servers/` のディレクトリ構造（存在リスト）だけを知り、必要なファイルをオンデマンドで読む。`search_tools` に `detail_level` パラメータを持たせることでさらに細かく制御できる。

2. **データのサンドボックス内処理**：全件取得 → サンドボックス内でフィルタ/集計 → 結果だけモデルに返す。従来は全データをモデルのコンテキストに流していた。

### Blender / Unity との相性

この構成はもともと実行環境を内包したツールと特に相性が良い。Blender は `bpy` という Python API を持っており、「コードを受け取って実行する」ツール1つで全機能をカバーできる。Unity も同様に EditorスクリプトのAPIがそのままコードAPIになる。

逆に Slack や Salesforce のような外部サービスは実行環境を持たないため、記事のように `servers/` にラッパーを別途用意する手間が発生する。Blender/Unity はその手間がほぼ不要な点で優位。

### 今後のMCPの方向性

`search_tools` + `exec_code` という2ツール構成は、MCPサーバー設計の一つの理想形として今後普及していく可能性がある。既存のBlender MCPが多ツール方式で実装されているなら、このパターンへの移行を検討する価値がある。
