---
date: 2026-05-16
status: read
relevance: A
tags: [claude-code, output-format, html, markdown, claude-md, anthropic-blog, thariq-shihipar, design-philosophy]
source_urls:
  - https://simonwillison.net/2026/May/8/unreasonable-effectiveness-of-html/
  - https://pasqualepillitteri.it/en/news/2243/html-vs-markdown-claude-code-thariq-anthropic
  - https://news.ycombinator.com/item?id=48071940
  - https://www.agentupdate.ai/news/claude-html-unreasonable-effectiveness-ai-output/
  - https://note.com/masa_wunder/n/nf4cf2e257da2
experiment_dir: null
---

# 「The Unreasonable Effectiveness of HTML」— Anthropic Claude Code チームが Markdown より HTML を推す内部既定への転換

## 3行要約

- 2026-05-08、Anthropic Claude Code チームの Thariq Shihipar が "Using Claude Code: The Unreasonable Effectiveness of HTML" を公開。**Claude の出力フォーマットを Markdown から HTML に切り替える** ことで、Anthropic 自身が plans / code reviews / design systems / reports に対する **内部既定を HTML に変えた** ことを明かす。48時間で 750k views / 14k likes / 1.6k 引用ポストの大反響
- HTML が Markdown より優れる具体例: **(1) 真のテーブル（column-span / row-span / 行ハイライト / sticky header）**、**(2) インライン SVG ダイアグラム**（ASCII art ではなく実際のベクター図）、**(3) インライン CSS による意味的強調**（成功/失敗/警告のカラーコード）、**(4) 折り畳み details/summary**、**(5) ハイパーリンクの ahref と画像 inline 埋め込み**。Markdown は「人間が書く」前提のフォーマットで、AI が大量に生成する場合にむしろ表現力で負ける
- 同時に **生成 HTML を 20 件のギャラリーで公開**。Hacker News のトップ、Simon Willison の解説含め、コミュニティで「2026 年下半期の Claude 系プロンプト設計の主要トピック」化している

## 自分への関連度: A

ナレッジベース運用と CLAUDE.md 設計に直接効く文脈で、関心領域 2（実践的な設定・運用ノウハウ）と 1（ワークフロー改善）両方に効く:

- **CLAUDE.md の出力指示**: 現状はデフォルト Markdown を前提に書いているが、特定の skill（catch-up / digest）で「テーブルが必要な場面」「ダイアグラムが必要な場面」だけ HTML 指定する選択肢が出てくる
- **Claude Code から HTML 出力を受け取って markdown ファイルに埋め込む**: Markdown は HTML を `inline` で受けるため、entries/ 配下の Markdown に `<table>`/`<details>`/`<svg>` を埋め込めば、GitHub レンダリングと VSCode プレビューの両方で正しく表示できる
- **catch-up Skill の改善余地**: 関連度 S/A エントリのサマリーテーブル、ソース URL の整理、ダイアグラムが必要な記事の構造化に HTML を試す価値がある
- **設計哲学の理解**: Anthropic が「人間用フォーマット」と「AI 用フォーマット」を分離する姿勢を明示した点が重要。harness-engineering（rkaga, 2026-05-02）や CLAUDE.md best practices と一貫した「AI に何を渡し何を生成させるか」の問いの延長
- **VSCode ネイティブ拡張ユーザの自分**: ターミナル出力ではなく VSCode 内で diff/preview を見るので、HTML 埋め込み Markdown はそのままレンダリングされる。Fullscreen renderer 制限の影響を受けない

## 詳細

### Thariq Shihipar の論点

- Markdown は人間が「短時間で書く」ためのフォーマットで、構造的表現が限定的
- AI は生成コストが安く、毎回フルの HTML を出すコストが人間より低い
- Markdown と HTML を AI が出力するときの実トークン数は、表現力に対して大きく差がない
- HTML は「カスタマイズなしで」table / svg / details / inline style が機能する
- ブラウザレンダリング、印刷、PDF 変換にもそのまま乗る

### 公開された 20 件ギャラリーの内容例

- Code review report（ファイル別の問題ハイライト、行リンク、信頼度色分け）
- Plan document（タスクツリーを `<details>` で展開可能に）
- Architecture diagram（インライン SVG）
- Data table（sticky header + row span + 集計行ハイライト）
- Comparison matrix（横軸：選択肢、縦軸：基準、cell color で評価）
- Migration guide（before/after を 2 カラムで並べる）

### コミュニティの反応

