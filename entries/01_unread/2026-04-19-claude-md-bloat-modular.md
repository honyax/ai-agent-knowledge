---
date: 2026-04-19
status: unread
relevance: A
tags: [claude-code, claude-md, workflow, context-management]
source_urls:
  - https://qiita.com/sinsinshosann23/items/f315d78af169305d1128
  - https://qiita.com/tomada/items/cb05d3a7aa00cb35c486
experiment_dir: null
---

# CLAUDE.md 1200行を超えて破綻した話 - モジュール分割で解決

## 3行要約

- グローバルCLAUDE.mdは60行（約1,200トークン）を超えると重要な指示がノイズに埋もれ始める
- `.claude/rules/` ディレクトリに機能別ルールを分割し、グローバルはインデックスのみに徹する設計が有効
- グローバル・プロジェクト・モジュール層の3層構造でモノレポでも破綻しない運用が可能

## 自分への関連度: A

自分のグローバルCLAUDE.mdも現在増殖傾向にある。60行ルールとモジュール分割の設計パターンは即座に適用できる実践的知見。特に今後複数プロジェクトで共通ルールと個別ルールを整理する際に役立つ。

## 詳細

**問題の発端**

CLAUDE.mdが成長するにつれ、Claudeが重要な指示を無視するようになる。原因はトークン経済: 60行（約1,200トークン）を超えるとコンテキストウィンドウ内でルールが埋もれ、Claudeの参照優先度が下がる。

**解決アーキテクチャ: 3層構造**

```
~/.claude/CLAUDE.md         # グローバル層: 60行以内のインデックスのみ
  └─ 詳細は各ファイルへのリンク

[project]/.claude/CLAUDE.md  # プロジェクト層: プロジェクト固有の基本方針
  └─ [project]/.claude/rules/ # モジュール層: 機能別ルールを外出し
       ├─ security.md
       ├─ testing.md
       └─ coding-style.md
```

**グローバルCLAUDE.mdの設計指針**
- 60行（約1,200トークン）を上限とする
- 内容はインデックス（詳細ファイルへの参照）のみに徹する
- 詳細ルールは `@path/to/rules.md` 形式でインポート

**`.claude/rules/` の動的ロード**
- Qiitaの別記事（tomada氏）では、`.claude/rules/` ディレクトリの特定ファイルをタスクに応じてロードする方法を紹介
- 不要なルールをロードしてClaudeを混乱させる問題を解消

**この設計で解決する問題**
- プロジェクトCLAUDE.mdの肥大化
- 不要なルールがClaudeの判断を混乱させる
- モノレポで複数コンテキストが混在する

## 試すなら

1. 現在のグローバル `~/.claude/CLAUDE.md` の行数を確認
2. 60行を超えていた場合、`~/.claude/rules/` ディレクトリを作成してルールを分割
3. グローバルCLAUDE.mdをインデックス化し、`@rules/XXX.md` 形式で参照
4. プロジェクト側も同様に `.claude/rules/` で機能別に分割
5. 分割後、Claudeの指示遵守度が改善するか確認

## ソース

- [CLAUDE.mdが1,200行を超えて破綻した話 - Qiita](https://qiita.com/sinsinshosann23/items/f315d78af169305d1128)
- [CLAUDE.mdの肥大化を防ぐ！.claude/rules/で動的にルールを読み込む方法 - Qiita](https://qiita.com/tomada/items/cb05d3a7aa00cb35c486)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
