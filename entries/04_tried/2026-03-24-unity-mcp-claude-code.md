---
date: 2026-03-24
status: tried
relevance: S
tags: [unity, mcp, claude-code, game-dev, tps, lighting, level-design]
source_urls:
  - https://dev.classmethod.jp/articles/unity-mcp-tps-game-claude-code-modification/
experiment_dir: null
---

# Unity MCP + Claude CodeでTPSゲームをAI改造 — 実用性と限界の検証

## 3行要約

- Unity公式MCPパッケージとClaude Code（Opus 4.6）を組み合わせ、ステージレイアウト生成・ライティング調整・カスタムMCPツール作成の3タスクを検証
- ステージ生成は12オブジェクト配置できたがマテリアル不統一・内部未検査等の問題あり。ライティング調整はCamera Captureと実際の表示の乖離が課題
- 結論: AI単独完結ではなく「人間がレビューしながらAIに作業させる」ワークフローが現実的

## 自分への関連度: S

Unity + MCP + Claude Codeの直接的な実践例。自分のカードバトルゲーム開発にも応用可能な知見（AIにレベルデザインやライティングを任せる際の注意点）。SceneQualityCheckerのようなカスタムMCPツールのアイデアは参考になる。

## 詳細

### 検証環境
- Unity 6000.3.10f1、Claude Opus 4.6、Windows 11

### 3つの検証
1. **ステージレイアウト生成**: 壁・床・天井等12オブジェクト配置。問題: マテリアル不統一、ドアウェイ接続失敗、室内照明なし、スケール不一致
2. **ライティング・ポストプロセス**: 減衰値、アンビエントカラー、フォグ、Bloom等。問題: Camera Captureが真っ黒でも実際の画面では異なる
3. **カスタムMCPツール**: SceneQualityCheckerでコリジョン漏れ・デフォルトマテリアル・ライティング異常を自動検出

### AIの限界
- 視覚的整合性の判断が困難（「夜間モード」等の感覚依存判断）
- 外部俯瞰視点のみで確認し、部屋内部をカメラで検査しない
- チェック項目がハードコードで柔軟な運用が困難

## 試すなら

1. Unity Package ManagerからMCPパッケージをインストール
2. Claude Codeの設定にUnity MCPサーバーを登録
3. Unity Editorで接続を承認
4. 簡単なタスク（オブジェクト配置、マテリアル変更等）で動作確認
5. カスタムMCPツール（品質チェッカー等）を作成して検証

## ソース

- [Unity MCP で TPS ゲームを Claude Code に改造してみた（DevelopersIO）](https://dev.classmethod.jp/articles/unity-mcp-tps-game-claude-code-modification/)

---

## 感想・考察

実験ファイル: [experiments/2026-03-24-unity-mcp-claude-code/](../experiments/2026-03-24-unity-mcp-claude-code/)

**良かった点**
- セットアップからオブジェクト配置まで一通り実際に動作確認できた。`claude mcp add --scope user` でグローバル登録、Unity Editor で Allow するだけで接続完了と、思ったよりハードルが低い。
- シーン内 GameObject の名前・位置取得、Cube の配置指示（座標指定）はどちらも正確に動作した。
- 「AIが配置 → 人間がレビュー → AIが修正」というループは、自分のゲーム開発（カードUIレイアウトやステージ配置）にそのまま使えるワークフロー。

**微妙だった点・制限**
- Unity Editor が起動しているたびに接続承認（Allow）が必要。セッションごとに Claude Code の再起動も必要。
- Signature Valid: No と表示されるが動作上の問題はなし（Entrust CA 署名の既知の挙動）。
- Camera Capture と実際の画面表示の乖離は現時点では根本的な制限。ライティング調整をAIに任せる際は目視確認が必須。

**自分のワークフローへの適用**
- カードバトルゲームの開発で、盤面オブジェクトのプロトタイプ配置に Unity MCP + Claude Code を使える見通しが立った。
- SceneQualityChecker に「カード配置の重複チェック」「グリッドスナップ確認」などゲーム固有のルールを追加すると実用的になる。

**次のアクション**
- SceneQualityChecker を実プロジェクトの `Assets/Editor/MCP/` に配置してビルドエラーがないか確認する。
- カードバトルゲームのプロジェクトで Unity MCP を試し、実際の開発タスクに使えるか検証する。

**総評・雑感**
- Unity 公式の MCP サーバーがようやく出てくれて助かる。今回は簡単な動作検証だけだが、今後必要に応じて活用していきたい。
- カスタムツールの追加はエディタスクリプトに `[McpTool]` Attribute を付けるだけで、コンパイル完了時にそのまま登録されていた。手間がかかると思っていたが想像以上に楽。
- デフォルトで用意されているツールはそれぞれ詳細はよくわからないが、それなりの数がある。Profiler 関連も多く含まれており、実際の開発現場で活用できる可能性は十分ある。
- MCP のリレーサーバーが `~/.unity/relay/relay_win.exe` というバイナリ形式なのは少し気になるが、手動で触ることはないだろうしまぁ良いか。

