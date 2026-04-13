---
date: 2026-04-09
status: unread
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

個人利用（Pro プラン）では機能追加は少なく、GAによるエンタープライズ機能が中心。ただし Zoom MCP コネクタは会議録をエージェントに渡す具体的なユースケースであり、MCPエコシステムの活用事例として参考になる。以前の Cowork 試用済みエントリ（`entries/04_tried/2026-03-24-cowork.md`）の続報として。

## 詳細

### 全有料プランへの拡大

- macOS・Windows の Claude Desktop アプリ上で利用可能
- Pro / Team / Enterprise 全プランで利用開始（Maxプランも含む）

### Zoom MCP コネクタ（注目）

- Zoom ミーティングのトランスクリプトをリアルタイムで取り込む
- 会議後に自動でアクションアイテム・要約を Cowork セッションに反映
- 既存の Zoom integration と同様の OAuth 接続フロー

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
3. Enterprise ユーザーは管理コンソールで RBAC と支出上限を設定してみる
4. Analytics API に Cowork の利用データが含まれるか確認（開発者向け）

## ソース

- [Anthropic takes Claude Cowork out of preview and straight into the enterprise - The New Stack](https://thenewstack.io/anthropic-takes-claude-cowork-out-of-preview-and-straight-into-the-enterprise/)
- [Anthropic Opens Claude Cowork to All Paid Plans - eWeek](https://www.eweek.com/news/claude-cowork-general-availability-enterprise-controls/)
- [Claude Cowork is now available for enterprise use - TechRadar](https://www.techradar.com/pro/claude-cowork-is-now-available-for-enterprise-use-adds-analytics-access-controls-and-more)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
