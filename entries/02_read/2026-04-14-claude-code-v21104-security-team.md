---
date: 2026-04-14
status: read
relevance: A
tags: [claude-code, security, release, team, enterprise]
source_urls:
  - https://github.com/anthropics/claude-code/releases
  - https://code.claude.com/docs/en/changelog
experiment_dir: null
---

# Claude Code v2.1.104 — コマンドインジェクション修正・チームオンボーディング・企業TLS対応

## 3行要約

- LSPバイナリ検出のPOSIX whichフォールバックに存在したコマンドインジェクション脆弱性を修正（セキュリティフィックス）。
- `/team-onboarding` コマンドが追加され、ローカルのClaude Code使用状況から新メンバー向けランプアップガイドを自動生成できる。
- OSのCAストアをデフォルト信頼するようになり、企業のTLSプロキシが追加設定なしで動作するようになった（`CLAUDE_CODE_CERT_STORE=bundled`で旧動作に戻せる）。

## 自分への関連度: A

コマンドインジェクション修正はセキュリティ上すぐに更新すべき案件。企業TLS対応は自分には直接影響しないが、チーム導入時の障壁が下がる。`/team-onboarding`はチーム展開時に使えるコマンド。

## 詳細

### セキュリティ修正: コマンドインジェクション

`which` コマンドのPOSIXフォールバックを使うLSPバイナリ検出パスにコマンドインジェクションの脆弱性があった。悪意あるプロジェクト設定ファイルや環境変数経由で任意コードが実行される可能性があったため、アップデートを優先すること。

### /team-onboarding コマンド

```
/team-onboarding
```

ローカルのClaude Code使用ログ・CLAUDE.mdの内容・設定を分析し、新しいチームメンバーが素早く立ち上がれるランプアップガイドをMarkdownで生成する。既存チームのノウハウを自動文書化できる。

### OS CA証明書ストアの信頼

企業環境でのTLSインターセプトプロキシ（BurpやZscalerなど）が追加設定なしで動作するようになった。これまではCA証明書を手動でバンドルに追加する必要があった。

### その他の修正

- 仮想スクローラーで長セッション時にメッセージリストの履歴コピーが蓄積するメモリリークを修正
- `--resume`/`--continue` 関連の安定性向上

## 試すなら

1. `claude --version` でv2.1.104以降であることを確認（またはnpm update -g @anthropic-ai/claude-code）
2. `/team-onboarding` を実行して生成されるガイドを確認
3. 企業TLSプロキシ環境での動作を確認（該当する場合）

## ソース

- [Releases · anthropics/claude-code](https://github.com/anthropics/claude-code/releases)
- [Changelog - Claude Code Docs](https://code.claude.com/docs/en/changelog)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
