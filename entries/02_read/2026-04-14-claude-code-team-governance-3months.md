---
date: 2026-04-14
status: read
relevance: B
tags: [claude-code, team, governance, claude-md, workflow]
source_urls:
  - https://qiita.com/hikariclaude01/items/28f79a280dd45d105ad6
experiment_dir: null
---

# チームでClaude Codeを導入して3ヶ月 — CLAUDE.md標準化・レビュー運用・ガバナンス設計の全記録

## 3行要約

- 5人チームに3ヶ月かけてClaude Codeを導入した全記録。初月は請求額が想定の3倍になり、CLAUDE.mdの解釈もバラバラになるという典型的な混乱期を経験。
- CLAUDE.mdを「Global → Team → Project → Personal」の4階層に分離し、Team層はPRレビュー必須にすることで属人化を防いだ。
- PRのdescriptionに「Claudeへの指示内容（プロンプト要約）」を記載するルールを導入し、レビュー効率が大幅に改善した。

## 自分への関連度: B

現在ソロ開発だが、将来チームでClaude Codeを使う場合の設計指針として有用。CLAUDE.md 4階層設計とPRへのプロンプト要約記載というプラクティスは今でも個人ワークフローに応用できそう。

## 詳細

### 導入初月の混乱

- 各メンバーが独自のCLAUDE.mdを書き始め、コーディング規約の解釈が分岐
- 請求額が想定の3倍（ガバナンスなしで並列実行が暴走）
- AI生成コードのレビューが「何を意図したか分からない」問題

### CLAUDE.md 4階層設計

```
Global（~/.claude/CLAUDE.md）  ← 個人の共通設定
  └─ Team（.claude/team/CLAUDE.md）  ← チーム共通規約・PRレビュー必須
      └─ Project（CLAUDE.md）  ← プロジェクト固有の設定
          └─ Personal（.claude/personal/CLAUDE.md）  ← 個人の作業スタイル
```

Team層の変更にPRレビューを必須化することで、暗黙知のコード化と属人化防止を両立。

### PRへのプロンプト要約記載ルール

PRのdescriptionに以下を追加:
```
## Claudeへの指示
<Claudeに与えたプロンプトの要約>
```

レビュアーが「何を頼んだか」を把握できるため、AI生成コードのレビュー効率が大幅改善。

### 3ヶ月後の結果

- 安定期にはコード品質指標が導入前を上回る水準に
- コストも初月比で大幅に削減

## 試すなら

1. 記事を読んで4階層CLAUDE.md設計を理解
2. 個人ワークフローで「Global vs Project」の2階層から試す
3. PRにプロンプト要約を記載するルールを自分のプロジェクトで試す

## ソース

- [チームでClaude Codeを導入して3ヶ月 — CLAUDE.md標準化・レビュー運用・ガバナンス設計の全記録 - Qiita](https://qiita.com/hikariclaude01/items/28f79a280dd45d105ad6)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
