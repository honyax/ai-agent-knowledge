---
date: 2026-03-31
status: todo
relevance: A
tags: [unity, unity-ai, game-dev, assistant, generators, sentis, mcp]
source_urls:
  - https://unity.com/ja/features/ai
  - https://docs.unity3d.com/6000.3/Documentation/Manual/unity-ai.html
  - https://discussions.unity.com/t/unity-ai-beta-2026-is-here/1703625
experiment_dir: null
---

# Unity AI Beta 2026 — Assistant・Generators・Sentis・Gateway の全体像

## 3行要約

- Unity AI は Editor 統合の3本柱：**Assistant**（コード生成・自律タスク）、**Generators**（スプライト/テクスチャ/サウンド等の生成）、**Sentis**（端末上でMLモデルを実行）
- 2026年ベータでは Assistant のエージェント機能が強化され、複数オブジェクトの一括配置やファイルの一括リネームなどを自律実行できるように
- **Unity AI Gateway**（2026年中に提供予定）はサードパーティAIエージェント（Claude Code 等）を Unity Editor に安全に接続する公式インターフェース

## 自分への関連度: A

ゲーム開発20年の立場から直接使える機能が揃っている。特に：
- **Generators** でアセットのプロトタイプ生成を高速化できる（プレースホルダーの迅速な用意）
- **Sentis** でゲーム内AIロジック（行動選択・アニメーション制御等）を端末上で動かせる
- **Gateway** が正式公開されれば、既存の Claude Code + Unity MCP 連携（`com.unity.mcp` / `com.unity.ai.assistant` による公式MCP、実動作確認済み）をさらに堅牢・安定した形で拡張できる可能性がある

## 詳細

### 3つの主要コンポーネント

| 機能 | 概要 | 用途 |
|------|------|------|
| **Assistant** | Editor 内で動作する生成AI。コード生成・デバッグ・タスク自動化 | スクリプト作成、Inspectorの一括設定、シーン操作の自動化 |
| **Generators** | テキストプロンプトからスプライト・テクスチャ・マテリアル・アニメーション・サウンドを生成 | プロトタイプ用アセット、プレースホルダー素材の迅速な生成 |
| **Sentis** | 学習済みMLモデルを Editor または端末上のランタイムで実行 | ゲームAI行動、アニメーション制御、推薦システム |

### Unity AI Gateway（2026年予定）

- サードパーティAIエージェントを Unity Editor に**安全に接続**するための公式 API
- シーン階層・アセット情報・プラットフォームターゲット等のコンテキストを外部エージェントに提供
- Early Access Beta 申し込みが開始されている

### 対応バージョン・提供状況

- **必要バージョン**: Unity 6.0.60f1 以上、または Unity 6.3（6000.3）以上
- **現在の状態**: クローズドベータ（無料）
- **価格**: GA に近づいた段階で発表予定

### 2026 Beta の主な強化点（2026/01/13 リリース）

- **Agent モード** 追加：プロンプトに基づいてエディタ内のタスクを自律実行
- **Orchestration** 追加：複雑な指示を複数ステップに分解し、最適なツール・サブエージェントに委譲
- **3D Model Generator** 追加：テキストまたは参照画像から3Dメッシュを生成（props・キャラクター対応）
- **UI Toolkit レイアウト生成** 追加：テキスト指示から UXML・USS を含む UI レイアウトを構築
- シーン画像をビジョンモデルで解析する機能
- パフォーマンス最適化の提案機能
- コード生成の安全性向上
- 旧 2025 Beta は 2026年1月12日で終了（旧ポイントは引き継がれない）

### Assistant の3モード詳細

| モード | 概要 |
|--------|------|
| `/ask` | プロジェクトを変更せずに Unity API・エディター情報を質問 |
| `/run` | エディター内でタスクを自律実行（例：球体を30個円形に配置） |
| `/code` | Unity API 準拠の C# スクリプトを構文チェック付きで生成 |

### Generators の対応アセットタイプ

- Texture / Sprite / Material / Sound / Animation（旧来から対応）
- **3D Model**（Beta 2026 で追加）
- **UI Toolkit レイアウト**（Beta 2026 で追加）

生成アセットはプロジェクト内の `GeneratedAssets` フォルダに保存。Inspector の「Generate」ボタンで再編集可能。

### セットアップ手順

1. Unity Hub で Unity 6.3（6000.3）プロジェクトを作成
2. プロジェクトを **Unity Cloud にリンク**（作成時にクラウドアイコンが表示される）
3. エディター上部の **AI ボタン** から「Agree and install Unity AI」を選択
4. インストール完了後、AI メニューから各機能を呼び出し

### 料金

- ベータ期間中は**無料**
- GA 後は「Unity Points」購入制（有料プランに月次付与あり）

## 試すなら

1. Unity Hub で Unity 6.3（6000.3）プロジェクトを用意する
2. プロジェクトを Unity Cloud にリンクした状態でエディターを開く
3. 上部の **AI ボタン** → 「Agree and install Unity AI」でインストール
4. まず **Generators** でテクスチャやスプライトをプロンプトから生成してみる
5. **Assistant の `/run` モード** にシーン操作（オブジェクトの一括配置など）を指示して自律実行を確認
6. **3D Model Generator** で参照画像からプロトタイプメッシュを生成してみる
7. Unity AI Gateway Beta の申し込みページで早期アクセスに登録する

## ソース

- [Unity AI（公式フィーチャーページ）](https://unity.com/ja/features/ai)
- [Unity AI（マニュアル）](https://docs.unity3d.com/6000.3/Documentation/Manual/unity-ai.html)
- [Unity AI Beta 2026 is here!（Unity Discussions）](https://discussions.unity.com/t/unity-ai-beta-2026-is-here/1703625)
- [Unity AI Gateway Early Access Beta](https://create.unity.com/UnityAIGatewayBeta)
- [Unity AI Beta 2026 が提供開始（ゲームメーカーズ、2026/01/15）](https://gamemakers.jp/article/2026_01_15_128490/)
- [Unityで生成AIを活用する方法・Unity AIの使い方（CG研究所）](https://cg-kenkyujo.com/unity-seisei-ai/)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
