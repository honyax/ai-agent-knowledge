---
date: 2026-05-18
status: read
relevance: B
tags: [cursor, ide, parallel-execution, pr-splitting, subagent, bugbot, multitask, agent-comparison]
source_urls:
  - https://cursor.com/changelog
  - https://releasebot.io/updates/cursor
  - https://blog.mean.ceo/cursor-news-may-2026/
---

# Cursor 3.3（2026-05-07） — Build in Parallel・PR 自動分割・Explore Subagent 制御 など Claude Code と機能が一気に並ぶ

## 3行要約

- 2026-05-07 リリースの Cursor 3.3 で、Claude Code とよく対比される機能が一気に並ぶ。目玉は **Build in Parallel**: ボタン一発でプランの独立部分を識別し、async subagent で同時実行。依存関係のあるステップは順序を保つ。Claude Code の worktree + 並列 agents セッションに相当
- **Split Changes into PRs**: 1セッションで作った変更を「論理的なまとまり」で複数 PR に自動分割するクイックアクション。バックアップスナップショットを取って split プランをユーザ承認させる安全設計付き。Claude Code 側の `/ultraplan` や custom skill で組まないと実現できない領域に標準機能で踏み込んだ
- 他にも **Quick Action Pills**（よく使うスキルをピン留めしてチップ表示）、**Bugbot Effort Levels**（high / custom）、**Explore Subagent controls**（モデル選択・親と同じ・無効化）、PR Review の inline thread 表示、`/multitask` 非同期サブエージェント、Cloud Agents 用 dev environment 改善（5/13）など。Claude Code 側の効率化（[[claude-code-parallel-worktrees]]、[[claude-code-routines-scheduling]]）と同型の戦いに

## 自分への関連度: B

CLAUDE.md の関心領域 9 番（他のAIコーディングエージェント比較）に該当。Claude Code を主力にしている自分の運用にすぐ影響するわけではないが、**「Claude Code に欲しい機能」のリトマス試験紙として Cursor のリリースを見る**価値がある。特に PR Splitting と Build in Parallel は、「1セッションで作りすぎて PR が肥大化する」問題への Cursor の答えであり、Claude Code 側で類似ワークフローを自作できるか考えるネタになる。

## 詳細

### Cursor 3.3 の主要機能
| 機能 | 内容 | Claude Code での近似 |
|------|------|----------------------|
| Build in Parallel | プランの独立タスクを async subagent で同時実行 | worktree + background agents、[[claude-code-parallel-no-watching]] |
| Split Changes into PRs | チャットコンテキストで論理スライス分割、PR を分けて作成 | 標準機能なし。`/ultraplan` + 手動分割 |
| Quick Action Pills | よく使うスキルをピン留めしてチップ UI | `/skills` 検索（[[claude-code-may-update-skills-async-hooks]]） |
| Bugbot Effort Levels | High / Custom（自然言語で条件指定） | `/effort` スライダ（Opus 4.7 の xhigh） |
| Explore Subagent controls | モデル選択・親継承・無効化 | Subagent ファイルで `model:` 指定 |
| `/multitask` | 非同期サブエージェント実行 | background agents + Monitor tool |
| PR Review | inline review thread / top-level comments | [[claude-code-ultrareview]]、`/review` skill |

### 後続更新
- **5/11**: Bugbot Effort Levels + Microsoft Teams integration
- **5/13**: Cloud Agents 用 dev environment（マルチリポジトリ、Dockerfile 設定、環境セキュリティ）

### 機能の方向性
- Cursor は Claude Code に対して **「IDE 統合 + マネージドクラウド + チーム管理」** の強みで差別化
- Claude Code は **「CLI/ハーネス設計の柔軟性 + プラグイン/スキル拡張 + native binary」** で対抗
- 両者の機能セットが半年でかなり収束しつつあり、ユーザの選択は「IDE 派 vs ターミナル派」に近づいている

### 興味深い差分
- Cursor の Build in Parallel は「独立部分を自動識別」する **計画段階の AI 判断** が前面に出ている
- Claude Code は明示的な worktree + agents の **明示的並列性** が中心
- 自動化の度合いと制御性のトレードオフが両者の設計哲学の差

## 試すなら

1. Cursor 3.3 を試用環境に入れ、Build in Parallel を中規模 plan に対して走らせ、`Claude Code worktree` ワークフローと比較
2. Split Changes into PRs を試し、「PR 分割の論理スライス判定」がどこまで賢いか確認
3. Cursor の Bugbot Effort と Claude Code の `/effort` の挙動差を同一バグで比較
4. 自分の Claude Code 環境にも「複数 PR に自動分割する custom skill」を作れるか検討

