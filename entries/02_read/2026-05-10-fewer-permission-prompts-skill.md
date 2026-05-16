---
date: 2026-05-10
status: read
relevance: S
tags: [claude-code, skill, permission, allowlist, settings-json, approval-fatigue, automation, v2.1.111, v2.1.114]
source_urls:
  - https://zenn.dev/akasara/articles/dac2e93c27557f
experiment_dir: null
---

# `/fewer-permission-prompts` スキル徹底解説 — トランスクリプト走査で `.claude/settings.json` の allowlist を AI が自動提案

## 3行要約

- Claude Code v2.1.111 で導入（v2.1.114 で `/fewer-permission-prompts` にリネーム、それ以前は `/less-permission-prompts`）された組込みスキル。**過去セッションのトランスクリプトを走査して、頻出する read-only な Bash・MCP ツール呼び出しを抽出し、`.claude/settings.json` の `permissions.allow` に追加すべきリストを優先順位付きで提案**する
- Anthropic が「approval fatigue（承認疲れ）→ 危険コマンドの誤承認」を安全性リスクと認識し、手動編集・Auto Mode（Max 限定）・Sandboxing に次ぐ **「第3の選択肢」** として実装。Boris Cherny が「Opus 4.7 活用 6 Tips」の 2 番目として公式推奨
- 抽出対象は **read-only のみ**（`rm`・`git push` 等の書き込みは提案されない）。Bash ルールは正規表現ではなく **プレフィックスマッチ**で、`deny > allow` の優先順位設計で安全性が担保される。週1回ルーティン化が推奨運用

## 自分への関連度: S

**このスキルは自分の Claude Code 環境に既に組み込み済み**（user-invocable skills 一覧で確認可）。ナレッジベース運用が長くなるにつれ、`/catch-up` や `/try` 実行時に Bash・WebFetch・Grep 系の承認プロンプトを何度も押している現状があり、本記事の運用テクニックを取り入れれば「日常の `/catch-up` 1回あたりの承認回数」が確実に減る。特に:

- **チーム共有設定管理**: 自分はソロ運用だが、`.claude/settings.json`（チーム）と `settings.local.json`（個人）の使い分けは整理しておきたい
- **MCP ツールの自動整理**: 自分は Blender MCP・Discord MCP・Gmail/Drive/Calendar MCP を使うので、`mcp__server__tool` 形式が自動整理されるのは効く
- **deny ルール並行整備**: `curl`・`sudo`・秘密鍵アクセスを明示的に禁止する設計は、CVE-2025-59536（Hook 経由 RCE）対策（2026-05-04 Agent SDK エントリ参照）と連動する

著者（akasara）公開: 2026-04-19、更新: 2026-04-21。

## 詳細

### 内部動作（3ステップ）

1. **スキャン**: 過去セッションのトランスクリプトを走査
2. **抽出**: read-only ツール呼び出しのみ特定（破壊的操作は除外）
3. **提案**: 優先順位付きの allowlist を生成 → `.claude/settings.json` へ書き込み

生成例:

```json
{
  "permissions": {
    "allow": [
      "Bash(ls:*)",
      "Bash(git log:*)",
      "mcp__github__list_prs"
    ]
  }
}
```

### v2.1.111 で同時リリースされた 3 つの承認削減策

- **glob パターン自動許可**: `ls *.ts` のような展開を毎回確認しない
- **`cd` 誘発ミスの予防**
- **残った頻出コマンドの一掃**（本スキル）

### パーミッションルール構文

| ルール | 意味 |
|--------|------|
| `Bash` | すべての Bash コマンド許可 |
| `Bash(npm run:*)` | `npm run` + 任意引数 |
| `Bash(git *)` | `git` 関連全て |
| `mcp__github__list_prs` | MCP ツール（括弧不可） |

**優先順位**: `deny > allow`（deny ルールが allow を上書きしない）。

### 5つのユースケース

1. **長時間セッション終了前の棚卸し**: 日次/週次ルーティン化で実態を allowlist に反映
2. **チーム共有設定管理**: git 管理の `.claude/settings.json` を PR レビュー経由で更新
3. **Max プラン非保有者向け**: Auto Mode（Max 限定）の代替手段
4. **複雑な MCP 環境の初期化**: 複数 MCP 接続時に `mcp__server__tool` 形式を自動整理
5. **新規プロジェクト探索後の効率化**: 探索フェーズ後にプロンプト数を先制削減

