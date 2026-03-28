---
date: 2026-03-28
status: unread
relevance: A
tags: [claude-api, web-search, tool-calling, ga, code-execution]
source_urls:
  - https://platform.claude.com/docs/en/release-notes/overview
experiment_dir: null
---

# Claude API: Web Search & Programmatic Tool Calling が正式GA化

## 3行要約

- Web SearchツールとProgrammatic Tool Callingがベータから正式GA（betaヘッダー不要に）
- Web Search / Web Fetchにコード実行による動的フィルタリング機能が追加。検索結果がコンテキストに入る前にフィルタリング可能
- トークンコスト削減とパフォーマンス向上が見込める

## 自分への関連度: A

API経由でWeb検索を使う際にbetaヘッダーが不要になり、プロダクション利用のハードルが下がった。ゲーム内AIにリアルタイムWeb情報を組み込む可能性が広がる。動的フィルタリングはトークン節約に直結し、コスト面で実用的。

## 試すなら

1. 既存のAPI呼び出しからbetaヘッダーを削除してWeb Searchが動くことを確認
2. 動的フィルタリング（code execution filter）のドキュメントを確認
3. Web Fetch + フィルタリングで特定情報のみ取得するサンプルを作成
4. トークン使用量の変化を計測

## ソース

- [Claude Platform Release Notes](https://platform.claude.com/docs/en/release-notes/overview)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
