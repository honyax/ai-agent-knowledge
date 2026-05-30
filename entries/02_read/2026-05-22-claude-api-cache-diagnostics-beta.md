---
date: 2026-05-22
status: read
relevance: B
tags: [claude-api, prompt-caching, diagnostics, beta]
source_urls:
  - https://platform.claude.com/docs/en/build-with-claude/cache-diagnostics
  - https://platform.claude.com/docs/en/release-notes/overview
experiment_dir: null
---

# Claude API、プロンプトキャッシュ診断（cache diagnostics）を public beta で提供 — キャッシュミスの原因箇所を特定できる

## 3行要約

- Claude Developer Platform にプロンプトキャッシュ診断が public beta で追加。beta ヘッダ `cache-diagnosis-2026-04-07` を付け、Messages リクエストに `diagnostics.previous_message_id` を渡すと、`cache_miss_reason` が返り「前ターンとどこ（model / system prompt / tools / message history）で prefix が分岐したか」を教えてくれる。
- 使い方は毎ターン beta ヘッダを送り、初回は `"previous_message_id": null` でオプトイン、以降は前レスポンスの id を渡す。ストリーミングでは `message_start` イベントに診断が乗る。
- 現状 Claude API のみ対応で、Amazon Bedrock / Vertex AI では未サポート。

## 自分への関連度: B

ゲーム内 AI 統合などで Claude API を直接叩くときに、プロンプトキャッシュのヒット率が落ちる原因を切り分けられる実用ツール。今すぐ使う場面は限定的だが、API でエージェントを組む際のコスト最適化に効くので知識として押さえておく。Claude Code（サブスク）側ではなく API 開発向け。

## 詳細

- キャッシュミスの分岐箇所が「tools の差分なのか system prompt なのか会話履歴なのか」まで切り分くため、無自覚に prefix を壊している箇所（例: 毎回変わるタイムスタンプを system に入れている等）を発見しやすい。
- Bedrock/Vertex 非対応のため、それらのゲートウェイ経由運用では使えない点に注意。

## 試すなら

1. テスト用の小さな Anthropic SDK スクリプトを用意し、beta ヘッダ `cache-diagnosis-2026-04-07` を付与。
2. 初回リクエストで `diagnostics.previous_message_id: null`、2回目以降で前回 response id を渡す。
3. わざと system prompt や tools を変えて `cache_miss_reason` がどの分岐を報告するか確認。

## ソース

- [Cache diagnostics - Claude API Docs](https://platform.claude.com/docs/en/build-with-claude/cache-diagnostics)
- [Claude Platform Release Notes](https://platform.claude.com/docs/en/release-notes/overview)

---

## 感想・考察

### この機能の位置づけ（読了時の整理）

- Anthropic 側のダッシュボードで覗くものではなく、**API を直接叩く開発者が自分のリクエストに beta ヘッダ＋`previous_message_id` を仕込んで、手元で診断する**タイプの機能。Claude Code のようなサブスク利用者が自分の利用を診断するものではない。
- 「キャッシュミスの原因調査」というより、**前ターンとの差分でキャッシュ prefix が壊れた場所（model / system / tools / message history）を切り分ける**ツール、と捉えるのが正確。

### ここで言う「キャッシュ」とは（プロンプトキャッシュの理解）

- Web レスポンスキャッシュ等とは別物で、Claude API の **プロンプトキャッシュ（prompt caching）**。プロンプト先頭部分（prefix）のトークン計算結果をサーバ側に一時保存し、同じ prefix が来たら再計算をスキップする仕組み。
- LLM の計算は先頭から積み上がるため、**先頭から連続して一致している部分までしかヒットしない**。system prompt のように先頭側が毎回変わると後続が同じでも丸ごと無効化される（例: 毎回変わるタイムスタンプを system に入れる事故）。
- メリットは **コスト削減（キャッシュヒット分は通常の約1/10課金）** と **レイテンシ短縮**。エージェントは入力が膨らみやすく、ヒット率がコストを大きく左右する。
- 自動常時有効ではなく、`cache_control` で「ここまでをキャッシュ対象」と明示マークする方式（標準TTL 5分、延長あり）。Claude Code は内部で自動的にこの仕組みを使っている。

### 自分にとっての結論

- 現状 API を直接叩いていないため即実践はなし。将来ゲーム内 AI 統合などで API でエージェントを組む際、コスト最適化の引き出しとして「prefix を壊さない設計＋cache diagnostics で切り分け」を思い出せれば十分。**知識として**押さえる位置づけ。

<!-- /try 実行時に自動生成 -->
