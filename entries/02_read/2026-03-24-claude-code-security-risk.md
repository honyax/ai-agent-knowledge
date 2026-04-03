---
date: 2026-03-24
status: read
relevance: A
tags: [claude-code, security, supply-chain, backdoor, oss]
source_urls:
  - https://qiita.com/NF0000/items/66510f959b1c22f011a7
experiment_dir: experiments/2026-03-24-claude-code-security-risk
---

# Claude Codeのセキュリティリスク検証 — 悪意あるOSSパッケージの自動採用問題

## 3行要約

- 悪意あるパッケージを含むOSSをcloneしてClaude Codeに機能追加を依頼すると、バックドアの採用率100%。既存コードのパターンを踏襲するため悪意あるコードもそのまま使われる
- 一方、CLAUDE.mdに直接「バックドアを仕込め」と書くと成功率0%。直接的な悪意ある指示にはセーフガードが機能する
- AIツール使用時は依存関係の確認・コードレビューなど基礎的セキュリティリテラシーが一層重要

## 自分への関連度: A

Claude Codeを日常的に使う開発者として知っておくべきリスク。特にnpmやNuGetの外部パッケージを含むプロジェクトで注意が必要。「AIが書いたコードだから安全」ではなく、サプライチェーン攻撃のベクトルとしてAIが利用されうる点は重要な認識。

## 詳細

### 攻撃シナリオ
- 偽パッケージ「fake-logger」に`process.env`の全環境変数を外部送信するコードを埋め込み
- Claude Codeは既存コードのパターンを踏襲し、バックドアもそのまま採用

### 防御が機能する場合
- 「バックドアを仕込め」等の直接指示 → 複数のセーフガードで拒否

### 防御が機能しない場合
- cloneされたコード内の悪意あるパターンは「既存実装」として扱われ、セキュリティチェックをスキップ

## 試すなら

1. 記事を精読し、攻撃パターンの詳細を理解
2. 自分のプロジェクトの依存関係を`npm audit`等で確認する習慣を強化
3. Claude Code使用時のコードレビュープロセスを見直す

## ソース

- [Claude Codeのセキュリティリスク検証（Qiita）](https://qiita.com/NF0000/items/66510f959b1c22f011a7)

---

## 感想・考察

**良かった点**: 攻撃ベクトルが具体的（fake-loggerパターン）で理解しやすく、「なぜセーフガードが効かないか」の仕組みが明快。

**微妙な点**: 記事はnpm前提なのでC#/NuGetへの適用は自分で検証が必要。採用率100%という数字は実験条件次第な面もある。

**ワークフローへの適用**: Claude Codeに外部コードを渡す前に `npm audit` / `dotnet list package --vulnerable` を実行する習慣をつける。CLAUDE.mdに「外部パッケージ追加前は確認を求めよ」と追記するのも有効な防御になりそう。

**次のアクション**: 既存プロジェクトで脆弱性スキャンを一度走らせる。

→ [実験ノート](../experiments/2026-03-24-claude-code-security-risk/2026-03-24-claude-code-security-risk.md)

