---
date: 2026-03-24
status: read
relevance: A
tags: [claude-code, settings, agents, skills, hooks, tdd, hackathon]
source_urls:
  - https://zenn.dev/ttks/articles/a54c7520f827be
experiment_dir: null
---

# everything-claude-code — ハッカソン優勝者の実践的設定集

## 3行要約

- Anthropic x Forum Venturesハッカソン優勝者が公開したClaude Code設定集。agents/skills/commands/rules/hooks/mcp-configsの6カテゴリで構成
- テスト駆動開発（RED→GREEN→REFACTOR）を中核とし、80%以上カバレッジ必須。コードレビュー・セキュリティチェック・デバッグコード排除を自動化
- コンテキストウィンドウ管理が重要: 200kから70kまで縮小する可能性があるため、MCPは10個以下を推奨

## 自分への関連度: A

サブエージェント構成（計画、設計、レビュー、セキュリティ等の専門エージェント）やスキルの設計パターンは、自分のClaude Code設定をレベルアップさせる参考になる。ただし丸ごと採用より、自分のワークフローに合わせてピックアップするのが推奨。

## 詳細

### 6カテゴリ
- **agents/**: 計画・設計・レビュー・セキュリティチェック等の専門サブエージェント
- **skills/**: 再利用可能なワークフロー定義
- **commands/**: /tdd、/plan等のスラッシュコマンド
- **rules/**: プロジェクト全体のガイドライン
- **hooks/**: イベント駆動の自動化
- **mcp-configs/**: 外部サービス連携設定

### 重要な知見
- MCPは10個以下に抑える（コンテキストウィンドウ圧迫防止）
- 全部採用せず、自分に合うものだけカスタマイズ

## 試すなら

1. GitHubリポジトリ「everything-claude-code」を確認
2. agents/のサブエージェント構成を参考に、自分用のレビューエージェントを作成
3. TDD用のスラッシュコマンド（/tdd）を自分のプロジェクトに導入

## ソース

- [everything-claude-code解説（Zenn）](https://zenn.dev/ttks/articles/a54c7520f827be)

---

## 感想・考察

- **commands / rules**: 現在は skills に統合されていく流れなので、このリポジトリの commands・rules カテゴリをそのまま採用するより、skills として設計し直す方が自分の運用に合う
- **agents**: サブエージェント構成（レビュー・セキュリティ等）は参考になりそうで、別途試す予定
- **hooks**: 確認ダイアログの表示や、処理完了時の通知など、UX改善に使いたい。イベント駆動の自動化という切り口で自分のワークフローに組み込む余地がある
