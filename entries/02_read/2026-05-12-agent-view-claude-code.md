---
date: 2026-05-12
status: read
relevance: B
tags: [claude-code, agent-view, v2.1.139, multi-session, background, parallel, dashboard, research-preview]
source_urls:
  - https://claude.com/blog/agent-view-in-claude-code
  - https://code.claude.com/docs/en/agent-view
  - https://github.com/anthropics/claude-code/releases/tag/v2.1.139
  - https://www.buildfastwithai.com/blogs/claude-code-agent-view-guide
experiment_dir: null
---

# Claude Code に Agent View 搭載（v2.1.139, 2026-05-11 Research Preview） — 複数バックグラウンドセッションを1画面で管理

## 3行要約

- 2026-05-11、Claude Code v2.1.139 で **Agent View** が Research Preview として公開。**並行実行中の複数 Claude Code セッションを1つの CLI ダッシュボードで一覧管理** できる。左矢印キーまたは `claude agents` で起動
- 各行に **(1) セッション状態（入力待ち／処理中／完了）、(2) 最後のレスポンス内容、(3) 最後のインタラクション時刻** を表示。`/bg` または `claude --bg [task]` で新規セッションをバックグラウンドへ送り、必要になったタイミングだけ画面を呼び戻す運用が成立
- 対象: Pro / Max / Team / Enterprise / API 利用者すべて。標準レート制限が適用。**Managed Agents の Multi-agent orchestration（5/6 公開）と組み合わせると、ローカル並行セッション ↔ クラウド並行 subagent を同じメンタルモデルで扱える**

## 自分への関連度: S

これは自分のワークフローに直撃する Tier-S 機能:

- **catch-up / digest / try Skill 並列化**: 現在は一つの Claude Code 端末で逐次実行している自前ナレッジ Skill 群が、別セッションでバックグラウンド化できる。「catch-up を回しながら別 PR をレビュー」が成立
- **Worktree との組合せ**: 2026-05-10 v2.1.127-133 で worktree が **ローカル HEAD 派生に修正** された直後の Agent View 公開は、Anthropic が「並列 worktree × バックグラウンドセッション」を一級ワークフローとして本気で推している証拠
- **「呼び戻すタイミングだけ画面に戻る」操作モデル**: VSCode ネイティブ拡張ユーザの自分にとって、ターミナル側で完結する dashboard は IDE 切替コストを下げる
- **rkaga harness engineering（2026-05-02）** が言う「別エージェント・別セッションによる検証ループ」を、ローカル側で実現する直接の手段

`/fewer-permission-prompts`（2026-05-10 エントリ）で承認プロンプト数を減らした上で Agent View を回せば、本当に「人間が画面を見るのは Claude が止まった時だけ」というスタイルに到達できる。

## 詳細

### 起動方法

- **左矢印キー**: 通常の対話セッション内から Agent View へ切り替え
- **`claude agents`**: 専用コマンドで Agent View を直接起動
- **`/bg`** (対話中): 現セッションをバックグラウンドへ送る
- **`claude --bg [task]`**: 新規セッションを最初からバックグラウンド起動

### 表示される情報（各行）

| カラム | 内容 |
|--------|------|
| 状態 | running / blocked on you（入力待ち）/ done |
| 最終レスポンス | 最後にエージェントが返した内容のスニペット |
| 最終インタラクション | あなたが最後に応答した時刻 |

### インライン応答

セッションを選択して中身を確認し、意思決定が必要な場合は **画面遷移せずその場で回答** できる。フル対話に戻りたい場合は通常通り遷移。

### 想定ユースケース（公式ブログより）

1. **複数アイデアを並行検証**: 3〜5本の方針案を別セッションで同時走らせ、最初に結果が出たものから採用
2. **長時間稼働エージェント監視**: PR 管理ダッシュボード更新、夜間 routine 等
3. **PR レビュー横断**: 複数 PR への対応セッションを束ねて管理
4. **ブロッキング時の切替**: A セッションが confirmation 待ちになったら B に移って作業継続

### 利用条件

- **Research Preview** 段階（GA ではない）
- 対象: Pro / Max / Team / Enterprise / API ユーザ全プラン
- 標準レート制限が適用される（バックグラウンドも消費）

