---
date: 2026-05-02
status: read
relevance: A
tags: [harness-engineering, claude-code, agent-sdk, claude-md, skills, hooks, mcp, orchestration, presentation]
source_urls:
  - https://speakerdeck.com/rkaga/how-to-approach-harness-engineering
  - https://zenn.dev/r_kaga/articles/329afdc151899f
  - https://restato.github.io/blog/harness-engineering-guide-claude-code/
experiment_dir: null
---

# ハーネスエンジニアリングにどう向き合うか — User-Side / Agent-Side の二層整理（rkaga 発表）

## 3行要約

- rkaga 氏が「ハーネスエンジニアリング」を **Agent-Side（プラットフォーム提供者が作る基盤層: Claude Code 本体・Codex 本体）** と **User-Side（CLAUDE.md / Skills / Hooks / MCP / 検証ループ等、ユーザが組む層）** の二層に整理。Anthropic・OpenAI・LangChain で定義が揺れている現状への交通整理
- User-Side で多くのチームが「CLAUDE.md と Skills を書いて終わり」と止まりがちなのに対し、見落とされやすいのは **(1) lint/型/テストを Hook 経由でエージェントループに組み込む計算的センサー、(2) 別エージェント・別セッションによる検証ループ、(3) ハーネスが本当に成果を改善しているかの計測** の3つ
- 関連発表でも、Y Combinator CEO の「Thin Harness, Fat Skills」（オーケストレーションは ~200 行で薄く、知性は Skills と決定論的ツールに置く）や、Claude Agent SDK でパイプラインを書きながら決定論的ステップ（lint・branch push）とエージェント自由度（実装・CI fix）を交互に置くハイブリッド設計が紹介されている

## 自分への関連度: A

すでに自分は User-Side ハーネス（CLAUDE.md・カスタム Skills・catch-up/digest/try フロー）を構築済みで、4/23 の概念エントリとも接続する。ただし「計測系」と「別エージェントによる検証ループ」は未整備で、自分のナレッジベース運用にも応用できる余地がある。Speakerdeck 本体は今回 fetch 不能だったため、同著者の Zenn 記事と関連解説記事から再構成した内容なので、後日スライド本体を読み直す必要あり。

## 詳細

### 用語が揺れている問題

「ハーネスエンジニアリング」は提唱者ごとに定義が異なる：

- **Anthropic** は内部のオーケストレーションループや検証システムを指す文脈で使用
- **OpenAI**（Codex 5ヶ月実験の文脈）は「権限・検証・承認・監査・エスカレーション経路」を含む組織的な働かせ方として整理（[2026-04-23 ハーネスエンジニアリング概念エントリ](../02_read/2026-04-23-harness-engineering-concept.md) 参照）
- **LangChain など** は別ニュアンスで使う

rkaga の整理は、この混乱に **「誰がそのハーネスを設計しているか」** で線を引く。

### Agent-Side ハーネス（Builder Harness）

プラットフォーム提供者（Anthropic / OpenAI）が作る基盤：

- オーケストレーションループ（プロンプト→ツール呼び出し→結果評価→次プロンプト の制御）
- ツールルーティング（Tool Search・Tool Permission・Subagent ルーティング）
- メモリ管理（コンテキスト圧縮・Auto-compact）
- 検証システム（モデル側の安全層）

Claude Code や Codex を使う時点でこの層は既に組み込まれており、ユーザーが直接書くものではない。

### User-Side ハーネス

ユーザー（あるいはチーム）が設計する層。Claude Code 文脈では以下が中核：

- **CLAUDE.md / AGENTS.md**: ルールファイル
- **Skills**: 標準化された手順
- **Hooks**: 決定論的な振る舞いの強制（PreToolUse / PostToolUse / Stop 等）
- **MCP**: 外部ツール統合
- **テスト・Lint**: 計算的な検証

### 多くのチームが見落とすもの

CLAUDE.md と Skills を書いて満足しがちだが、以下が抜けやすい：

