---
date: 2026-04-03
status: unread
relevance: B
tags: [anthropic, security, jailbreak, ai-safety]
source_urls:
  - https://www.anthropic.com/research/next-generation-constitutional-classifiers
  - https://arxiv.org/abs/2601.04603
experiment_dir: null
---

# Constitutional Classifiers++ — ジェイルブレーク対策の次世代版（計算コスト40分の1）

## 3行要約

- Anthropicが次世代ジェイルブレーク防御システム「Constitutional Classifiers++」を発表。前世代比で計算コスト40分の1に削減しながら防御性能を維持
- 2段階アーキテクチャを採用：Claudeの内部活性化を監視する軽量プローブが全トラフィックをスクリーニングし、疑わしいケースのみ詳細分類器にエスカレーション
- 本番トラフィックでの誤拒否率0.05%を達成（前世代は0.38%）。1,700時間以上のレッドチーミングで有効性を検証済み

## 自分への関連度: B

ゲーム内AI統合でClaude APIを使う場合、ユーザー入力に対するセキュリティ層として参考になる考え方。またAIセーフティへのAnthropicの取り組み理解として知識的に有用。

## 詳細

**前世代との比較**:
| 項目 | 第1世代 | Constitutional Classifiers++ |
|------|---------|------------------------------|
| 追加計算コスト | +23.7% | 約+1%（40倍削減） |
| 誤拒否率 | +0.38% | 0.05% |

**アーキテクチャ**:
1. **プローブ**: Claudeの内部活性化（中間層の出力）を監視する軽量分類器
2. **Exchange Classifiers**: 会話のフル文脈でモデルレスポンスを評価する詳細分類器
3. **カスケード**: 軽量分類器が全トラフィックをスクリーニング → 疑わしい場合のみ詳細分類器へ

**論文**: [Constitutional Classifiers++: Efficient Production-Grade Defenses against Universal Jailbreaks](https://arxiv.org/abs/2601.04603)

## 試すなら

1. Anthropicの研究ブログ記事を読んで手法を理解する
2. 自分のプロジェクトでClaude APIを使う際の入力バリデーション設計に参考にする

## ソース

- [Next-generation Constitutional Classifiers (Anthropic Research)](https://www.anthropic.com/research/next-generation-constitutional-classifiers)
- [Constitutional Classifiers++ paper (arXiv)](https://arxiv.org/abs/2601.04603)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->

