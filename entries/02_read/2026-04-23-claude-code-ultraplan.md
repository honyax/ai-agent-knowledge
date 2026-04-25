---
date: 2026-04-23
status: read
relevance: S
tags: [claude-code, ultraplan, workflow, cloud, planning]
source_urls:
  - https://code.claude.com/docs/en/ultraplan
  - https://the-decoder.com/claude-codes-new-ultraplan-feature-moves-task-planning-to-the-cloud/
  - https://betterstack.com/community/guides/ai/claude-code-ultraplan/
experiment_dir: null
---

# Claude Code Ultraplan — クラウドで計画し、ローカルで実行

## 3行要約

- `/ultraplan <タスク説明>` でClaude Codeのプランニングをクラウドにオフロードし、ブラウザのWebエディタで確認・コメントできる機能がリサーチプレビュー開始
- クラウドで計画を起草している間もローカルターミナルは別作業に使え、承認後はWebまたはローカルで実行可能
- Pro/MaxプランとGitHubホスト型リポジトリが必要、v2.1.91以降対応（完全機能はv2.1.101+）

## 自分への関連度: S

Ultrareview（コードレビュー）に続くクラウド協働機能。大規模タスクの計画フェーズをターミナルの制約なしにブラウザで確認・修正できるのは実務直結。複雑な機能追加やリファクタリング時に今すぐ試せる。

## 詳細

**使い方**

```bash
/ultraplan migrate the auth service from sessions to JWTs
```

プロンプト中に "ultraplan" というキーワードを含めるだけでも起動する。

**ワークフロー**

1. CLIでUltraplanコマンドを実行 → クラウドのClaude Codeがplan modeで起草開始
2. ブラウザでプランを確認（セクション別にコメント・修正依頼が可能）
3. 承認後：Webで実行してPRを作成 or ローカルターミナルに引き戻して実行

**要件**

- Claude Code v2.1.91以降（完全機能はv2.1.101以降）
- GitHubホスト型リポジトリ
- ProまたはMaxサブスクリプション

## 試すなら

1. Claude Codeを最新版にアップデート（`claude update`）
2. GitHubリポジトリで試したいタスクを選ぶ
3. `/ultraplan [タスク説明]` を実行してブラウザUIを確認
4. セクション別コメントでプランを調整し、承認後に実行
5. Ultrareview（コードレビュー）と組み合わせた計画→実装→レビューフローを評価

## ソース

- [Claude Code Ultraplan 公式ドキュメント](https://code.claude.com/docs/en/ultraplan)
- [Claude Code's new Ultraplan feature moves task planning to the cloud (The Decoder)](https://the-decoder.com/claude-codes-new-ultraplan-feature-moves-task-planning-to-the-cloud/)
- [Claude Code Ultraplan: Cloud-Based Interactive Planning (Better Stack)](https://betterstack.com/community/guides/ai/claude-code-ultraplan/)

---

## 感想・考察

公式ドキュメント（[code.claude.com/docs/en/ultraplan](https://code.claude.com/docs/en/ultraplan)、[permission-modes](https://code.claude.com/docs/en/permission-modes)）を取得し、自分の現運用と比較して評価。**結論: 現状ではUltraplanを使うメリットは薄い。**

### Ultraplanの本質

- Claude Code on the web を plan mode で起動する専用ハンドオフ機能
- 提供価値は**UIのみ**（新しいAI能力ではない）。差分は次の3点:
  - インラインコメント（プラン中のパッセージをハイライトしてコメント）
  - 絵文字リアクション（セクション単位の承認/懸念表明）
  - アウトラインサイドバー（長文プランのセクション間ジャンプ）
- ローカルCLIから起動する意義は **"teleport back to terminal"** による元セッションへのプラン注入。別ターミナル起動では再現できない会話継続性。

### 自分の現運用（通常モード + 独自フォーマットの実装計画書）との比較

**Ultraplanで失うもの**

- フォーマットの自由度: plan modeの固定フォーマットに縛られ、独自テンプレート（受け入れ条件・影響範囲・ロールバック手順等）を強制できない
- 環境の自由度: GitHubホスト型リポジトリ必須、Bedrock/Vertex/Foundry不可
- リポジトリにファイルとして残す自然さ: Ultraplan結果はクラウドセッション側

**代替手段で大体カバーできる**

- インラインコメント → GitHub PR/Issue/Discussion で `.md` をレビュー
- アウトライン → VSCodeのMarkdown Outline
- 並列ドラフト → 別ターミナルで普通に走らせる

### Plan mode自体の検証で得た知見（Ultraplanとは独立に有用）

- Plan modeは**編集禁止の権限モード**に過ぎず、ファイル自動出力はしない（`~/.claude` に出力されるという認識は誤り、もしくはUltraplanの「Cancel」オプション特有挙動）
- 計画フォーマット（Mermaidクラス図・シーケンス図・フェーズ分解等）はCLAUDE.md・カスタムスラッシュコマンド・Skillsで自由に制御可能
- リポジトリへの計画書保存は**通常モード運用の方が構造的に筋が良い**（plan mode中はファイル書き込み不可のため、計画書を保存するには一旦exitする必要がある）
- GitHubはMermaidブロックを標準レンダリングするので、`.md` に図を埋め込めばPRレビュー時にも見える

### いつ試すか

- 大規模設計タスクで**複数人レビュー**が必要なとき（セクション別インラインコメントの価値が出る）
- それ以外は現運用（通常モード + 独自フォーマット計画書 + GitHub PRレビュー）で十分以上

ステータス: read（試さず完了扱い）。同種のクラウド協働機能（Ultrareview等）の評価軸として、本記事の比較フレームを再利用できる。
