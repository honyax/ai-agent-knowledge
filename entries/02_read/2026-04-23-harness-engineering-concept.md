---
date: 2026-04-23
status: read
relevance: A
tags: [harness-engineering, openai, codex, context-engineering, prompt-engineering, agent, workflow]
source_urls:
  - https://openai.com/ja-JP/index/harness-engineering/
  - https://aitc.dentsusoken.com/column/harness-engineering/
  - https://www.infoq.com/news/2026/02/openai-harness-engineering-codex/
experiment_dir: null
---

# ハーネスエンジニアリング — AIエージェントが継続的に成果を出す「働き方の仕組み」を設計する

## 3行要約

- OpenAIが提唱する「ハーネスエンジニアリング」は、AIエージェントに職場（権限・検証・承認・監査・エスカレーション経路）を設計する新しいエンジニアリング規律
- OpenAI社内では5ヶ月でベータ製品約100万行を人間ゼロ行で構築する実験を実施（2025年8月開始）。エンジニアの主な仕事は「コードを書く」から「エージェントが働ける環境を作る」に変わった
- 電通総研のコラムは「プロンプト（頼み方）→ コンテキスト（見せ方）→ ハーネス（働かせ方）」の三層モデルで整理、企業でPoC止まりを防ぐ鍵と位置づけている

## 自分への関連度: A

すでに自分はCLAUDE.md・Skills・Hooks・Agentsでエージェント環境を設計している。その実践が「ハーネスエンジニアリング」という名前で業界用語化されつつあり、Level 5育成（同日エントリ）と同じ方向性。関連エントリ（2026-03-27 Anthropicのハーネス設計）と合わせて自分の活動を体系化できる。

## 詳細

### ハーネスの語源とメタファー

「ハーネス（馬具）」は手綱・鞍・轡からなる装具一式。パワフルだが予測不能な馬（AIモデル）を正しい方向に導くための装備。モデル自体は改良できないが、その周囲の装具は設計できる。

### 三層モデル（電通総研の整理）

| レベル | 別名 | 何を設計するか |
|--------|------|----------------|
| プロンプトエンジニアリング | 頼み方 | 指示文の工夫 |
| コンテキストエンジニアリング | 見せ方 | 社内規則・手順書・判断材料 |
| **ハーネスエンジニアリング** | **働かせ方** | **権限設定・検証手順・承認フロー・監査・エスカレーション経路** |

企業導入ではモデル選定よりも運用設計（ハーネス層）が成否を分ける。PoC止まりを防ぐ鍵。

### OpenAIの実践（Codexでの5ヶ月実験）

- 2025年8月末に空のリポジトリから開始
- 数週間でベータ製品をリリース、最終的に約100万行
- **人間が書いたソースコードは0行**（アプリロジック・テスト・CI設定・ドキュメント・観測性・内部ツールすべてCodexが生成）
- 推定で手書きの約1/10の時間
- 主要な学び：
  - **コンテキストは希少リソース**: 1000ページのマニュアルではなく「地図」を与える
  - **Depth-first分解**: 大目標を設計・コード・レビュー・テスト等の小ブロックに分割
  - **アーキテクチャ制約が早期に必要**: 通常は数百人規模で導入する制約を、エージェント時代は初期から入れないと速度が落ちる

### 既存知識との接続

- 同リポジトリの [2026-03-27 Anthropicハーネス設計エントリ](../02_read/2026-03-27-harness-design-long-running-apps.md) は「Planner+Generator+Evaluator」のマルチエージェント実装パターン
- 同日生成の [Level 5育て方エントリ](./2026-04-23-claude-code-level5-development.md) は個人開発でのハーネス構築段階論
- この3つが「概念・実装・段階」として三位一体の知識体系になる

## 試すなら

1. 自分の現在のClaude Code環境を「プロンプト・コンテキスト・ハーネス」の三層で棚卸しする
2. ハーネス層で不足しているもの（自動検証・承認フロー・エスカレーション）を特定
3. Hooksで自動検証を追加、Agentsでレビュー自動化を加える
4. OpenAIの「depth-first分解」の思考法で、大きなゲーム開発タスクを小ブロックに分解する練習をする
5. 電通総研のコラムを読み、企業導入の観点も把握する

