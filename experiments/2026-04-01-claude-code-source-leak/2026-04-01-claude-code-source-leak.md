# 実験ログ: Claude Code v2.1.88 ソースコード流出インシデント

実施日: 2026-04-03
対応エントリ: [entries/2026-04-01-claude-code-source-leak.md](../../entries/2026-04-01-claude-code-source-leak.md)

---

## Step 1: バージョン確認

```
$ claude --version
2.1.87 (Claude Code)
```

**結果**: 現在のバージョンは **v2.1.87** — 流出が発生した v2.1.88 の1つ前。直接の影響なし。
npm で確認した最新版は **v2.1.90**（流出修正済み）。アップデートは `claude update` で対応可能。

---

## Step 2: npm audit（このリポジトリ）

このリポジトリには `package.json` が存在しないため、npm audit の対象外。
グローバル npm パッケージ一覧:

```
C:\Users\honya\AppData\Roaming\npm
+-- @google/gemini-cli@0.1.7
+-- @zed-industries/claude-code-acp@0.10.0
+-- opencode-ai@1.0.51
+-- typescript@5.4.5
+-- yarn@1.22.22
```

---

## Step 3: typosquatting 確認

```
$ npm list -g --depth=1 | grep -i -E "axo|axxio|axoi|axiox"
→ typosquatting candidates: none found
```

**結果**: 疑わしいパッケージは検出されなかった。

### axios サプライチェーン攻撃の詳細

- **感染バージョン**: `axios@1.14.1` / `axios@0.30.4`
- **感染ウィンドウ**: 2026-03-31 00:21〜03:29 UTC（3時間程度）
- **感染確認方法**: `package-lock.json` に `plain-crypto-js` の記載があれば要注意
- **Claude Code バイナリ自体**: 228MB の単一実行ファイル（`node_modules` なし）のため、axios 攻撃の直接的な影響範囲外

---

## Step 4: 流出コードの分析（公開情報より整理）

### 流出の原因

- Bun ランタイムがビルド時にデフォルトでソースマップ（`.map`）を生成
- `.npmignore` に `*.map` の除外設定がなかった
- 59.8MB のソースマップファイルが v2.1.88 の npm パッケージに混入
- 約1,900 ファイル・512,000行以上の TypeScript ソースが閲覧可能に

### KAIROS プロジェクト

流出コード内で最も注目された内部コードネーム。

| 項目 | 内容 |
|------|------|
| 語源 | 古代ギリシャ語「kairos」= 適切なタイミング |
| 言及数 | 150回以上 |
| 概要 | 常時稼働バックグラウンドエージェント（デーモンモード） |
| 動作 | ユーザーのアイドル時に自律的にタスクを処理 |

**autoDream 機能**: ユーザーのアイドル中にメモリ統合を行う。
- バラバラな観察・情報を統合
- 論理的矛盾を除去
- 曖昧なインサイトを確定的な事実に変換
- ユーザーが戻ったとき、エージェントのコンテキストが整理された状態になる

現在の Claude Code は基本的にリアクティブ（ユーザーの指示待ち）だが、KAIROS は**プロアクティブな自律エージェント**への進化を示す。

### その他の流出コードから判明した機能

- **Undercover Mode**: 何らかの隠密動作モード（詳細非公開）
- **Fake Tools**: テスト用のモックツール群
- **Frustration Regexes**: ユーザーのフラストレーション検出パターン
- **Feature Flags**: 107個以上の機能フラグが存在（段階的リリース管理）

### ビジネス的文脈

- Claude Code の ARR: **25億ドル**（エンタープライズが80%）
- 競合他社に「高エージェンシーAIの実装ブループリント」が渡った形

---

## 観察・考察

### セキュリティの教訓

- Source map の混入は「高度な攻撃」ではなく `.npmignore` の設定漏れという**初歩的なミス**
- 「AI時代のセキュリティは高度な機能ではなく基礎的な設定から崩れる」という指摘が的確
- 自分のプロジェクトでも npm publish 時の `.npmignore` / `files` フィールドは要確認

### KAIROS が示すもの

- Claude Code の次のフェーズは「指示待ち」から「自律的な常時稼働」へ
- autoDream は「寝ている間に整理しておく」という、人間的な作業管理に近い概念
- Unity × AI 連携でいえば、バックグラウンドでアセットの依存関係分析や最適化提案をするようなユースケースが現実になりうる

### 自分の環境への影響

- v2.1.87 を使用中 → 直接の流出影響なし
- Claude Code は単一バイナリなので axios 攻撃の影響範囲外
- npm プロジェクトでは `plain-crypto-js` の有無チェックを習慣化する価値あり
- v2.1.90 へのアップデートは次回の作業開始前に実施予定

---

## 参照

- [VentureBeat: Claude Code's source code appears to have leaked](https://venturebeat.com/technology/claude-codes-source-code-appears-to-have-leaked-heres-what-we-know)
- [The Information: Claude Code Leak Reveals Always-On 'Kairos' Agent](https://www.theinformation.com/newsletters/ai-agenda/claude-code-leak-reveals-always-kairos-agent)
- [Alex Kim's blog: The Claude Code Source Leak](https://alex000kim.com/posts/2026-03-31-claude-code-source-leak/)
- [claudefast.com: Claude Code Source Leak Everything Found](https://claudefa.st/blog/guide/mechanics/claude-code-source-leak)
