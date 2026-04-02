---
date: 2026-04-01
status: tried
relevance: S
tags: [claude-code, security, incident]
source_urls:
  - https://qiita.com/kai_kou/items/14f36ce73bffdd43a9e8
  - https://note.com/keen_godwit1349/n/n1b057306b78f
  - https://qiita.com/LostMyCode/items/a867e1954b80e78cf146
  - https://techcrunch.com/2026/03/31/anthropic-is-having-a-month/
experiment_dir: null
---

# Claude Code v2.1.88 ソースコード流出インシデント

## 3行要約

- Claude Code v2.1.88 の npmパッケージに `.map` ファイルが含まれており、約51万行のソースコードが閲覧可能な状態になっていた
- 同時期に axios サプライチェーン攻撃（RAT を含む悪意あるパッケージ）も発生し、Claude Codeユーザーへの複合的なリスクが指摘された
- 流出コードを Python に書き直して GitHub 公開した事例も報告され、著作権・ライセンス上の問題が議論された

## 自分への関連度: S

Claude Code を日常的に使用しており、セキュリティインシデントの内容把握と対策は直接業務に影響する。axios サプライチェーン攻撃はあらゆるJS開発者に関係する。

## 詳細

### ソースコード流出の経緯

v2.1.88 の npm パッケージに JavaScript の source map ファイル（`.map`）が誤って含まれた。Source map は本来デバッグ用にミニファイ済みコードと元コードを対応付けるものだが、今回は元の TypeScript ソースが丸ごと展開可能な状態だった。

### axios サプライチェーン攻撃

同時期に `axios` の typosquatting パッケージ（例: `axois`, `axxios`）に RAT (Remote Access Trojan) が仕込まれた攻撃が確認された。Claude Code の依存関係を調査し、不審なパッケージがないか確認が必要。

### 流出コードの二次拡散

流出したソースコードを Python に書き直した派生物が GitHub に公開された事例が発生し、オープンソースでない Claude Code のライセンス問題として議論を呼んだ。

## 試すなら

1. 使用中の Claude Code バージョンを確認: `claude --version`
2. npm audit で依存関係の脆弱性チェック: `npm audit`
3. `node_modules` 内に typosquatting 疑いのあるパッケージがないか確認
4. 対策済みバージョン（v2.1.88 以降の修正版）へのアップデート
5. [axios 公式パッケージ](https://www.npmjs.com/package/axios) のバージョンと checksum を確認

## ソース

- [Claude Code ソースコード流出と axios 攻撃 — 完全対策ガイド (Qiita)](https://qiita.com/kai_kou/items/14f36ce73bffdd43a9e8)
- [Claude Codeの全ソースコードが流出 — npmの.mapファイル1つで51万行が丸見えになった話 (note)](https://note.com/keen_godwit1349/n/n1b057306b78f)
- [Claude Code の流出ソースコードを著作権回避でGitHubに公開した件 (Qiita)](https://qiita.com/LostMyCode/items/a867e1954b80e78cf146)
- [Anthropic is having a month (TechCrunch)](https://techcrunch.com/2026/03/31/anthropic-is-having-a-month/)

---

## 感想・考察

詳細な実行ログ: [experiments/2026-04-01-claude-code-source-leak/](../experiments/2026-04-01-claude-code-source-leak/2026-04-01-claude-code-source-leak.md)

**良かった点**: KAIROS（常時稼働バックグラウンドエージェント＋autoDream）の存在が確認できた。「指示待ちAI」から「自律的に整理・準備するAI」への進化が具体的な実装として存在しており、今後のリリースが楽しみ。

**自分への影響**: 現在 v2.1.87 を使用中（流出版の直前）。Claude Code は単一バイナリなので axios 攻撃の影響範囲外。npm プロジェクトでは `plain-crypto-js` の存在チェックを習慣にしたい。

**Unity × AI の文脈**: KAIROS のようなバックグラウンドエージェントが実用化されれば、UnityプロジェクトでAIがアイドル中にアセット分析や最適化提案を行うユースケースが現実的になる。

**次のアクション**: `claude update` で v2.1.90 へアップデートする。npm publish するプロジェクトでは `.npmignore` の `*.map` 除外を確認する。