## ソース

- [What's New in Cursor — Latest Updates & Release Notes](https://cursor.com/changelog)
- [Cursor Release Notes - May 2026 Latest Updates - Releasebot](https://releasebot.io/updates/cursor)
- [Cursor News | May, 2026 (STARTUP EDITION)（mean.ceo blog）](https://blog.mean.ceo/cursor-news-may-2026/)

---

## 感想・考察

### Cursor は一方的に追随しているのか

機能セットは半年で収束しつつあるが、**Split Changes into PRs のように Cursor が先行した領域もある**。比較表（27-35行目）を読み直すと:

- **Cursor が追随**: Build in Parallel、`/multitask`、Explore Subagent controls 等は Claude Code 側に既存の仕組みを IDE 機能として吸収
- **Cursor が先行**: Split Changes into PRs は Claude Code 標準にない。`/ultraplan` や custom skill で組まないと再現できない

「Claude Code に欲しい機能のリトマス試験紙として Cursor を見る」という当初の評価軸（22行目）は妥当。

### Split Changes into PRs の正体

肥大化した既存 PR の分割ではなく、**PR を作る手前で1セッションの working changes を論理スライスに分けて複数 PR として作成** するクイックアクション。チャットコンテキスト（意図情報）を持っているから境界判定ができる、というのが従来 `git add -p` 系ツールとの差。

**使い道**:
- **rescue ケース**: コミット粒度を疎かにして巨大 diff を抱えた場合の救済
- **積極ケース**: 探索的に広範囲を触り、最後に AI に整理させる「コミット規律を AI 側にオフロードする」スタイル

**構造的弱点**: チャットコンテキストに依存するので、`/compact` 相当の圧縮が走ると意図情報が劣化し分割品質が落ちる。**結論として「コミット規律を完全に捨てるための機能」ではなく、「意図境界が明確なうちに最終整形を委ねる機能」** として使うのが現実的。コミット粒度 = 意図の保存単位、という古い知恵は AI 時代でも意味を持つ。

### 設計哲学の差（並列化の判断を誰がやるか）

| | 並列化の判断 | 実装作業 |
|---|---|---|
| Cursor (Build in Parallel) | AI | AI |
| Claude Code (worktree + agents) | ユーザ | AI |

Cursor は「考えるのも走らせるのも AI」で楽だが、依存関係の誤判定で衝突するリスクがある。Claude Code は worktree という物理的分離をユーザが先に作るので **そもそも衝突しようがない構造** を人間が用意する。

### game dev 文脈での適用可能性

worktree 方式は Unity/UE5 のキャッシュ問題（`Library/`、`Intermediate/`、`DerivedDataCache/` の worktree ごとの再生成）で破綻する。が、**Cursor 方式も game dev では限界がある**:

- `.meta` GUID 衝突、`.unity` シーンのマージ困難、`.uasset` のバイナリ非マージ性は worktree かどうかに関係ない構造的制約
- AI が「独立」と判断しても共通 Prefab を触れば結局衝突

**game dev で実用的な並列パターン**:
1. **役割分担型**: 1 worktree のまま、background agent にはエンジンを触らない仕事（ドキュメント、テストコード、データ生成スクリプト等）を回す
2. **読み取り専用エージェント**: コードレビュー・静的解析を並行
3. **モジュール境界での分離**: Package / Plugin 単位で独立性が高い場合のみ worktree（共有 `Library/` のシンボリックリンクは Asset Database が壊れるので非推奨）
4. **UE5 限定**: 共有 DDC をネットワーク/ローカル共有に置けば worktree 間キャッシュ共有可能

**結論**: 「Claude Code 方式は game dev に向かない」ではなく、**「worktree 並列は向かない、background agent の役割分担型なら使える」**。game dev では結局どちらの方式も "並列化対象を人間が選ぶ" 領域に収束する。

### アクション候補

- Claude Code 環境で「複数 PR に自動分割する custom skill」を作れるか検討（56行目）。チャット履歴 + `/ultraplan` 計画書を入力に `git add -p` 自動化する構成
- background agent を「エンジンに触らない仕事」（ドキュメント生成、テストコード、データ加工スクリプト）に割り当てる運用を試す。Unity プロジェクトでも適用可能
