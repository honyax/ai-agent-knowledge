# AI Agent Knowledge Base - CLAUDE.md

## このリポジトリについて

AIコーディングエージェント・AI開発ツール関連の情報をキャッチアップし、実践・考察を蓄積するナレッジベース。
現在のメイン対象はClaude（Claude Code, Claude API, Claude.ai）。今後、他のエージェント（Cursor, GitHub Copilot, OpenAI Codex等）にも拡張予定。

## 自分のコンテキスト（情報フィルタリング用）

### スキルセット
- ゲーム開発20年（Unity, C#, TypeScript）
- AI開発ツール: Claude Code, GitHub Copilot を日常的に使用
- Blender MCP連携でのプロシージャル3D制作経験あり
- Web開発: React/TypeScript（学習中）

### 関心領域（優先度順）
1. Claude Code の新機能・ワークフロー改善（直接業務に影響）
2. Claude Code の実践的な設定・運用ノウハウ（CLAUDE.md設計、Hooks/Skills/MCP構成、並列実行、自律開発手法など、コミュニティの知見を含む）
3. AI開発ツールのセキュリティリスクと対策（サプライチェーン攻撃、コードレビューの注意点等）
4. Unity × AI連携（Unity MCP、AIによるレベルデザイン・アセット生成、ゲーム開発ワークフローへのAI統合）
5. Claude API の変更・新モデル（ゲーム内AI統合の可能性）
6. MCP関連のアップデート（Blender連携、ツール連携）
7. Claude.ai の新機能（Cowork、日常の情報収集・分析に使用）
8. Anthropicの方針・ビジョン（長期的な技術判断に影響）
9. 他のAIコーディングエージェント（Cursor, Copilot, Codex等の動向比較）

### 情報の評価基準
- **即実践**: 今の開発ワークフローにすぐ取り入れられるもの
- **要検証**: 面白そうだが自分の環境で試す必要があるもの
- **知識として**: 直接は使わないが知っておくべきもの
- **スキップ**: 自分の領域に関係が薄いもの

## Git / GitHub ルール

- コミットメッセージ・PRのタイトル・本文はすべて日本語で記載する

## シェルツールの優先順位

- Bash tool を優先して使うこと。既存の許可リスト（permissions allow rules）が Bash 前提で構築されているため、PowerShell tool を使うと許可プロンプトが頻発する
- PowerShell tool は以下の場合のみ使用可:
  - Bash で実現できない PowerShell 固有の操作（Windows レジストリ、PSDrive、Get-* 系 cmdlet 等）
  - ユーザーが明示的に PowerShell を指示した場合
- `git`/`npm`/`node` 等の標準 CLI は Bash tool 経由で実行する

## ディレクトリ構成

```
entries/
  01_unread/    # 未読（自動生成直後）
  02_read/      # 読了・完了（読むだけでOK）
  03_todo/      # 読了・実践待ち（試したいもの）
  04_tried/     # 実践済み
  05_archived/  # アーカイブ（スキップ含む）
experiments/    # 実践コード・実験結果
templates/      # エントリのテンプレート
```

## エントリのステータス管理

ステータスはフォルダで管理。ステータス変更時はファイルを該当フォルダへ移動する。
各エントリのYAMLフロントマター `status` もフォルダと一致させること。

フロー:
```
01_unread → 02_read     （読むだけでOK、完了）
                 └─→ 03_todo  （実践したい）→ 04_tried
```
