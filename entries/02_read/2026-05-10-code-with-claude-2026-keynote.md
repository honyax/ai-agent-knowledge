---
date: 2026-05-10
status: read
relevance: S
tags: [anthropic, claude-code, code-with-claude, conference, routines, managed-agents, dreaming, outcomes, multi-agent, rate-limit, keynote]
source_urls:
  - https://simonwillison.net/2026/May/6/code-w-claude-2026/
  - https://claude.com/blog/introducing-routines-in-claude-code
  - https://claude.com/blog/new-in-claude-managed-agents
  - https://www.lennysnewsletter.com/p/code-with-claude-the-5-biggest-updates
  - https://www.contextstudios.ai/blog/code-with-claude-the-may-6-readiness-field-guide
  - https://qiita.com/kai_kou/items/ba88f403caf78fe5242b
experiment_dir: null
---

# Code w/ Claude 2026 基調講演（5/6 SF） — Routines GA・Managed Agents 3新機能・レート制限2倍・SpaceX Colossus

## 3行要約

- 2026-05-06、サンフランシスコの招待制 Claude 開発者カンファレンス「Code with Claude 2026」開催。新モデル発表はなく **既存モデルの上で動かす"ハーネス層"の刷新** に焦点。Claude Code Routines（クラウド自動化）が GA、Managed Agents に Dreaming・Outcomes・Multi-agent orchestration の3新機能、Pro/Max/Enterprise の Claude Code 5時間レート制限を **2倍** に拡大、ピーク時間帯の制限も撤廃
- Routines: 「プロンプト＋レポ＋コネクタ」を一度設定すると、cron/API/webhook トリガで Claude Code クラウドが自律実行。GitHub webhook は PR ごとに永続セッションを保持。Pro 5/日・Max 15/日・Team & Enterprise 25/日 の daily limit
- Managed Agents 三本柱: **(1) Dreaming** = スケジュールバッチで過去セッションを内省し記憶を更新（research preview）、**(2) Outcomes** = 成功基準ルーブリックを別 grader が判定し自律修正（社内検証で +8.4% 文書生成タスク改善）、**(3) Multi-agent orchestration** = lead が subagent に並列委譲（共有ファイルシステム上、Console で実行系列を可視化）。後者2つは public beta

## 自分への関連度: S

関心領域 1（Claude Code 新機能）と 2（実践運用ノウハウ）に直撃する大型アップデート群。特に:

- **Routines GA**: 自分のナレッジベースの `/catch-up` を「Routines で毎週日曜2時に自動実行→PR で要約を提案」へ昇格できる可能性。既存の自前 catch-up Skill との統合余地あり（既存エントリ 2026-04-18-claude-code-routines-scheduling.md は research preview 段階の解説、本エントリは GA 後の運用情報をカバー）
- **レート制限2倍**: Max プラン利用者として直接の体感メリット。ピーク帯の絞りが消えることで催促されにくくなる
- **Managed Agents Dreaming**: 自前の auto-memory システム（`MEMORY.md` + 個別 .md）の上位互換的アイデア。クラウド側で自動キュレートされる仕組みを試しに見ておきたい
- **Multi-agent orchestration**: 自前 Skill 群（catch-up・digest・try）を lead → subagent 構造で並列化する設計と整合。既存エントリ 2026-04-03-claude-code-agent-teams.md・2026-04-23 ハーネス・2026-05-02 rkaga 発表 の延長線

「新モデルは出ず、ハーネス層を厚くする」という方針自体が、自分が追っている harness engineering の流れと完全に重なる。

## 詳細

### Claude Code Routines（GA）

**仕組み**: プロンプト・リポジトリ・コネクタを一度設定すると、Claude Code のクラウドインフラ上で実行される自律タスク。3種類のトリガ:

1. **Scheduled**: hourly / nightly / weekly。例: 「毎晩2時に Linear のトップバグを取り、修正試行して draft PR を開く」
2. **API**: 各 routine に専用エンドポイント＋認証トークン。CI・モニタリング・社内ダッシュボードから叩ける
3. **Webhook**: 現状 GitHub のみ。`Claude will create a new session for every PR matching your filters` で PR ごとに永続セッションが立ち、コメントや CI 失敗にも続けて反応

