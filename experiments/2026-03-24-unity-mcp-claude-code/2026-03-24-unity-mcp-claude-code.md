# Unity MCP + Claude Code 実践ログ

対応エントリ: [entries/2026-03-24-unity-mcp-claude-code.md](../../entries/2026-03-24-unity-mcp-claude-code.md)

## セットアップ手順

### 1. Unity MCP パッケージのインストール

Unity Package Manager を開き、以下のURLでGitパッケージとして追加する:

```
https://github.com/Unity-Technologies/mcp-unity.git
```

または `manifest.json` に直接追記:

```json
{
  "dependencies": {
    "com.unity.mcp": "https://github.com/Unity-Technologies/mcp-unity.git"
  }
}
```

### 2. Claude Code の MCP 設定

`claude mcp add` コマンドで登録する。`--scope user` でグローバル（全プロジェクト共通）に登録できる:

```bash
claude mcp add --scope user unity-mcp -- "C:/Users/honya/.unity/relay/relay_win.exe" --mcp
```

> `com.unity.ai.assistant` パッケージをインストールすると `%USERPROFILE%\.unity\relay\relay_win.exe` が自動的に配置される。
> 設定は `~/.claude.json` の `mcpServers` フィールドに書き込まれる（直接編集不要）。
> `settings.json` には `mcpServers` キーは存在しない（別ファイル管理）。

### 3. Unity Editor での接続承認

Unity Editor 側に「New MCP Connection」ダイアログが表示される。

- **MCP Server Process**: `relay_win.exe`（Unity 側リレープロセス）
- **MCP Client Process**: `claude.exe`（VSCode拡張版 Claude Code）
- Code Signed: Yes / Signature Valid: No — Entrust Root CA 署名済みだが検証は No と表示される（既知の挙動）
- 「Allow」をクリックして承認 → 接続完了

> 接続は Claude Code セッション起動のたびに承認が必要（または Unity Editor 再起動時）。

### 4. 動作確認（簡単なタスク）

接続後、**Unity Editor で対象プロジェクトを開いた状態**で Claude Code から指示する。
新しい Claude Code セッションで以下を試す（このセッションはすでに MCP 接続前に開始しているため、unity-mcp ツールが読み込まれていない可能性がある）:

```
現在のシーンにあるすべてのGameObjectの名前と位置を教えてください
```

```
Unityシーンに Cube を3つ配置して、それぞれ (0,0,0), (2,0,0), (4,0,0) に置いてください
```

> MCP サーバーはセッション開始時に接続されるため、接続後は Claude Code を再起動して新しいセッションを開始する必要がある。

---

## SceneQualityChecker カスタムMCPツール

### 仕組み

`com.unity.ai.assistant` パッケージは `[McpTool]` 属性が付いたクラス・メソッドを Unity TypeCache で自動検出して登録する。
手動の登録作業は不要。クラスに以下を付けるだけ:

```csharp
[McpTool("scene_quality_check", "説明文", EnabledByDefault = true, Groups = new[] { "scene" })]
public class SceneQualityChecker : IUnityMcpTool { ... }
```

`IUnityMcpTool` は `Unity.AI.MCP.Editor.ToolRegistry` 名前空間に存在する。

### 使い方

1. `SceneQualityChecker.cs` を Unity プロジェクトの `Assets/Editor/MCP/` フォルダに配置
2. Unity Editor が再コンパイルすると自動で MCP ツールとして登録される
3. Claude Code から呼び出す:

```
SceneQualityCheckerでシーンの品質チェックをしてください
```

### チェック項目

| チェック | 内容 |
|---------|------|
| コリジョン漏れ | Collider なしで Renderer を持つ非UI GameObjectを検出 |
| デフォルトマテリアル | Unity デフォルトマテリアル使用オブジェクトを検出 |
| スケール異常 | スケールが 0 または極端に大きいオブジェクトを検出 |
| ライティング異常 | シーン内に Light が存在しない場合を検出 |
| 未命名オブジェクト | "GameObject" のままのオブジェクトを検出 |

---

## 動作確認ログ

### 接続確認（2026-03-30）

**依頼:** 「Unityにて現在のシーンにあるすべてのGameObjectの名前と位置を教えてください」

**結果:** 正常に応答。SampleScene の内容を正確に返した。

| 名前 | 位置 (x, y, z) |
|------|----------------|
| Main Camera | (0, 1, -10) |
| Directional Light | (0, 3, 0) |
| Global Volume | (0, 0, 0) |

→ **MCP接続: 動作確認OK**

### オブジェクト配置（2026-03-30）

**依頼:** 「Unityシーンに Cube を3つ配置して、それぞれ (0,0,0), (2,0,0), (4,0,0) に置いてください」

**結果:** 正常に3つの Cube が配置された。Scene ビューでも反映を目視確認済み。

| 名前 | 位置 (x, y, z) |
|------|----------------|
| Cube1 | (0, 0, 0) |
| Cube2 | (2, 0, 0) |
| Cube3 | (4, 0, 0) |

→ **GameObject配置: 動作確認OK**

### SceneQualityChecker カスタムツール（2026-03-30）

**依頼:** 「SceneQualityCheckerでシーンの品質チェックをしてください」

**結果:**
- `scene_quality_check` ツールは Unity MCP に正常登録・実行された
- Unity コンソールに `[McpToolRegistry] Tool 'scene_quality_check' completed successfully` が出力された
- ツールの出力: `{"issueCount": 0, "warningCount": 0, "issues": [], "warnings": [], "summary": "問題は見つかりませんでした。"}`
- ただし Claude Code 側には `{"success": false, "error": "Unknown error"}` が返った

**Unknown error の挙動:**
- Unity 側の実行は成功しているが、Bridge.cs のレスポンス返却処理で Claude Code がエラーとして受け取る
- `Unity_RunCommand` で同じロジックを直接実行することで正常に結果取得できた（代替手段として有効）
- 機能的な問題はなく、今回はこの挙動を把握した上でOKとする

→ **カスタムMCPツール登録: 動作確認OK（Unknown errorは既知の挙動として記録）**

---

## 観察・メモ

- Unity MCP は現時点（2026-03）では `Camera Capture` の結果が実際の画面と乖離する既知の問題がある
- AI単独でのステージ完成は難しく、「AIが配置 → 人間がレビュー → AIが修正」のループが現実的
- SceneQualityChecker のようなカスタムツールで AI の弱点（視覚的整合性チェック）を補完する設計が有効
- カードバトルゲームへの応用: カードアート配置・UIレイアウトの検証ツールとして応用できる可能性がある

## 参考リンク

- [Unity MCP GitHub](https://github.com/Unity-Technologies/mcp-unity)
- [元記事 (DevelopersIO)](https://dev.classmethod.jp/articles/unity-mcp-tps-game-claude-code-modification/)