### v2.1.139 のその他変更点

- **`/usage` の weekly reset 表示が時刻ではなく日付に**修正
- **`/usage` Ctrl+S スクリーンショットコピーが Linux/X11 でハングする問題** を修正
- ただし `/usage` の進捗バーが特定条件下でレンダリングされない **regression** が報告中（[Issue #58111](https://github.com/anthropics/claude-code/issues/58111)）— v2.1.140 以降での hotfix を待つ

### Managed Agents との関係

- **Local 並列**: Agent View（本機能、CLI 1台で複数セッション）
- **Cloud 並列**: Multi-agent orchestration（2026-05-06 公開、lead → subagent）

両者を併用すると「ローカル端末で 5 セッションを束ねて、各セッションがクラウド側で multi-agent オーケストレーションを呼ぶ」という二重並列が成立。

### サードパーティ可視化ツールとの位置づけ

非公式の Claude Code 並列管理ツール（`patoles/agent-flow`、`disler/claude-code-hooks-multi-agent-observability`、`claude-studio` 等）が存在していたが、公式機能としての Agent View 登場で **「観察可能性は公式、付加価値レイヤはサードパーティ」** の構図が固まる見込み。

## 試すなら

1. `claude update` で v2.1.139（または v2.1.140+ 公開後）にアップデート
2. 適当なタスク（例: `/catch-up` 相当の長時間処理）を 1 つ起動して、対話途中で `/bg` を試す
3. もう 1 つ別タスクを `claude --bg "..."` で起動し、Agent View（左矢印 or `claude agents`）に切替
4. 両セッションの状態確認＋一方が confirmation 待ちになったらインライン応答
5. 自前 Skill（`/catch-up`・`/digest`・`/try`）の中で **時間がかかるもの＝バックグラウンド化できるもの** を仕分けし、運用パターンを記述化

## ソース

- [Agent view in Claude Code（Claude 公式ブログ）](https://claude.com/blog/agent-view-in-claude-code)
- [Manage multiple agents with agent view（Claude Code Docs）](https://code.claude.com/docs/en/agent-view)
- [Release v2.1.139（GitHub）](https://github.com/anthropics/claude-code/releases/tag/v2.1.139)
- [Claude Code Agent View: Manage Multiple AI Agents in One Dashboard（BuildFastWithAI）](https://www.buildfastwithai.com/blogs/claude-code-agent-view-guide)

---

## 感想・考察

### 自分の運用への当てはめ: S → B に格下げ

公式発表のインパクトとしては Tier-S だが、**自分の環境（VSCode ネイティブ拡張＋複数タブで並列セッション運用）に照らすと B 相当**。理由:

- **すでにタブが「UI スロット付きの Agent View」として機能している**: Agent View 最大の売りである「セッション一覧」「選択→切替」は VSCode タブで既に実現済み。各タブが視認可能な状態カードの役割を果たしているので、CLI ダッシュボードへ切替える動機が薄い
- **`/bg`・`claude --bg` だけは固有価値が残る**: 「タブを占有せずバックグラウンドで走らせる」運用は VSCode タブでは再現できない。ただし、自分の現状は同時 2〜3 セッションが上限で、タブが枯渇する状況には至っていない。**5 本以上の並行 / 夜間 routine** を回し始めたら再評価
- **VSCode ネイティブ拡張からの起動性が未確認**: Agent View は CLI 側の機能で、左矢印キーや `claude agents` がネイティブ拡張の UI と競合する可能性。試すには別途ターミナルで `claude` を起動する必要があり、その時点で「VSCode 内完結」のメリットを失う（[[user_environment]] と整合）

### いつ再評価するか

- 並列セッション数が常時 5 本超になった時
- `/catch-up`・`/digest`・`/try` を本格的にバックグラウンド化したくなった時（現状は逐次で問題なし）
- VSCode ネイティブ拡張側で Agent View 相当の UI が統合された時（こちらの方が筋が良さそう）

当面は「タブ運用で困った時に思い出す」程度のフォロー対象に留める。実践（04_tried 化）は保留。
