---
date: 2026-07-20
status: read
relevance: A
tags: [claude-code, /fork, /subtask, subagents, background, mcp, websearch, 上限設定]
source_urls:
  - https://github.com/anthropics/claude-code/releases/tag/v2.1.212
  - https://qiita.com/picnic/items/add203914888f26a1658
  - https://qiita.com/saitoko/items/992631068d62dfef46b0
  - https://code.claude.com/docs/en/changelog
experiment_dir: null
---

# Claude Code v2.1.212〜: `/fork` は background セッション化、subagent はデフォルトで background 実行に

## 3行要約

- **`/fork` の仕様変更**: 会話のコピーを **新しい background セッション**（`claude agents` に独立した行として出現）として起動する形に。従来の「セッション内 subagent を起動する」挙動は **`/subtask`** という別コマンドに分離。fork 中も手元の作業を継続できる。
- **subagent がデフォルトで background 実行に**: メインの Claude は subagent の実行中も作業を続け、完了時に通知を受ける。複数タスクを複数 subagent に投げつつ自分の作業も並行、という使い方が標準に。subagent は親セッションの permission mode を継承（Task ツールの `mode` パラメータは非推奨・無視）。
- **暴走防止の上限が複数追加**: subagent spawn は 1 セッション 200 まで（`CLAUDE_CODE_MAX_SUBAGENTS_PER_SESSION`、`/clear` でリセット）、WebSearch は 200 回まで（`CLAUDE_CODE_MAX_WEB_SEARCHES_PER_SESSION`）。**2 分超の MCP ツール呼び出しは自動で background へ**移動（`CLAUDE_CODE_MCP_AUTO_BACKGROUND_MS` で調整/無効化）。

## 自分への関連度: A

subagent の background デフォルト化は、[[2026-07-01-loop-engineering-boris-cherny]] 以来の「並列エージェント運用」を harness 標準に格上げする変更で、catch-up のような検索の多いタスクの体感が変わる可能性がある。WebSearch 200 回上限は catch-up を 1 セッションで何度も回す場合に接触し得る点に注意。MCP 自動 background 化は Unity / Blender MCP の長時間処理（ビルド、ベイク等）でセッションがブロックされなくなる実利がある。`/verify` `/code-review` が「明示呼び出し時のみ実行」に変わった点は、自作クロスレビュー skill（[[project_custom_skills]]）との使い分けが単純になる方向。

## 詳細

### `/fork` と `/subtask` の分離（v2.1.212）

| コマンド | 挙動 |
|---------|------|
| `/fork`（新） | 会話をコピーして **background セッション**として起動。`claude agents` に独立した行。手元の作業は継続 |
| `/subtask`（新設） | 従来の `/fork` 相当。**セッション内 subagent** を起動 |

「試行錯誤の分岐」は fork、「今の文脈の一部を切り出して並行処理」は subtask、という住み分けに。

### subagent の background デフォルト化

- subagent 実行中もメイン Claude が作業を継続し、完了時に通知を受ける。
- X 上でも「複数タスクを複数 subagent に投げつつ他の作業も進められる。使い方が変わりそう」との反応（Kuu 氏）。
- **permission mode の継承**: subagent は親セッションの permission mode をデフォルトで継承。Task ツールの `mode` パラメータは**非推奨・無視**（v2.1.214 のセキュリティ修正 [[2026-07-20-claude-code-v21210-v21214-security]] と合わせ、「子が親より緩い権限で動く」穴を塞ぐ方向）。

### 暴走防止の上限（ループエンジニアリングのガードレール）

| 上限 | デフォルト | 環境変数 |
|------|-----------|---------|
| subagent spawn / セッション | 200 | `CLAUDE_CODE_MAX_SUBAGENTS_PER_SESSION`（`/clear` でリセット） |
| WebSearch / セッション | 200 | `CLAUDE_CODE_MAX_WEB_SEARCHES_PER_SESSION` |
| MCP ツールの自動 background 化閾値 | 2 分 | `CLAUDE_CODE_MCP_AUTO_BACKGROUND_MS` |

`/loop` `/goal` 系の自律運用（[[2026-07-01-loop-engineering-boris-cherny]]）で「delegation ループが止まらない」「検索し続ける」事故を harness 側で止める設計。

### その他の変更（v2.1.211〜214 の非セキュリティ分）

