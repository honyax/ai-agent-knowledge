---
date: 2026-04-17
status: read
relevance: S
tags: [claude-code, auto-mode, hooks, mobile-push, windows, pr-autofix]
source_urls:
  - https://code.claude.com/docs/en/whats-new
  - https://releasebot.io/updates/anthropic/claude-code
  - https://code.claude.com/docs/en/changelog
experiment_dir: null
---

# Claude Code 最新機能群 — Auto Mode / conditional if hooks / mobile push / Windows PowerShell

## 3行要約

- **Auto Mode（Research Preview）**: 権限プロンプトをAIが自動分類し、安全なアクションは無中断で実行、リスクある操作だけブロック。
- **conditional if hooks**: settings.json のフック定義に条件分岐を追加可能になり、状況に応じた自動化が実現。
- **Windows ネイティブ PowerShell ツール**、**モバイルプッシュ通知**、**Web版 PR auto-fix**、**/tui fullscreen** など多数の新機能が追加。

## 自分への関連度: S

Claude Code を日常的に使っているため、Auto Modeと conditional hooks は今すぐワークフローに取り込める。Windowsユーザーとして PowerShell ネイティブ対応も直接恩恵あり。

## 詳細

### Auto Mode（Research Preview）
権限プロンプトの自動処理。従来は全ての権限プロンプトにユーザー応答が必要だったが、AIクラシファイアが「安全」と判断したアクションは自動承認。「リスクあり」と判断した場合のみブロック。並列エージェント運用時の「張り付き」問題を軽減。

### conditional if hooks
settings.json でフックに条件式を記述できるようになった。例: ファイルタイプに応じてフック処理を切り替える、特定コマンド実行後のみ別処理を走らせる、など。

### その他の新機能
- **モバイルプッシュ通知**: Remote Control + 設定で有効化すると、Claudeがタスク完了時にモバイルに通知
- **PR auto-fix on Web**: WebバージョンでPRの問題を自動修正
- **transcript search**: `/` で会話履歴をインクリメンタル検索
- **Windows ネイティブ PowerShell ツール**: Windows環境での実行信頼性が向上
- **Computer Use（Desktop app）**: デスクトップアプリでコンピュータ操作ツールが利用可能
- **/tui fullscreen**: フリッカーフリーレンダリングへの切り替えコマンド

### v2.1.104 の主な変更
- チームオンボーディングガイド自動生成 `/team-onboarding` コマンド追加
- コマンドインジェクション脆弱性の修正
- エンタープライズ TLS プロキシ環境向け OS CA ストアサポート改善
- 40件超のバグ修正

## 試すなら

1. Claude Code で `/auto` または設定から Auto Mode を有効化して動作確認
2. settings.json に conditional if hooks を書いてみる（例: `.cs` ファイル変更後のみ Unity ビルド確認）
3. モバイルプッシュ通知を Remote Control と組み合わせて設定
4. Windows で PowerShell ツールの動作を確認

## ソース

- [What's new — Claude Code Docs](https://code.claude.com/docs/en/whats-new)
- [Claude Code Release Notes — Releasebot](https://releasebot.io/updates/anthropic/claude-code)
- [Changelog — Claude Code Docs](https://code.claude.com/docs/en/changelog)

---

## 感想・考察

conditional if hooks は記述がやや複雑になるが、AIに設定を書いてもらえば使い勝手はアリ。設定ファイル自体をAIと一緒に作るという運用で十分実用的になると思う。
