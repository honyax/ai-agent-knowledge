# Claude Code on the Web — 実践ガイド

作成日: 2026-03-29
ステータス: 手順ガイド（未実行）

---

## 前提条件

- claude.ai の Pro/Max/Team/Enterprise プランへの加入
- GitHub アカウント（GitLab 等は不可）
- ターミナルに Claude Code CLI がインストール済み（`--remote` フラグ使用時）

---

## Step 1: セットアップ

### 1-1. Claude GitHub App のインストール

1. [claude.ai/code](https://claude.ai/code) にアクセス
2. "Connect GitHub" からアカウント連携
3. 対象リポジトリに **Claude GitHub App** をインストール
   - リポジトリ単位でインストール可能（全リポジトリへの許可は不要）
   - Unity プロジェクトリポジトリを対象にする場合はここで選択

### 1-2. 初回テスト（ウェブ UI から）

簡単なタスクで動作確認:

```
タスク例: "README.md の最初のセクションに、プロジェクトの概要を3行で追記してください"
```

> **実施済み（2026-03-29）**: Claude Code Web から本ドキュメントの「初回テスト」セクションへの文面追記タスクを実行。VM のプロビジョニング・ブランチ作成・編集・コミット・プッシュまで一連の流れを確認。この一文自体が Claude Code Web によって記述されました。

確認ポイント:
- [ ] VM のプロビジョニング時間（初回は数十秒かかる場合あり）
- [ ] ブランチが自動作成されているか
- [ ] diff ビューで変更内容を確認できるか
- [ ] PR 作成フローが正常に動くか

---

## Step 2: ターミナルから `--remote` で並列実行

```bash
# 基本構文
claude --remote "タスクの説明"

# 例: ゲーム開発中に別タスクを並列実行
claude --remote "Fix all compiler warnings in Assets/Scripts/"
claude --remote "Update CHANGELOG.md with recent commits"
claude --remote "Add XML documentation to public methods in GameManager.cs"
```

確認ポイント:
- [ ] 複数コマンドが独立したセッションとして並列起動するか
- [ ] claude.ai/code のダッシュボードでセッション一覧が見えるか
- [ ] レート制限の消費量（他の Claude 使用と共有）

---

## Step 3: `/teleport` でローカルに引き継ぎ

ウェブセッションの作業をローカルに持ってくる:

```
ウェブ UI 上で: /teleport
または: /tp
```

引き継ぎ後の動作確認:
- [ ] ブランチが自動チェックアウトされるか
- [ ] 会話履歴が復元されるか
- [ ] 追加の修正をローカルで続けられるか

---

## ゲーム開発ワークフローへの応用案

### パターン A: バックグラウンドタスク実行

Unity でメイン開発中に、別タスクを並列で走らせる:

```bash
# メイン開発中に実行
claude --remote "Run all unit tests and report failures"
claude --remote "Generate API documentation from code comments"
```

### パターン B: Plan Mode + Remote 実行

```
1. ローカルで /plan モードで設計・承認
2. claude --remote で実装をリモートに委譲
3. 実装完了後、/teleport でレビュー・マージ
```

### パターン C: モバイルからタスク投入

Unity 開発中に別端末（スマホ）から:
- バグ修正タスクを投入
- 完了後にローカルへ /teleport で引き継ぎ

---

## 注意事項

- **レート制限**: 通常の Claude 使用と共有。大量の `--remote` 同時起動は注意
- **GitHub 限定**: GitLab や Bitbucket は現時点で非対応
- **Unity プロジェクト**: Unity 固有のビルド確認は VM 上では難しい（コンパイルエラー検出程度）
- **セットアップスクリプト**: `.claude/setup.sh` を用意すると VM 起動時に依存関係を自動インストールできる

---

## 実行ログ

<!-- 実際に試した後にここに記録 -->
