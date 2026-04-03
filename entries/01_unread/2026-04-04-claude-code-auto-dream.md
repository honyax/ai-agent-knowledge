---
date: 2026-04-04
status: unread
relevance: A
tags: [claude-code, memory, auto-dream, auto-memory, workflow]
source_urls:
  - https://claudefa.st/blog/guide/mechanics/auto-dream
  - https://www.geeky-gadgets.com/claude-autodream-memory-files/
  - https://medium.com/@joe.njenga/how-im-using-new-claude-code-dream-auto-dream-to-never-lose-memory-again-ba0575f2881a
experiment_dir: null
---

# Claude Code Auto Dream — メモリ自動統合・劣化防止機能

## 3行要約

- Auto Memoryの「時間経過による劣化問題」を解決する **Auto Dream** 機能がClaude Codeに追加された（現在はサーバーサイドfeature flag制御のプレビュー）。
- セッション間の「オフ時間」にClaude Codeが自身のメモリファイルを自動レビューし、古い情報の削除・矛盾の解消・関連情報のマージを行う。
- MEMORY.mdを常に200行以内・最新状態に保ち、次セッション開始時の記憶品質を維持する。

## 自分への関連度: A

自分もClaude Codeのauto memoryを使い始めているが、セッションが増えるにつれて記憶の矛盾や陳腐化が心配だった。自動メンテナンスが入れば管理コスト大幅減。

## 詳細

### Auto Dreamが解決する問題

Auto Memory（v2.1.59〜デフォルト有効）でClaudeはプロジェクトメモをMEMORY.mdに蓄積するが、20セッション以上経つと以下の問題が出る:
- 矛盾するエントリが混在（「ExpressからFastifyに移行」後も旧情報が残る）
- 相対日付（「昨日」など）が意味をなさなくなる
- 削除済みファイルへの参照が残る
- 重複・類似エントリで「ノイズ」になる

### Auto Dreamの仕組み

セッション終了後（オフ期間）にバックグラウンドで実行:
1. **Pruning**: 古いエントリ・削除済みファイル参照・矛盾情報を削除
2. **Merging**: 関連情報を統合し重複を排除
3. **Refreshing**: 相対日付を絶対日付に変換、現在のプロジェクト状態に合わせて更新

MEMORY.mdの200行制限（起動時に読み込まれる上限）を守るよう最適化される。

### 現在の状況

- `/memory` メニューから確認可能だが、サーバーサイドfeature flagで制御されており完全ローールアウト未実施（2026-03〜04時点）
- 手動の `/dream` コマンドは有効
- コミュニティ製の [dream-skill](https://github.com/grandamenium/dream-skill) でも類似機能を再現可能

## 試すなら

1. `/memory` メニューでAuto Dream設定を確認
2. 手動で `/dream` を実行してメモリ整理の動作を確認
3. 整理前後のMEMORY.mdの変化を観察
4. feature flagが有効になっていない場合は dream-skill で代替
5. 長期運用後のMEMORY.md品質を評価

## ソース

- [Claude Code Dreams: Anthropic's New Memory Feature - claudefa.st](https://claudefa.st/blog/guide/mechanics/auto-dream)
- [AutoDream: Claude Code's New Trick for Memory Management - Geeky Gadgets](https://www.geeky-gadgets.com/claude-autodream-memory-files/)
- [How I'm Using Claude Code /dream & Auto Dream - Medium](https://medium.com/@joe.njenga/how-im-using-new-claude-code-dream-auto-dream-to-never-lose-memory-again-ba0575f2881a)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->
