---
date: 2026-04-27
status: read
relevance: A
tags: [claude-code, plugin, setup, automation, hooks, mcp, skills]
source_urls:
  - https://zenn.dev/shirochan/articles/1a9c4b51f4ef7b
experiment_dir: null
---

# Anthropic公式プラグイン claude-code-setup — プロジェクトに合った自動化を提案してくれる

## 3行要約

- Anthropic公式の `claude-code-setup` プラグインが登場。コードベースを解析して、そのプロジェクトに合った Hooks・MCP Servers・Skills・Subagents・Plugins を提案する
- 「自然言語で `recommend automations for this project` と話しかけると対話的にレポートを生成」する仕組み。提案内容は開発者が手動で実装する必要がある（自動適用ではない）
- 検出パターン例: Prettier 検出時 → 自動フォーマット Hook、React 検出 → Playwright MCP、`.env` 検出 → 編集ブロック PreToolUse Hook

## 自分への関連度: A

新規プロジェクトでの Claude Code 自動化セットアップを高速化できそう。特に Hooks/Skills/MCP の組み合わせを毎回ゼロから考えるコストが減る。手動実装は必要だが「何を作ればいいか」のテンプレが得られるのは大きい。既存ナレッジベースリポジトリにも適用可能。

## 詳細

### 動作フロー（3フェーズ）

1. **コードベース読み取り** — package.json、`.env`、`tsconfig.json` 等の構成ファイルを解析
2. **パターン照合** — Prettier・ESLint・React・Python・Rust 等の典型構成にマッチ
3. **レポート出力** — 5カテゴリ（Hooks/MCP/Skills/Subagents/Plugins）で提案

### 提案カテゴリ

| カテゴリ | 用途 |
|---------|------|
| Hooks | ツールイベント連動の自動処理（PreToolUse/PostToolUse） |
| MCP Servers | 外部ツール統合（Playwright、Slack、Notion 等） |
| Skills | 再利用可能なワークフロー |
| Subagents | 並列レビュアー・専門エージェント |
| Plugins | 複数 Skill のバンドル |

### 実装例

- **`.env` 保護 Hook**: PreToolUse で `.env` の編集をブロック（API キー漏洩防止）
- **自動テスト実行**: PostToolUse でファイル変更後に関連テストを実行
- **Prettier 自動フォーマット**: 変更後ファイルを自動整形

### インストール

```bash
/plugin install claude-code-setup@claude-plugins-official
/reload-plugins
```

### 注意点

- 提案精度はコードベース整備度に依存（README が薄いと提案も浅くなる）
- 自動適用機能はなく、提案された設定は手動で実装が必要
- 既存の `.claude/` 設定がある場合の差分提案については記事内で言及なし

## 試すなら

1. `/plugin install claude-code-setup@claude-plugins-official` を実行
2. `/reload-plugins` で読み込み
3. 既存プロジェクト（このナレッジベース等）で「recommend automations for this project」と問いかけ
4. 提案された Hook を1つだけ手動で `.claude/settings.json` に追加して動作確認
5. 不要な提案は無視し、既存の Skill/Hook と被らない部分だけ取り入れる

## ソース

- [Anthropic公式プラグイン『claude-code-setup』でClaude Codeの初期設定を効率化する (Zenn)](https://zenn.dev/shirochan/articles/1a9c4b51f4ef7b)

---

## 感想・考察

### ソース確認: 新規/既存どちらも対象

記事内に「Claude Code を使い始めたばかりの方にも、すでに使い込んでいるが拡張設定が後回しになっている方にも、試してみる価値はある」との記述あり。新規プロジェクト専用ではなく、むしろ既存プロジェクトのほうが設定ファイル（package.json 等）から多くの情報を読み取れて提案精度が上がる構造。提案レポートを出すだけで自動上書きはしないため、既存の `.claude/` 設定を壊す心配もない。

### 実際にこのリポジトリで試した結果

CLI で `recommend automations for this project` を実行 → いくつか提案を受けたが、このナレッジベースリポジトリでは既に必要な仕組み（カスタム Skill、エントリ管理フロー等）が揃っており、採用するほどの提案ではなかった。ナレッジベースという用途が一般的な「コード開発プロジェクト」のパターンに合いにくいのも一因と思われる。

### 価値が出そうな使いどころ

「すでに稼働中だが Claude Code 拡張設定（Hooks/Skills/MCP）が手付かずのプロジェクト」が本命。特に普段の業務リポジトリのように、毎日触っているのに `.claude/` を整備する時間が取れていないプロジェクトで効果が大きそう。次回はそういう既存プロジェクトに対して回してみる。
