---
date: 2026-03-24
status: unread
relevance: S
tags: [claude-code, settings, claude-md, hooks, skills, mcp, permissions]
source_urls:
  - https://qiita.com/emi_ndk/items/56b2fc8bf4e7ed5ba7f3
experiment_dir: null
---

# Claude Code完全設定ガイド2026 — 7つの設定レイヤーを体系化

## 3行要約

- Claude Codeの設定を7レイヤー（CLAUDE.md、Auto Memory、rules、settings.json、Hooks、Skills、MCP）に体系化した包括ガイド
- CLAUDE.mdの黄金律: 「人間だけが知っていること」（なぜこのアーキテクチャか、過去のインシデント、ビジネス制約）を記載し、ソースから読み取れる情報は書かない
- 権限設定は最小権限の原則に基づき許可リスト方式。設定全体をパッケージ化しワンコマンドでプロビジョニング可能

## 自分への関連度: S

Claude Codeの設定を体系的に理解・整備するための決定版ガイド。自分のCLAUDE.mdの書き方やHooks・Skills・MCP設定の見直しに直結する。特にCLAUDE.mdの黄金律（コードから読み取れないことだけ書く）は即実践可能。

## 詳細

### 7つの設定レイヤー
1. **CLAUDE.md** — 指示書（プロジェクトの文脈・制約）
2. **Auto Memory** — 自動学習（ユーザーの好みや修正パターン）
3. **.claude/rules/** — 条件付きルール（ファイルパターンに応じた指示）
4. **settings.json** — 権限管理（コマンド許可リスト）
5. **Hooks** — 17イベントの自動化（PreToolUse、PostToolUse等）
6. **Skills** — カスタムコマンド（スラッシュコマンド）
7. **MCP** — 外部ツール連携

### 設定の優先順位
管理ポリシー → CLIフラグ → ローカル設定 → 共有設定

## 試すなら

1. 記事を精読し、7レイヤーの全体像を把握
2. 自分のCLAUDE.mdを「黄金律」に沿って見直し
3. .claude/rules/にファイルパターン別のルールを追加
4. Hooksで頻出パターン（テスト実行、lint等）を自動化

## ソース

- [Claude Code完全設定ガイド2026（Qiita）](https://qiita.com/emi_ndk/items/56b2fc8bf4e7ed5ba7f3)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->

