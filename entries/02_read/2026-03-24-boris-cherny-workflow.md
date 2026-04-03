---
date: 2026-03-24
status: read
relevance: A
tags: [claude-code, workflow, parallel, opus, plan-mode, boris-cherny]
source_urls:
  - https://qiita.com/ot12/items/66e7c07c459e3bb7082d
experiment_dir: null
---

# Claude Code開発者Boris Chernyの次世代開発ワークフロー

## 3行要約

- Claude Codeコアメンバーが実践する10〜15並列インスタンス運用。ローカルとWebに分散し、通知トリガーで非同期指揮するスタイル
- Opus 4.5 with thinkingを採用。生成速度より「手戻りゼロ」を重視し、トータルで高速化を実現
- CLAUDE.mdをGit管理しAIのミスをルール化。チーム全体でAI精度を向上させる「複利効果」を生み出す

## 自分への関連度: A

CLAUDE.mdのGit管理・ミスのルール化は即実践可能。並列実行のパターンもClaude Code on the Webと組み合わせれば有効。Plan Mode徹底→一気に実装のアプローチは自分のゲーム開発でも参考になる。

## 詳細

### 6つの柱
1. **並列処理の最大化**: 10〜15インスタンスをローカル+Webに分散
2. **モデル選択の経済合理性**: 軽量モデルより高精度モデルで手戻り削減
3. **組織的な知識蓄積**: CLAUDE.mdをGit管理、ミスをルール化
4. **計画重視（Plan Mode）**: 十分に議論→一気に実装
5. **自動化**: スラッシュコマンド、サブエージェント、フック、MCP連携
6. **検証ループの自律化**: AIが自ら動作確認・修正を繰り返す

## 試すなら

1. 自分のCLAUDE.mdにAIが過去に間違えたパターンをルールとして追記
2. Plan Modeで設計を固めてから実装に移る流れを試す
3. Claude Code Web（--remote）を使い2-3並列で小タスクを同時実行

## ソース

- [Claude Code開発者のワークフロー解説（Qiita）](https://qiita.com/ot12/items/66e7c07c459e3bb7082d)

---

## 感想・考察

並列開発は普段の業務でも実践済み。気になったのは「Claude Codeが入力待ちになるとOS通知が飛ぶ」という部分で、Hookの `Notification` イベントを使えばWindowsでも実現できる（PowerShellでトースト通知）。VSCode拡張経由でも同じHook設定が有効なはず。

ルーチン作業のスラッシュコマンド化は、現在は Agent Skills（`.claude/skills/`）が推奨。このナレッジベース自体も `/status` や `/try` をSkillsで実装しており実践済み。

サブエージェントについては「Claude Codeが自律的に生成・活用する」イメージだったが、`.claude/agents/` 配下にMarkdownでルールを定義することで、役割を持ったカスタムエージェント（レビュー担当・テスト担当等）を事前に用意しておくことも出来る。Skillsはユーザーが起動、Agentsは Claude が自律的に spawn という役割分担。