1. **計算的センサー** — Lint・型チェック・テスト結果をエージェントループに直接フィードバックする組み込み（Hook 経由が現実解）
2. **検証ループ** — エージェント自身に「自分の出力が妥当か」を判断させると緩い基準になりがちなので、別エージェント／別セッションでレビューする
3. **計測系** — そのハーネス構成が実際にエラー率を下げ、サイクルを速めているかをトラッキングする仕組み

### 「Thin Harness, Fat Skills」哲学

Y Combinator CEO が推している設計思想として紹介されている：

- オーケストレーション側のロジックは ~200 行程度に抑える（薄い harness）
- 知性は Skills と決定論的ツール側に置く（fat skills）
- harness 自体が複雑化すると、エージェントが内部状態に左右される度合いが増えて再現性が落ちる

これは Anthropic の「Skills を中心に置く」方針とも整合する。

### ハイブリッドオーケストレーション

rkaga の発表では Claude Agent SDK でパイプラインコードを書きつつ、

- **決定論的ステップ**（lint 通す・branch push・テスト走らせる・CI ステータス確認）
- **エージェント自由度の高いステップ**（実装・CI fix・リファクタリング判断）

を交互に配置する設計が紹介されている。重要なのは「全部エージェントに任せる」でも「全部スクリプトで縛る」でもなく、**決定論で固められる部分は固め、判断が要る部分だけ自由度を残す** こと。

### 関連発表・記事との位置づけ

- 同著者の別発表 [テストから始める Agentic Coding](https://speakerdeck.com/rkaga/agentic-coding-starts-with-testing) — テストを harness の出発点に置く設計論
- [Restato 解説記事](https://restato.github.io/blog/harness-engineering-guide-claude-code/) は User-Side ハーネスを「CLAUDE.md / 設定とパーミッション / フック / スキル / カスタムコマンド」の5本柱で整理

### 既存エントリとの接続

- [2026-04-23 ハーネスエンジニアリング概念](../02_read/2026-04-23-harness-engineering-concept.md) — OpenAI 視点の組織的・運用的ハーネス
- [2026-04-23 Claude Code Level 5 育て方](../02_read/2026-04-23-claude-code-level5-development.md) — User-Side ハーネスを段階的に育てるロードマップ
- [2026-04-19 CLAUDE.md 肥大化対策（モジュール化）](../02_read/2026-04-19-claude-md-bloat-modular.md) — User-Side の中核要素の運用ノウハウ
- [2026-04-08 MCP コード実行エンジニアリング](../02_read/2026-04-08-mcp-code-execution-engineering.md) — User-Side ハーネスとしての MCP 設計

「概念（4/23）」「実装（Anthropic Planner+Generator+Evaluator）」「段階（Level 5）」に加え、本エントリで「**層の切り分け（誰が作るのか）**」が揃い、ハーネスエンジニアリング理解の四点セットが完成する。

## 試すなら

1. 自分の現在の `.claude/` 構成を「User-Side ハーネス」として棚卸しし、CLAUDE.md / Skills / Hooks / MCP の各層に何が入っているか可視化する
2. 「計算的センサー」が抜けていないか確認 — PostToolUse Hook で Lint / 型チェック / テストの失敗が必ずエージェントに返る経路を1本作る
3. 「検証ループ」を1個導入 — 自作 Skill 実行後に **別の Subagent でレビュー** する Hook、もしくは ultrareview 連携を試す
4. 「Thin Harness, Fat Skills」原則で、自作 Skill のうち手順が太いもの・分岐が多いものを Skill 側に押し出し、上位フローを薄く保てるか試す
5. Speakerdeck 本体（接続不可だったが後で再アクセス可能になり次第）を実際に閲覧し、本エントリの再構成と齟齬がないか確認する

## ソース

- [ハーネスエンジニアリングにどう向き合うか / How to approach harness engineering (rkaga, Speaker Deck)](https://speakerdeck.com/rkaga/how-to-approach-harness-engineering)
- [What Is and Isn't Harness Engineering: Agent-Side vs. User-Side Harnesses (rkaga, Zenn)](https://zenn.dev/r_kaga/articles/329afdc151899f)
- [Harness Engineering: The Complete Guide to Configuring Claude Code (Restato)](https://restato.github.io/blog/harness-engineering-guide-claude-code/)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