### ベストプラクティス

- **週1回実行** で動的な利用パターンに追従
- **生成結果は目視レビュー必須**（プレフィックスマッチで意図しないコマンドにマッチするリスクがある）
- **ファイル分け戦略**: チーム共有は `settings.json`、個人は `settings.local.json`
- **deny ルール並行整備**: `Bash(curl:*)`、`Bash(sudo:*)`、`Read(~/.ssh/**)` 等を明示拒否
- 独自 skill 大量導入時は `SLASH_COMMAND_TOOL_CHAR_BUDGET` で context 圧迫を調整

### 制約

- read-only コマンドのみ対象（書き込み・破壊的操作は提案されない）
- 過去トランスクリプト必須（新規セッション直後は無効）
- Bash ルールはプレフィックスマッチ（正規表現ではない）

### 他手段との比較

| 手段 | 削減量 | 安全性 | 手間 | プラン要件 |
|------|--------|--------|------|-----------|
| 本スキル | 大 | 高 | 小 | なし ✓ |
| Auto Mode | 最大 | 中 | 最小 | Max 限定 |
| Sandboxing | 84%減 | 最高 | 中 | なし |
| `--dangerously-skip-permissions` | 100% | 最低 | 0 | なし |

著者推奨: **Sandboxing + 本スキル** または **Auto Mode + 本スキル** の併用。

## 試すなら

1. `claude update` で v2.1.114 以降にする（自分は v2.1.133+ なので OK）
2. 数時間〜数日の作業履歴を作った後、`/fewer-permission-prompts` を実行
3. 提案された `permissions.allow` を目視レビュー（プレフィックスマッチで広すぎるものは削る）
4. `.claude/settings.local.json` 側に `deny` ルールも並行整備（`curl:*`、`sudo:*`、`~/.ssh/**` Read 等）
5. 週1回ルーティン化（カレンダー or Routines で自動リマインド）

## ソース

- [Claude Code の許可プロンプトを AI に自動削減させる「/fewer-permission-prompts」完全解説（Zenn / akasara）](https://zenn.dev/akasara/articles/dac2e93c27557f)

---

## 感想・考察

### 自分の運用との比較

現在は **承認ダイアログが出るたび、その場で `.claude/settings.json` に追記する**運用で回せている。この前提だと本スキルの必要性は確かに薄い。それでも残るメリットを整理した:

1. **取りこぼしの回収**: 急いでいて「許可」だけ押した／Shift+Tab で一時許可した、というケースは settings.json に残らない。トランスクリプト走査なら後から拾える
2. **プレフィックス幅の最適化**: その場追加は「目の前のコマンドそのまま」になりがちで、`Bash(git log --oneline -10:*)` のように狭すぎる登録が増える。走査結果は頻度ベースなので `Bash(git log:*)` といった適切な粒度の提案が期待できる
3. **allowlist の棚卸し**: 長期運用で「もう使ってないルール」「重複ルール」が溜まる。週1走査は「現状の使用パターン vs 既存ルール」のギャップ可視化として効く（記事の推奨運用もここ）
4. **MCP ツールの網羅性**: Blender/Discord/Gmail/Drive/Calendar MCP のように多MCP環境だと、`mcp__server__tool` 形式の都度追加は取りこぼしやすい。走査で一括整理できる
5. **別環境への移植**: 新プロジェクトや別マシンに移ったとき、過去パターンから一気に allowlist を作れる

### 結論

ソロ運用＆都度追加が回っているなら、本スキルの優先度は B〜C 相当。試すとしても「月1で棚卸し用に走らせて差分だけ見る」程度の使い方が現実的。日次/週次ルーティン化はオーバーキル。

`.claude/settings.local.json` 側の **deny ルール並行整備**（`Bash(curl:*)`、`Bash(sudo:*)`、`Read(~/.ssh/**)`）の方が、本スキル運用より優先度が高いと判断。これは [[2026-05-04-claude-code-agent-sdk-permission-design]] の CVE-2025-59536 対策とも連動する論点。