- **`/verify` / `/code-review` は明示的に呼んだときのみ実行**（Claude が自発的に走らせない）。
- **ログイン期限の事前警告**、agent status / manual mode バッジの明確化。
- **リトライ強化**: `CLAUDE_CODE_RETRY_WATCHDOG` が非容量系一時エラーのデフォルトリトライを 300 に、`CLAUDE_CODE_MAX_RETRIES` の上限 15 を撤廃。
- **macOS の background セッション修正**: 誤った低メモリ検知で attach が 15〜20 秒固まる問題、daemon のセッショントークン失効で永久に無反応になる問題を修正。
- **MCP roots**: セッションの additional working directories が `roots/list` に含まれ、変更時に `notifications/roots/list_changed` を送信。
- VS Code の remote control 設定追加、起動時メモリ削減、ストリーミング応答性改善。

## 試すなら

1. v2.1.212 以降に更新し、`/fork` が background セッションを作ること、`/subtask` が従来のセッション内分岐になることを確認。
2. 複数 subagent を投げるタスク（例: catch-up の検索を並列化）で、メインセッションがブロックされずに動き続けることを体感。
3. Unity / Blender MCP で 2 分超の処理（ベイク等）を走らせ、自動 background 化でセッションが使い続けられるか確認。
4. catch-up を長時間回す際、WebSearch 200 回上限に接触しないかを意識（接触したら `/clear` またはセッション分割）。
5. VSCode 拡張版（[[user_environment]]）での `/fork` 挙動と background セッションの見え方を確認（CLI 先行パターンに注意）。

## ソース

- [Release v2.1.212 · anthropics/claude-code (GitHub)](https://github.com/anthropics/claude-code/releases/tag/v2.1.212)
- [Claude Code v2.1.212の/fork仕様変更とセキュリティ修正まとめ (Qiita, picnic)](https://qiita.com/picnic/items/add203914888f26a1658)
- [Claude Code 週次アップデートまとめ 2026/07/11週 (Qiita, saitoko)](https://qiita.com/saitoko/items/992631068d62dfef46b0)
- [Claude Code changelog (公式)](https://code.claude.com/docs/en/changelog)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->

- **VSCode 拡張版でも subagent の background デフォルト化は確認できた**: 自分自身（Claude、VSCode 拡張版セッション）の Agent tool 説明文に「Agents run in the background by default」とあり、Monitor / TaskOutput / TaskStop / SendMessage（ID 指定で agent を再開）といった background agent 管理用のツール一式が実際に存在する。ここは推測ではなく確度の高い事実として確認できた。
- **`/fork` と `claude agents` パネルの VSCode 拡張での見え方は未確認のまま**: 公式 changelog にも IDE 統合での挙動の明示的な記述はなかった（WebFetch で確認済み）。`claude agents` は CLI のターミナル UI 前提の一覧表示なので、[[user_environment]] に記録した「fullscreen renderer 依存機能（`/focus` 等）は VSCode 拡張で使えない」という過去の知見と同種の懸念が残る。「試すなら」5番は引き続き実機確認待ち。
- **上限系（200 subagent/セッション、WebSearch 200 回、MCP 2 分自動 background 化）は UI 非依存の harness レベルのガードレール**と考えられるため、CLI / VSCode 拡張問わず同様に効くはず。
- **`/fork` と `/subtask` は background 化の「見え方」が違う**: `/fork` は独立した別セッションとして `claude agents` に別行で出現し後から attach できるのに対し、`/subtask` はメインセッションに従属するタスクとして完了通知だけが返る。「別物として並走」か「今の作業の一部を裏で処理」かで追跡単位が変わる。
- **同期→非同期への体感変化が実際の利用で確認できた**: 「サブエージェントの終了を待つ」と言った直後に処理（ターン）が終わっているように見える場面を最近よく見かけるようになった、という気づきがあった。これはまさに background デフォルト化の副作用で、「待ちます」と言った時点でそのターンの応答が一旦区切られ、完了は別ターンの通知として後から差し込まれる設計。ただし2パターンの区別が必要で、(a) 後で続報（完了報告）が来れば正常系、(b) 続報が来ないまま止まっていれば VSCode 拡張側の background 通知経路に問題がある可能性（IDE 統合の未確認ポイントと関連）。次に同じ場面に出くわしたら続報の有無を確認する。
