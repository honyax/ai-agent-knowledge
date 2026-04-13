---
date: 2026-04-09
status: read
relevance: B
tags: [cowork, claude-desktop, enterprise, mcp, zoom, opentelemetry]
source_urls:
  - https://thenewstack.io/anthropic-takes-claude-cowork-out-of-preview-and-straight-into-the-enterprise/
  - https://www.eweek.com/news/claude-cowork-general-availability-enterprise-controls/
  - https://www.techradar.com/pro/claude-cowork-is-now-available-for-enterprise-use-adds-analytics-access-controls-and-more
experiment_dir: null
---

# Claude Cowork GA — 全有料プラン提供開始・Zoom MCP・エンタープライズ機能

## 3行要約

- Claude Cowork がプレビューを終え、Pro/Team/Enterprise の全有料プランで GA（一般提供）開始（2026-04-09）
- **Zoom MCP コネクタ**を新設: ミーティング要約・アクションアイテム・トランスクリプトをCowork に取り込み可能
- エンタープライズ向けに役割ベースアクセス制御（RBAC）・グループ支出上限・OpenTelemetry 対応・管理ダッシュボード分析機能を追加

## 自分への関連度: B

会社では API 課金のため、エンタープライズ機能（RBAC・支出上限等）は関係なし。Zoom MCP コネクタは業務で Zoom を使っているため場合によっては使える可能性がある。詳細仕様（リアルタイムか事後処理か、必要な Zoom プラン等）は未確認。以前の Cowork 試用済みエントリ（`entries/04_tried/2026-03-24-cowork.md`）の続報として。

## 詳細

### 全有料プランへの拡大

- macOS・Windows の Claude Desktop アプリ上で利用可能
- Pro / Team / Enterprise 全プランで利用開始（Maxプランも含む）

### Zoom MCP コネクタ（注目）

- Zoom ミーティングのトランスクリプトを取り込む
- 会議後に自動でアクションアイテム・要約を Cowork セッションに反映
- 既存の Zoom integration と同様の OAuth 接続フロー
- 詳細仕様（リアルタイム処理か事後処理か等）は未確認

### エンタープライズ機能

| 機能 | 内容 |
|------|------|
| RBAC | Enterprise プランで Cowork の使用権限をロール別に設定 |
| グループ支出上限 | チームの月次トークン消費量を管理者が制限可能 |
| 利用分析 | 管理ダッシュボードおよび Analytics API でCharts 確認可能 |
| OpenTelemetry | より深い可観測性のために外部 OTel バックエンドに送信可能 |

## 試すなら

1. Claude Desktop を最新版に更新して Cowork タブが GA 状態になっているか確認
2. Zoom 連携が必要な場合: 設定 → Connectors → Zoom で OAuth 接続
3. Analytics API に Cowork の利用データが含まれるか確認（開発者向け）

## ソース

- [Anthropic takes Claude Cowork out of preview and straight into the enterprise - The New Stack](https://thenewstack.io/anthropic-takes-claude-cowork-out-of-preview-and-straight-into-the-enterprise/)
- [Anthropic Opens Claude Cowork to All Paid Plans - eWeek](https://www.eweek.com/news/claude-cowork-general-availability-enterprise-controls/)
- [Claude Cowork is now available for enterprise use - TechRadar](https://www.techradar.com/pro/claude-cowork-is-now-available-for-enterprise-use-adds-analytics-access-controls-and-more)

---

## 感想・考察

- Zoom MCP コネクタ: 業務で Zoom を使っているため可能性はあるが、詳細仕様を確認してから判断
- RBAC 等のエンタープライズ機能: API 課金のため関係なし
- GA になったこと自体は確認済み。Cowork 自体の使い勝手は以前の試用エントリ参照
