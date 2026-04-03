---
date: 2026-03-26
status: read
relevance: A
tags: [mcp, mcp-apps, ui, anthropic, openai, sep-1865]
source_urls:
  - https://inkeep.com/blog/anthropic-openai-mcp-apps-extension
  - https://www.latent.space/p/ainews-anthropic-launches-the-mcp
experiment_dir: null
---

# MCP Apps Extension（SEP-1865）— MCPサーバーにインタラクティブUI機能を追加

## 3行要約

- MCP Apps Extension（SEP-1865）により、MCPサーバーがHTMLベースのインタラクティブUIをサンドボックスiframe内で提供可能に。`ui://`URIスキームでUIリソースを事前宣言する設計
- AnthropicとOpenAIが共同で策定。Postman、Shopify、Hugging Face、ElevenLabs等も参加。既存MCP実装との後方互換性を維持
- MCPを「エージェンティックアプリランタイム」に進化させる構想。AIモデル・ユーザー・アプリケーション間の新たなインタラクション基盤

## 自分への関連度: A

ゲーム開発ツールのMCPサーバー（Blender MCP等）にUI機能が追加される可能性。将来的にUnity MCPサーバーでレベルエディタUIやアセット管理UIを表示できるようになるかもしれない。また、自作MCPサーバーにUI機能を追加する際のリファレンスとして有用。

## 詳細

### 技術的アーキテクチャ
- **UIリソース**: `ui://`URIスキームをツールメタデータで参照。予測可能で監査しやすい設計
- **HTMLレンダリング**: `text/html`コンテンツをサンドボックスiframeで表示。ユニバーサル互換性を優先
- **通信**: 既存のMCP JSON-RPCプロトコル上でpostMessageによる双方向通信
- **セキュリティ**: iframeサンドボックス、事前宣言されたテンプレート、監査可能なメッセージ、ユーザー同意メカニズム

### 参加組織
- **共同策定**: Anthropic, OpenAI
- **コミュニティ**: MCP-UI creators、MCP UI Community Working Group
- **企業**: Postman, Shopify, Hugging Face, ElevenLabs, Goose

## 試すなら

1. SEP-1865の仕様書を確認（MCP公式リポジトリ）
2. 対応クライアントの実装状況をチェック（Claude Code/Claude.aiでのサポート状況）
3. 簡単なHTMLを返すMCPサーバーを作成してUI表示をテスト
4. Blender MCP等の既存サーバーでUI対応の動きがあるか確認

## ソース

- [Anthropic and OpenAI Join Forces: MCP Apps Extension（Inkeep）](https://inkeep.com/blog/anthropic-openai-mcp-apps-extension)
- [AINews: Anthropic launches the MCP Apps open spec（Latent Space）](https://www.latent.space/p/ainews-anthropic-launches-the-mcp)

---

## 感想・考察

`ui://` はブラウザからアクセスするURLではなく、MCPプロトコル内部のリソース識別子。UIはMCPクライアント（Claude.ai / Claude Code）のチャット画面内にサンドボックスiframeとして描画される。Slackのモーダルに近いイメージ。

「UIをクライアントに完全に任せる」ではなく「UIの実行環境（ウィンドウ・サンドボックス）をクライアントに任せる」が正確で、HTMLの記述自体は開発者が行う。ただしWPF/WinForms/Electronのようなネイティブフレームワークが不要になり、配布も「MCPサーバーへ接続」に変わる。

**重要な気づき**: MCPという標準インターフェースが決まることで、AIエージェントへの指示の「ゴール」が明確になる。「Windowsアプリを作って」は曖昧だが、「MCPサーバーとして作って」と言えば出力物の形が一意に決まる。機能・UI（HTML/JS）・接続部分（定型コード）をそれぞれ明確に指示できるため、ツール作成の大部分をAIエージェントに委任しやすくなる。ゲーム開発でも「UnityシーンビューアーをMCPサーバーで」という指示が現実的になりそう。
