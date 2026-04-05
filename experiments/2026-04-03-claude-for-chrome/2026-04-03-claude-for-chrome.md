# Claude for Chrome 実践ログ

実施日: 2026-04-05  
対応エントリ: [entries/04_tried/2026-04-03-claude-for-chrome.md](../../entries/04_tried/2026-04-03-claude-for-chrome.md)

---

## 実施内容

Claude Code セッション中に `--chrome` フラグで起動し、Claude for Chrome の各機能を実際に試した。

### 1. 接続確認 + 基本動作デモ

`tabs_context_mcp` でブラウザ状態を取得し、`navigate` で example.com に移動、`get_page_text` でページ内容を読み取ることに成功。

```
tabs_context_mcp → navigate(example.com) → get_page_text
→ "Example Domain" のテキストを取得
```

**確認できた仕組み:**
```
Claude Code (CLI)
    ↕ MCP プロトコル
Chrome拡張機能
    ↕ Chrome DevTools API
Chromeブラウザ
```

### 2. GitHub PR 一覧取得（public リポジトリ）

`honyax/ai-agent-knowledge` の Pull Request ページに移動し、ページ内 JSON データから PR 情報を抽出。  
全10件（#1〜#10、全て MERGED）を一覧化した。

**反省点:** public リポジトリは GitHub API や `gh` コマンドでも取得可能で、Claude for Chrome の強みが活かせないユースケースだった。

### 3. Unity Asset Store 購入済みアセット取得（要ログイン）

これが今回の本命。ログイン必須の Asset Store から全87件のアセット情報を取得した。

#### セットアップの流れ

1. `assetstore.unity.com` にアクセス → 未ログイン状態を確認
2. ユーザーが手動でログイン
3. `/account/assets` に移動 → ログイン済み状態で購入済みリストを表示

#### データ取得の試行錯誤

**試み1: `get_page_text`（ページ1のみ有効）**  
ページ1（25件）は正常取得できた。ただし SPA のため、ページ遷移後の `get_page_text` は前ページのコンテンツを含む場合があり不安定。

**試み2: 内部 API 呼び出し**  
`/api/en-US/account/assets?offset=0&limit=100` → HTML が返却されて失敗。

**試み3: GraphQL エンドポイント発見**  
`read_network_requests` で `/api/graphql/batch` への POST リクエストを確認。ただし直接呼び出しは未試行。

**試み4: JavaScript DOM 抽出（成功）**  
アセット名とファイルサイズが隣接するパターンを正規表現で抽出。

```javascript
const text = document.querySelector('main').innerText;
const matches = [...text.matchAll(/([^\n]+)\n(\d[\d,]*\.\d+ [KMGB]+)\n購入時刻： ([\d年月日 ]+)/g)];
matches.map(m => ({ name: m[1].trim(), date: m[3].trim() }))
```

#### ページネーション操作

「次へ」ボタンを JavaScript でクリックしてページを切り替えた:

```javascript
const buttons = document.querySelectorAll('nav[aria-label="ページネーション"] button');
const nextBtn = Array.from(buttons).find(b => b.textContent.trim() === '次へ');
nextBtn.click();
```

4ページ（各25件 + 最終12件）を順に取得し、全87件を収集した。

#### 取得結果サマリー

- 総件数: 87件
- 最古の購入: 2011年7月27日（Tower）
- 最新の購入: 2026年1月31日（PlayersPrefs Editor）
- 取扱終了アセット: 6件

---

## 使用ツール

| ツール | 用途 |
|--------|------|
| `tabs_context_mcp` | タブ一覧・接続確認 |
| `navigate` | URL移動 |
| `get_page_text` | ページテキスト取得 |
| `read_page` | アクセシビリティツリー確認 |
| `find` | 自然言語で要素検索 |
| `javascript_tool` | DOM操作・データ抽出・ボタンクリック |
| `read_network_requests` | API エンドポイント調査 |

---

## 気づき・注意点

- **SPA（React）サイトでは `get_page_text` が不安定**: 仮想DOMの都合でページ遷移後も前ページのコンテンツが混在することがある
- **`javascript_tool` が最も強力**: DOM へのフルアクセスが可能で、データ抽出・ボタン操作・API 呼び出しまで対応できる
- **ネットワーク監視は事前起動が必要**: `read_network_requests` はツールを最初に呼んだ後のリクエストしか記録されない
- **querySelector 系は効きにくいことがある**: React の動的クラス名に依存しない `innerText` ベースの抽出が安定していた
- **ログイン状態を引き継げる**: これが最大の強み。API トークン不要でユーザーがログイン済みのサイトにアクセスできる
