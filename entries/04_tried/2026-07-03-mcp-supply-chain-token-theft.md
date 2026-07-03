---
date: 2026-07-03
status: tried
relevance: S
tags: [security, mcp, supply-chain-attack, npm, oauth, token-theft, claude-code]
source_urls:
  - https://www.csoonline.com/article/4181230/claude-code-has-an-mcp-security-problem-and-your-developers-are-already-using-it.html
  - https://thenewstack.io/agentjacking-sentry-mcp-attack/
  - https://www.darkreading.com/application-security/flaws-claude-code-developer-machines-risk
  - https://www.esecurityplanet.com/threats/claude-code-mcp-attack-enables-persistent-token-theft/
experiment_dir: null
---

# npm パッケージの post-install hook が `~/.claude.json` を書き換え、MCP 経由の OAuth トークンを窃取する攻撃

## 3行要約

- Mitiga Labs の研究者が、悪意ある npm パッケージの post-install hook が **`~/.claude.json`（Claude Code が MCP トラフィックをルーティングする設定ファイル）を書き換える**攻撃チェーンを発表。書き換えられると Claude Code の認証済みリクエストが攻撃者インフラに向き、その過程で保存済み OAuth トークン（Jira / Confluence / GitHub など接続済み SaaS 全て）が横取りされる。
- 監査ログ上は「MCP サーバーエンドポイントの変更」として現れるだけで、新規 localhost プロキシや見慣れない外部エンドポイントの追加として検知しないと気づきにくい。
- 過去にも同種の脆弱性あり: **CVE-2025-59536**（リポジトリの settings ファイルに仕込まれた悪意ある hook による RCE、trust dialog を読む前に実行される）、**CVE-2026-21852**（環境変数上書きによる API キー窃取）。

## 自分への関連度: S

CLAUDE.md 関心領域 3「AI開発ツールのセキュリティリスクと対策」に直結。自分は RTK ([[2026-07-01-rtk-rust-token-killer]])、Discord 連携 Skill、複数の MCP サーバーを日常的に使っており、`~/.claude.json` は既に攻撃対象になり得る状態。npm パッケージの post-install hook 経由という「サプライチェーン攻撃」の形態は、普段 `npm install` するときの警戒心を上げる必要がある具体例。

## 詳細

### 攻撃チェーン

1. 正規ユーティリティ/ラッパーに見える **悪意ある npm パッケージ**をインストール
2. インストール中に **post-install hook が静かに実行**
3. hook が `~/.claude.json`（MCP トラフィックのルーティング設定ファイル）を書き換え
4. Claude Code の認証済みリクエストが **攻撃者制御のインフラ**に向くようになる
5. その過程で **OAuth トークンが transit 中に横取り**される
6. 攻撃者は接続済み全 SaaS（Jira, Confluence, GitHub 等）への **長命 bearer トークン**を手に入れる

### 検知の難しさ

- 監査ログには「MCP サーバーエンドポイントの変更」としか残らない
- 新規 localhost プロキシアドレスや見慣れない外部エンドポイントの追加を **能動的にアラート**しないと気づけない
- 通常の開発フローでは `~/.claude.json` を頻繁にチェックする習慣がない

### 過去の関連脆弱性（2026年2月, Check Point Research）

- **CVE-2025-59536**: リポジトリの settings ファイルに仕込まれた悪意ある hook による RCE。ユーザーが trust dialog を読む **前に** コードが実行される。
- **CVE-2026-21852**: 単一の環境変数を上書きすることで API キーを外部に持ち出す。

### 関連する別の攻撃（The New Stack）

- 「public Sentry key が Claude Code / Cursor / Codex を乗っ取るのに十分」という別系統の記事も同時期に出ている（agentjacking, Sentry MCP attack）。MCP エコシステム全体でこの種の「設定ファイル書き換え→トークン窃取」パターンが増えている可能性。

## 試すなら

1. `~/.claude.json` の内容を今すぐ確認し、身に覚えのない MCP サーバーエンドポイント（特に localhost プロキシ）が無いか目視チェック。
2. `~/.claude.json` を git 管理下に置くか、変更検知（`chattr`, ファイル整合性監視ツール, または簡易な hash 比較スクリプト）を仕込む。
3. 新規 npm パッケージをインストールする際、`--ignore-scripts` フラグを使って post-install hook を無効化する運用を検討（ただし正当な hook が必要なパッケージは動作しなくなる点に注意）。
4. 接続済み MCP サーバーの OAuth トークンを定期的に確認し、不要なものは `claude mcp logout` で失効させる（[[2026-07-01-claude-code-v21180-v21193]] の `claude mcp login/logout` 参照）。
5. CVE-2025-59536 / CVE-2026-21852 のパッチが自分の Claude Code バージョンに適用済みか `claude --version` で確認。

## ソース

