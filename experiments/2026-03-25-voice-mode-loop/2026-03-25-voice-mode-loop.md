# Claude Code Voice Mode & /loop 実践ログ

実施日: 2026-03-29
環境: Claude Code v2.1.87 / Windows 11

## バージョン確認

```
$ claude --version
2.1.87 (Claude Code)
```

v2.1.76 以降なので両機能対応済み。

## /loop の仕組み確認

### 内部実装

`/loop` はグローバルスキルとして提供されており、内部で以下のツールを使う:
- `CronCreate` — ループジョブを登録
- `CronList`  — 登録済みジョブを一覧表示
- `CronDelete` — ジョブを削除

これらは「deferred tools」として遅延ロードされており、MCP Tool Search と組み合わさっている。

### CronList 実行結果

```
No scheduled jobs.
```

現在アクティブなループなし。

### /loop の使い方

```
/loop 10m "git status"         # 10分おきに git status
/loop 6h /catch-up             # 6時間おきに catch-up スキルを実行
/loop 1d /status               # 1日おきにステータス確認
```

このナレッジベースで最も使えそうなパターン:
```
/loop 6h /catch-up
```
GitHub Actions の代替として、Claude Code セッション中に定期的に最新情報を収集できる。
ただし「セッション中のみ有効」であり、PC をシャットダウンすると止まる。
常時稼働させたい場合は `schedule` スキル（RemoteTrigger経由）の方が適切。

## /voice の確認

テキストベースのセッションでは試せないが、以下は把握済み:
- Push-to-talk方式（スペースバー長押し → 離して送信）
- 常時リスニングではないので誤認識リスクが低い
- 20言語対応（日本語含む）
- ゲーム開発中に手がふさがっている時（3Dモデル操作中など）に有用な可能性

## /voice 実機検証（2026-03-29）

Windows 11 のターミナルで実際に試した。

### 手順と結果

1. `claude` でCLIセッション起動（VS Code統合ターミナル）
2. `/voice` → `Voice mode enabled. Hold Space to record.` 表示
3. スペースバー長押し → `listening` 表示は出るがマイクに話しかけても無反応

### 原因の特定

Windowsのマイク権限リストに Claude Code が載っていなかった（Node.js として記録されていた）。
セッション起動時に以下のメッセージが出ており、npm版からネイティブバイナリへの移行が行われていた:

```
Claude Code has switched from npm to native installer. Run `claude install`
```

### `claude install` 実行後

- Windows のマイクアクセス一覧に「**Claude Code**」が 2026/03/29 15:15:23 として追加された
- 権限問題は解消
- しかし音声を話しかけても入力欄への文字起こしは依然発生しない

### 結論

- 権限の問題ではなく、音声認識バックエンドが未割り当て（段階的ロールアウト中）かバグの可能性が高い
- 現時点では `/voice` は実用不可

### Win+H との比較

Windows標準の音声入力（`Win+H`）はカーソル位置に文字を入力できるため、
Claude Code のターミナル入力欄でも問題なく使える。

| 比較項目 | /voice | Win+H |
|---|---|---|
| 動作確認 | ❌ 現状不動 | ✅ 動作する |
| 対象 | Claude Code 専用 | OS全体どこでも |
| 設定 | 不要 | 不要 |
| 将来の優位性 | コード文脈対応の認識モデルの可能性 | なし |

**現実的な結論**: 今は `Win+H` で代替する。`/voice` はロールアウト完了後に再確認。

## まとめ

| 機能 | 状態 |
|------|------|
| /loop 仕組み確認 | ✅ CronCreate/List/Delete ツールで実装 |
| CronList 動作確認 | ✅ 実行済み（アクティブジョブなし） |
| /loop 6h /catch-up 構想 | ✅ 実行可能なことを確認 |
| /voice（実機検証） | ❌ 権限は解消済みだが音声認識バックエンドが未動作 |
| Win+H（代替手段） | ✅ Claude Code 入力欄で利用可能 |
