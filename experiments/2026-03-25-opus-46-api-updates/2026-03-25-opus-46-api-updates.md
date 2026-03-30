# Claude Opus 4.6 & API主要アップデート — 実験ノート

> ローカル実行なし。リリースノート精読・考察のみ（2026-03-30、Claude Code Web）

## 主要変更点の整理

### Opus 4.6 スペック
- 128k出力トークン、1Mコンテキスト（GA、長文コンテキストプレミアム廃止）
- Adaptive Thinking: `thinking: {type: "adaptive"}` でモデルが思考深度を動的判断
- effortパラメータがGA（betaヘッダー不要）
- Fast Mode: `speed: "fast"` で最大2.5倍速（$30/$150 per MTok）

### Context Compaction
- サーバーサイド自動要約で実質無限の会話が可能
- 長時間セッションで特に有効

### モデル廃止（対応必須）
| 廃止モデル | 移行先 |
|-----------|--------|
| `claude-3-7-sonnet-20250219` | `claude-sonnet-4-6` |
| `claude-3-5-haiku-20241022` | `claude-haiku-4-5-20251001` |

## ゲーム開発への応用メモ

- **Adaptive Thinking**: ゲームシナリオ生成の品質/コストを動的調整。単純な応答はeffort低め、複雑な戦略AIには高め
- **Compaction**: 長時間RPGセッションのNPC対話やゲームマスターAIに活用できる
- **Fast Mode**: $30/MTokはゲーム内リアルタイム応答には高すぎる。バッチ処理や開発用途に限定

## 移行チェックリスト（ローカル確認用）

- [ ] `claude-3-7-sonnet-20250219` を使っているコードを検索 → `claude-sonnet-4-6` に更新
- [ ] `claude-3-5-haiku-20241022` を使っているコードを検索 → `claude-haiku-4-5-20251001` に更新
- [ ] betaヘッダーで effort を指定している箇所を削除
