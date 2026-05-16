---
date: 2026-05-16
status: read
relevance: A
tags: [security, supply-chain, claude-code, infostealer, malware, google-ads, powershell, abe-bypass, developer-target]
source_urls:
  - https://www.theregister.com/security/2026/05/11/cookie-thieves-caught-stealing-dev-secrets/5238248
  - https://www.infosecurity-magazine.com/news/fake-claude-code-installer/
  - https://hackread.com/fake-claude-code-installer-devs-browser-credential-stealer/
  - https://www.malwarebytes.com/blog/news/2026/03/fake-claude-code-install-pages-hit-windows-and-mac-users-with-infostealers
  - https://www.trendmicro.com/en_us/research/26/e/installfix-and-claude-code.html
experiment_dir: null
---

# 偽 Claude Code インストーラ「InstallFix」キャンペーン — Google Ads 経由でブラウザ Cookie/Password/支払い情報を窃取

## 3行要約

- 2026-05-11 The Register 報道。**Google で「install Claude code」を検索したユーザを偽サイトに誘導**するキャンペーンが拡大中。スポンサー結果から **`events.msft23.com` ホスト**（本物は `claude.ai`）が示す PowerShell コマンドを実行させる手口で、600KB の難読化 PowerShell スクリプトが落ちてくる
- ペイロードは未知のマルウェア族で、**Chromium 系ブラウザ（Chrome / Edge / Brave / Vivaldi / Opera）の Cookie・パスワード・決済情報を復号化して窃取**。**Google の App-Bound Encryption (ABE) を `payload_x64.bin` という小型 native helper を `process hollowing` で正規ブラウザプロセスに注入してバイパス**（2026-03-24 ビルドの新型）
- 開発者狙いの supply-chain 攻撃として、2026-04-01 の **Claude Code ソース npm リーク** や 2026-03 月の偽インストールページ infostealer キャンペーン（Malwarebytes 報告）と連続する流れ。Trend Micro は「InstallFix」と命名し継続トラッキング中

## 自分への関連度: A

CLAUDE.md の関心領域3「AI開発ツールのセキュリティリスクと対策」に直撃。さらに以下の理由で実害リスクが高い:

- **Windows 環境ユーザ**: 攻撃ペイロードは PowerShell + Chromium ABE bypass で、自分の環境（Windows 11 + Chromium 系ブラウザ）はターゲットど真ん中
- **Claude Code を実際に使っているため標的層**: 「install Claude code」を検索する開発者を狙う設計。普段から正規 URL（`claude.ai/install` / `code.claude.com`）をブックマーク済みでも、別 PC からアクセスする際にうっかり Google 経由でアクセスする可能性
- **既存エントリ群と連続する文脈**: 2026-04-01 [claude-code-source-leak.md](../04_tried/2026-04-01-claude-code-source-leak.md)、2026-03 月の Malwarebytes 報告と一連。Anthropic 公式名を悪用したサプライチェーン攻撃のテンプレートが確立しつつある
- **Cookie 窃取は SSO セッション乗っ取りに直結**: GitHub・Anthropic コンソール・Google Workspace 等のセッションが盗まれると、被害は単発のクレデンシャル流出を遥かに超える
- **PowerShell ベース**: v2.1.143 で Windows の PowerShell tool が `-ExecutionPolicy Bypass` 既定化された直後のタイミングで、PowerShell 経由のマルウェア対策の重要性が一層上がっている

## 詳細

### 攻撃チェーン

1. **被害者**: Google で「install Claude code」「Claude code download」等を検索
2. **誘導**: 最上位のスポンサー（Google Ads）結果が偽ページに誘導
3. **見た目**: ページは本物の Anthropic 風 UI で、インストールコマンドを表示
4. **本物との違い**: コマンドのホスト名が **`claude.ai` → `events.msft23.com`** に置換されている
5. **実行**: ユーザがコマンドを PowerShell に貼り付けて実行 → `Invoke-RestMethod` で 600KB の難読化 PowerShell スクリプトを取得・実行
6. **永続化**: `payload_x64.bin`（24 March 2026 build、ABE bypass 用）を展開し、Chromium ブラウザプロセスに **process hollowing** で注入
7. **窃取**: Cookie・保存パスワード・支払い情報を ABE をバイパスして復号、C2 へ送信

### 技術的特徴

- **未知のマルウェア族**: 既知の infostealer 系列（RedLine / Lumma / Vidar 等）と署名が一致せず、独自実装
- **ABE バイパス**: Google が Chrome に導入した App-Bound Encryption は本来 cookie 暗号化を OS プロセス境界で守る仕組み。これを **正規ブラウザプロセスへの注入で復号化済みデータをメモリから抜く** ことで回避
- **対象**: Windows + Chromium 系（Chrome / Edge / Brave / Vivaldi / Opera）。Malwarebytes の先行報告では Mac も別ペイロードで標的化

