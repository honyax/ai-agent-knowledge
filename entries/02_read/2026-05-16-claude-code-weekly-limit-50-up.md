---
date: 2026-05-16
status: read
relevance: A
tags: [claude-code, rate-limit, pro-plan, max-plan, weekly-limit, anti-codex, promotion, colossus]
source_urls:
  - https://pasqualepillitteri.it/en/news/2494/claude-code-weekly-limits-50-percent-anti-codex-anthropic-2026
  - https://apidog.com/blog/claude-code-weekly-limits-50-percent-increase-july-2026/
  - https://pasqualepillitteri.it/en/news/2614/claude-resets-rate-limits-5-hour-weekly-may-15-2026
  - https://note.com/tothinks/n/ne489f28d6b01
experiment_dir: null
---

# Claude Code の週次利用制限が +50% に — 2026-07-13 まで限定、5/15 にカウンタ全リセットも

## 3行要約

- 2026-05-13 (SF 14:19)、ClaudeDevs が **Claude Code の週次（weekly）利用制限を全有料プラン (Pro/Max/Team/seat-based Enterprise) で +50% 引き上げ** ると発表。**期限は 2026-07-13 18:00 PDT まで**の約2ヶ月限定。Free プランは対象外。ユーザ側の操作不要で自動適用
- これは 2026-05-06 の **5時間レート上限 2倍化 + ピーク時間ペナルティ撤廃**（SpaceX Colossus 契約と同時発表）に続く施策で、5週間で 3回目の容量増強。背景には xAI/Anthropic Colossus 1 確保による GPU 容量増があり、対 Codex / 対 Cursor の競争圧力という分析（pasqualepillitteri）
- 2026-05-15 には **Anthropic が全ユーザの 5h/週次カウンタを一斉リセット** することも発表。レート制限に当たって縮こまっていたユーザにフレッシュなキャパを再配布する形

## 自分への関連度: A

Pro プラン契約の自分にとって、向こう約2ヶ月の **実効使用上限が +50% になる** 直接的なメリット:

- **catch-up / try / experiment Skill のヘビーセッション**: 週次上限を意識して節制していた catch-up の検索回数や try の検証ループを増やせる
- **Opus 4.7 を Fast mode 既定で多用できる**: v2.1.143 で Fast mode 既定が Opus 4.7 に切り替わったタイミングと重なり、Opus 利用が増えても余裕がある
- **「Programmatic Credits 分離（6/15、別エントリ）」とのコントラスト**: Anthropic は interactive 側を増量して programmatic 分離への不満を緩和する戦略。Pro ユーザの自分にとっては、対話モードでの Claude Code 利用は **当面より使える** が、`claude -p` などヘッドレス利用は 6/15 以降は別枠
- **5/15 のレート制限リセット**: もしすでに今週分を使い切っていた場合、リセット直後に重い処理（catch-up 全件、ultraplan、ultrareview など）を回せる
- 関心領域 1（Claude Code のワークフロー改善）と 2（実践的な運用ノウハウ）に直接効く

## 詳細

### 増量の内容

- **対象**: Pro / Max / Team / seat-based Enterprise すべての有料プラン
- **適用**: 即時、自動。ユーザ側で操作不要
- **期限**: 2026-07-13 18:00 PDT（JST 7/14 10:00）
- **対象上限**: 週次 (weekly) リミットのみ。5時間リミット (5h) は別途 5/6 に 2倍化済みで、両方が 7/13 まで同時に有効

### 5/15 のレート制限リセット

- Anthropic 公式が全ユーザの **5時間ウィンドウと週次ウィンドウのカウンタを一斉に 0 へリセット**
- これは恒久的な制度変更ではなく、Colossus 1 のキャパ稼働開始のタイミングで「フレッシュなキャパを配布」する形のキャンペーン
- ユーザは リセット直後の数時間〜数日、通常より重い処理を回すチャンス

### 背景と業界文脈

