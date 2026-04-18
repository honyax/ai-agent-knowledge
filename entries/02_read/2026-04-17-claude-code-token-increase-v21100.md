---
date: 2026-04-17
status: read
relevance: A
tags: [claude-code, token, cache, cost, performance]
source_urls:
  - https://qiita.com/yurukusa/items/c0acc6da4cb1c90fa431
experiment_dir: null
---

# Claude Code v2.1.100以降でトークン消費が40%増加 — cache_creation膨張の原因と対策（Qiita）

## 3行要約

- Claude Code v2.1.100 以降、`cache_creation_input_tokens` が約 49,726 → 69,922 と約 40% 増加している事例が報告。
- 原因はシステムプロンプトやコンテキストの膨張。同バージョンで追加された新機能が含まれている可能性。
- 削減方法の調査・実践が記事の主題で、バージョン比較や設定変更での対策が紹介される。

## 自分への関連度: A

Claude Code を日常的に使っているためコスト直結の問題。v2.1.100 以降を使っているなら現在の消費量を確認すべき。

## 詳細

### 症状
v2.1.100 以前と比べて同程度の作業でもトークン消費が約 40% 増加。特に `cache_creation_input_tokens` の増大が目立つ。

### 推定原因
新バージョンで追加された機能（Auto Mode分類器、モバイルプッシュ、conditional hooks のロジックなど）がシステムプロンプトに含まれ、キャッシュ作成トークンが増加している可能性。または会話コンテキストの保持量増加。

### 対策の方向性
- 不要な設定・機能を無効化してシステムプロンプトのサイズを削減
- `/compact` や context compaction の活用
- 作業スコープを小さく保ち、不要なコンテキストを引き継がない
- バージョンを一時的に固定する（ただし公式サポートなし）

## 試すなら

1. `claude --show-usage` などでトークン消費量を確認し、v2.1.100 前後で比較
2. 会話開始時の `cache_creation_input_tokens` を記録して推移を把握
3. CLAUDE.md をコンパクトにするか、不要な設定を削除してみる
4. `/compact` コマンドを定期的に実行してコンテキストを整理

## ソース

- [Claude Code v2.1.100でトークン消費が40%増えた — Qiita](https://qiita.com/yurukusa/items/c0acc6da4cb1c90fa431)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
