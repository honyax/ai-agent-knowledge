---
date: 2026-04-04
status: read
relevance: B
tags: [anthropic, claude-code, subscription, openclaw, policy, third-party]
source_urls:
  - https://techcrunch.com/2026/04/04/anthropic-says-claude-code-subscribers-will-need-to-pay-extra-for-openclaw-support/
  - https://venturebeat.com/technology/anthropic-cuts-off-the-ability-to-use-claude-subscriptions-with-openclaw-and
  - https://the-decoder.com/anthropic-cuts-off-third-party-tools-like-openclaw-for-claude-subscribers-citing-unsustainable-demand/
  - https://help.apiyi.com/en/anthropic-claude-subscription-third-party-tools-openclaw-policy-en.html
experiment_dir: null
---

# Anthropic、Claude Pro/MaxサブスクリプションでのOpenClaw等サードパーティツール利用を禁止

## 3行要約

- 2026年4月4日正午（太平洋時間）より、Claude Pro/MaxサブスクリプションはOpenClaw等のサードパーティハーネスでの利用不可になった
- 理由は「サードパーティツールの使用パターンがサブスクリプション設計の想定外」（Boris Cherny）で、今後は都度課金（pay-as-you-go）が必要
- 補償として1ヶ月分の一時クレジット（4月17日まで）と最大30%オフの事前購入バンドルを提供

## 自分への関連度: B

現在Claude Codeを直接使っておりOpenClawは使っていないため直接の影響はない。ただしサードパーティツールへの展開制限がどこまで広がるか注意が必要。

## 詳細

**影響範囲**:
- 初期はOpenClawのみ。「すべてのサードパーティハーネス」に順次展開予定
- Claude Codeなど公式Anthropicツールは引き続き通常通り利用可能

**背景**:
- Boris Cherny（Claude Codeヘッド）によると、サードパーティツールのプロンプトキャッシュヒット率が低く、サブスクリプションの収支が合わない
- AnthropicはOpenClawのキャッシュ効率改善にPRを出すなど協力を試みたが、根本的な問題は解決できなかった
- OpenClaw作者Peter Steinbergerは交渉を試みたが、施行を1週間延期させるにとどまった

**代替手段**:
- AnthropicのAPI（pay-as-you-go）を使ってOpenClaw等を利用する形に移行

## 試すなら

（対応不要。現在Claude Codeのみ使用中のため影響なし）

## ソース

- [Anthropic says Claude Code subscribers will need to pay extra for OpenClaw - TechCrunch](https://techcrunch.com/2026/04/04/anthropic-says-claude-code-subscribers-will-need-to-pay-extra-for-openclaw-support/)
- [Anthropic cuts off third-party tools like OpenClaw for Claude subscribers - The Decoder](https://the-decoder.com/anthropic-cuts-off-third-party-tools-like-openclaw-for-claude-subscribers-citing-unsustainable-demand/)
- [Interpreting Anthropic's Third-Party Tool Ban - Apiyi](https://help.apiyi.com/en/anthropic-claude-subscription-third-party-tools-openclaw-policy-en.html)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
