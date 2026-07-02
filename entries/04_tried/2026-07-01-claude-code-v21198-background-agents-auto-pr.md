---
date: 2026-07-01
status: tried
relevance: S
tags: [claude-code, v2.1.198, background-agents, 自動PR, worktree, notification-hook]
source_urls:
  - https://github.com/anthropics/claude-code/releases
  - https://www.claudeupdates.dev/version/2.1.198
  - https://freeai.help/blog/claude-code-v21198-background-agents-can-now-commit_en
  - https://dev.classmethod.jp/en/articles/20260702-cc-updates-v2-1-198/
experiment_dir: null
---

# Claude Code v2.1.198: Background agents が worktree で自律的に commit / push / Draft PR まで走る

## 3行要約

- v2.1.198（7/1〜2 リリース）で、`claude agents` 経由の background agent が worktree でコード作業を終えた際、**確認を止めずに自動で commit + push + Draft PR 作成**するように。従来の「終わったら質問して停止」から「完成品を提出まで持っていく」へ挙動変更。
- 新しい **Notification hook イベント**として `agent_needs_input` と `agent_completed` を追加。background agent が入力を必要としたとき / 完了したときにフックが発火し、Slack / 通知パイプラインへ流せる。
- 32 変更のリリース。Sonnet 5 デフォルト化（v2.1.197 で完了）に続き、ループエンジニアリング的な「エージェント→検証→PR」までの Close-the-loop を harness 側で完結させる方向性。[[2026-07-01-loop-engineering-boris-cherny]] の実装補完。

## 自分への関連度: S

[[2026-07-01-loop-engineering-boris-cherny]] で書いた「loop エンジニアリング + agent loop を閉じる」を、まさに Claude Code 本体が提供してきた形。[[user_planning_workflow]] で通常モード + 独自計画書運用の自分にとって、「PR まで自動で行く」は運用の質を変えうる。CLAUDE.md の「Executing actions with care」ルール（push など前確認必須）と衝突するので、**auto-mode の設定と permission ルールでどう制御するか**を先に確認する必要がある。v2.1.183 の auto-mode guardrail ([[2026-07-01-claude-code-v21180-v21193]]) と組み合わせて設計判断。

## 詳細

### v2.1.198 主要変更

#### 1. Background agent auto-complete

- `claude agents` で起動した background agent が worktree でコード作業完了時、以下を自動実行:
  1. `git add` + `git commit`（AI が作った変更を全部）
  2. `git push`（該当ブランチへ）
  3. **Draft PR** を GitHub に作成
- 従来: 「作業終わったよ、これで push していい？」と human に confirm 要求。今回: 完成品を PR まで持っていく。
- 意図: [[2026-07-01-loop-engineering-boris-cherny]] の「エージェントが自分でループを閉じる」設計の実装。

#### 2. Notification hook 新イベント

- `agent_needs_input`: background agent が入力・許可を必要としたとき
- `agent_completed`: background agent が作業完了したとき
- 用途: Slack / Discord / Push 通知 / 独自ダッシュボードに繋いで、複数エージェントの状況を集中監視。並列エージェント運用時の「どれが止まっているか」把握。

#### 3. その他 (32 changes)

- Sonnet 5 デフォルト（v2.1.197 で完了、v2.1.198 は継続）
- Fable 5 のグローバル復活（[[2026-07-01-fable5-mythos5-export-lifted]]）に伴うルーティング調整
- MCP、hooks、UI の細部修正

### 「Close the agent loop」の実装補完

- Boris Cherny の「もうプロンプトは書かない」（[[2026-07-01-loop-engineering-boris-cherny]]）を実現する要素:
  - `/goal`: 完了条件を別モデルで判定
  - `/loop`: 反復実行
  - Dreaming: 記憶自動整理
  - **今回追加**: 完成品を PR まで持っていく自動化 = 人間の介入点を最小化
- これで「タスク投げる → 起きたら Draft PR が並んでる」ワークフローが公式実装。

### auto-mode との関係

