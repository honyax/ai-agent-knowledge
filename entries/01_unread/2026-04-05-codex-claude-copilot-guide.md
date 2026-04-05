---
date: 2026-04-05
status: unread
relevance: A
tags: [claude-code, codex, github-copilot, workflow, agents-md, comparison]
source_urls:
  - https://zenn.dev/motowo/articles/codex-claude-code-copilot-2026
  - https://zenn.dev/yuche/articles/codex-plugin-claude-code
  - https://zenn.dev/akasara/articles/d1303ce284a33f
experiment_dir: null
---

# Codex・Claude Code・Copilot を適材適所で使い分ける実践ガイド【2026年4月】

## 3行要約

- Copilot（IDE補完）+ Claude Code（設計・実装）を並行し、Codexプラグイン（`codex-plugin-cc`）でコミット前レビューをかける三段構成が推奨ワークフロー
- リポジトリルートに置く `AGENTS.md` で3ツール共通の行動規則を統一管理できる
- `codex-plugin-cc` を使うと Claude Code セッション内に Codex をプラグインとして組み込み、2つのAIが相互レビューする環境を構築できる

## 自分への関連度: A

TypeScript/Unity開発でClaude Codeを使っており、Copilotも日常的に使用している。AGENTS.mdによる統一管理と相互レビューの仕組みは即実践できる。

## 詳細

**推奨ワークフロー**:
1. Copilotでコード補完しながら書く（素早いインライン提案）
2. Claude Codeで設計判断・実装ロジックを考える（コンテキスト理解が深い）
3. `codex-plugin-cc`（事前commit review）→ GitHub PR（チームレビュー）

**AGENTS.md**:
- リポジトリルートに置くことでClaude Code・Codex・Copilot全ての行動を統一制御
- CLAUDE.mdのマルチツール版に相当する存在

**注意点**:
- レビューゲートの無限ループとStopフックのパスミスマッチバグ（Issue #59）に注意

**コア哲学**:
- Copilotで書く、Claude Codeで考える、Codexで精査する
- 重複する部分はコストと効果で判断

## 試すなら

1. `AGENTS.md` をリポジトリルートに作成し、3ツール共通のルールを記述
2. `npm install -g @openai/codex` でCodex CLIをインストール
3. Claude Codeセッションで `codex-plugin-cc` プラグインを設定
4. コミット前にCodexレビューが走るよう設定し、相互レビューを試す

## ソース

- [Codex・Claude Code・Copilot を適材適所で使い分ける実践ガイド【2026年4月】 - Zenn](https://zenn.dev/motowo/articles/codex-claude-code-copilot-2026)
- [Claude CodeにCodexプラグインを入れて、2つのAIに相互レビューさせる - Zenn](https://zenn.dev/yuche/articles/codex-plugin-claude-code)
- [Claude Code × Codex プラグインで堅牢なAIクロスレビュー環境を - Zenn](https://zenn.dev/akasara/articles/d1303ce284a33f)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