- **対 Codex / 対 Cursor の競争**: pasqualepillitteri の分析では、これは Anthropic の「anti-Codex move」と位置付けられている
- **SpaceX Colossus 1 契約**: 2026-05-06 発表で 220K GPU 規模の compute を確保（既存エントリ 2026-05-10）。今回の +50% はその直接の還元
- **5週間で3回目の介入**: ピーク時間撤廃（4月）→ 5h 2倍 + 高い限度（5/6）→ 週次 +50%（5/13）。Anthropic がレート制限に対する継続的なコミュニケーションを意識的に行っている

### スタッキング

5h 限度の 2倍化（5/6） と 週次 +50%（5/13）は **同時並行で有効** で、7/13 まで両方の恩恵を受けられる。

## 試すなら

1. `/usage` または `claude usage` で現在の利用状況を確認（リセット後の数値を把握）
2. 5/15 リセット直後にこれまで保留していた重いタスクを実行（複数 PR の review、ai-agent-knowledge の catch-up + try まとめ実行など）
3. 7/13 期限を `/schedule` または個人カレンダーに登録（恒久ではない点を忘れないため）
4. 6/15 の Programmatic Credits 分離との関係を整理: 対話的 Claude Code 利用は当面増量、ヘッドレス系は分離後別枠
5. ai-agent-knowledge 上で `experiments/` の検証コードを実行する際、これまで「上限気にして節制」していた箇所を見直して実装と検証を厚くする

## ソース

- [Claude Code Increases Weekly Limits by 50% Through July 13 2026: Anthropic's Anti-Codex Move (pasqualepillitteri)](https://pasqualepillitteri.it/en/news/2494/claude-code-weekly-limits-50-percent-anti-codex-anthropic-2026)
- [Claude Code Weekly Limits Just Jumped 50% Through July 13 (apidog)](https://apidog.com/blog/claude-code-weekly-limits-50-percent-increase-july-2026/)
- [Claude Resets Rate Limits: 5-Hour and Weekly Counters for Everyone, May 15 2026 (pasqualepillitteri)](https://pasqualepillitteri.it/en/news/2614/claude-resets-rate-limits-5-hour-weekly-may-15-2026)
- [Claude Code 週次利用制限が 50% 増加｜7月13日まで限定の朗報 (note.com / ひで)](https://note.com/tothinks/n/ne489f28d6b01)

---

## 感想・考察

### 「分離して、対話側は増量」戦略としての読み解き

少し前（6/15 予定）に `claude -p` 等の **ヘッドレス/プログラマティック利用は別クレジット枠に分離** されたのと、今回の対話ユーザー向け +50% 増量はセットで見ると整合的:

- 重い自動化ワークロードを回す層 → 別会計に分離（対話ユーザーの枠を食い潰す構造を解消）
- 通常の Claude Code 利用層 → 5h 2倍 + 週次 +50% + 5/15 全リセットで **大盤振る舞い**

つまり「課金構造の重い側を切り離す」のと「軽い側を増量する」のは矛盾ではなく、**棲み分けによる満足度最適化** とみるべき。Anthropic としては Pro/Max のリテンション維持を優先しつつ、自動化ヘビーユーザーからは別途回収するという二段構え。

### Colossus 1 契約との連動

5/6 の [SpaceX × xAI の Colossus 1 全容量確保](../02_read/2026-05-10-anthropic-spacex-colossus-deal.md)（300MW・220K GPU）が GPU 容量側の前提条件で、今回の +50% はその **直接の還元** という位置付け。Anthropic の compute マルチサプライヤ戦略（Google TPU / AWS Trainium / SpaceX×xAI GPU / 軌道上）の一角が稼働した結果が、Pro プランの自分の手元にも届くという構図。

### Pro プランの自分への実利（再確認）

- 7/13 まで catch-up / try / experiment Skill の検証ループを **節制せず回せる期間**
- 5/15 リセット直後（昨日）にすでにフレッシュなキャパが配布済み。今週は重い処理を仕込みやすい
- Opus 4.7 Fast mode 既定（v2.1.143）と重なるため、Opus 多用してもまだ余裕がある計算
- 7/13 期限と 6/15 の Programmatic Credits 分離だけは個別にカレンダーで意識しておく
