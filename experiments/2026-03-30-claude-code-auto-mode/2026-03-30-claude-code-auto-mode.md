# Claude Code Auto Mode 実践ログ

実施日: 2026-03-31
Claude Code バージョン: v2.1.87

## 検証方法

`--dangerously-skip-permissions` なしで Auto Mode の機能を確認。
実際にタスクを流すのではなく、コマンドで設定内容・ルール構造を把握する方針。

## 検証結果

### フラグ確認

エントリには `claude --auto` と記載があったが、実際の正しい呼び出し方は:

```bash
claude --permission-mode auto
```

`--help` の `--permission-mode` オプションに `auto` が含まれている:

```
--permission-mode <mode>  Permission mode to use for the session
  (choices: "acceptEdits", "bypassPermissions", "default", "dontAsk", "plan", "auto")
```

### サブコマンド

`claude auto-mode` サブコマンドが存在する:

```bash
claude auto-mode config     # 現在の有効な設定をJSONで表示
claude auto-mode defaults   # デフォルトのALLOW/DENY/環境ルールを表示
claude auto-mode critique   # カスタムルールへのAIフィードバック
```

### ルール構造

`claude auto-mode defaults` で確認。3つのカテゴリがある:

#### ALLOW（自動承認）: 7ルール

| ルール名 | 概要 |
|---|---|
| Test Artifacts | ハードコードされたテストAPIキー・プレースホルダー認証情報 |
| Local Operations | ワーキングディレクトリ内のローカルファイル操作 |
| Read-Only Operations | GETリクエスト・読み取り専用APIコール |
| Declared Dependencies | manifest宣言済みパッケージのインストール（`npm install` 等） |
| Toolchain Bootstrap | 公式インストーラー経由のツールチェーン導入 |
| Standard Credentials | 自分のconfigからの認証情報読み取りと対応プロバイダーへの送信 |
| Git Push to Working Branch | セッション開始ブランチまたはエージェント作成ブランチへのpush |

#### SOFT_DENY（ブロック対象）: 25ルール

注目すべきルール:

| ルール名 | 概要 |
|---|---|
| Git Destructive | force push、リモートブランチ削除、履歴書き換え |
| Git Push to Default Branch | main/master への直接push |
| Code from External | `curl \| bash` や外部コードの実行 |
| Production Deploy | 本番環境へのデプロイ・DBマイグレーション |
| Irreversible Local Destruction | セッション開始前のファイルを `rm -rf` 等で削除 |
| Self-Modification | エージェント自身の設定ファイル（settings.json, CLAUDE.md）の変更 |
| Data Exfiltration | 機密データの外部エンドポイント送信 |
| Real-World Transactions | 購入・支払い・メール送信等の実世界トランザクション |

#### Environment: デフォルト設定

- Trusted repo: セッション開始時の git リポジトリのみ
- 信頼済み内部ドメイン・クラウドバケット・サービス: なし（カスタム設定可）

### Teamプラン要件について

エントリでは「Team プラン以上が必要」と記載されていたが、v2.1.87 では `--permission-mode auto` が `--help` に表示される（Proプランでも利用可能な可能性あり）。実際に動かして確認するには別途試す必要あり。

## 気づき

1. **soft_deny は「ブロック」ではなく「代替案を促す」**: ルール名が `soft_deny` であることから、ハードブロックではなく「別のアプローチをとるよう誘導する」設計と推測される
2. **Self-Modification ルールが興味深い**: `settings.json` や `CLAUDE.md` の変更がブロック対象 — エージェントが自分のルールを書き換えて制約を回避することを防いでいる
3. **Environment 設定でカスタマイズ可能**: 信頼済みドメインやクラウドバケットを設定に追加できる → チームのインフラに合わせてルールを緩められる
