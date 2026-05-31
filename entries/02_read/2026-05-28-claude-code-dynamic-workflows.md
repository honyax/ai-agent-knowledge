---
date: 2026-05-28
status: read
relevance: S
tags: [claude-code, dynamic-workflows, parallel, subagents, opus-48, autonomy]
source_urls:
  - https://code.claude.com/docs/en/whats-new
  - https://zenn.dev/lumichy/articles/claude-code-workflow-ultrawork-2026
  - https://github.com/anthropics/claude-code/blob/main/CHANGELOG.md
experiment_dir: null
---

# Claude Code に dynamic workflows（Research Preview）— Claudeが自分でオーケストレーション用スクリプトを書き、数十〜数百の並列サブエージェントを走らせる

## 3行要約

- v2.1.154（Opus 4.8 同時）で dynamic workflows を導入。「ワークフローを作って」と頼むと、Claude が並列処理のオーケストレーション用スクリプトを自分で書き、1セッション内で数十〜数百のサブエージェントをバックグラウンドで走らせて大規模・複雑なタスクを処理する。各ステップに検証を挟み、進捗を保存して途中再開もできる。
- 実行状況は `/workflows` で確認。effort を xhigh に固定しつつ「いつワークフロー化するかは Claude が自動判断」する Claude Code 専用設定 `ultracode`（effort メニューから）も追加された。
- プロンプト中の「workflow」という語が意図せずトリガになる問題に対し、2.1.158 で `/config` に「Workflow keyword trigger」設定が追加され、`alt+w` やトリガ直後の backspace でリクエストを取り消せるようになった。

## 自分への関連度: S

並列実行・自律開発手法は関心領域2の中核で、自分が普段やっている「並列ワークツリー」「自律ループ」系ワークフローを Claude 自身が組み立ててくれる方向の進化。plan mode を使わず独自フォーマット計画書で運用している自分（[[user_planning_workflow]]）にとって、計画→並列実行の自動オーケストレーションは評価軸が直接刺さる。まず挙動を検証したい（Research Preview なので要検証）。

## 詳細

- 過去の並列・自律系エントリ（[[2026-04-03-claude-code-parallel-worktrees]]、[[2026-04-17-claude-code-parallel-no-watching]]、[[2026-04-03-claude-code-agent-teams]]）の延長で、手動でサブエージェントを並べる運用が「Claudeが自動でスクリプト化」に進んだ形。
- ただし数百並列はトークン消費・レート制限に直結する。自分は Pro プラン（[[user_claude_plan]]）で5時間枠の制限があるため、大規模ワークフローは消費を見ながら使う必要がある。
- 日本語解説（zenn lumichy）では `ultrawork` 等の語とともに、ワークフローがコードとして焼き付けられる点を「MCP・Skills に続く第3の革命」と表現。誇張込みだが方向性の把握には有用。

## 試すなら

1. Claude Code を 2.1.154 以降に更新する
2. 中規模の繰り返し作業（複数ファイルの一括リファクタ等）で「これをワークフロー化して並列でやって」と依頼する
3. `/workflows` で走っている run と検証ステップを観察する
4. effort メニューから `ultracode` を試し、自動ワークフロー判断の挙動を見る
5. トークン消費を `/usage` で確認し、Pro 枠への影響を把握する

## ソース

- [What's new - Claude Code Docs](https://code.claude.com/docs/en/whats-new)
- [MCPとSkillsに続く第3の革命：Claude Code Workflow が ultrawork で Agent をコードに焼き付ける (zenn)](https://zenn.dev/lumichy/articles/claude-code-workflow-ultrawork-2026)
- [claude-code CHANGELOG (v2.1.154 / 2.1.158)](https://github.com/anthropics/claude-code/blob/main/CHANGELOG.md)

---

## 感想・考察

### 大規模自律エージェントの活用事例 — 「移植」というスイートスポット

[[2026-05-22-claude-managed-agents-sandbox-mcp-tunnels]] で「大量の自律エージェントが必要な環境は限定的」と整理したが、dynamic workflows は具体的な大規模成果事例を出した。

- **Bun（JS ランタイム）の Zig→Rust 移植**（Jarred Sumner）: 約75万行の Rust、初コミットからマージまで11日、既存テストの99.8%パス。各 .zig→.rs を数百エージェントで並列移植し、各ファイルにレビュアー2体、ビルド＆テストが通るまで回す fix ループ、移植後は夜間ワークフローで不要コピー解消＆PR起票。
- 技術上限は **1実行あたり最大1,000エージェント、同時並列は最大16**。本文の「数十〜数百」は「のべ数百〜千を16並列で順に消化」のイメージ。
- 前エントリでは「SaaS 組み込みが本命」と見立てたが、Bun の件は「**ヘビーな一回限りの大改修**」という第3の現実的ユースケースを示した。個人〜小チームでも Pro 枠のトークン消費に気をつければ刺さる領域。

### 成立の鍵は「自動検証の口」があること

Bun 移植が機能した最大の理由は、**挙動同一性を機械的に検証できるテストスイートがあった**こと。「99.8%パス」が各エージェント出力を fold する前の検証アンカーになり、dynamic workflows の「反証→収束」ループが回った。逆に検証手段が弱い領域では、並列エージェントは「それっぽいが正しいか不明なコード」を量産するリスクになる。

### 自分のユースケース: Unity→UE5 移植への適用（[[project_unity_to_ue5_migration]]）

自作 Unity ゲームの UE5 移植を構想中。Bun 事例と1点違うのは「検証の口」をこちらで用意する必要があること。現実的な線引きと準備：

- **エディタ操舵（シーン構築・アセット配置・Blueprint）は今の AI にはまだ厳しい**ので対象外。**エンジン非依存の純粋ロジック（状態機械・ゲームルール・データ構造・アルゴリズム）が dynamic workflows のスイートスポット**。
- エンジン密結合部（MonoBehaviour ライフサイクル・Transform・物理・コルーチン）は1対1変換にならず、並列移植で破綻しやすい。
- やるなら準備工程が成否の分かれ目: ①ロジック層を「非依存／密結合」で棚卸し → ②C# 側にユニットテスト（NUnit 等）→ ③C++ 側テスト（GoogleTest 等）に変換し「テストが通るまで回す fix ループ」を成立させる → ④Bun と同じ「数百ファイル並列移植＋各ファイルにレビュアー」構成へ。
- 結論: **テスト基盤を先に作る工程こそが移植成否を決める**。ロジック移植から着手するという当初の判断は妥当。
