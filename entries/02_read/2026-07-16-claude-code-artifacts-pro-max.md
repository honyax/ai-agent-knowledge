---
date: 2026-07-16
status: read
relevance: S
tags: [claude-code, artifacts, pro-plan, mockup, 共有]
source_urls:
  - https://zenn.dev/canly/articles/64f112e3053834
  - https://zenn.dev/lnest_knowledge/articles/claude-code-artifacts-verification
  - https://code.claude.com/docs/en/changelog
experiment_dir: null
---

# Claude Code Artifacts が Pro / Max プランに開放（7/3〜）

## 3行要約

- Claude Code の **Artifacts 機能**（HTML/Markdown を claude.ai 上のプライベート Web ページとして公開し、URL で共有できる機能。6/19 頃登場）が、当初の **Team / Enterprise 限定から 7/3 に Pro / Max プランへ拡大**された。
- 動くモック・レポート・ダッシュボードを Claude Code から直接デプロイし、**URL 1 つで配布**できる。デフォルトは非公開（本人のみ）で、明示的に共有設定した場合のみ他者が閲覧可能。
- Zenn の実践記事（canly 氏）では「動くモックを組織内 URL で配る」ワークフローを紹介。mermaid 図のネイティブ描画、テーマ対応（light/dark）、外部リソース遮断（CSP）などの制約と特性も整理されている。

## 自分への関連度: S

**6 月に「Team/Enterprise 限定で自分には使えなかった」という検証記事（lnest）を読んで見送った機能が、Pro プランの自分でも使えるようになった**。今この瞬間から試せる。ナレッジベースの週次サマリーの可視化、Unity プロジェクトの設計図・進捗ダッシュボード、RTK/Headroom のトークン削減実績の可視化（[[2026-07-04-claude-code-doctor-fix-desktop-browser]] の `/dataviz` skill と組み合わせ）など、用途がすぐ思いつく。実際このセッション環境にも Artifact ツールと `/dataviz` skill が来ている。

## 詳細

### Artifacts とは（Claude Code 版）

- Claude Code が生成した **HTML / Markdown ファイルを claude.ai ホスティングの Web ページとして公開**する機能。
- デフォルトは**非公開**（作成者のみ閲覧可）。共有を選択すればチームメイトへ URL 配布可能。
- 同じファイルパスで再デプロイすると**同じ URL が更新**される（バージョン管理あり）。

### 主な特性・制約

- **自己完結必須**: 厳格な CSP により外部ホストへのリクエスト（CDN スクリプト、外部フォント、画像、fetch）は全てブロック。CSS/JS はインライン、アセットは data: URI で埋め込む。
- **mermaid 図はネイティブ描画**（外部ライブラリ不要）。
- **テーマ対応**: 閲覧者の light/dark テーマに追従。
- **レスポンシブ前提**: 幅広コンテンツはコンテナ内スクロール。

### プラン展開の経緯

| 時期 | 状態 |
|------|------|
| 6/19 頃 | Artifacts 登場（Team / Enterprise 限定） |
| 6 月下旬 | Pro ユーザーの「使えなかった」検証記事が出る（lnest） |
| **7/3** | **Pro / Max プランへ拡大** |

### ユースケース（canly 記事より）

- 動く UI モックを実装前にステークホルダーへ URL 配布
- 調査レポート・技術ドキュメントの配布
- データ可視化ダッシュボード（`/dataviz` skill と相性が良い）

## 試すなら

1. Claude Code で簡単な HTML（例: このナレッジベースの週次サマリーの可視化ページ）を書き、Artifact として公開してみる。
2. URL がプライベート（自分のみ閲覧可）で発行されることを確認し、ブラウザで表示品質（テーマ追従、mermaid 描画）を確認。
3. 同じファイルを編集して再デプロイし、同一 URL が更新されることを確認。
4. `/dataviz` skill を読み込ませた上でトークン消費や catch-up エントリ統計のダッシュボードを作り、実用性を評価。
5. 有用なら `/digest` skill の出力先として Artifact を組み込む改修を検討（ナレッジベースの状態を常設 URL で見られるように）。

## ソース

- [【2026/6/19最新アプデ】Claude Code 新機能『Artifacts』が登場！動くモックを組織内 URL で配ってみた (Zenn, canly)](https://zenn.dev/canly/articles/64f112e3053834)
- [Claude Code Artifactsを試したら、Team/Enterprise限定で自分には使えなかった話 (Zenn, lnest)](https://zenn.dev/lnest_knowledge/articles/claude-code-artifacts-verification)
- [Claude Code changelog (公式)](https://code.claude.com/docs/en/changelog)

---

## 感想・考察

読了時に公式ドキュメント（code.claude.com/docs/en/artifacts）を確認し、疑問点を整理した（2026-07-16）。

### ホスティングの実態

- Artifacts は claude.ai（Anthropic のインフラ）が静的ページとして配信する仕組み。自分でサーバーを用意する必要はない。
- ただし汎用ホスティング（Netlify / GitHub Pages 等）とは別物。サーバーサイドコードは動かず、厳格な CSP で外部通信（CDN、フォント、fetch/XHR、WebSocket）が全遮断されるため、API を叩くアプリは作れない。「作業成果物を URL 1 つで見せる共有機能」と捉えるのが正確。
- レンダリング後のページサイズは 16 MiB 以下。単一ページのみで相対リンクは解決されない（ページ内アンカーで代用）。

### 共有範囲はプラン依存（エントリ本文の補正）

本文の「チームメイトへ URL 配布」は Team/Enterprise の話で、Pro プランでは事情が異なる。

- **Team / Enterprise**: 組織内の特定メンバーまたは組織全体に共有。閲覧者は組織メンバーとして claude.ai へのサインインが必要。パブリック共有は Owner が許可した場合のみ。
- **Pro / Max（自分の場合）**: 共有手段は**パブリックリンクのみ**。リンクを知っている人は claude.ai サインイン不要で誰でも閲覧できる（Claude アカウント不要）。
- つまり Pro での選択肢は「自分だけ（デフォルト非公開）」か「リンクを知る全員（公開）」の二択。中間の共有範囲はない。

### 運用上の注意

- ページヘッダーに作者名が表示され、自分のギャラリー（claude.ai/code/artifacts）へのリンクも付く。公開リンク配布は「誰が作ったか」が見える前提。
- 公開リンクは誰でも開けるため、機密情報を含むページは公開しない。
- Share メニューで閲覧者に見せるバージョンを固定するか、常に最新を見せるかを選べる。
- 利用には claude.ai アカウントでの `/login` が必須（API キー経由のセッションでは publish 不可）。

### 使いどころ

個人利用（Pro）では「他者共有」より「自分用の常設ビュー」が主用途になりそう。ナレッジベースの週次サマリーや catch-up 統計のダッシュボード（/dataviz と組み合わせ）を非公開のまま常設 URL で持つ運用が現実的。他者に見せる場合はパブリックリンクになる点だけ意識する。