- [Claude Code has an MCP security problem — and your developers are already using it (CSO Online)](https://www.csoonline.com/article/4181230/claude-code-has-an-mcp-security-problem-and-your-developers-are-already-using-it.html)
- [A public Sentry key is all it takes to hijack Claude Code, Cursor, and Codex (The New Stack)](https://thenewstack.io/agentjacking-sentry-mcp-attack/)
- [Flaws in Claude Code Put Developers' Machines at Risk (Dark Reading)](https://www.darkreading.com/application-security/flaws-claude-code-developer-machines-risk)
- [Claude Code MCP Attack Enables Persistent Token Theft (eSecurity Planet)](https://www.esecurityplanet.com/threats/claude-code-mcp-attack-enables-persistent-token-theft/)

---

## 感想・考察

### 実際に `~/.claude.json`（`C:\Users\honya\.claude.json`）を確認（2026-07-03）

「試すなら」の手順 1 を実施。全体を読み、`mcpServers` セクション（トップレベル + 全プロジェクト個別設定）を確認した結果:

| サーバー名 | 種別 | 内容 | 判定 |
|-----------|------|------|------|
| `unity-mcp` | stdio | ローカルの `relay_win.exe` を起動 | 正規（Unity 連携） |
| `godot-mcp` | http | `http://127.0.0.1:3000/mcp` | **本人に確認 → 正規**（Godot エンジン開発用に自分で設定したもの） |
| `blender-mcp`（複数プロジェクト） | stdio | `uvx blender-mcp` をローカル起動 | 正規（Blender 連携） |
| `elicitation-demo` | stdio | ローカル Python スクリプト | 正規（自分の実験用） |

`claudeAiMcpEverConnected` には Google Drive / Gmail / Google Calendar / Claude Code Remote の Anthropic 公式コネクタのみ。不審な外部エンドポイントへの書き換えは見つからなかった。

`godot-mcp` が localhost の http 型で、記事が警告する「localhost プロキシの追加」パターンに形が似ていたため一度立ち止まって確認したが、本人が意図して設定したものと確定。**「見慣れない設定に気づいたら都度本人確認する」というチェックの実演**になった。

### 今後の運用方針: 「常時警戒」ではなく「npm install の瞬間」に絞る

この攻撃は npm エコシステムの構造的な問題（post-install hook が任意コード実行できる）に起因するため、**一度直して終わりにはならない継続的なリスク**と判断。ただし現実的な運用としては:

1. 新しい npm グローバルツールを入れた直後だけ `~/.claude.json` の `mcpServers` セクションを一瞥する
2. マイナー/知らない npm パッケージをインストールする時は一段警戒する
3. Claude Code 自体を最新版に保つ（v2.1.196 で `.mcp.json` 自動起動範囲が絞られるなど、Anthropic 側も継続対応している。[[2026-07-03-claude-code-v21196-v21197]]）
4. Claude Code の挙動がおかしいと感じたら、その時点で確認する

「毎日全部チェック」のような重い運用ではなく、リスクが顕在化するタイミング（npm install 直後）に絞った軽い習慣で十分と判断。

### 根本的な疑問: これは Claude 側の設計問題では？

「無関係な npm パッケージの post-install hook が、なぜ Claude Code の設定ファイルを書き換えられるのか」という疑問を検討した結果、**2 つの異なるレイヤーが混在している**ことが分かった。

**レイヤー1（Claude側の問題ではない）**: post-install hook はユーザー権限で動く任意コード実行であり、OS のファイルパーミッションはアプリ単位の隔離を提供しない。`~/.ssh/`、`~/.aws/credentials`、`~/.npmrc` なども同じ構造的リスクを抱える。npm/Node.js エコシステム全体の弱点（`event-stream` 事件等の系譜）であり、Claude Code 固有の問題ではない。

**レイヤー2（Claude側の設計判断が関わる）**: 「実行された後に盗める価値のあるものが平文で置いてある」かどうかは設計次第。他の多くの CLI ツール（git credential manager、各種クラウド CLI）は OAuth トークン等を **OS ネイティブの暗号化ストレージ**（macOS Keychain / Windows Credential Manager / Linux libsecret）に保存し、同じ任意コード実行が起きても簡単には読み出せないようにしている。Claude Code がトークンをプレーン JSON に置く設計を採っているなら、それは「実行後に何が盗めるか」の範囲を自ら広げている。

**結論**: 「無関係なコードに書き換えられること自体」は npm/OS の構造問題で Claude 側では解決不可能だが、「書き換えられた結果トークンが盗めてしまうこと」は Claude Code の保存方式の設計判断であり、正当な改善要求として持ち得る視点。「サプライチェーン攻撃だから仕方ない」で思考停止せず、「なぜ OS のセキュアストレージを使っていないのか」を今後の Claude Code アップデートで注視する価値がある。

<!-- /try 実行時に自動生成 -->