### 防御策

1. **Anthropic 公式 URL を直打ち**: `claude.ai/install`・`code.claude.com`・`docs.claude.com`・`github.com/anthropics/claude-code` をブックマーク
2. **Google 検索でのスポンサー結果を回避**: 「Sponsored」「広告」表記のリンクをクリックしない
3. **インストールコマンドのホスト名を必ず目視確認**: `claude.ai` でなければ実行しない
4. **PowerShell 実行ポリシー**: `Get-ExecutionPolicy` で確認、不審なスクリプト実行時はホスト名と署名を確認
5. **ブラウザ拡張・パスワードマネージャ**: 1Password / Bitwarden 等の vault を分離、ブラウザ保存のみに依存しない
6. **Cookie 流出時の対応**: GitHub / Anthropic コンソール / Google Workspace の **session-revoke**（全セッションの強制ログアウト）を実行手順としてドキュメント化

## 試すなら

実環境で攻撃を再現するのは危険なため、検証は以下の範囲で:

1. 自分の Windows マシンで `claude --version` の出力と、`where claude` の実行パスを確認（正規バイナリの場所を把握）
2. ブラウザに保存されている Anthropic Console・GitHub・主要 SaaS のセッション Cookie を点検し、不審なものは削除
3. 各種 SaaS で「全セッション強制ログアウト」の手順を確認しドキュメント化（後の有事に備える）
4. `https://www.virustotal.com/` 等で `events.msft23.com` の評価を確認
5. ai-agent-knowledge の `experiments/` 配下にセキュリティチェックリスト Skill を作成検討（CLAUDE.md のセキュリティ関心領域に沿った形）

## ソース

- [Cookie thieves caught stealing dev secrets via fake Claude Code installers (The Register, 2026-05-11)](https://www.theregister.com/security/2026/05/11/cookie-thieves-caught-stealing-dev-secrets/5238248)
- [Fake Claude Code Page Pushes PowerShell Stealer at Devs (Infosecurity Magazine)](https://www.infosecurity-magazine.com/news/fake-claude-code-installer/)
- [Fake Claude Code Installer Targets Developers With Browser Credential Stealer (Hackread)](https://hackread.com/fake-claude-code-installer-devs-browser-credential-stealer/)
- [Fake Claude Code install pages hit Windows and Mac users with infostealers (Malwarebytes, 2026-03)](https://www.malwarebytes.com/blog/news/2026/03/fake-claude-code-install-pages-hit-windows-and-mac-users-with-infostealers)
- [InstallFix and Claude Code: How Fake Install Pages Lead to Real Compromise (Trend Micro)](https://www.trendmicro.com/en_us/research/26/e/installfix-and-claude-code.html)

---

## 感想・考察

Claude Code は最近は一般人にも情報が流れてきているので、これからエンジニアを目指そうとしている人も大勢使うでしょうし、そこらへんを狙った手口ということだろう。攻撃者の視点で整理すると以下の構図が見える:

- **ターゲット層の広がり**: 従来 infostealer は「Adobe / Notion / Zoom の偽インストーラ」が定番だったが、今回 **Claude Code が同じレイヤーに格上げされた** という事実が重要。攻撃者が「開発ツール名 = 一般人も検索する語」と認識した証拠
- **開発者 = 価値の高い被害者**: 一般ユーザの Cookie より、開発者の **GitHub / npm / クラウドコンソール / Anthropic API キー** のセッションの方が圧倒的に高値で売れる。エンジニア初学者は「dev secrets はまだ持ってない」と思いがちだが、**学習用アカウントでも GitHub Student Pack / 個人 OpenAI/Anthropic キー / クレカ紐付け済み** だったりで十分カモになる
- **「install Claude code」という検索語の罠**: 公式ドキュメントを読み慣れた人は `npm i -g @anthropic-ai/claude-code` を直接叩くが、初学者ほど **Google で「install」を検索して最上位のスポンサー結果をクリック** しがち。Google Ads の審査をすり抜けるのも今回のキャンペーンの特徴
- **PowerShell ワンライナー文化との相性の悪さ**: 「公式が PowerShell コマンドを案内する」のは Claude Code / winget / Scoop / oh-my-posh など今や普通で、**ホスト名さえ差し替えれば見分けがつかない**。これは初学者だけでなく中級者も引っかかり得る

つまり「Claude Code が普及した副作用として、攻撃面が一気に拡大した」というのが本質的な変化。CLAUDE.md の関心領域3（セキュリティ）にも、関心領域1（Claude Code の普及動向）にも刺さる。