## ソース

- [ハーネスエンジニアリング: エージェント主導の世界でCodexを活用する (OpenAI 日本語版)](https://openai.com/ja-JP/index/harness-engineering/)
- [ハーネスエンジニアリングとは (電通総研 AITC)](https://aitc.dentsusoken.com/column/harness-engineering/)
- [OpenAI Introduces Harness Engineering (InfoQ)](https://www.infoq.com/news/2026/02/openai-harness-engineering-codex/)

---

## 感想・考察

### アーキテクチャ制約 = ハーネスエンジニアリングの中核

OpenAI の Codex 5ヶ月実験で語られた「アーキテクチャ制約を初期から入れる」は、ハーネスエンジニアリング全体で見ても特に重要なポイント。理由は単純で、**エージェントは人間の何倍ものスループットでコードを生成するため、制約のないコードベースに大量生成すると依存が双方向に絡み合った「泥団子」が一気に育ち、後から境界を引き直すコストがエージェントの生産速度を上回る** から。人間チームでは数百人規模になってから入れるような制約を、エージェント時代は初期から入れる必要がある。

OpenAI が実験で採用していた制約の骨格は `Types → Config → Repo → Service → Runtime → UI` の一方向依存。逆向きや層飛ばしを禁止し、Codex はこの檻の中でしか動けない設計になっていた。

### 「機械可読な境界」の正体は structural tests

Martin Fowler の整理（context engineering / architectural constraints / garbage collection）の2番目「アーキテクチャ制約」は、抽象論ではなく具体的には **structural tests（依存グラフの静的検査をテストとして書く手法）** に落ちる。原理は単純で、ソースを AST レベルでパースして import / using の依存グラフを作り、「この方向の矢印は禁止」というルールにアサーションを書くだけ。

各言語に専用ライブラリがある：

- C# / Unity: NetArchTest, ArchUnitNET
- TypeScript: dependency-cruiser, eslint-plugin-boundaries
- Java: ArchUnit（元祖）
- Python: import-linter

依存方向だけでなく、命名規則・公開範囲・循環依存・禁止 API（例：ゲームロジック層では `UnityEngine.Debug.Log` 禁止）・シリアライズ境界なども機械検証できる。**「コードを書いたら自動で守られる手すり」を CI で回す** のがハーネスの実体。

### 自分の運用フロー案

要件定義段階で以下の流れを試したい：

1. 要件 → エージェントに「アーキテクチャ制約案」を複数生成させる
2. **別エージェント（または別セッション）にレビューさせる** — 自身に提案させると「満たしやすい緩い制約」を出しがちなので、批評役を分ける
3. 制約を機械検証可能な形（NetArchTest / dependency-cruiser のルール）まで落とす
4. CI または Claude Code の Hooks（PostToolUse）で違反を検出

特に Hooks に組み込めば、セッション内で違反コードが書かれた瞬間にフィードバックが返るので、生成サイクルが短くなる。

### ゲーム開発でのアーキテクチャ制約候補

- データ層 / ロジック層 / 表示層の分離
- セーブデータ DTO は外部依存を持たない（バージョン互換性のため）
- ホットパス（Update / FixedUpdate 内）と非ホットパスの分離
- ゲームロジック層から `UnityEngine.Debug.Log` を直接呼ばず、独自 Logger を経由
- ScriptableObject / MonoBehaviour 境界の明確化

### 三位一体での理解

- **概念**: 本エントリ（ハーネスエンジニアリング）
- **実装**: 2026-03-27 Anthropic ハーネス設計（Planner+Generator+Evaluator）
- **段階**: 同日生成の Level 5 育て方エントリ

この中で structural tests は「概念」と「実装」をつなぐ具体ピース。Level 5（自律実行）に到達するためには、エージェントが脱線しても自動で軌道修正される手すりが必要で、それが structural tests。
