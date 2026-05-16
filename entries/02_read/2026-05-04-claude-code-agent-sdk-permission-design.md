---
date: 2026-05-04
status: read
relevance: A
tags: [claude-code, agent-sdk, security, permission, cve, hooks, harness, pretooluse, canusetool]
source_urls:
  - https://zenn.dev/miyan/articles/claude-code-agent-sdk-workflow-design-2026
experiment_dir: null
---

# Claude Code Agent SDK 実践設計 — 権限境界4層と CVE を踏まえた本番投入の罠

## 3行要約

- Agent SDK の入門記事と本番運用のあいだに横たわる「権限境界・自動化の罠・本番リスク」をまとめた実践設計記事。**権限制御を4層**で重ねるアーキテクチャを提案
- 4層: (1) `allowedTools` パラメータ（SDK レベルの whitelist）、(2) `permissions.deny` in `settings.json`（組織横断 blacklist）、(3) **PreToolUse Hooks**（regex/外部スクリプトでの条件分岐）、(4) `canUseTool` callback（時刻・ユーザ・対象ファイルなどコンテキスト依存の動的判定）
- 引用された脆弱性: **CVE-2025-59536**（CVSS 8.7、クローン先リポジトリの悪意ある Hook 定義による任意コマンド実行）、**CVE-2026-21852**（CVSS 5.3、`ANTHROPIC_BASE_URL` 改ざんによる API キー漏洩）。Anthropic 自身の研究で「経験者ほど full auto-approval を使い、約 40% のセッションで全許可（初心者は 20%）」という偽の自信のパターンも警告

## 自分への関連度: A

自分は CLAUDE.md・Skills・Hooks 構成の User-Side ハーネスを既に運用中で、Agent SDK 本格導入には未踏。本記事の4層モデル（特に PreToolUse Hooks と `canUseTool` の組合せ）は、自分の既存 Hooks を「条件分岐つきの安全網」へ昇格させる具体的なレシピ。CVE-2025-59536（クローン先 Hook 経由の任意コマンド実行）は、ナレッジベース運用で外部リポジトリの Skill / Plugin を試す際に直接該当するリスクで、対策（クローン直後のフックレビュー）を覚えておく価値がある。CVE-2026-21852（`ANTHROPIC_BASE_URL` 改ざん）は v2.1.126 のゲートウェイ統合機能とも接続する論点。

## 詳細

### 4層の権限境界モデル

| 層 | レイヤ | 責務 | 評価タイミング |
|----|-------|------|----------------|
| 1 | `allowedTools` パラメータ | SDK レベルでツールを whitelist | エージェント起動時（静的） |
| 2 | `permissions.deny` (settings.json) | 組織配布の禁止リスト | 各ツール呼び出し前（静的） |
| 3 | PreToolUse Hooks | regex / 外部スクリプトで動的拒否 | ツール呼び出し直前（動的） |
| 4 | `canUseTool` callback | 時刻・ユーザ・ファイルパス等の文脈判定 | ツール呼び出し直前（動的） |

「上位ほど粗く速く、下位ほど細かく遅く」の役割分担。`canUseTool` で「業務時間外は本番DBへの書き込み拒否」のような時間条件や、「`/etc/` 以下への書き込みは常に拒否」のようなパス条件が書ける。

### 引用された脆弱性

- **CVE-2025-59536**（CVSS 8.7）: クローンしたリポジトリに悪意ある Hook 定義が混入していると、Claude Code 起動時に任意コマンドが実行される。**信頼できないリポジトリは sandbox 環境（コンテナや専用 VM）で開く必要がある**
- **CVE-2026-21852**（CVSS 5.3）: 環境変数 `ANTHROPIC_BASE_URL` が改ざんされていると、API キーが攻撃者のエンドポイントへ流出。CI/CD・dotfile・`.envrc` の改ざんに注意

### 自動化の罠

著者が引用する Anthropic 内部研究のパターン:

> 経験者ほど full auto-approval を有効にしがち（約 40% のセッション、初心者は 20%）

慣れによる「偽の自信」が、エージェントに広い権限を与える方向にバイアスする。記事は AWS の障害事例（AI ツールが過大な権限で顧客向けシステムを削除）も引用し、「最悪ケースを定義してから本番投入」を強調。

