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

## ディレクトリ構成

```
entries/          # 日付別の情報エントリ（自動生成）
experiments/      # 実践コード・実験結果
templates/        # エントリのテンプレート
```

## エントリのステータス管理

各エントリのYAMLフロントマターで管理:
- `status: unread` - 未読（自動生成直後）
- `status: read` - 読了（要約を確認済み）
- `status: tried` - 実践済み
- `status: archived` - アーカイブ（スキップ含む）
