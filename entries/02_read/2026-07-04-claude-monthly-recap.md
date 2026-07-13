---
date: 2026-07-04
status: read
relevance: B
tags: [claude-ai, monthly-recap, reflect, memory, beta]
source_urls:
  - https://support.claude.com/en/articles/12138966-release-notes
  - https://releasebot.io/updates/anthropic/claude
experiment_dir: null
---

# Claude.ai に Monthly Recap: 1ヶ月の利用傾向を振り返る機能（Settings > Reflect）

## 3行要約

- Claude.ai に **Monthly Recap** 機能がベータ追加。**Settings > Reflect** から、その月に時間を使ったトピック、最も活発だった曜日・ピーク時間帯、Claude との働き方に関する観察（observations）を表示する。
- 対象は **Free / Pro / Max プラン**、Web と Claude Desktop で利用可。**memory 機能がオンであることが前提**。
- 「Claude がユーザーの利用パターンを解析して振り返りを提示する」タイプの機能で、Spotify Wrapped 的な自己分析レイヤー。Claude 側の memory / 解析基盤（Dreaming 系）の応用形と見られる。

## 自分への関連度: B

日常の情報収集・分析に Claude.ai を使っている（CLAUDE.md 関心領域 7）ので対象ユーザーではあるが、ワークフローを変える機能ではなく「見ると面白い」系。自分の Claude 利用の時間帯・トピック分布を客観視できるのは、このナレッジベース運用（catch-up の実行タイミング最適化など）の参考データになる可能性が少しある。Pro プランで使えるので試すコストはほぼゼロ。

## 詳細

### 表示内容

- **トピック**: その月に時間を使った話題の分布
- **アクティビティ**: 最も活発だった曜日、ピーク時間帯
- **観察（observations）**: Claude との働き方についての所見

### 前提条件

- **memory 機能オン**が必須（memory の蓄積データを解析する構造のため）
- Free / Pro / Max プランのベータ、Web + Claude Desktop

### 文脈

- Claude の memory / 振り返り基盤の応用。Auto Dream（[[2026-04-04-claude-code-auto-dream]]）や Dreaming（[[2026-07-01-loop-engineering-boris-cherny]]）が「エージェントの自己改善のための記憶整理」なら、Monthly Recap は「**ユーザーに提示するための記憶集計**」。
- 「Settings > Reflect」という設置場所から、今後 Reflect 配下に振り返り系機能が増える可能性。

## 試すなら

1. Claude.ai（Web）の Settings > Reflect を開き、Monthly Recap が表示されるか確認（Pro プランで対象のはず）。
2. memory がオンになっているか確認（オフなら有効化して翌月まで蓄積を待つ）。
3. 表示されたら、自分のトピック分布・ピーク時間帯を確認し、catch-up 実行タイミングや情報収集習慣の見直し材料にする。

## ソース

- [Release notes (Claude Help Center)](https://support.claude.com/en/articles/12138966-release-notes)
- [Claude Updates by Anthropic - July 2026 (Releasebot)](https://releasebot.io/updates/anthropic/claude)

---

## 感想・考察

### 機能の理解

- Claude.ai(チャット側)に月間の利用傾向を振り返る Monthly Recap がベータ追加された、という理解で正しい。Spotify Wrapped の Claude 版。
- Dreaming / Auto Dream が「エージェント自身の自己改善のための記憶整理」なのに対し、Monthly Recap は同じ記憶基盤を「ユーザーに見せるための集計」に使った応用形。「Reflect」という設定カテゴリの新設は、今後この配下に振り返り系機能が増える布石と読める。

### 実際に確認した結果(2026-07-14)

- 自分の claude.ai(Web)の設定画面には Reflect セクションが表示されなかった(一般 / アカウント / プライバシー / 請求 / 使用量 / 機能 / Claude Code / Claude in Chrome / スキル / コネクタ / プラグイン のみ)。
- リリースノート(7/9付)で正式記載を確認: Free / Pro / Max 対象、Settings > Reflect、memory 有効化が前提。同じ Reflect 系で「Settings > Time and focus」(休憩リマインダー・静かな時間)も追加。
- 表示されない原因の候補: (1) memory がオフ(Reflect は memory の蓄積データを解析する構造のため、オフだとセクション自体が出ない可能性)、(2) ベータのアカウント単位の段階的ロールアウト。
- 次のアクション: 「機能」タブで memory 設定を確認し、オンであれば表示されるまで待つ。表示されたらトピック分布・ピーク時間帯を catch-up 実行タイミングの参考にする。
