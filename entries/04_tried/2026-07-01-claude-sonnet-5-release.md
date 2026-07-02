---
date: 2026-07-01
status: tried
relevance: S
tags: [claude-sonnet-5, model-release, claude-code, benchmark, pricing, 1m-context]
source_urls:
  - https://techcrunch.com/2026/06/30/anthropic-launches-claude-sonnet-5-as-a-cheaper-way-to-run-agents/
  - https://thenewstack.io/claude-sonnet-5-launch/
  - https://www.marktechpost.com/2026/06/30/anthropic-claude-sonnet-5-vs-sonnet-4-6-vs-opus-4-8-agentic-coding-benchmarks-api-pricing-and-cost-performance-tradeoffs-compared/
  - https://platform.claude.com/docs/en/about-claude/models/whats-new-sonnet-5
experiment_dir: null
---

# Claude Sonnet 5 リリース: Claude Code の新デフォルト、Opus 4.8 に近い性能で $2/$10（8/31 まで）

## 3行要約

- 6/30、Anthropic は Claude Sonnet 5（`claude-sonnet-5`）をリリース。**Claude Code の新デフォルト**、Claude.ai の Free/Pro デフォルト、Cursor/VS Code/GitHub Copilot でも利用可。ネイティブ 1M トークンコンテキスト。
- 導入価格 **$2 / $10 per Mtok**（8/31 まで、9/1 以降は $3/$15 で Sonnet 4.6 と同じ）。ベンチマーク: SWE-bench Pro 63.2%、OSWorld-Verified 81.2%、HLE 57.4%。Sonnet 4.6 を全指標で上回り、Opus 4.8 に肉薄（GDPval-AA v2 では超え、Humanity's Last Exam や prompt-injection 安全性ではほぼ同等）。
- 注意点: **新トークナイザーが同じテキストで 1.0〜1.35 倍のトークン数**をカウントする。単価表は Sonnet 4.6 と同じでも、実タスク単位のコストは超えるケースあり。

## 自分への関連度: S

Claude Code のデフォルトモデルが変わるのは業務直撃案件。自分は Opus 4.7 を使っているが（システムプロンプト参照）、Sonnet 5 が Opus 4.8 に肉薄する性能で 1/6 程度の単価となれば、モデル選択の再検討は避けられない。1M ネイティブコンテキストは Auto Compaction ([[2026-03-31-autocompact-trap]]) との相性も変わる。トークナイザー変更による実コスト増は要ベンチ。CLAUDE.md の関心領域 5 (Claude API 変更・新モデル) にも直接該当。

## 詳細

### 性能ベンチマーク

