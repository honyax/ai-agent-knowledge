---
date: 2026-03-27
status: unread
relevance: A
tags: [claude-agent-sdk, multi-agent, harness-design, long-running, evaluator, playwright-mcp]
source_urls:
  - https://www.anthropic.com/engineering/harness-design-long-running-apps
experiment_dir: null
---

# ハーネス設計パターン — 長時間アプリ開発のためのマルチエージェント構造（Anthropic Engineering）

## 3行要約

- GANにインスパイアされた「生成器＋評価器」のマルチエージェント構造で、人間の介入なしに完全なアプリケーションを構築するハーネス設計。Claude Agent SDKで実装
- 長時間タスクの2大課題を解決: (1) コンテキスト満杯時の「文脈不安」→文脈リセット（新エージェント起動＋構造化引き継ぎ）、(2) 自己評価の甘さ→独立した評価エージェントの分離
- Planner→Generator→Evaluatorの3エージェント構成で、Playwright MCPによるUI自動テストとスプリント方式の反復開発を実現。実行時間3〜6時間、コスト$100〜200

## 自分への関連度: A

Claude Agent SDKを使った実践的なマルチエージェント設計のリファレンス。特に「文脈リセット」パターンと「評価器の分離」は、自分のゲーム開発でClaude Codeを長時間使う際の知見として直接活用できる。Playwright MCPによる自動評価も応用の可能性あり。

## 詳細

### 核心的な設計原則

**文脈不安（Context Anxiety）**: コンテキストウィンドウが満杯に近づくと、モデルは仕事を早期終了させようとする。解決策は完全に新しいエージェントを起動し、構造化されたアーティファクトで状態を引き継ぐ「文脈リセット」。

**自己評価の甘さ**: モデルが自分の成果を評価すると、客観的品質が低くても確信を持って褒める。生成と評価を別エージェントに分離し、評価器を「懐疑的」に調整する方が扱いやすい。

### 3エージェント・アーキテクチャ

| エージェント | 役割 | 詳細 |
|------------|------|------|
| **Planner** | 仕様策定 | 1〜4文のプロンプトを完全な製品仕様に展開。AI機能の統合機会を発見 |
| **Generator** | 実装 | React/Vite/FastAPI/SQLiteスタック。スプリント方式で1機能ずつ。Git版管理 |
| **Evaluator** | 品質評価 | Playwright MCPでUI操作テスト。APIエンドポイント・DB状態も検証。「完了」の定義を事前に交渉 |

### 評価の4基準（フロントエンド）
1. **Design Quality**: 色・タイポグラフィ・レイアウトの一貫性
2. **Originality**: テンプレート既定値ではないカスタム判断の証拠
3. **Craft**: タイポグラフィ階層、間隔、色調和、コントラスト比
4. **Functionality**: ユーザビリティとアクションの発見しやすさ

### コスト比較

| 手法 | 実行時間 | コスト | 品質 |
|------|---------|--------|------|
| 単一エージェント | 20分 | $9 | コア機能が動作せず |
| フルハーネス | 6時間 | $200 | 物理演算含む動作可能なゲーム |

### Opus 4.6での簡素化

モデル改善に伴いハーネスを段階的に単純化:
- スプリント構造を削除（計画能力の向上により不要）
- 評価器を単一パスの最終評価に変更
- DAW生成: 3時間50分、$124.70で機能性を達成

> "最もシンプルな解決策を見つけ、必要な場合にのみ複雑さを増す"

## 試すなら

1. Claude Agent SDKで最小限のGenerator+Evaluator構成を作成
2. Playwright MCPを評価エージェントに接続してUI自動テストを実装
3. 文脈リセットパターンを試す: 構造化JSONで状態を引き継ぐ新エージェント起動
4. 自分のゲームプロジェクトで小規模な機能（UIコンポーネント等）をハーネスで生成してみる

## ソース

- [Harness Design for Long-Running Application Development（Anthropic Engineering）](https://www.anthropic.com/engineering/harness-design-long-running-apps)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
