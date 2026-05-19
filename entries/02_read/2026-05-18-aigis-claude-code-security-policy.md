---
date: 2026-05-18
status: read
relevance: A
tags: [claude-code, security, hooks, policy, audit, aigis, enterprise, compliance, ai-governance-jp]
source_urls:
  - https://qiita.com/sharu389no/items/ab5bf50d9f68e7c8de56
experiment_dir: null
---

# AIGIS — Claude Code のツール呼び出しを横取りして「可視化・制御・監査・規制対応」を全部入れる OSS hook

## 3行要約

- @sharu389no（2026-05-15 Qiita）が、情シスが AI ツール導入を却下する「可視化・制御・監査・規制対応」の **4要件をすべて Claude Code に後付けで満たす OSS** として AIGIS を発表。`pip install pyaigis` で導入し、`.claude/hooks/aigis-guard.py` を **pre-tool-use フック** に登録するだけで、Bash の `rm -rf /` や SSH 鍵アクセス、git 強制 push など危険操作を ブロック / 確認 / 許可 に振り分けられる
- 設定は **`aigis-policy.yaml`** に正規表現で書く。既定ポリシーで再帰削除・秘匿鍵 read・force push・任意プロセス kill などを deny / review に分類しており、組織独自ルール（社内ドメインへの curl のみ許可等）を追記する形で運用する想定
- 監査面は **JSON Lines 形式で全ツール呼び出しを記録**し、`aigis logs --alerts` で警告だけ抽出可能。**「AI事業者ガイドライン」など日本規制の39項目への対応** をマッピングする機能まで持ち、社内導入の説得材料を最初から梱包している点が特徴

## 自分への関連度: A

[[user_hooks_usage]] で hooks をほぼ使っていない自分にとっても「pre-tool-use hook がここまで使える」という具体例として強い。CLAUDE.md の関心領域 3 番（AI開発ツールのセキュリティリスクと対策）に直撃。[[claude-code-security-self-check]] や [[fake-claude-code-installer-cookie-stealer]] のような「事後対応」ではなく、**「ローカルでの権限境界をエージェント側で組む」** という今後主流になりそうな方向の OSS リファレンス実装。

## 詳細

### 4要件と AIGIS の対応
| 要件 | AIGIS の機能 |
|------|------------|
| 可視化 | tool_use を全件 JSONL ログに記録、`aigis logs` で検索 |
| 制御 | pre-tool-use hook で deny / review / allow を判定 |
| 監査 | `aigis logs --alerts` で警告ログ抽出、エクスポート可 |
| 規制対応 | AI事業者ガイドライン等39項目とのマッピング表を同梱 |

### ポリシー記法（抜粋）
```yaml
rules:
  - id: dangerous-rm
    pattern: "rm\\s+-rf\\s+/"
    action: deny
    reason: "ルートからの再帰削除を禁止"
  - id: ssh-keys
    pattern: "(~/.ssh|id_rsa|id_ed25519)"
    action: review
  - id: force-push
    pattern: "git push.*--force"
    action: review
```

### 仕組み
- Claude Code の hooks 機構（`settings.json` の `hooks.preToolUse`）に Python スクリプトを登録
- スクリプトは tool 名と引数を受け取り、ポリシーに照らして exit code で deny / pass を返す
- deny 時に Claude へ理由を返すことで、エージェント側に「なぜ却下されたか」を理解させ、迂回ではなく代替案を出させる設計

### 既存 hook と比較した位置付け
- 単に正規表現ブロックだけなら数十行の自作スクリプトで足りるが、**ログ JSONL の形式・規制マッピング・既定ポリシー・CLI ツール（`aigis logs`）まで揃ったパッケージは少ない**
- 「個人開発で hooks を使っていなかったが、社内導入時に整える」用途で参照価値が高い

## 試すなら

1. `pip install pyaigis` で導入し、`.claude/hooks/aigis-guard.py` を pre-tool-use として `settings.json` に登録
2. デフォルトの `aigis-policy.yaml` で `rm -rf` や `curl` を実行させ、deny / review がどう Claude に伝わるか確認
3. 自分用に「ホストの allow リスト（github.com, anthropic.com のみ curl 許可）」などのルールを追記
4. `aigis logs --tail` で運用中のツール呼び出しをリアルタイム観察
5. 既存の [[fewer-permission-prompts-skill]] と比較し、「permission allowlist で済む範囲」と「hook で動的判定が必要な範囲」を切り分ける

## ソース

- [Claude Code を社内で使うための「AIエージェントセキュリティ」実践編 #Security - Qiita（@sharu389no、2026-05-15）](https://qiita.com/sharu389no/items/ab5bf50d9f68e7c8de56)

---

## 感想・考察

### 開発主体の調査結果

エントリ本文には作者の明示記載がなかったため、ソースを辿った。

| 場所 | ハンドル |
|------|---------|
| Qiita 紹介記事 | @sharu389no |
| GitHub owner | killertcell428（個人アカウント、Org 配下ではない） |
| PyPI Author | Charles389no |

「389no」が3アカウント共通で、**同一人物が複数ハンドルを使い分けている可能性が高い**。

### 「公式サイト」aigis-platform.vercel.app の実態

プロダクト LP の見た目はあるが、組織性は薄い:
- `/about`, `/contact`, `/pricing` は全部 404（ナビにも未掲載）
- 会社名・住所・連絡先・法人格・チーム紹介の記載ゼロ
- フッターは `© 2025 Aigis Security Platform` のみ（リリースは 2026 年で年表記もズレ）
- GitHub / ドキュメント / SNS / コミュニティへのリンクなし
- Vercel 無料サブドメイン（独自ドメインなし）
- 料金プラン（Starter 無料 / Pro $49 / Enterprise カスタム）は掲げているが、**Enterprise プランを謳いながら契約相手の法人名が公開されていない**

### 評価: 個人 OSS としては真っ当 / 業務導入は不可

- **野良ではない側面**: Apache 2.0、PyPI 正式登録、Production/Stable、リリース履歴あり → 個人 OSS としてはまとも
- **組織製ではない側面**: 法人実体・契約相手が不明。セキュリティ製品でこれは信頼境界に置けない

### 結論

**導入は見送り**。理由は2つ:

1. **ベンダー実体不明のセキュリティ製品を信頼境界に置けない**。ツール呼び出しを横取りする pre-tool-use hook は、それ自体がエージェントの権限境界の中核。バックドアや意図せぬ送信があっても発見しづらい。個人開発者の OSS にこの位置を任せるなら、最低でもコードを全部読み切れる規模であることが前提だが、「規制マッピング・JSONL ログ・CLI まで揃ったパッケージ」を謳う以上、それなりの行数があるはず。
2. **発想だけなら自前で組める**。pre-tool-use hook で正規表現ポリシーを judge する仕組み自体は、[[user_hooks_usage]] の状況を変える動機にはなるが、それは AIGIS を入れなくてもできる話。`settings.json` の `hooks.preToolUse` に数十行のスクリプトを置けば最小構成は組める。

### 派生して残しておきたいアイデア

- 自分用には [[fewer-permission-prompts-skill]] の allowlist 拡張で当面足りる。**「allowlist で済む静的な許可」と「hook で動的判定が必要な操作」の境界**を一度整理しておくと、将来本気で hook を導入する判断がしやすい
- もし hook を入れるなら最初の対象は: `rm -rf` 系、`~/.ssh` 配下の read、`git push --force`、`curl` の送信先ホスト allow リスト あたり。AIGIS のデフォルトポリシーは「最初に何を守るべきか」の参考リストとしては使える