| ベンチマーク | Sonnet 5 | Sonnet 4.6 | Opus 4.8 |
|--------------|----------|------------|----------|
| SWE-bench Pro | 63.2% | (下回る) | (Sonnet 5 が近い) |
| OSWorld-Verified | 81.2% | (下回る) | (Sonnet 5 が近い) |
| HLE (Humanity's Last Exam) | 57.4% | (下回る) | (Sonnet 5 tools 57.4% vs Opus 4.8 57.9%) |
| GDPval-AA v2 (知識ワーク) | 1618 | (下回る) | **1615（Sonnet 5 が超え）** |
| Prompt-injection 安全性 | tie | (下回る) | tie（Sonnet 5 と同等） |

「Sonnet 5 が Sonnet 4.6 を全て上回る」+「Opus 4.8 に一部で追いつく or 追い越す」がヘッドライン。

### 価格と可用性

- **プロモ価格（〜8/31）**: input $2 / output $10 per Mtok
- **通常価格（9/1〜）**: input $3 / output $15 per Mtok（= Sonnet 4.6 と同一）
- **Claude Code**: 新デフォルトモデル
- **Claude.ai**: Free / Pro のデフォルト
- **API**: `claude-sonnet-5` で直接指定可
- **他プラットフォーム**: Cursor / VS Code / GitHub Copilot に即日展開

### 1M コンテキストがネイティブに

- 従来 Sonnet 系は long context が「ベータ」扱い/追加料金だった。
- Sonnet 5 は **ネイティブ 1M**（追加料金なし、標準扱い）。
- Auto Compaction ([[2026-03-31-autocompact-trap]]) や compaction ([[2026-03-29-api-compaction]]) の実感が変わる可能性大。

### トークナイザーの罠

- Sonnet 5 のトークナイザーは同一テキストで **1.0〜1.35 倍**のトークン数をカウント。
- 単価表は同じでもタスクあたりコストは超えるケースがある。
- 特に日本語や絵文字、コード関連は膨らみやすい。cache diagnostics ([[2026-05-22-claude-api-cache-diagnostics-beta]]) と Headroom ([[2026-06-02-project-headroom-token-compression]]) や RTK ([[2026-07-01-rtk-rust-token-killer]]) の意義が上がる。

### 位置づけ

- 「エージェント運用を安く回す」がキャッチコピー。Sonnet 4.6 の役割（性能 vs コストのバランス）を継承しつつグレードアップ。
- Opus 4.8 は「最も高性能だが高い」枠、Sonnet 5 は「Opus に近い性能で 5 倍安い」枠に。
- Claude Code v2.1.197（6/30 リリース）でデフォルト切替。

## 試すなら

1. Claude Code を最新化（v2.1.197+）、デフォルトモデルが `claude-sonnet-5` になったか確認。
2. 自分の Opus 4.7 常用を維持するか、Sonnet 5 に切替えるか判断するため、同一タスク（例: 既知のバグ調査 1 セッション）を両モデルで走らせ、（a）品質差（b）実消費トークン数（c）実料金 を測る。
3. トークナイザー変化を確認: 短いプロンプトで `token_count` API（[[2026-05-22-claude-api-cache-diagnostics-beta]] 参照）を叩き、Sonnet 4.6 との比率を測定。
4. 1M ネイティブコンテキストを活かし、大きめのリポジトリを丸ごと投入するセッションを試す。auto-compaction の発火タイミングが変わるか確認。
5. プロモ期間（〜8/31）中に集中的にベンチ、9/1 以降の $3/$15 でも採算に合うか事前判断。

## ソース

- [Anthropic launches Claude Sonnet 5 (TechCrunch)](https://techcrunch.com/2026/06/30/anthropic-launches-claude-sonnet-5-as-a-cheaper-way-to-run-agents/)
- [Anthropic Sonnet 5: It closes the gap with Opus 4.8 (The New Stack)](https://thenewstack.io/claude-sonnet-5-launch/)
- [Sonnet 5 vs Sonnet 4.6 vs Opus 4.8 Benchmarks (MarkTechPost)](https://www.marktechpost.com/2026/06/30/anthropic-claude-sonnet-5-vs-sonnet-4-6-vs-opus-4-8-agentic-coding-benchmarks-api-pricing-and-cost-performance-tradeoffs-compared/)
- [What's new in Claude Sonnet 5 (Claude Platform Docs)](https://platform.claude.com/docs/en/about-claude/models/whats-new-sonnet-5)

---

## 感想・考察

### Opus 4.8 の挙動不安定と Sonnet 5 への切り替え（2026-07-03）

**Opus 4.8 の実感**: Opus 系の中では少し**挙動が不安定**という体感がある。具体的な症状は継続観察中だが、Opus 4.7 の安定感には及ばない印象。そのため常用モデル選定に悩んでいた（システムプロンプト上は Opus 4.7）。

**Sonnet 5 の登場で判断**: Sonnet 5 が Opus 4.8 に肉薄する性能（SWE-bench Pro 63.2%、GDPval-AA v2 では超え、HLE ほぼ同等）＋ **1/6 程度の単価**というアナウンスなら、「不安定な Opus 4.8 を頑張って使う」より「安定感を含めて Sonnet 5 で試す」方が合理的。

**決定と実行**: **しばらくは Sonnet 5 をデフォルトで使うことにし、切り替え済み**。

### 観察したい点（今後のセッションで蓄積）

Sonnet 5 を常用しつつ、以下の観察を続ける:

1. **品質面**: Opus 4.7 / Opus 4.8 と比べて、コード生成品質・タスク完遂率・レビューでのミス率がどう変わるか
2. **実消費トークン**: 新トークナイザーが 1.0〜1.35 倍カウントする件が、実タスクでどれくらいコスト増になるか
3. **1M ネイティブコンテキスト**: [[2026-03-31-autocompact-trap]] の Auto Compaction 発火タイミングが変わるか
4. **挙動安定性**: Opus 4.8 で感じていた「不安定さ」が Sonnet 5 では出ないか
5. **プロモ期間終了後（9/1〜）の $3/$15 でも採算に合うか**

### 戻す条件

- Sonnet 5 の品質が想定より落ちる場合 → Opus 4.7 に戻す（Opus 4.8 には戻らない）
- 特定タスク（大規模リファクタ、複雑な設計判断）で Sonnet 5 が力不足なら、そのタスクだけ Opus 4.7 に切り替える運用

<!-- /try 実行時に自動生成 -->
