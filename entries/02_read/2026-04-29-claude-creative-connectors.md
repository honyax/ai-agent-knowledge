---
date: 2026-04-29
status: read
relevance: S
tags: [anthropic, claude, connector, blender, autodesk, sketchup, adobe, ableton, mcp, 3d, creative]
source_urls:
  - https://www.anthropic.com/news/claude-for-creative-work
  - https://9to5mac.com/2026/04/28/anthropic-releases-9-new-claude-connectors-for-creative-tools-including-blender-and-adobe/
  - https://www.cgchannel.com/2026/04/ai-developer-anthropic-becomes-blenders-latest-corporate-patron/
experiment_dir: null
---

# Anthropic、クリエイティブツール向け Claude コネクター9種を「公式統合」 — Blender・Adobe・Ableton 等

## 3行要約

- 2026-04-28、Anthropic が Claude.ai に「クリエイティブツール向けコネクター」9 種を公式統合として掲載。Blender・Adobe Creative Cloud・Ableton Live/Push・Autodesk Fusion・SketchUp・Affinity・Splice・Resolume Arena/Wire
- 「Anthropic がリリース」という見出しが多いが、実態は **Anthropic 自社開発ではなく、各ベンダーが作った統合を Claude 公式パートナー一覧に掲載した**形。Blender コネクターは Blender Lab（Blender Foundation 公式）が作成した MCP サーバー、Anthropic は Blender Development Fund のパトロンとして参加
- 9 種の実装方式は混在しており、ソース上で **MCP ベースが確認できるのは Blender と Autodesk Fusion のみ**。他ツールは公開情報からは API ベースか MCP ベースか判別不能

## 自分への関連度: S

関心領域 4（Unity × AI 連携）の周辺、特に既存の Blender MCP 連携経験（プロシージャル 3D 制作）に直結。サードパーティの Blender MCP 実装（コミュニティ製）を使ってきた立場として、Blender Foundation 公式版 MCP サーバーは継続性とメンテ品質で乗り換え検討に値する。他 8 種は自分の制作フローには直接関係薄い（DAW・VJ・Adobe・Splice 等）。

## 詳細

### 9つのコネクターと実装方式

| # | ツール | 主な機能 | 実装方式 |
|---|--------|---------|---------|
| 1 | Ableton Live/Push | 公式ドキュメント基盤の回答（DAW・ハードウェア） | 不明 |
| 2 | Adobe Creative Cloud | Photoshop/Premiere/Express 等 50+ ツール連携 | 不明 |
| 3 | Affinity by Canva | 画像調整・レイヤー命名等の自動化 | 不明 |
| 4 | Autodesk Fusion | 会話で 3D モデルの作成・修正（要 Fusion 購読） | **MCP**（"Fusion Model Context Protocols"）+ Autodesk Assistant の併存 |
| 5 | Blender | Python API への自然言語インターフェース | **MCP**（公式に明記） |
| 6 | Resolume Arena/Wire | VJ 向けリアルタイムコントロール | 不明 |
| 7 | SketchUp | 自然言語 → 3D モデリング変換 | 不明（Trimble 独自統合） |
| 8 | Splice | ロイヤリティフリーサンプル検索 | 不明 |

### Blender コネクターの詳細（最重要）

- **開発主体**: **Blender Lab**（Blender Foundation の実験プログラム）。Anthropic ではない
- **Anthropic の役割**: (1) Claude.ai の Connectors 一覧に公式統合として掲載、(2) Blender Development Fund のパトロンに参加（資金提供）
- **実装**: Model Context Protocol (MCP) ベース。"built on the Model Context Protocol (MCP), the open standard for connecting AI models to external data"
- **対応バージョン**: **Blender 5.1 以降**（自分の環境バージョン確認が必要）
- **Anthropic 専有ではない**: "can be used by other LLMs, not just Anthropic's" と明記。Claude 以外の LLM からも接続可能
- **機能**: シーン全体の分析・デバッグ、オブジェクトへのバッチ変更スクリプト、Blender の Python API 経由で UI に新ツール追加

### 既存のサードパーティ Blender MCP との違い

- 従来コミュニティ実装（個人開発の Blender MCP サーバー群）と異なり、Blender Foundation 公式
- Blender 本体と一緒にメンテされる継続性が期待できる
- Claude.ai 側のコネクター棚から有効化する正規ルートが用意される

### 「Anthropic がリリース」という表現について

メディア見出しは「Anthropic がリリース」と書いているが、実態は各ベンダーが作った統合を Claude 公式パートナー一覧に掲載した形。Anthropic 自社開発のコネクターは（少なくとも本発表からは）読み取れない。

### 利用条件

公式発表ページにはプラン・API・料金の具体記載なし。Claude.ai のコネクター一覧から有効化する形と推測される。Blender 5.1+ 等、各ツール側のバージョン制約に注意。

### 関連する過去エントリ

- [2026-03-24-unity-mcp-claude-code.md](../04_tried/2026-03-24-unity-mcp-claude-code.md) ← Unity 側の MCP 連携
- [2026-03-26-mcp-apps-extension.md](../02_read/2026-03-26-mcp-apps-extension.md) ← MCP の拡張動向
- [2026-04-08-mcp-code-execution-engineering.md](../02_read/2026-04-08-mcp-code-execution-engineering.md)

## 試すなら

1. 手元の Blender バージョンを確認（**5.1 以上が必須**）
2. Blender Lab 公式の MCP サーバーのインストール手順を Blender 公式から入手
3. Claude.ai 側のコネクター一覧で Blender コネクターを有効化（または Claude Code から MCP 設定として直接接続）
4. 既存のサードパーティ Blender MCP 実装と機能・安定性を比較
5. シーン分析・バッチスクリプト生成を簡単なシーンで試し、自分のプロシージャル制作フローに組み込めるか検証

## ソース

- [Claude for Creative Work (Anthropic 公式)](https://www.anthropic.com/news/claude-for-creative-work)
- [Anthropic releases 9 Claude connectors for creative tools (9to5Mac)](https://9to5mac.com/2026/04/28/anthropic-releases-9-new-claude-connectors-for-creative-tools-including-blender-and-adobe/)
- [Anthropic becomes Blender's latest Corporate Patron (CG Channel)](https://www.cgchannel.com/2026/04/ai-developer-anthropic-becomes-blenders-latest-corporate-patron/)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