**Daily limit**: Pro 5 / Max 15 / Team & Enterprise 25。超過分は追加課金。

**ユースケース**: Backlog triage + Slack 要約 / docs drift 検出と修正 / post-deploy smoke test / アラート相関 / Python→Go ポーティング / カスタム PR レビューチェックリスト

### Claude Managed Agents — 三新機能

| 機能 | 状態 | 概要 | 内部数値 |
|------|------|------|----------|
| **Dreaming** | research preview（要申請） | 過去セッション・記憶ストアを定期スキャンし、繰り返しの間違い・収束したワークフロー・チーム共通の好みをパターン抽出して memory を curate | — |
| **Outcomes** | public beta | 成功基準を rubric として書き、別の grader agent が出力を採点。タスクごとに自律修正 | 内部テストでタスク成功率 +最大10pt、文書生成タスクで +8.4% |
| **Multi-agent orchestration** | public beta | lead agent が subagent（独自モデル・プロンプト・ツール）に並列委譲。共有 filesystem。Claude Console で系列可視化 | 顧客例: Harvey（法務文書6倍速）、Netflix（並列ログ解析）、Spiral（編集ガイドライン強制）、Wisedocs（書類審査50%短縮） |

### Claude Code 製品アップデート

- **Code Review tool**: Anthropic 全社で利用中（社内 dogfooding 完了）
- **Remote Agents**: スマホからラップトップを操作・制御
- **CI auto-fix**: PR の CI 失敗を自動修正
- **Security Reviews**: 自動脆弱性検出
- **Desktop application**: フルスクリーン GUI とプレビュー機能

### インフラ・契約

- **SpaceX × Colossus 1（Memphis）**: 全コンピュート容量 300MW・H100/H200/GB200 計 220k GPU を Anthropic が確保。今月中に利用開始。Pro/Max のキャパシティ改善に直結。さらに**「複数GW 規模の orbital AI compute」** にも合意（軌道上データセンター構想）
- **Pro/Max/Team/Enterprise レート制限2倍**: Claude Code 5時間枠が倍増、ピーク時間帯の絞り撤廃
- **API 利用量 17倍 YoY**: Anthropic 公式数字

### 「新モデル発表なし」の意味

会場の関心は「Opus 4.7 の上で何を動かすか」へシフト。Anthropic は **advisor strategy**（大きいモデルが小さいモデルを補助して全体コスト効率を上げる）を強調。新モデルではなく **ハーネス（Routines・Managed Agents・連携機能）** で性能を引き出す方針。

## 試すなら

1. Claude Code を最新版（v2.1.129 以降）にアップデート
2. Web 版 Claude Code（claude.com）にログインし `Routines` メニューを開く
3. 試しに `weekly` 頻度で「`/catch-up` 相当の処理（このリポジトリ向け）」を1つ作成し、`--dry-run` 的に手動トリガで動作確認
4. Managed Agents の Outcomes を試したい場合、`anthropic-beta: managed-agents-2026-04-01` ヘッダで API 叩いてルーブリック実験
5. レート制限の体感差を観察するため、これまで「ピーク帯で詰まった」シナリオを再実行

## ソース

