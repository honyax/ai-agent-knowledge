---
date: 2026-03-28
status: read
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

実際のGA化は2026-02-17（リリースノート確認済み）。

変更点を整理すると以下のとおり：

| 項目 | Beta時代 | GA後 |
|------|----------|------|
| betaヘッダー | 必要 | 不要 |
| 検索結果のトークン | 全量投入 | フィルタリング後のみ |
| コード実行コスト（Search/Fetch時）| 課金 | 無料 |

エンドユーザー視点では見た目・挙動はほぼ変わらない。開発者としては `anthropic-beta: ...` の1行を削除するだけで動作は変わらない。

重要なのはコスト面と安定性：
- Dynamic Filteringを使えばトークンが減り、さらにコード実行が無料なのでフィルタリングコストの上乗せもない
- GAになると後方互換性が保証されるため、betaのように予告なく仕様変更・廃止されるリスクがなくなりプロダクション利用の信頼性が上がる

なお、エントリ未記載だった重要点として「コード実行がWeb Search/Web Fetch併用時に無料」がある（リリースノートに明記）。
