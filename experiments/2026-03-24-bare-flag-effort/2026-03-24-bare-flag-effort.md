# 実験結果: --bare フラグ & effort フロントマター

## 実施日: 2026-03-29（再検証: 2026-03-29）
## Claude Code バージョン: v2.1.86（初回: v2.1.80）

---

## 1. `--bare` フラグ

### 実行コマンド
```bash
claude --bare -p "1+1は？短く答えて"
```

### 結果: 実装済み（v2.1.86で確認）

**v2.1.80時点（初回）:**
```
error: unknown option '--bare'
(Did you mean --name?)
```

**v2.1.86時点（再検証）:**
```
Not logged in · Please run /login
```

`--bare` オプション自体は認識されている。ただしエラーが発生。

### 原因と重要な仕様

`--bare` は **keychain（OAuthトークン）を意図的にスキップ** する設計。
`claude --help` での説明:
```
Minimal mode: skip hooks, LSP, plugin sync, attribution, auto-memory, background prefetches,
keychain reads, and CLAUDE.md auto-discovery. Sets CLAUDE_CODE_SIMPLE=1.
Anthropic auth is strictly ANTHROPIC_API_KEY or apiKeyHelper via --settings
(OAuth and keychain are never read).
```

### 使用するには

通常の OAuth ログインではなく、**環境変数 `ANTHROPIC_API_KEY` が必須**:
```bash
ANTHROPIC_API_KEY=sk-ant-... claude --bare -p "1+1は？"
```

GitHub Actions 等のスクリプト実行では `ANTHROPIC_API_KEY` を secrets に設定することで利用可能。

---

## 2. `effort` フロントマター

### 実行内容
`.claude/skills/status/SKILL.md` のフロントマターに `effort: low` を追加

### 結果: サポート済み（v2.1.84以降）

公式ドキュメント（[Skills](https://code.claude.com/docs/en/skills.md) / [Changelog](https://code.claude.com/docs/en/changelog.md)）で確認:
- v2.1.84 で `effort` フロントマターが正式追加
- スキル（SKILL.md）・スラッシュコマンド（commands/*.md）の両方で使用可能

**有効な値:**
| 値 | 説明 |
|----|------|
| `low` | 軽量・高速（状態確認など単純タスク向け） |
| `medium` | 標準 |
| `high` | 高精度（コードレビューなど重いタスク向け） |
| `max` | 最大（Opus 4.6 のみ） |

### IDE 警告について

VS Code 拡張機能で以下の警告が出るが **誤検知（false positive）**:
```
Attribute 'effort' is not supported in skill files.
```
拡張機能のスキーマ定義が v2.1.84 の変更に追従できていないため。CLI の実際の動作には影響しない。

### 使用例（status スキルに適用済み）
```yaml
---
allowed-tools: Read, Bash, Grep, Glob
description: ナレッジベースの状態を一覧表示する
effort: low
---
```

---

## 3. `ANTHROPIC_CUSTOM_MODEL_OPTION`（未実施）

API キーが必要なため、`--bare` と合わせて API キー設定後に検証予定。

---

## 結論

| 機能 | v2.1.80 | v2.1.86 | 備考 |
|------|---------|---------|------|
| `--bare` フラグ | 未実装 | 実装済み | `ANTHROPIC_API_KEY` が必要 |
| `effort` フロントマター | 未実装 | 実装済み（v2.1.84〜） | IDE 警告は誤検知 |

### 実用上のポイント

- `--bare` は GitHub Actions や CI/CD での `-p` 呼び出しに最適。`ANTHROPIC_API_KEY` を secrets に設定するだけで使える
- `effort: low` をシンプルなスキル（/status 等）に付与しておくと、応答速度が向上する見込み
- IDE の警告は無視してよい（拡張機能のスキーマ更新待ち）
