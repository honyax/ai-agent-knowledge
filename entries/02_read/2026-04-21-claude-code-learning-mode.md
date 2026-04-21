---
date: 2026-04-21
status: read
relevance: B
tags: [claude-code, learning-mode, output-style, workflow, productivity]
source_urls:
  - https://zenn.dev/amu_lab/articles/claude-code-learning-mode
  - https://code.claude.com/docs/en/output-styles
  - https://github.com/anthropics/claude-code/tree/main/plugins/learning-output-style
experiment_dir: null
---

# Claude Code Learning Mode 完全ガイド — /output-style と TODO(human) の実務運用

## 3行要約

- Claude Code の Learning Mode は `/config` → Output style → Learning で有効化。コード生成時に `TODO(human)` マーカーを挿入し、人間が戦略的に実装すべき箇所を明示してくれる。
- `TODO(human)` に自分で実装を書くと、Claude が PRレビュースタイルでフィードバックを返す「対話的学習サイクル」が成立する。
- Output Style は System Prompt に注入されるためセッション再起動が必要（セッション中途の切り替え不可が仕様）。プロンプトキャッシュ安定化のための意図的な制約。

## 自分への関連度: B

業務的には「丸投げ」モードで使うことが多く、Learning Mode の出番は限られる。ただしコードを書きながら Claude の判断を学ぶ副次的学習効果は面白い。TypeScript 等の慣れていない部分を学ぶ際に試す価値はある。

## 詳細

**有効化手順:**
```
/config → Output style → Learning
```
または `/output-style learning`（ただしセッション再起動が必要）

**動作の流れ:**
1. Claude がコード生成時に学習価値の高い箇所を `TODO(human)` として残す
2. ユーザーが `TODO(human)` を実装
3. Claude が実装に対して PR レビュースタイルでフィードバック
4. このサイクルを繰り返す

**TODO(human) の挿入基準:**
- ビジネスロジックで複数の正解がある実装
- 高い学習価値がある箇所（Claude が判定）
- 単純な boilerplate は対象外

**実務での注意点:**
- Output Style はセッション再起動後に有効化（セッション内での切り替え不可）
- `plugins/learning-output-style` として公式プラグインも提供されている
- Learning と Normal の切り替えはプロジェクト単位で管理すると管理しやすい

## 試すなら

1. `/config` を開き Output style → Learning を選択
2. Claude Code を再起動して新規セッション開始
3. 新機能の実装を依頼して `TODO(human)` の挿入を確認
4. `TODO(human)` を自分で実装してフィードバックを受ける
5. Normal モードと比較してワークフローの違いを把握

## ソース

- [Claude Code Learning Mode 完全ガイド（Zenn: amu_lab）](https://zenn.dev/amu_lab/articles/claude-code-learning-mode)
- [Output styles - Claude Code Docs](https://code.claude.com/docs/en/output-styles)
- [learning-output-style plugin（GitHub）](https://github.com/anthropics/claude-code/tree/main/plugins/learning-output-style)

---

## 感想・考察

Learning Mode は「Claude Code を使うとコードを書かなくなって学習機会が失われる」という批判への回答として作られた側面がある。

`TODO(human)` は Claude が実装を進める中で「あなた自身が書くべき箇所」に残すマーカー。正解が一つではないビジネスロジックや学習価値の高い箇所が対象で、定型コードは対象外。自分が実装した後に Claude が PR レビュースタイルでフィードバックを返す。

普段の「丸投げ」運用では使う場面は少ないが、新しい言語やフレームワークを触り始めた際に「動くものは欲しいが中身も理解したい」ときの選択肢として覚えておく程度でよい。
