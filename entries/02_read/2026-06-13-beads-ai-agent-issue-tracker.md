---
date: 2026-06-13
status: read
relevance: A
tags: [beads, claude-code, plugin, agent-memory, issue-tracker, mcp]
source_urls:
  - https://github.com/gastownhall/beads
  - https://gastownhall.github.io/beads/
  - https://ianbull.com/posts/beads/
experiment_dir: null
---

# Beads (bd): AIコーディングエージェント向け分散グラフ問題追跡システム

## 3行要約

- AIエージェントの長期タスクで「マークダウン計画書が肥大化してコンテキストを失う」問題への解として、Doltバックエンドの依存関係グラフでタスクを管理するOSSツール。
- Claude Code / Copilot / Codex 向けにJSON出力、ハッシュベースID（衝突しない）、ready（ブロッカーなし）タスク検出、セマンティック減衰による古いタスク圧縮をサポート。
- Claude Codeに `/plugin marketplace add ./beads && /plugin install beads` でプラグイン化可。MCPサーバとして動作する。

## 自分への関連度: A

自分の運用課題に直結する。Claude Codeで複数日にまたがる作業をするとき、CLAUDE.mdや計画書だけでは「いま何が終わって何が残っているか」が曖昧になりがち。Beadsは [[user_planning_workflow]] にある「通常モード+独自フォーマット計画書」をグラフDBで補強する選択肢になりうる。既存 [[project_custom_skills]] の自作Skill群との重複度を見極めたい。

## 詳細

主要機能:
- **ハッシュベースID** (`bd-a1b2`形式) でマルチエージェント/マルチブランチでもID衝突なし。
- **依存関係グラフ**: 関連、重複排除、上書き、返信などのリンクタイプ。
- **メモリ減衰**: 古いタスクの自動圧縮でコンテキスト節約。
- **メッセージング型問題タイプ**: スレッド機能付きでエージェント間の議論を追跡。
- **2つの動作モード**: 組み込み（外部サーバ不要、単一ライター） / サーバ（複数ライター）。

主要コマンド:
- `bd ready` - ブロッカーなしの作業可能タスク一覧
- `bd create "Title"` - タスク追加
- `bd update <id> --claim` - 所有権宣言（複数エージェント協調時）
- `bd prime` - エージェント用ワークフローコンテキスト出力

Claude Code連携: 公式プラグインがあり、インストール後の再起動でMCPサーバが有効化される。`mcoquet/beads_skill` という非公式skillもコミュニティから出ている。

## 試すなら

1. `curl -fsSL https://raw.githubusercontent.com/gastownhall/beads/main/scripts/install.sh | bash` で導入。
2. 適当なテストリポジトリで `bd init` → `bd create "サンプルタスク"` で動作確認。
3. Claude Codeから `/plugin marketplace add ./beads && /plugin install beads` で接続、再起動。
4. 既存の自作計画書（[[user_planning_workflow]]）を1案件分Beadsに移してみて、グラフ管理のメリットが体感できるか評価。
5. Claude Code セッションで `bd ready` を呼ばせて、エージェントが計画書なしでも次タスクを把握できるか確認。

## ソース

- [GitHub - gastownhall/beads: Beads - A memory upgrade for your coding agent](https://github.com/gastownhall/beads)
- [Beads Documentation](https://gastownhall.github.io/beads/)
- [Beads - Memory for your Agent and The Best Damn Issue Tracker - ianbull.com](https://ianbull.com/posts/beads/)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
