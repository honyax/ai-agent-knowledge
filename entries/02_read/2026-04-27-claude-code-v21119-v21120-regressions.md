---
date: 2026-04-27
status: read
relevance: S
tags: [claude-code, release, regression, bug, workaround, version-pinning]
source_urls:
  - https://qiita.com/yurukusa/items/61fa9b858f9687e899d4
  - https://gist.github.com/yurukusa/a866b4cd2976486156a00c190c39cef6
  - https://qiita.com/moha0918_/items/54918006b98ea36880cc
experiment_dir: null
---

# Claude Code v2.1.119/v2.1.120 で発生した8つの退行バグ — v2.1.117 へのピン推奨

## 3行要約

- v2.1.119 と v2.1.120 が 24 時間以内に立て続けにリリースされ、合計 8 件の重大な退行バグが報告された（自動更新破損・モデル黙々スワップ・resume クラッシュ・CLAUDE.md 無視等）
- 最も致命的なのは「opus-4-7 を選んでも 1M 版に黙ってスワップされて課金が増える」「`--resume` が TypeError でクラッシュ」「CLAUDE.md がコンテキスト容量に余裕があるのに参照されない」の3つ
- 推奨される最速の退避策は v2.1.117 へのダウングレード + 自動更新無効化（`DISABLE_UPDATES=1`）

## 自分への関連度: S

自動更新が有効なら既に被弾している可能性が高い。CLAUDE.md 無視バグは特にこのナレッジベース運用に直結する致命傷。即座に手元の Claude Code バージョンを確認し、影響を受けているなら v2.1.117 にピンする必要がある。`DISABLE_UPDATES` の存在は v2.1.118 エントリで把握済み。

## 詳細

### 8つの退行バグ一覧

| # | バグ | 影響 | 回避策 |
|---|------|------|--------|
| 1 | `--resume` 起動時 TypeError クラッシュ | 既存セッション復帰不可 | v2.1.117 へダウングレード |
| 2 | モデル選択無視（opus-4-7 → 1M 版へ黙々スワップ） | 課金増・性能差 | `/status` で実モデル確認 |
| 3 | UI 重複表示（リサイズ時） | 表示崩壊 | `Ctrl+L` で一時回避 |
| 4 | 自動更新が動作せず | パッチ適用不能 | 手動 `claude update` |
| 5 | WSL2 で `/mcp` メニュー凍結 | MCP 操作不能 | 新規セッション開き直し |
| 6 | CLAUDE.md 無視（コンテキストに余裕あり） | プロジェクト指示が効かない | 明示的な再読み込み指示 |
| 7 | `sandbox.excludedCommands` 失敗 | 一部 CLI 動作不能 | 設定見直し |
| 8 | Worktree 作成時のハング | git 操作ブロック | 標準ブランチ切替に変更 |

### 最速の退避手順

```bash
# 1. v2.1.117 へピン
npm install -g @anthropic-ai/claude-code@2.1.117

# 2. 自動更新を無効化
export DISABLE_UPDATES=1

# 3. シェル再起動
```

### v2.1.120 で意図された改善（参考）

- PowerShell ツールでダブルクォート + 空白を含む引数の auto-allow を撤廃
- `/env` が PowerShell ツールにも適用（従来は Bash のみ）
- `/usage` で「Current week (Sonnet only)」バーを Pro/Enterprise 向けに非表示
- `X-Claude-Code-Session-Id` ヘッダ追加（プロキシでセッション集約可能）
- `.jj`・`.sl`（Jujutsu/Sapling）を VCS 除外リストに追加
- `--resume` の "tool_use ids without tool_result blocks" エラー修正（v2.1.85 以前のセッション）

しかし上記改善より退行が深刻、というのが現状。

### 関連する過去エントリ

- [v2.1.118 リリース (2026-04-24)](../02_read/2026-04-24-claude-code-v21118-vim-themes.md) ← `DISABLE_UPDATES` の初出
- [品質低下ポストモーテム (2026-04-24)](../02_read/2026-04-24-claude-code-quality-postmortem.md) ← ハーネス側の品質問題

## 試すなら

1. `claude --version` で現バージョン確認
2. v2.1.119 または v2.1.120 ならば `npm install -g @anthropic-ai/claude-code@2.1.117`
3. `DISABLE_UPDATES=1` を環境変数に追加
4. `/status` でモデル名（opus-4-7 が選ばれているか）を毎回確認する習慣化
5. CLAUDE.md が読まれていないと感じたら明示的に「@CLAUDE.md を再読み込みして」と指示

## ソース

- [Claude Code v2.1.119/v2.1.120で報告された8つの退行バグと回避策 (Qiita)](https://qiita.com/yurukusa/items/61fa9b858f9687e899d4)
- [Claude Code v2.1.119/v2.1.120 Survival Checklist (GitHub Gist)](https://gist.github.com/yurukusa/a866b4cd2976486156a00c190c39cef6)
- [Claude Code v2.1.120 リリース｜毎日Changelog解説 (Qiita)](https://qiita.com/moha0918_/items/54918006b98ea36880cc)

---

## 感想・考察

### 2026-04-30 追記: v2.1.123 時点での状況確認

手元バージョンが v2.1.123 だったため、v2.1.121〜v2.1.123 の公式 Changelog で本エントリの8件のバグの修正状況を確認した。

**明示的に修正済み（ダウングレード不要）**

- #1 `--resume` クラッシュ → v2.1.121 で修正（"Fixed `--resume` crashing on startup in external builds" / "Fixed `--resume` failing on large sessions with corrupted transcript lines"）
- #3 UI 重複表示 → v2.1.121 で修正（"Fixed scrollback duplication with Ctrl+L or redraw in non-fullscreen mode"）
- #4 自動更新が動作せず → v2.1.123 まで自動更新で到達できている時点で実質解消

**Changelog に明示的記載なし（要警戒継続）**

- #2 モデル黙スワップ（opus-4-7 → 1M）
- #5 WSL2 で `/mcp` 凍結
- #6 CLAUDE.md 無視（auto-load 時の表示バッジ重複修正は v2.1.123 にあるが、「無視」自体への直接言及なし）
- #7 `sandbox.excludedCommands` 失敗
- #8 Worktree 作成時のハング

**運用方針**

v2.1.117 へのピンは不要。ただし #2 と #6 は本ナレッジベース運用に直結するため、`/status` での実モデル確認、CLAUDE.md の効きの体感確認、効いていない場合は `@CLAUDE.md` での明示再読み込み指示、を継続する。

**教訓**

「リリース直後の x.y.z は数日待ってから入れる」運用が、自動更新環境でも `DISABLE_UPDATES=1` で実現できるという選択肢を、退行が立て続けに出た時の安全策として頭の引き出しに入れておく。今回は数日後の v2.1.123 で主要な退行が解消されたため、結果的にダウングレードせず待っていればよかった。
