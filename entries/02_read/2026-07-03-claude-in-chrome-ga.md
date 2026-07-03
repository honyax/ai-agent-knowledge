---
date: 2026-07-03
status: read
relevance: B
tags: [claude-in-chrome, browser-extension, ga, claude-code, agentic-browsing]
source_urls:
  - https://claude.com/claude-for-chrome
  - https://support.claude.com/en/articles/12012173-get-started-with-claude-in-chrome
  - https://code.claude.com/docs/en/chrome
  - https://www.xda-developers.com/claude-chrome-extension-regret/
experiment_dir: null
---

# Claude in Chrome が GA、Claude Code とブラウザが連携

## 3行要約

- 4/27 からベータ提供されていた Chrome 拡張 **Claude in Chrome** が 7 月に **GA（一般提供）**。Pro / Max / Team / Enterprise の全有償プランで Chrome Web Store から利用可能に。
- ブラウザ内をナビゲート、情報抽出、操作（フォーム入力・クリック等）を Claude が代行。**Claude Code とも連携**し、コード変更を実ブラウザで検証するようなワークフローに対応（`code.claude.com/docs/en/chrome`）。
- 同時に Claude Code 側もエージェントワークフロー拡張（background notification、draft PR handoff、failover 改善）と `/dataviz` skill を追加。

## 自分への関連度: B

普段の Unity/UE5.8 開発ではブラウザ操作の自動化は直接使わないが、CLAUDE.md の「UIやフロントエンド変更はブラウザで実際に確認する」方針と相性が良い可能性がある。Web 系のサブプロジェクト（React/TypeScript学習中）で、Claude Code が生成した UI をブラウザで自動検証させる用途に使える見込みがあれば A に格上げ。

## 詳細

### GA での変更点

- ベータ期間（4/27〜）は招待制/制限付きだったが、GA で **Chrome Web Store から誰でもインストール可能**（有償プラン前提）。
- 主要機能: ページナビゲーション、情報抽出、フォーム操作、複数タブ間の操作。

### Claude Code との連携

- `code.claude.com/docs/en/chrome` に専用ドキュメントが用意されている。
- Claude Code がコードを変更した後、**実際のブラウザで動作確認までさせる**ワークフローが想定されている。
- [[2026-07-01-loop-engineering-boris-cherny]] の「エージェントが自分の出力を検証する」原則の実装の一つと解釈できる（フロントエンド版の self-verification）。

### 同時リリースの関連機能

- **`/dataviz` skill**: チャート/ダッシュボード設計のガイダンス skill。実行可能なカラーパレット validator 付き（今回の catch-up 中にも `dataviz` skill が利用可能として案内されている）。
- **Gateway 拡張**: Claude Platform on AWS (`anthropicAws`) が upstream provider に追加、model-not-found 時の failover chain 改善（[[2026-07-01-claude-apps-gateway-bedrock-vertex]] の続報）。

### レビュー記事の反応

- XDA Developers: 「もっと早く使えばよかったと後悔した」との高評価レビュー。実用性の評判は良さそう。

## 試すなら

1. Chrome Web Store で Claude 拡張をインストールし、Pro プランでの利用可否を確認。
2. `code.claude.com/docs/en/chrome` を読み、Claude Code との連携セットアップ手順を確認。
3. React/TypeScript 学習中のサブプロジェクトで、UI 変更後にブラウザ上での自動検証を試す（CLAUDE.md の「UIやフロントエンド変更はブラウザで実際に確認する」方針の自動化）。
4. `/dataviz` skill を試しに使い、既存のトークン消費データ（RTK/Headroom の削減実績等）を可視化してみる。

## ソース

- [Claude for Chrome (Anthropic 公式)](https://claude.com/claude-for-chrome)
- [Get started with Claude in Chrome (Claude Help Center)](https://support.claude.com/en/articles/12012173-get-started-with-claude-in-chrome)
- [Use Claude Code with Chrome (Claude Code Docs)](https://code.claude.com/docs/en/chrome)
- [I finally tried the Claude Chrome extension, and I regret waiting this long (XDA Developers)](https://www.xda-developers.com/claude-chrome-extension-regret/)

---

## 感想・考察

### 「ページ内情報へのアクセス」だけでなく「操作」と「Claude Code の自己検証」も含む（2026-07-03）

「ページ内の情報にアクセスできる」という理解は正しいが、実は 2 つの側面がある。

**1. Claude.ai の会話中にブラウザを操作させる**

- 情報抽出: 開いているページのテキスト・表・構造を読み取って回答に使う
- 操作: フォーム入力、ボタンクリック、タブ間の移動
- ナビゲーション: リンクをたどる、ページ遷移

これは Claude.ai 単体（chat.claude.ai）ではできなかった「今画面に映っているものを見て」を可能にする拡張。

**2. Claude Code と連携させる（こちらがより実用的）**

Claude Code がコードを書いた後、**Claude Code 自身がその拡張機能経由でブラウザを開いて実際に動いているか確認する**という使い方。例えば React コンポーネントを修正 → Claude Code が拡張機能でローカルサーバーを開く → スクリーンショットや DOM を見て表示を確認 → 問題があれば自分で気づいて直す、というループ。

CLAUDE.md の「UIやフロントエンド変更は実際にブラウザで確認する」という方針を、**人間ではなく Claude 自身にやらせる**形。今まで「動いてるはずです」で終わっていたところを、実際に見て確認させられる点が本質。[[2026-07-01-loop-engineering-boris-cherny]] の「エージェントが自分の出力を検証する」原則のフロントエンド版実装と言える。

<!-- /try 実行時に自動生成 -->