- [[2026-07-01-claude-code-v21180-v21193]] の v2.1.183 で `git push --force` などが destructive block されるようになった。
- v2.1.198 の auto-push が block 対象になるか、確認要。通常 push は許可される想定（destructive ではない）だが、要検証。
- permission 設定で `Bash(command:git push)` を明示 allow / deny 指定できる（v2.1.178 の `Tool(param:value)` 構文）。

### 「暴走リスク」への懸念

- ミスコードが自動で Draft PR まで行くと、レビュー負荷は増える。
- CI / lint / test を PR に自動走らせ、失敗時は agent 側にフィードバックするループ設計が前提。
- Draft PR なので merge されるわけではないが、reviewer への通知は飛ぶ。チーム利用時のポリシー整備が必要。

## 試すなら

1. `npm update -g @anthropic-ai/claude-code` で v2.1.198 以降に更新、`claude --version` で確認。
2. 小さなダミーリポジトリで `claude agents` を使って簡単なタスク（例: README 誤字修正）を投げ、自動 commit/push/Draft PR が発生することを確認。
3. Notification hook を書き、`agent_completed` で Discord にメッセージを飛ばす設定を試す（[[user_hooks_usage]] の「使う動機」探し）。
4. Auto-mode の permission と衝突しないか（`git push` が destructive block されないか）を実運用ブランチで確認。必要なら `Bash(command:git push)` を allow 明示。
5. 自作 Unity プロジェクトで、`/goal` + background agents + auto-PR を組み合わせた「夜間のバグ調査ループ」を試す。翌朝に Draft PR がキューされている運用の実感を得る。
6. Notification hook 経由で「複数の並列 agent が今どこにいるか」を可視化するダッシュボード（tail -f 相当）を軽く作る。

## ソース

- [Releases · anthropics/claude-code](https://github.com/anthropics/claude-code/releases)
- [Claude Code v2.1.198 Release Notes - 32 Changes (Claude Updates)](https://www.claudeupdates.dev/version/2.1.198)
- [Claude Code v2.1.198: Background Agents Can Now Commit, Push, and Open PRs (freeai.help)](https://freeai.help/blog/claude-code-v21198-background-agents-can-now-commit_en)
- [Claude Code v2.1.197 to v2.1.198 Major Updates (Classmethod DevelopersIO)](https://dev.classmethod.jp/en/articles/20260702-cc-updates-v2-1-198/)

---

## 感想・考察

### 会社の UE5.8 環境で試した結果（2026-07-03）

会社で `claude agents` の自動 commit + push + Draft PR を試したところ:

- **push まで**: 通った
- **Draft PR 作成**: **認証で弾かれた**

推測される原因: agent が PR 作成のために内部で叩いていると思われる GitHub API 呼び出し（`gh pr create` 相当）が、会社環境の GitHub 認証（gh CLI の PAT / GitHub Enterprise の SSO / SAML SSO Enforce など）を通していない。push は git 側の credential で通るが、PR 作成は別ライヤーの認証が必要というのが実感。

### 判断: 今のところ深追いしない

「頑張れば通せるかも」だが、以下の理由で自動化は現状 push まででよいという印象:

1. **PR 作成の GitHub 認証パスを整えるコスト**が、得られる自動化の利便より重く見える（会社環境は SSO などレイヤーが厚い）。
2. **push まで自動 → PR は手動で作る**が現時点の運用としては十分。Draft PR 生成は 1 コマンドで済む。
3. [[user_planning_workflow]]（通常モード + 独自計画書運用）とも整合。エージェントに完全に任せるより、PR を作る瞬間に人間がレビューポイントを整理する方が結果的に速い可能性。

### 次に検討する条件

会社環境で以下が揃えば再検討する価値あり:

- gh CLI の GitHub Enterprise 認証がセッション永続で通っている
- CI / lint / test が失敗時に agent へフィードバックするループが組める
- `agent_completed` の Notification hook で、Draft PR 作成失敗時に人間へ通知が飛ぶ設計

### 個人 Unity プロジェクトなら

会社と違い認証が個人 GitHub 1 本なので、個人プロジェクトで再度試す価値はある（[[project_unity_to_ue5_migration]] のロジック層テスト整備タスクなど、夜間ループに向くタスクがある）。

<!-- /try 実行時に自動生成 -->