- [Live blog: Code w/ Claude 2026（Simon Willison）](https://simonwillison.net/2026/May/6/code-w-claude-2026/)
- [Introducing routines in Claude Code（Anthropic 公式）](https://claude.com/blog/introducing-routines-in-claude-code)
- [New in Claude Managed Agents: dreaming, outcomes, and multiagent orchestration（Anthropic 公式）](https://claude.com/blog/new-in-claude-managed-agents)
- [Code with Claude: The 5 biggest updates explained（Lenny's Newsletter）](https://www.lennysnewsletter.com/p/code-with-claude-the-5-biggest-updates)
- [Code with Claude: The May 6 Readiness Field Guide（Context Studios）](https://www.contextstudios.ai/blog/code-with-claude-the-may-6-readiness-field-guide)
- [Code with Claude 2026 完全解説 — SpaceX提携とClaude Codeレート制限2倍（Qiita）](https://qiita.com/kai_kou/items/ba88f403caf78fe5242b)

---

## 感想・考察

### 5時間レート制限の理解

「5時間枠」は **最初のメッセージのタイムスタンプから5時間** で固定されるローリングウィンドウ。8:00 に話し始めて9:30 に上限に達しても、リセットは 13:00（早く使い切るほど待ち時間が長くなる）。今回の2倍化は SpaceX × Colossus のキャパシティ追加を背景にした **恒久措置** として発表されており、期間限定の文言なし。ただし Pro 枠は Max の 1/5〜1/20 相当なので、絶対量としては依然タイト。

### Managed Agents の位置づけ

Claude.ai（チャット）/ ローカル Claude Code / API / Agent SDK と並ぶ第5のレイヤーで、**「エージェントとしてのループ・記憶・サンドボックス・並列化を Anthropic 側で全部やってくれる」** マネージドサービス。今回の3新機能（Dreaming・Outcomes・Multi-agent orchestration）はいずれも **クラウド常駐だからこそ実現できる機能** という共通点があり、ローカル Claude Code への上乗せではなく **別系統の進化** と理解するのが正しい。

特に **grader エージェント**（Outcomes の核）は LLM-as-a-Judge / Critic model をエージェントループに正式に組み込む仕組みで、自分の `/catch-up` Skill にも応用余地あり（メイン: エントリ生成、grader: relevance タグ妥当性・関連度具体性・ソース URL 健全性を採点）。

### このプロジェクトのクラウド化構想

ゲーム開発（Unity Editor・GPU・ローカルアセット依存）と違い、**AI-Agent-Knowledge プロジェクトは「Web から取って整形してリポジトリに書く」だけ** なのでクラウド化との相性が極めて良い。想定する三段構え:

| フェーズ | 場所 | 手段 |
|---------|------|-----|
| **生成** | Anthropic クラウド | Routines で `/catch-up` を週3回（月・水・金朝）スケジュール実行 → PR を `entries/01_unread/` に自動作成 |
| **読解・Q&A** | スマホ | Claude iOS/Android アプリ。Project 機能にリポジトリor 該当エントリを入れて深掘り質問 |
| **整理（既読化・移動）** | スマホ or 自宅PC | GitHub Mobile で PR レビュー/マージ、フォルダ移動は帰宅後にローカルでまとめて |

これにより **「外出先のスキマ時間で情報収集が完結する」** 状態が作れる。

### Pro プラン枠での現実的な制約

- **Routines 5/日**: 週3回 catch-up に使っても 15/35 で余裕。スポット深掘り実行にも回せる
- **5時間レート制限**: 外出時のスマホ Q&A で使い切ると、自宅でのコーディング枠が足りなくなる懸念 → **「外出時の調査枠」と「自宅の開発枠」を意識的に分ける運用が必要**
- **Managed Agents の課金体系**: Pro プランに含まれるか API 別課金かは Console で要確認

### 残課題（試す前にチェック）

1. `/catch-up` Skill がプロジェクト配下（`.claude/skills/`）にコミット済みか、ローカル `~/.claude/skills/` だけか — 後者だと Routines から見えない
2. Routine 実行環境で WebSearch / WebFetch が有効化されているか
3. 既存エントリの参照（重複生成回避）が Routine のクローン後にちゃんと効くか
4. Managed Agents（特に Dreaming）が Pro プランで利用可能か

### この発表の意味（メタ）

「**新モデルは出さず、既存モデルの上のハーネス層を厚くする**」という方針は、自分が追ってきた harness engineering（[[2026-04-23 harness 系エントリ]]、[[2026-05-02 rkaga 発表]]、[[2026-04-03 agent teams]] の系譜）と完全に重なる。**性能向上 = モデル更新** ではなく **性能向上 = エージェント運用設計の改善** という時代に明確に移行しており、自分の自前 Skill・auto-memory・Plan ファイル運用の延長線上に Anthropic 公式の Managed Agents があるという理解が得られた。
