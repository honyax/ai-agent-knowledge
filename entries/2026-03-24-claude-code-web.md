---
date: 2026-03-24
status: tried
relevance: S
tags: [claude-code, web, remote, teleport, cloud, parallel]
source_urls:
  - https://code.claude.com/docs/ja/claude-code-on-the-web
experiment_dir: null
---

# Claude Code on the Web — ブラウザからクラウドVMでClaude Codeを実行

## 3行要約

- claude.aiからClaude Codeをクラウド上で実行可能。GitHub連携でリポジトリをクローンし、セキュアなVM上でタスクを非同期実行する
- `--remote`フラグでターミナルからウェブセッションを起動し、複数タスクを並列実行可能。`/teleport`でウェブセッションをローカルに引き継げる
- Pro/Max/Team/Enterpriseプランで利用可。セットアップスクリプトやネットワーク制御など環境カスタマイズも可能

## 自分への関連度: S

ローカルPCを使わずにClaude Codeタスクを並列実行できるため、ゲーム開発中にバックグラウンドで別タスク（テスト修正、ドキュメント更新等）を走らせるワークフローが可能になる。`--remote`で複数セッションを同時起動し、完了後にteleportで引き継ぐ流れは即実践可能。

## 詳細

### 主要機能
- **--remote**: ターミナルからウェブセッションを作成（`claude --remote "Fix bug in auth.ts"`）
- **--teleport / /tp**: ウェブセッションをローカルに引き継ぎ（ブランチ自動チェックアウト、会話履歴の復元）
- **並列実行**: 各`--remote`コマンドは独立したセッションとして同時実行
- **Plan Mode連携**: ローカルでプランモードで設計→リモートで実行のパターン
- **モバイル対応**: iOS/Androidアプリからタスク投入・進捗監視が可能
- **diff ビュー**: PR作成前にアプリ内で変更を確認・コメント・イテレーション

### クラウド環境
- Ubuntu 24.04ベースの分離VM
- Python, Node.js, Ruby, PHP, Java, Go, Rust, C++がプリインストール
- PostgreSQL 16, Redis 7.0が利用可能
- セットアップスクリプトで依存関係の自動インストール可能

### ネットワーク
- デフォルトは「Limited」（主要パッケージレジストリのみ許可）
- 「Full」または「No internet」に変更可能
- セキュリティプロキシ経由で全トラフィックが通過

### 制限事項
- GitHubリポジトリのみ対応（GitLab等は不可）
- セッションのテレポートは同一アカウント限定
- レート制限は他のClaude使用と共有

## 試すなら

1. claude.ai/code にアクセスしGitHubアカウントを接続
2. 対象リポジトリにClaude GitHub Appをインストール
3. 簡単なタスク（README修正等）でウェブセッションを試す
4. ターミナルから`claude --remote "タスク内容"`で並列実行を体験
5. `/teleport`でウェブセッションをローカルに引き継いでみる

## ソース

- [ウェブ上の Claude Code（公式ドキュメント）](https://code.claude.com/docs/ja/claude-code-on-the-web)

---

## 感想・考察

実験ファイル: [experiments/2026-03-24-claude-code-web/2026-03-24-claude-code-web.md](../experiments/2026-03-24-claude-code-web/2026-03-24-claude-code-web.md)

**良かった点**
`--remote` で複数タスクを並列起動し、完了後に `/teleport` でローカルに引き継ぐフローは、Unity 開発の合間にバックグラウンドタスクを走らせるワークフローとして理想的。モバイルからタスク投入できる点も魅力。

**微妙な点・制限**
GitHub 限定なのは今のところ問題ないが、Unity のビルド確認が VM 上でできないのでコンパイルエラーの検出程度に留まる。レート制限の共有は並列実行しすぎると枯渇リスクあり。

**ワークフローへの適用**
Plan Mode でローカル設計 → `--remote` でリモート実装委譲 → `/teleport` でレビュー・マージ、という分業フローが即実践できる。試す価値 S 級。

**次のアクション**
1. 対象リポジトリに Claude GitHub App をインストール
2. 簡単なタスク（README修正等）で動作確認
3. `--remote` での並列実行を体験

