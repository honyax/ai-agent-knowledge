---
date: 2026-07-20
status: read
relevance: S
tags: [claude-code, security, permissions, prompt-injection, worktree, powershell, hooks, 破壊的変更]
source_urls:
  - https://qiita.com/picnic/items/2623cb5a5f81928b4477
  - https://qiita.com/picnic/items/bae2dd474405eaa87513
  - https://qiita.com/emi_ndk/items/73478cc5aa6a9337afad
  - https://dev.classmethod.jp/en/articles/20260718-cc-updates-v2-1-214/
  - https://code.claude.com/docs/en/changelog
experiment_dir: null
---

# Claude Code v2.1.210〜v2.1.214: permission チェックの穴を一斉修正、信頼境界の再定義

## 3行要約

- 7/15〜18 の 4 バージョンでセキュリティ修正が集中。**v2.1.210**: Agent ツールへの間接プロンプトインジェクション対策、worktree 分離の修正。**v2.1.212**: plan mode の permission バイパス（critical）、worktree 経由のリポジトリ外書き込み（high）を修正。
- **v2.1.214**（47 変更、うちセキュリティ 9 件）: `Edit(src/**)` のような **単一セグメント `dir/**` allow ルールが、ツリー内の任意の場所の同名ディレクトリを自動承認していたバグ**を修正（本来は `<cwd>/dir` のみ対象）。**Windows PowerShell 5.1 セッションでの permission チェックバイパス**も修正。Bash の fd リダイレクト解釈の食い違いは fail-closed に。
- **破壊的変更**: hook の `if:` 条件に書いた単一セグメント `dir/**` パターンが `<cwd>/dir` のみにマッチするよう変更（任意の深さでマッチさせたい場合は `**/dir/**` に書き換え）。deny / ask の permission ルールは従来どおり任意の深さでマッチ。`EndConversation` ツール（Claude が abusive なセッションを終了できる）も追加。

## 自分への関連度: S

CLAUDE.md の「シェルツールの優先順位」は **permissions allowlist が前提**の運用なので、allow ルールが意図しないスコープを承認していたバグ（`dir/**` 問題）は自分の設定の再点検が必要。**Windows 11 + PowerShell 併用環境**（[[user_environment]]）なので PowerShell 5.1 バイパス修正も直撃。worktree 分離修正は background agents 運用（[[2026-07-01-claude-code-v21198-background-agents-auto-pr]]）の安全性に直結。hooks の破壊的変更は自分は hooks 未活用（[[user_hooks_usage]]）のため実害なしだが、更新は必須。

## 詳細

### バージョン別の主要セキュリティ修正

| バージョン | 日付 | 修正内容 |
|-----------|------|---------|
| v2.1.210 | 7/15 | Agent ツールへの**間接プロンプトインジェクション対策**、worktree 分離の修正、ultracode キーワード誤発火修正 |
| v2.1.212 | 7/17 | **plan mode の permission バイパス（critical）**、worktree 経由の**リポジトリ外書き込み（high）** |
| v2.1.214 | 7/18 | permission ルールのスコープ過大承認、**PowerShell 5.1 バイパス**、Bash fd リダイレクトの fail-closed 化ほか計 9 件 |

### v2.1.214 の permission 修正の中身

- **`dir/**` の過大マッチ**: `Edit(src/**)` のような単一セグメント allow ルールが、`<cwd>/src` だけでなく**ツリー内の任意の `src/` ディレクトリ**への書き込みを自動承認していた。修正後は `<cwd>/dir` のみ。
- **PowerShell 5.1 バイパス**: Windows PowerShell 5.1 セッションで実行されるコマンドの permission チェックをすり抜けられる問題を修正。
- **Bash fd リダイレクト**: bash と permission アナライザーでファイルディスクリプタリダイレクトの解釈が食い違う形式について、**fail-closed（安全側に倒す）**に変更。

### 破壊的変更（hooks の `if:` 条件）

- 単一セグメント `dir/**` パターン → `<cwd>/dir` のみマッチに変更。
- 任意の深さでマッチさせたい場合は `**/dir/**` に書き換えが必要。
- **deny / ask の permission ルールは従来どおり任意の深さのまま**（allow だけ厳格化）— 安全側に非対称な設計。

### 信頼境界の再定義（emi_ndk 氏の分析）

- この 2 週間の一連の修正を「**信頼境界を『セッションの内/外』から『人間が生成したものか否か』へ引き直した**」と整理する分析記事。
- 象徴的な例として「『承認しました』とモデル自身が書いたテキストを承認として扱ってしまう」類の問題——モデル出力・リポジトリにコミットされたファイルも「信頼できない入力」として扱う方向へ。
- [[2026-07-03-mcp-supply-chain-token-theft]]（設定ファイルの信頼性）、v2.1.196 の `.mcp.json` 自動起動制限（[[2026-07-03-claude-code-v21196-v21197]]）、auto-mode guardrails（[[2026-07-01-claude-code-v21180-v21193]]）と一貫した流れ。

### その他

- **`EndConversation` ツール**: Claude 側から abusive なセッションを終了できる。
- v2.1.213〜214 では Fable 5 が Max / Team Premium プランに 50% 制限で標準含有される変更も（Pro は対象外）。

## 試すなら

1. `claude --version` で v2.1.214 以降へ更新（セキュリティ修正のため優先度高）。
2. `.claude/settings.json` / settings.local.json の permissions を確認し、`Edit(src/**)` 型の単一セグメント allow ルールが「cwd 直下のみ」で意図どおりか再点検する。
3. PowerShell 系の allow ルール（自分は Bash 優先運用だが PowerShell ツールも存在）が适切かを確認。
4. hooks を使い始める際は `if:` 条件の新仕様（`**/dir/**`）で書く（既存 hooks はないので書き換え作業は不要）。
5. emi_ndk 氏の分析記事を読み、「モデル出力を信頼しない」設計原則を自作 skill（catch-up / try）のプロンプト設計にも反映できないか考える。

## ソース

- [Claude Code v2.1.210で強化された間接プロンプトインジェクション対策とworktree分離の修正 (Qiita, picnic)](https://qiita.com/picnic/items/2623cb5a5f81928b4477)
- [Claude Code v2.1.214の権限脆弱性修正とhooks破壊的変更を解説 (Qiita, picnic)](https://qiita.com/picnic/items/bae2dd474405eaa87513)
- [「承認しました」と書いたのはモデルだった: Claude Codeがこの2週間で引き直した信頼境界 (Qiita, emi_ndk)](https://qiita.com/emi_ndk/items/73478cc5aa6a9337afad)
- [Claude Code v2.1.213 to v2.1.214 Major Updates (Classmethod DevelopersIO)](https://dev.classmethod.jp/en/articles/20260718-cc-updates-v2-1-214/)
- [Claude Code changelog (公式)](https://code.claude.com/docs/en/changelog)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