- **Hacker News**: トップ。「テーブルでハマる Markdown の限界を Anthropic 自身が認めた」「VSCode の preview や GitHub での render を考えると現実的」など実用面の評価
- **Simon Willison**: 「Anthropic が内部で何を変えているか」のシグナルとして詳細解説。同氏のブログで Markdown vs HTML 比較スニペット公開
- **批判**: 「コピペで AI 同士のチェーンに使う場合 HTML は冗長」「人間のレビュー時に diff が読みにくい」など

### 自分の Skill / CLAUDE.md への反映候補

1. catch-up Skill のサマリー部分のうち、関連度ハイライトを `<table>` で出力する選択肢
2. ultraplan / try Skill で生成する plan を `<details>` ベースに変える
3. CLAUDE.md の出力指示に「テーブル・ダイアグラムが必要なときは HTML を使うこと」を明示
4. entries/ テンプレートの「詳細」セクションで HTML 要素のサンプルを追記

## 試すなら

1. 元記事と 20 件ギャラリーを通読し、自分が使えそうなパターンを 3〜5 個ピックアップ
2. ai-agent-knowledge の `templates/entry-template.md` に HTML サンプル（`<details>`, 簡単なテーブル）を追記
3. 既存 entry の中で表現力に困っていたもの（比較系・ベンチマーク系）を 1 件 HTML 化して GitHub の preview で確認
4. CLAUDE.md に「数値比較や階層構造が必要な箇所では HTML 要素を Markdown 内に埋め込んでよい」を追記
5. catch-up Skill 出力のサマリーセクションを `<table>` 化する PoC

## ソース

- [Using Claude Code: The unreasonable effectiveness of HTML (Simon Willison's blog, 2026-05-08)](https://simonwillison.net/2026/May/8/unreasonable-effectiveness-of-html/)
- [HTML vs Markdown in Claude Code: Why Anthropic's Thariq Changed the Default (pasqualepillitteri)](https://pasqualepillitteri.it/en/news/2243/html-vs-markdown-claude-code-thariq-anthropic)
- [Hacker News thread for "Using Claude Code: The unreasonable effectiveness of HTML"](https://news.ycombinator.com/item?id=48071940)
- [Anthropic's Claude Team Highlights HTML's Unreasonable Effectiveness (AgentUpdate)](https://www.agentupdate.ai/news/claude-html-unreasonable-effectiveness-ai-output/)
- [Claude Code は Markdown より HTML 出力？！公式が語るので解説します (note.com, masa_wunder)](https://note.com/masa_wunder/n/nf4cf2e257da2)

---

## 感想・考察

### 論点の整理（2026-05-17 やり取り）

最初は「Claude Code の出力を人間が見るときは HTML 推奨？」という理解だったが、より正確には **「最終的にレンダリングされた結果を人間が消費するケース」で HTML が優れる** という整理になる。用途を分けて考える:

**HTML が向くケース**
- レポート、code review、plan document、ダッシュボード的なまとめ
- table の表現力（colspan/rowspan/sticky header/cell の色分け）が Markdown table を大きく超える
- SVG ダイアグラムは Mermaid と違って「プラグイン不要・どこでもレンダリング・座標を自由に指定」できる
- `<details>` で長い plan を折り畳める
- VSCode preview / GitHub render / ブラウザ / PDF にそのまま乗る

**Markdown のままで良いケース**
- AI 同士のチェーン（HTML はトークンが冗長）
- diff レビューを人間がやる場面（HTML は diff が読みにくい、と批判もある）
- 単純な箇条書き・見出し中心の文書

### Mermaid vs インライン SVG

Mermaid の長所は「書きやすい DSL」だが、AI が出力する場合は書きやすさのメリットは消える。**表現自由度の高い SVG を直接吐く方が合理的**で、Mermaid 非対応環境（GitHub の一部 view、PDF 変換、メール）でも崩れない利点もある。Markdown を Mermaid プラグインでレンダリングしたものより HTML の方が見やすい、という直感は妥当。

### 自分の環境への当てはめ

VSCode ネイティブ拡張環境の markdown preview は HTML を素通しでレンダリングするため、HTML 埋め込み Markdown との相性は良い。特に効きそうな適用先:

1. **catch-up Skill のサマリー部分**: 関連度 S/A の一覧を `<table>` 化（箇条書きより一覧性が上がる）
2. **比較系エントリ**: 例「Claude Code v2.114.0 vs v2.114.3」の差分表
3. **アーキテクチャ図が要るエントリ**: インライン SVG で軽い図を埋める

### Next Action（要検証として保留）

- 即実践ではなく **要検証** に分類。catch-up Skill 出力の一部 `<table>` 化を PoC として試す価値あり
- templates/entry-template.md に HTML サンプルを足すかは、PoC の結果次第で判断
