---
date: 2026-03-31
status: read
relevance: A
tags: [claude-code, token, sdk, performance, cache, api]
source_urls:
  - https://qiita.com/sentinel_dev/items/04b6cfed0dabc194cec4
experiment_dir: null
---

# Claude Code SDK トークン消費95%削減 — プロセス常駐でキャッシュを活かす

## 3行要約

- CLI を毎ターン起動・終了すると**システムプロンプト全体のキャッシュが毎回再作成**され、1ターン約23万トークンを無駄に消費する
- `--input-format stream-json --output-format stream-json` でプロセスを常駐させると、2ターン目以降は `cache_read`（通常の1/12.5の単価）で済む
- 結果：cache_creation が **98.4%削減**、全体コストが **95%削減**（$1.72→$0.084/ターン相当）

## 自分への関連度: A

Claude Code SDK を使って外部ツール（ゲームAI、自動化スクリプト等）を組み込む場合に直接効く。
毎回 `claude` コマンドを spawn しているなら今すぐ見直せる。

## 詳細

### 問題のメカニズム

```
[毎回 spawn する場合]
ターン1: cache_creation 228,950 tokens → $1.72相当
ターン2: cache_creation 228,950 tokens → $1.72相当（キャッシュ無効）
...
```

Claude Code のシステムプロンプトは大きく、毎回新プロセスを立てると**プロンプトキャッシュが活用されない**。

### 解決策: プロセス常駐 + stream-json

```bash
# 常駐起動
claude --input-format stream-json --output-format stream-json
```

stdin に JSON を書き込み、stdout の stream-json イベントを受け取る方式に変更。

```
[常駐プロセスの場合]
ターン1: cache_creation 228,950 tokens（初回のみ）
ターン2: cache_read 228,950 tokens → $0.14相当（1/12.5の単価）
ターン3以降: 同上
```

### 実装のポイント

- **プロセス管理**: `spawn` で常駐、クラッシュ時の自動再起動を実装する
- **イベントルーティング**: `system`、`assistant`、`result`、`rate_limit_event` 等を適切にハンドリング
- **排他制御**: 複数クライアントから同時アクセスする場合はキューで管理

### 効果

| 指標 | 変更前 | 変更後 | 削減率 |
|------|--------|--------|--------|
| cache_creation（/ターン） | 228,950 tokens | 3,572 tokens | **98.4%** |
| 全体コスト（/ターン） | $1.72 | $0.084 | **95%** |
| レート制限到達 | 数十ターンで到達 | 大幅に緩和 | — |

## 試すなら

1. 現在の実装で `claude` を毎回 spawn しているか確認
2. `--input-format stream-json --output-format stream-json` フラグで常駐モードを試す
3. stream-json の各イベント種別（`system`、`assistant`、`result`）をハンドリングするコードを書く
4. 2ターン目以降のログで `cache_creation` が激減していることを確認

## ソース

- [【95%削減】Claude Codeエージェントのトークン消費、毎ターン23万トークン垂れ流してた（Qiita）](https://qiita.com/sentinel_dev/items/04b6cfed0dabc194cec4)

---

## 感想・考察

### Q&A メモ（2026-04-05）

**Q: Claude Code SDK とは何か？VSCode プラグインとして使っている場合、影響はあるか？**

Claude Code SDK は、自分のアプリやスクリプトから `claude` コマンドをプログラム的に呼び出す仕組みのこと。このエントリの問題は、外部プログラムが毎ターン `claude` プロセスを spawn する場合に発生する。

VSCode 拡張機能として使う場合は影響なし。拡張機能の内部でプロセスが常駐管理されており、キャッシュは機能している。

**Q: CLI から `claude` コマンドを起動して会話する場合は影響があるか？**

影響なし。インタラクティブに `claude` を起動して会話する場合、プロセスはセッション中常駐しており、2ターン目以降は `cache_read` が効いている。

問題になるのは `claude -p "..."` のようなワンショット実行をスクリプトからループで繰り返すケース。このパターンを使っている場合は stream-json 常駐方式への切り替えを検討する価値がある。

| 使い方 | プロセス | キャッシュ |
|--------|----------|------------|
| `claude` で起動して会話 | セッション中常駐 | 2ターン目から cache_read が効く |
| `claude -p "..."` を繰り返す | 毎回新規 | 毎回リセット（問題のパターン） |
| stream-json 常駐（解決策） | 常駐 | 2ターン目から cache_read が効く |
