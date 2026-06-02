---
date: 2026-06-02
status: read
relevance: A
tags: [コスト最適化, コンテキストエンジニアリング, トークン圧縮, OSS, Claude-Code]
source_urls:
  - https://joho-todai.com/ai-tokens-ninety-percent-garbage/
  - https://github.com/ (Project Headroom)
experiment_dir: null
---

# Project Headroom: LLMに送るトークンの9割は「圧縮可能なゴミ」だった

## 3行要約

- NetflixのシニアエンジニアTejas Chopra氏が、Claude Sonnetで287ドルの請求書を受け取ったのを機に、コストの大半が自分の指示ではなく機械生成のJSON/ログ/スキーマ等の冗長データだと気づき、トークンを「モデルに届く前に」圧縮するOSSツール Project Headroom を開発した。
- ローカルプロキシ（ポート8787）として動作し、`headroom wrap codex` のようにLLMをラップ。AST/JSON/DOMコンプレッサーと統計フィルター（スカッシャー）で60〜95%削減、圧縮は可逆で原文はRedis/SQLiteに保存（CCR機構でLLMが必要時にMCP経由で原文取得）。
- トークン削減はコストだけでなく精度も改善しうる（Stanfordの「lost in the middle」、Chromaの「context rot」研究）。2026年1月公開、5カ月で推定約70万ドル・2000億トークンを削減、GitHub 2000+スター。

## 自分への関連度: A

Claude Codeのトークン課金は「使い込むほど高くなる」構造で、UberやMicrosoftの事例が示すようにコストが直撃する領域。可逆圧縮＋ローカル完結という設計は、自分のClaude Code運用コスト削減に直接効きうる。さらに「context rot」の知見はCLAUDE.md/コンテキスト設計そのものに関わる即実践テーマ。要検証だが優先度は高い。

## 詳細

- **コスト構造の問題**: 座席ライセンスと違い、トークン課金は生産性が上がるほどコストも上がる。Uberは2025年12月にClaude Code導入後、3月までにエンジニアの84%がエージェント型に移行し2026年予算を4カ月で消化。1人月150〜250ドル、ヘビーユーザーは500〜2000ドル。MicrosoftはClaude Codeライセンスの大半を取り消しGitHub Copilot CLIへ移行と報道。
- **何が無駄か**: 2025年の研究ではユーザー入力の読み込みだけで全トークン消費の約76%。Chopra氏の推定ではトークンの最大90%がLLMにとって冗長。冗長なJSONスキーマ、ネストされたAPIレスポンステンプレート、繰り返すDBカラム定義など。
- **Headroomの仕組み**: ①CacheAligner（差分のみ送信しKVキャッシュ全置換を回避。システムプロンプトの日付やUUIDだけでキャッシュミスが起きる点を指摘）②ルーターが種類を推定し型別コンプレッサーへ振り分け ③スカッシャーが統計分析で関連部分だけ残す ④CCRが圧縮箇所にマーカーを残し、必要時にMCPサーバー経由でローカルから原文を可逆復元。
- **キャッシュとの違い**: Claudeのプレフィックスキャッシュはデフォルト5分TTL、1時間TTLは書き込み2倍コスト・読み込み90%節約で損益分岐は利用者任せ。Headroomはローカル完結＋可逆で、データが外部に出ない点が企業利用での差別化要因。
- **競合**: The Token Company（YC出資、クラウドAPI、最大20%）、RTK=Rust Token Killer（シェル出力CLI圧縮、不可逆）、LeanCTX（RTK＋MCPでファイル/プロジェクト圧縮、可逆60〜99%）。
- **マクロ**: Goldman Sachsはエージェント型AI普及でトークン消費が2030年までに24倍（月間120京トークン）と予測。「モデルが賢いか」より「毎回何を読ませているか」が問題の中心に移った。

## 試すなら

1. Project Headroom（GitHub）のREADMEで対応ランタイム（Python/Node.js）とインストール手順を確認する。
2. ローカルでプロキシを起動し、`headroom wrap <agent>` 形式でClaude Code/Codexをラップしてみる。
3. 普段のデバッグ/リファクタリング作業を1セッション流し、削減トークン数とコストのbefore/afterを記録する。
4. CCRの可逆復元が必要な場面（MCPツール出力・大きなファイル）で原文取得が正しく効くか確認する。
5. 効果が薄ければ、より手軽なRTK/LeanCTX等のCLI圧縮系も比較対象にする。

## ソース

- [AIトークンの9割はゴミだった（情報の灯台）](https://joho-todai.com/ai-tokens-ninety-percent-garbage/)
- Project Headroom (GitHub) ※記事の参照元リンク

---

## 感想・考察

### 仕組みの理解（2026-06-02 のやり取り）

**CacheAligner は「バグ回避」ではなく仕様への対処**
- 「システムプロンプトの日付やUUIDだけでキャッシュミスが起きる」のはバグではなく、プロンプトキャッシュの仕組み上の必然。prefix は先頭から連続一致する部分までしかヒットせず、先頭側に毎回変わる値が入ると後続が同じでも丸ごと無効化される。
- 同じ例を「検出する」のが [[2026-05-22-claude-api-cache-diagnostics-beta]]（cache diagnostics）。Headroom=自動で回避、diagnostics=原因箇所を特定、というアプローチの違い。どちらも修正されたバグの話ではない。
- 別件で実際に「修正された」キャッシュ系バグ（1時間TTL→5分ダウングレード [[2026-05-10-claude-code-v21127-v21133]] 等）とは無関係。

**なぜ Claude Code でも Codex でも同一ツールが使えるのか（API形式の話）**
- LLM API のリクエスト形式に公式標準はない。各社独自（OpenAI=Chat Completions / Anthropic=Messages、system の位置や content ブロック構造が違う）。バイト単位では別物。
- ただし OpenAI 互換がデファクト化し構造は収束気味（JSONをHTTPSで送る／role付きメッセージ配列／テキスト＋ツール呼び出し）。
- 両対応できる本当の理由は3つ: ①構造の相似 ②`headroom wrap codex` の `wrap` が示すとおりプロキシ側がエージェント／APIごとのアダプタで差異を吸収（※ソース未確認の推測） ③**削る対象（冗長JSON・ログ・ツール出力・コード）が API 形式に依存しない共通のデータ**。封筒が違っても中身のゴミは共通なので、型別コンプレッサー（AST/JSON/DOM）がプロバイダ横断で成立する。
- → 「統一されているから」ではなく「似た土台＋アダプタで差異吸収＋形式非依存のゴミを叩く」が正確な理解。

※ 実装の細部（アダプタ構成・wrap の挙動）は別途ソースコードで確認予定。

<!-- /try 実行時に自動生成 -->
