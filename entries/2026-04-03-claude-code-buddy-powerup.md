---
date: 2026-04-03
status: unread
relevance: B
tags: [claude-code, easter-egg, learning]
source_urls:
  - https://claudefa.st/blog/guide/mechanics/claude-buddy
  - https://help.apiyi.com/en/claude-code-buddy-terminal-pet-companion-activation-guide-en.html
  - https://code.claude.com/docs/en/changelog
experiment_dir: null
---

# Claude Code v2.1.90 — /buddy（エイプリルフール端末ペット）と /powerup（インタラクティブレッスン）

## 3行要約

- `/powerup` コマンドが追加され、Claude Codeの各機能をアニメーションデモ付きのインタラクティブなレッスンで学習できる
- `/buddy` コマンドはエイプリルフール2026の隠し機能で、アカウントIDから決定論的に割り当てられる端末ペット（18種）がターミナルに常駐する
- /buddyはソースコード流出（v2.1.88）で本リリースの1日前に発覚し、v2.1.89以降・Proプランで利用可能

## 自分への関連度: B

`/powerup` はClaude Codeの機能を体系的に学べる実用的な機能。`/buddy` はエイプリルフールネタだが、ゲーム開発者視点でUI/UX・エンゲージメント設計として興味深い。

## 詳細

**/powerup**:
- Claude Code v2.1.90で追加
- 機能ごとにアニメーションデモが用意されたインタラクティブなレッスン形式
- 新機能のオンボーディングに活用できる

**/buddy**:
- 18種類（duck, goose, cat, rabbit, owl, penguin, turtle, snail, dragon, octopus, axolotl, ghost, robot, blob, cactus, mushroom, chonk, capybara）
- レア度5段階あり、アカウントIDのハッシュで決定論的に割り当てられる
- ターミナル入力行の横にASCIIアートでペットが表示され、会話内容に反応してコメントする
- 4月1日〜7日のティーザーウィンドウ（試用期間）を設定。v2.1.88のソースコード流出で先行発覚した

## 試すなら

1. `npm update -g @anthropic-ai/claude-code` でv2.1.90以上に更新
2. Proプランのターミナルで `/powerup` を実行してレッスン一覧を確認
3. `/buddy` を実行して自分のペットを確認（4月7日まで）

## ソース

- [Claude Buddy: Anthropic April Fools Terminal Tamagotchi](https://claudefa.st/blog/guide/mechanics/claude-buddy)
- [Enable Claude Code Buddy terminal pet: complete guide](https://help.apiyi.com/en/claude-code-buddy-terminal-pet-companion-activation-guide-en.html)
- [Claude Code Changelog](https://code.claude.com/docs/en/changelog)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->

