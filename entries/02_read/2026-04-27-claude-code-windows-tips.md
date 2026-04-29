---
date: 2026-04-27
status: read
relevance: A
tags: [claude-code, windows, ime, tips, mcp, git]
source_urls:
  - https://qiita.com/saitoko/items/fd96304c9beb067446d0
experiment_dir: null
---

# Claude Code 実用TIPS集 — Windows × 日本語IME のハマりどころ

## 3行要約

- Windows + 日本語 IME 環境で Claude Code を使う際の典型的なハマりポイントとその回避策をまとめた記事
- 致命的なのは「IME 変換確定の Enter がプロンプト送信に取られる」「変換候補ウィンドウが画面端にずれる」の2つ。前者は根本解決策がまだなく短文プロンプトでの運用回避を推奨
- その他: MCP 設定後の新セッション必須・Git 設定（autocrlf/quotepath）の初期化・`.mcp.json` の Git 管理除外（セキュリティ）

## 自分への関連度: A

自分は Windows 11 環境で日本語入力を頻繁に使うため、直接該当する。特に IME Enter 誤送信は日常的に体験している既知の痛みで、「短文プロンプト運用」が現状の最善策と確認できたのは判断材料として有用。

## 詳細

### Windows × IME の主要トラブル

**TIPS 1: IME 変換確定の Enter 誤送信**
- 日本語変換確定時の Enter がプロンプト送信として処理される
- 根本解決策はまだ存在しない（Claude Code 側の制約）
- 回避策: 短文プロンプトでの運用に寄せる、または変換確定後 Shift+Enter で改行してから送信

**TIPS 2: 変換候補ウィンドウの位置ずれ**
- ターミナルのカーソル位置情報が正しく伝わらず、候補ウィンドウが画面端に表示される
- 機能自体は動作するため、表示位置に依存せず候補内容で判断して続行可能

### その他の重要TIPS

- **MCP 設定後の新セッション開始**が必須（既存セッションには反映されない）
- **Git 設定の初期化**: `core.autocrlf=false`・`core.quotepath=false`（日本語パスとファイル名対応）
- **`.mcp.json` の Git 管理除外**: API キー等が含まれる場合のセキュリティ対策
- **短文プロンプト最適化**: IME 問題回避と読みやすさの両面で有効

### 自分の現状確認ポイント

- グローバル `.gitignore` に `.mcp.json` が入っているか
- `git config --global core.autocrlf` の値（false 推奨）
- 長文プロンプトを書く際は別エディタ → 貼り付けの運用が安定

## 試すなら

1. `git config --global core.autocrlf` の現在値を確認、false でなければ変更を検討
2. グローバル `.gitignore` に `.mcp.json` を追加
3. 長文プロンプトは VS Code 等で書いて貼り付けるワークフローに切り替え
4. IME Enter 誤送信が頻繁な場合、Shift+Enter で改行してから送信する習慣化
5. 1週間運用して、IME 起因のミス送信頻度が下がったか確認

## ソース

- [Claude Code 実用TIPS集 — 知っていれば5秒、知らなければ1時間 (Qiita)](https://qiita.com/saitoko/items/fd96304c9beb067446d0)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
