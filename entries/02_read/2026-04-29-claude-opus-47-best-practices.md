---
date: 2026-04-29
status: read
relevance: S
tags: [claude-code, opus-4-7, best-practices, workflow, hooks, effort, focus-mode]
source_urls:
  - https://qiita.com/ot12/items/06420caf41a34a910c53
experiment_dir: null
---

# Claude Opus 4.7 ベストプラクティス — 卒業すべき6つの旧作法

## 3行要約

- Opus 4.7 リリース（2026-04-16）に伴い、Anthropic 公式と Boris Cherney（Claude Code 作者）が「これまでの使い方」のうち卒業すべき 6 つの旧作法を明示
- 主な変更点: 細かい指示のペアプロは逆効果に・effort は max ではなく **xhigh** がデフォルト最適・`--dangerously-skip-permissions` 廃止・Subagent は毎回呼ばない・Stop Hook での自動検証が最高効果施策
- 4.7 の設計思想は「ツール呼び出しより推論優先」「長時間自律実行性能の向上」。前提として Claude Code v2.1.111 以上が必須

## 自分への関連度: S

Opus 4.7 は既にデフォルトモデル（自分の環境）で利用中。これまでの「細かく指示するほど賢く動く」前提を変える必要があり、現在の自分のワークフロー（細かい確認を多用）を見直す直接的な必要がある。Stop Hook での自動検証は未導入で、即実装の価値が高い。

## 詳細

### 卒業すべき6つの旧作法

| # | 旧作法 | 4.7 での新作法 |
|---|--------|----------------|
| 1 | 細かく指示するペアプロ方式 | 初回プロンプトに Goal・Constraints・Acceptance criteria をまとめて提示し介入を減らす |
| 2 | effort = max を常用 | デフォルトは **xhigh**（max は考えすぎ傾向） |
| 3 | `--dangerously-skip-permissions` で権限無視 | Auto Mode（Max+ プラン）または `/fewer-permission-prompts` |
| 4 | 長時間セッションを見守り続ける | Focus Mode（`/focus`）と Recaps（`/recap`）で結果のみ受け取る（**VSCode 拡張では現時点で両方非対応**: `/focus` は fullscreen renderer 依存、`/recap` はターミナルフォーカス検出に依存。Recaps は VSCode 移植要望あり、将来対応の可能性中〜高） |
| 5 | 毎回 Subagent を指示 | 並列作業や独立タスク時のみ明示的に呼ぶ |
| 6 | 検証は人間任せ | Stop Hook でテスト/スクリーンショット自動検証（**最高効果施策**） |

### 推奨設定

```bash
claude --version
# 2.1.111 以上が必須

/model opus
/effort xhigh
```

### Stop Hook 設定例（Node.js プロジェクト）

```json
{
  "hooks": {
    "Stop": [
      {
        "hooks": [{ "type": "command", "command": "npm test" }]
      }
    ]
  }
}
```

### Task Budget（API、beta）

エージェントトークン予算を API で指定可能。長時間自律実行時の予算制御に有効。

### チェックリスト

- [ ] Claude Code v2.1.111 以上
- [ ] Opus を使用（Sonnet ではなく）
- [ ] effort = xhigh をデフォルト化
- [ ] 初回プロンプトを構造化（Goal/Constraints/Acceptance）
- [ ] Stop Hook に検証コマンドを組み込み

### 関連する過去エントリ

- [Claude Opus 4.7 リリース (2026-04-17)](../02_read/2026-04-17-claude-opus-47-release.md) ← 機能面の解説
- [Claude Code 品質低下ポストモーテム (2026-04-24)](../02_read/2026-04-24-claude-code-quality-postmortem.md) ← effort medium に黙々下げられていた事件
- [v2.1.119/v2.1.120 退行バグ (2026-04-27)](../01_unread/2026-04-27-claude-code-v21119-v21120-regressions.md) ← v2.1.111 以上の前提と矛盾しない最新版

## 試すなら

1. `/effort xhigh` を実行してデフォルト変更（max を使っていた場合）
2. 次のタスクで初回プロンプトに Goal/Constraints/Acceptance criteria を構造化して書く
3. `.claude/settings.json` に Stop Hook を追加（プロジェクトのテストコマンド）
4. Subagent 起動を「並列・独立タスク時のみ」に絞る運用を1週間試す
5. `/focus` を長時間タスクで試し、Recap の使い心地を確認

## ソース

- [Claude Opus 4.7 ベストプラクティス：6つの旧作法の終焉 (Qiita / ot12)](https://qiita.com/ot12/items/06420caf41a34a910c53)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
