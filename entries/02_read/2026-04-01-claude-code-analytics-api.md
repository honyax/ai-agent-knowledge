---
date: 2026-04-01
status: read
relevance: A
tags: [claude-code, api, analytics, team]
source_urls:
  - https://www.builder.io/blog/claude-code-updates
  - https://code.claude.com/docs/en/changelog
experiment_dir: null
---

# Claude Code Analytics API — 組織のAI利用メトリクスをプログラムで取得

## 3行要約

- 組織が Claude Code の利用メトリクス（生産性指標・ツール使用統計・コストデータ）に API 経由でアクセスできる Analytics API が提供開始
- 日次集計データをプログラム取得できるため、チームの AI 活用度や ROI の可視化が可能になる
- VS Code 向けにレート制限警告バナーも追加され、消費量の把握がしやすくなった

## 自分への関連度: A

チームやプロジェクト単位でのコスト管理・活用度可視化に使える。現時点では個人利用がメインだが、将来的にチーム展開する場合に重要になる機能。

## 詳細

### Analytics API の主な機能

- **日次集計メトリクス**: 組織レベルでのトークン消費量・コスト・アクティブユーザー数
- **ツール使用統計**: どのツール（Bash, Edit, Read等）がどれだけ使われているか
- **生産性指標**: コード生成量、承認率など（具体的な指標は今後拡充予定）
- **プログラムアクセス**: REST API 経由で外部ダッシュボードや Slack 通知との連携が可能

### VS Code レート制限警告バナー

Claude Code の VS Code 拡張で、レート制限に近づいた際に警告バナーが表示されるようになった。過去に突然制限に引っかかって作業が止まることがあったが、これで事前に気づけるようになる。

## 試すなら

1. Claude Code 公式ドキュメントで Analytics API のエンドポイントを確認
2. API キーを使ってサンプルリクエストを試す
3. 日次コストの CSV エクスポートを試してスプレッドシートに取り込む
4. チームの月次レポートに組み込むスクリプトを作成する

## ソース

- [Every Claude Code Update From March 2026, Explained (builder.io)](https://www.builder.io/blog/claude-code-updates)
- [Changelog - Claude Code Docs](https://code.claude.com/docs/en/changelog)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
