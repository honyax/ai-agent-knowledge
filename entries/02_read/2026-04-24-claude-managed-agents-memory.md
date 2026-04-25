---
date: 2026-04-24
status: read
relevance: B
tags: [claude-api, managed-agents, memory, persistent, beta]
source_urls:
  - https://claude.com/blog/claude-managed-agents-memory
  - https://platform.claude.com/docs/en/managed-agents/memory
  - https://sdtimes.com/anthropic/anthropic-adds-memory-to-claude-managed-agents/
experiment_dir: null
---

# Claude Managed Agents に永続メモリ機能（Memory Store）追加 — パブリックベータ開始

## 3行要約

- 2026年4月23日、Claude Managed Agentsにセッション跨ぎで状態を保持する「Memory Store」機能がパブリックベータで追加された
- メモリはファイルシステムに直接マウントされ、Claudeがbash・コード実行で書き込み・整理でき、APIでエクスポート・管理可能
- 全ての書き込みがコンソールのsession eventに記録され、ロールバックや編集も可能。企業利用での監査に対応

## 自分への関連度: B

Claude Code CLIをメインワークフローとしている自分には直接は関係しない。ただし、Managed Agentsを使った自動化を自作する場合（ゲーム内AIアシスタント等）には長期記憶の選択肢として有力。

## 詳細

### Memory Storeの仕組み

- デフォルトではManaged Agentsセッションは毎回フレッシュコンテキストから始まる
- Memory Storeをマウントすると、エージェントが書き込んだ内容がストアに永続化され、同じストアを共有するセッション間で同期される
- ファイルシステムとしてマウントされるため、Claudeが bash・コード実行で自然にファイル操作として扱える

### 企業利用向けの機能

- 全ての書き込みがClaude Consoleのsession eventとして記録
- エージェントが何をいつ学んだかトレース可能
- 問題発生時は個別の書き込みをロールバックまたは編集できる
- API経由でのエクスポート・管理に対応

### Managed Agents本体との関係

- Managed Agents自体は2026年4月8日にパブリックベータ開始
- 今回のMemoryはその拡張機能、別途ベータで提供
- 関連エントリ: [2026-04-09 Managed Agents + ant CLI](../02_read/2026-04-09-claude-managed-agents-ant-cli.md)

## 試すなら

1. Managed Agents公式ドキュメントの Memory セクションを確認
2. 既存のManaged Agentsセッションが無ければ、まず本体のクイックスタートを実施
3. Memory Storeをマウントした最小エージェントを試作
4. bashでメモリ内容を確認してどう整理されているか把握
5. ロールバック操作をConsoleから試してワークフローを評価

## ソース

- [Built-in memory for Claude Managed Agents (Anthropic Blog)](https://claude.com/blog/claude-managed-agents-memory)
- [Using agent memory (Claude API Docs)](https://platform.claude.com/docs/en/managed-agents/memory)
- [Anthropic adds memory to Claude Managed Agents (SD Times)](https://sdtimes.com/anthropic/anthropic-adds-memory-to-claude-managed-agents/)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
