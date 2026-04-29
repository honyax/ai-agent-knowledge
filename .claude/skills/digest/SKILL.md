---
name: digest
allowed-tools: Read, Bash, Grep, Glob
description: ナレッジベースの状態を一覧表示する（未読数、実践待ち、週次サマリー）。Claude Code ビルトインの /status とは別物
effort: low
---

# digest: ナレッジベース状態確認

## やること

1. `entries/` 配下の各ステータスフォルダ内のMarkdownファイルを読む
   - `entries/01_unread/` → 未読
   - `entries/02_read/` → 読了（完了、これ以上やることなし）
   - `entries/03_todo/` → 実践待ち（read後、実践したいと判断したもの）
   - `entries/04_tried/` → 実践済み
   - `entries/05_archived/` → アーカイブ
2. 以下のサマリーを表示:

```
ナレッジベース状態
━━━━━━━━━━━━━━━━━━━
未読:      X件（うち S/Aランク: Y件）
読了:      X件
実践待ち:   X件（うち S/Aランク: Y件）
実践済み:   X件
アーカイブ: X件

要対応（S/Aランクで未読・実践待ち）:
  - [日付] タイトル（ランクS）[未読]
  - [日付] タイトル（ランクA）[実践待ち]

最終更新: YYYY-MM-DD
```

3. 未読または実践待ちのS/Aランクがある場合、「`/try` で試してみますか？」を提案
