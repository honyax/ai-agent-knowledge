---
date: 2026-04-03
status: read
relevance: A
tags: [claude-code, workflow, parallel, git-worktrees, productivity]
source_urls:
  - https://medium.com/@tihomir.manushev/i-run-five-claude-code-instances-in-parallel-here-is-how-i-ship-20-prs-a-day-da36da29ae50
  - https://www.anthropic.com/engineering/building-c-compiler
  - https://qiita.com/patapim/items/b2ee281889f36831af62
  - https://code.claude.com/docs/en/common-workflows
experiment_dir: null
---

# Claude Code 並列実行ワークフロー — Git Worktreesで複数インスタンスを同時起動

## 3行要約

- Git Worktrees を使って複数のClaude Codeインスタンスを独立並列実行できる。各インスタンスは別ブランチ・別ディレクトリで動作し互いに干渉しない
- Anthropic Engineering公式記事で「16エージェントが並列で10万行のCコンパイラを構築」という事例が公開され、手法の有効性が実証された
- Qiitaユーザーは3インスタンス並列で待ち時間を実質ゼロにするワークフローを報告（2〜3倍の速度向上）

## 自分への関連度: A

Unityプロジェクトでの並列フィーチャー開発や、複数の独立したゲームシステム（AI, UI, ゲームロジック等）を同時に実装する場面に直接応用できる。即実践可能。

## 詳細

**基本セットアップ**:
```bash
# 新しいworktreeを作成
git worktree add ../project-feature-a feature/ai-system
git worktree add ../project-feature-b feature/ui-refactor

# それぞれのディレクトリでClaude Codeを起動
cd ../project-feature-a && claude
cd ../project-feature-b && claude
```

**並列実行のポイント**:
- 各worktreeは完全に独立したファイルシステムを持つため、ファイル競合なし
- tmuxやターミナルのタブで複数セッションを管理するのが標準的
- 作業完了後、メインブランチにマージして後片付け

**Anthropic Engineering事例**:
- 16エージェント並列でRust製Cコンパイラを構築
- 約2,000回のClaude Codeセッションを経て10万行のコンパイラを完成
- 人間の介入なしでエージェントチームが協調して動作

**実際のワークフロー例（Qiita報告）**:
- インスタンスA: 新機能実装
- インスタンスB: バグ修正
- インスタンスC: テスト・ドキュメント生成
- 待ち時間をほぼゼロに削減

## 試すなら

1. `git worktree add ../project-worktree-2 feature/parallel-task` で2つ目のworktreeを作成
2. 別ターミナルタブ（またはtmux）で該当ディレクトリに移動してclaudeを起動
3. 独立した2つのタスクを同時に指示して並列動作を確認
4. 完了後 `git worktree remove ../project-worktree-2` でcleanup

## ソース

- [I Run Five Claude Code Instances in Parallel (Medium)](https://medium.com/@tihomir.manushev/i-run-five-claude-code-instances-in-parallel-here-is-how-i-ship-20-prs-a-day-da36da29ae50)
- [Building a C compiler with a team of parallel Claudes (Anthropic Engineering)](https://www.anthropic.com/engineering/building-c-compiler)
- [Claude Codeを3つ同時に走らせる時の自分のワークフロー (Qiita)](https://qiita.com/patapim/items/b2ee281889f36831af62)
- [Common workflows - Claude Code Docs](https://code.claude.com/docs/en/common-workflows)

---

## 感想・考察

### Git Worktreeの理解メモ

**worktreeとcheckoutの違い**

`git checkout` は同じディレクトリのファイルを上書きするため、同時に1ブランチしか扱えない。worktreeは作業ディレクトリを別フォルダに増やす仕組みで、複数ブランチのファイルに同時アクセスできる。

**worktreeと別cloneの違い**

実用上のワークフローはほぼ同じだが、内部構造が根本的に異なる。

- clone: `.git`オブジェクトストア（全履歴・全データ）を丸ごとコピーする
- worktree: `.git`オブジェクトストアを共有し、作業ディレクトリだけを別フォルダに展開する

worktreeの新フォルダに置かれる `.git` はディレクトリではなく、メインの `.git` へのパスを書いたテキストファイル（ポインタ）。各worktree固有の状態（HEADとindex）は `.git/worktrees/<name>/` に保存される。

このためworktreeの作成はほぼ瞬時でネットワーク不要。追加ディスク消費は作業ファイル分のみ。

**ブランチ間のデータ共有**

- コミット済みの変更: `.git`オブジェクトが共有されているため、プッシュなしでworktree間でマージ可能
- 未コミットの変更（編集中・stash済み含む）: 各worktreeの作業ディレクトリに閉じており、他のworktreeには一切反映されない

**主なユースケース**

フィーチャー開発中に割り込みホットフィックスが入った場合など、「作業を中断・退避（stash）せずに別ブランチの作業を並行して始める」場面が典型。stashやWIPコミットという中断の手間をなくすのが主な恩恵。

**Unityプロジェクトでの注意**

worktreeを別Unityプロジェクトとして開くことは可能（Unity Hubで複数登録）。ただし `Library/` は各worktreeに独立して生成されるため、初回起動時にフルインポートが走る。同時起動も可能だがスペック次第。cloneとの優位差はディスク節約程度で、ワークフロー体験は大きく変わらない。

