---
date: 2026-04-24
status: read
relevance: S
tags: [claude-code, postmortem, quality, thinking, caching, verbosity, harness]
source_urls:
  - https://www.anthropic.com/engineering/april-23-postmortem
  - https://simonwillison.net/2026/Apr/24/recent-claude-code-quality-reports/
  - https://venturebeat.com/technology/mystery-solved-anthropic-reveals-changes-to-claudes-harnesses-and-operating-instructions-likely-caused-degradation
experiment_dir: null
---

# Claude Code 品質低下の公式ポストモーテム — 3つのバグが原因だった

## 3行要約

- Anthropicが2026年4月23日に「過去2ヶ月の品質低下は実在した」と認めるポストモーテムを公開。モデルではなくハーネス層（周辺システム）の3つの変更が原因だった
- 3つのバグ: (1) 3月4日 デフォルトreasoning effortをhigh→mediumに変更、(2) 3月26日 1時間アイドル後のthinking履歴削除キャッシュがターン毎に繰り返し発動、(3) 4月16日 回答語数制限プロンプト追加でコーディング評価3%低下
- 全サブスクライバーのusage limitをリセット、デフォルトreasoning effortを高めに戻し、キャッシュバグ修正、語数制限プロンプトを撤回

## 自分への関連度: S

2〜3月頃から「Claude Codeが以前より雑になった」と感じていた場合、これが原因。ハーネス設計の失敗例として学ぶところが大きく、特に「3つの独立した変更が重なると総合効果が読めなくなる」という教訓は自分のSkills/Hooks設計にも適用できる。リミットもリセットされているので実質的なメリットもある。

## 詳細

### 3つのバグの内訳

| 日付 | 変更 | 意図 | 実際の影響 |
|------|------|------|-----------|
| 3月4日 | reasoning effort デフォルトを high→medium | UIの「考え中」表示が固まって見えるのを回避 | 複雑なタスクで知能低下が顕著 |
| 3月26日 | 1時間アイドル後にthinking履歴を削除するキャッシュ最適化 | キャッシュ効率化 | バグで **毎ターン** 削除され続け、短期記憶を失い繰り返し・忘却が発生 |
| 4月16日 | システムプロンプトに語数制限（ツール呼び出し間25語未満・最終回答100語未満） | Opus 4.7の冗長さ抑制 | コーディング評価3%低下 |

### なぜ切り分けが困難だったか

3つの変更はそれぞれ別のトラフィックの割合・別のスケジュールで展開されたため、総合的な劣化パターンが「広範囲・非一貫」に見え、単一原因の特定が難しかった。

### ハーネス設計への教訓（ハーネスエンジニアリングの文脈）

- ハーネス層の変更はモデル自体より検出しにくい
- パフォーマンス最適化（キャッシュ・プロンプト圧縮）は意図しない副作用を生む
- 「UIの応答性」と「知的パフォーマンス」のトレードオフは慎重に扱う必要

### Anthropicの対応

- 3つのバグを全て修正・ロールバック
- 全サブスクライバーのusage limitをリセット
- 詳細なポストモーテムを公開（透明性の確保）

### 関連する過去エントリ

- [v2.1.100でトークン消費40%増 (2026-04-17)](../02_read/2026-04-17-claude-code-token-increase-v21100.md) ← この現象の一因が今回判明
- [ハーネスエンジニアリング概念 (2026-04-23)](./2026-04-23-harness-engineering-concept.md) ← 今回の教訓はこの概念の実証事例

## 試すなら

1. Claude Codeを最新版にアップデート（`claude update`）
2. `/usage` でリセットされたリミットを確認
3. 以前うまくいかなかった複雑なタスクを再試行し、改善を確認
4. ポストモーテム本文を読み、ハーネス設計の失敗パターンを自分のSkills/Hooksに照らす
5. 今後の変更をウォッチするため `claude --version` と `claude update` を定期実行

## ソース

- [An update on recent Claude Code quality reports (Anthropic Engineering)](https://www.anthropic.com/engineering/april-23-postmortem)
- [An update on recent Claude Code quality reports (Simon Willison)](https://simonwillison.net/2026/Apr/24/recent-claude-code-quality-reports/)
- [Mystery solved: Anthropic reveals changes to Claude's harnesses (VentureBeat)](https://venturebeat.com/technology/mystery-solved-anthropic-reveals-changes-to-claudes-harnesses-and-operating-instructions-likely-caused-degradation)

---

## 感想・考察

### 「harness」という用語の公式化

ソースを読み直して確認したところ、Anthropic 公式ポストモーテム本文で `harness` が定義なしに1回登場している（"we spend time before each release optimizing the harness and product for it"）。VentureBeat の見出しにも "Claude's harnesses" と入っており、Simon Willison も注釈なしで `harnesses` を使用。

つまり**コミュニティ用語が公式語彙に昇格する瞬間を観測している**状態。ただし API リファレンスや SDK 用語としてはまだ未定着で、ポストモーテム内でも1回のみ・見出しは "operating instructions" 寄りなので、「業界で確立した正式語」と呼ぶには早い。半分 Yes・半分 No。今後 Anthropic 公式ドキュメントでも増えるか要観察。

→ 関連: [ハーネスエンジニアリング概念 (2026-04-23)](../01_unread/2026-04-23-harness-engineering-concept.md) の用語がコミュニティで先行流行 → 数日後に Anthropic がポストモーテムで自然に同じ用語を採用、という時系列。

### バグ発見プロセスの教訓

ポストモーテム本文には「どう発見したか」がかなり率直に書かれている:

- **起点はユーザーレポート**（"We took reports about degradation very seriously"）
- 社内 eval も dogfooding も**当初再現できなかった**（"neither our internal usage nor evals initially reproduced the issues"）
- キャッシュバグは **6層の検証をすり抜けた**: 人間レビュー・自動レビュー・単体テスト・E2Eテスト・自動検証・社内利用
- **事後検証**で Opus 4.7 駆動の Code Review ツールに「当時このPRをレビューさせていたら気づけたか?」を遡及テスト
- 3つのバグが別々のロールアウト・別々のトラフィック割合で展開されたため、症状が「広範囲・非一貫」に見えて切り分け困難

→ 「6層すり抜け」を素直に書いている誠実さは評価できる。同時に**社内 eval だけに頼ったQAの限界**を示しており、ユーザーフィードバックループが品質保証の最終防衛線として依然として必須、という教訓。自分のSkills/Hooks設計でも「dogfoodingで気づけないバグはある」前提で組むべき。

### Mythos は今回使われていない

調査時に Mythos（Opus 超えの未公開内部モデル）が使われた可能性も考えたが、ポストモーテム・3つのソース全てで言及なし。**事後検証は Opus 4.7 の Code Review ツール**で、Mythos のような上位モデルは投入されていない。Anthropic がポストモーテム調査でどこまで未公開モデルを使うかは、今回の事例からは読み取れない。