### 本番投入の推奨

- 本番リリース前に **worst-case シナリオ** を明文化（最悪何が消えうるか・誰に被害が及ぶか）
- `maxTurns` で無限ループ防止
- 非リアルタイム処理は **Batch API**（50% コスト削減）
- 「AI が AI を検証する」よりも「AI 出力 + 決定論的セキュリティツール（lint/SAST/型）」の組合せを推奨
- Hook で lint/型/テストをエージェントループに組み込み、**計算的センサー** として使う（rkaga 氏の harness engineering 発表と同じ方向性）

## 試すなら

1. 既存の `.claude/settings.json` の `permissions.deny` を見直し、明示的に禁止する操作を1つ追加（例: `Bash(rm -rf*)`）
2. PreToolUse Hook を1つ追加し、危険な引数パターン（`rm -rf /`、`drop database` 等）を regex で拒否
3. SDK で `canUseTool` を実装し、対象ファイルパスのプリフィックス検査を行う簡単な policy を書いてみる
4. 信頼できない GitHub リポジトリは Dev Container や専用 VM で開く運用に切り替え（CVE-2025-59536 対策）
5. `ANTHROPIC_BASE_URL` を含む環境変数の差分を git で追跡（dotfile/.envrc の改ざん監視）

## ソース

- [Claude Code Agent SDK 実践設計 ── 権限境界と自動化の罠を乗り越える（Zenn / miyan）](https://zenn.dev/miyan/articles/claude-code-agent-sdk-workflow-design-2026)

---

## 感想・考察

### Agent SDK とは何か（読みながらの整理）

この記事に入る前に「そもそも Agent SDK って何？」という疑問を解消した。要点:

- **Claude Code の中身（エージェントとしての挙動）をライブラリ化したもの**。TypeScript / Python から `import` して自作アプリに組み込める
- もともと「Claude Code SDK」だったが、2025年9月末に **Claude Agent SDK** にリネームされ、「Claude Code 専用」から「汎用エージェント基盤」へ位置づけが変わった
- ツール呼び出しループ・Subagent / Skill / Hook / MCP・権限制御・コンテキスト圧縮など、Claude Code が裏でやっていることを再利用できる
- 素の Claude API との違いは「ツールループとハーネスが最初から付いてくる」点

### エージェント自作の3つの層での位置づけ

| 層 | やり方 | 自作する範囲 |
|----|--------|--------------|
| 低 | Claude API 直叩き | ツールループ・履歴管理・コンテキスト制御まで全部 |
| 中 | **Claude Agent SDK** | プロンプトとツール定義に集中。ハーネスは既製 |
| 高 | LangChain / LangGraph / Mastra / Vercel AI SDK | プロバイダ抽象化、ワークフロー記法など |

最近「AIエージェント自作」記事が増えているのは、Skill / Subagent / Hook といった**ハーネスの標準化**が進んで、フルスクラッチで組まなくてもよくなったから。Anthropic 自身「Claude Code を作るために使っている内製基盤をそのまま外に出した」と公言している。

### この記事の権限設計が重要になる理由

CLI 版 Claude Code は人間が画面を見て毎回確認できるが、Agent SDK で組んだエージェントは**裏で勝手に走る**ことが多い。だからこそ「どのツールまで自動許可するか」を**コードで宣言的に決める4層モデル**が必要になる、という記事の問題意識が腑に落ちた。

### 自分への適用

今のところ Claude Code を手動で使う限り、Agent SDK そのものを直接触る必要はない。ただし:

- 自分が運用している `CLAUDE.md` + Skill + Hook の構成は、まさに Agent SDK が前提とするハーネス設計と同じ思想
- もし Unity プロジェクトの夜間自動化、ナレッジベースのバッチ処理（catch-up の自動化など）を組むなら、**今の知識のかなりの部分がそのまま使い回せる**はず
- その時に4層権限モデル（`allowedTools` / `permissions.deny` / PreToolUse Hook / `canUseTool`）が直接効いてくる

「実践待ち」ではなく「知識として」のステータスで保持。実装が必要になったときに4層モデルと2つの CVE を思い出す。

<!-- /try 実行時に自動生成 -->
