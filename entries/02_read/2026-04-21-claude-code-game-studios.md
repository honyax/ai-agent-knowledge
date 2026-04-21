---
date: 2026-04-21
status: read
relevance: A
tags: [claude-code, game-development, multi-agent, unity, open-source, workflow]
source_urls:
  - https://github.com/Donchitos/Claude-Code-Game-Studios
  - https://aitoolly.com/ai-news/article/2026-04-19-claude-code-game-studios-transforming-ai-into-a-full-scale-development-environment-with-49-specializ
  - https://qiita.com/emi_ndk/items/e4c1fbad2bf2f73c5091
experiment_dir: null
---

# Claude Code Game Studios: 49体のAIエージェントでゲームスタジオを構築するOSSテンプレート

## 3行要約

- Claude Code を本物のゲームスタジオに見立て、ディレクター・部門リード・スペシャリストの3階層に分かれた49体の専門AIエージェントと72のワークフロースキルを提供するOSSテンプレート（MIT）。
- エージェントはゲーム開発の全工程（企画・プログラミング・アート・オーディオ・QA・プロダクション）をカバーし、スタジオの組織構造と意思決定フローを模している。
- 人間は方向性の決定と確認だけを行い、エージェントチームが設計・レビュー・コミット品質チェックを自律的に実施する「ワンオペAAAスタジオ」を目指した設計。

## 自分への関連度: A

ゲーム開発20年のバックグラウンドと Claude Code 日常使用の両方が刺さる。Unity プロジェクトにそのまま適用できるかは要検証だが、CLAUDE.md 設計・Skills 構成・エージェント階層の実例として参考価値が高い。現在の開発ワークフローを大きく変える可能性あり。

## 詳細

**プロジェクト概要:**
- 開発者: Donchitos（GitHub）
- ライセンス: MIT（無償・OSSとして公開）
- 公開日: 2026-04-18

**エージェント構成（49体）:**
- Studio Director（ビジョン管理）
- 部門リード: Game Design Lead, Engineering Lead, Art Lead, Audio Lead, QA Lead, Production Lead
- スペシャリスト: レベルデザイナー、シェーダーエンジニア、AIプログラマー、サウンドデザイナー等

**72ワークフロースキル例:**
- ゲームデザインドキュメント（GDD）テンプレート生成
- システム設計レビュー
- コミット前品質チェック（アセット・コード・ドキュメント）
- プロダクションドキュメント自動生成

**設計哲学:**
- 「あなたはすべての決定を下す。しかし今や、正しい質問をし、早期にミスを捕まえ、ブレインストーミングから発売まで整理し続けるチームがいる」

## 試すなら

1. `git clone https://github.com/Donchitos/Claude-Code-Game-Studios` でテンプレートを取得
2. CLAUDE.md とエージェント定義（`/skills`）の構造を確認
3. 小規模ゲームプロジェクト（Unity）のディレクトリにテンプレートを適用してみる
4. ゲームデザインドキュメント生成スキルから試す
5. エージェント階層の設計方法を参考に自前スタジオ構成をカスタマイズ

## ソース

- [GitHub - Donchitos/Claude-Code-Game-Studios](https://github.com/Donchitos/Claude-Code-Game-Studios)
- [Claude Code Game Studios: 49 AI Agents for Game Dev | AIToolly](https://aitoolly.com/ai-news/article/2026-04-19-claude-code-game-studios-transforming-ai-into-a-full-scale-development-environment-with-49-specializ)
- [【衝撃】49体のAIが同時にゲームを作る時代が来た！ - Qiita](https://qiita.com/emi_ndk/items/e4c1fbad2bf2f73c5091)

---

## 感想・考察

**49体という数字について：**
ゲーム開発の役割を細かく分割した結果その数になっただけで、特に革新的な点はない。エージェント数が多いこと自体はセールスポイントにはならない。

**アート制作について：**
technical-artist エージェントは「最終アート資産の作成はしない」と明示されており、シェーダー開発・アセットパイプライン構築・技術検証が役割。3Dモデリングやテクスチャ制作は完全に人間側の作業として前提されている。アート制作が考慮に入っていない時点で、アート工程を重視するプロジェクトではほぼ意味がない。

**レベルデザインについて：**
level-designer エージェントができるのはレイアウト設計（ASCII・文字描写形式が想定）、敵配置、難易度曲線、導線設計など「設計書・仕様文書を作るところまで」。Elden Ring のような複雑な3D地形・ダンジョン構造を実際に組む作業は対象外。

**総括：**
このシステムが得意なのは「コード・設計文書・仕様書の生成」であり、「企画〜プログラミング」工程には強いが「アート〜レベル実装」工程はほぼノータッチ。エントリの関連度は A としたが、実態は B 程度。CLAUDE.md やエージェント階層の設計例として参考にする程度にとどめる。

「AAAスタジオに勝てる」「一人で AAA ゲームを作れる」という訴求は完全な誇大広告。アート・サウンド・レベル実装など制作工程の大部分は人間がやる前提であり、このシステムが自動化できるのはコードと仕様書の一部にすぎない。
