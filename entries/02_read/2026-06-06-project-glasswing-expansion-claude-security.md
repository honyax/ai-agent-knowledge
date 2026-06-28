---
date: 2026-06-06
status: read
relevance: B
tags: [security, glasswing, anthropic, Claude-Security, 重要インフラ, mythos]
source_urls:
  - https://www.anthropic.com/news/expanding-project-glasswing
  - https://www.helpnetsecurity.com/2026/06/03/anthropic-project-glasswing-expansion/
  - https://9to5mac.com/2026/06/02/anthropic-expands-glasswing-as-it-promises-public-claude-mythos-class-model-releases/
experiment_dir: null
---

# Project Glasswing 拡張: 150組織追加・重要インフラへ展開・Claude Securityリリース

## 3行要約

- Anthropicが Project Glasswing を拡張し、電力・水道・医療・通信・ハードウェア分野を中心に15カ国以上の約150組織を新規追加（合計約200組織に拡大）。既存パートナーがMythos Previewでコードベースをスキャンし、10,000件超の高・重大脆弱性を発見済み。
- 新製品「Claude Security」をリリース。最新公開モデル（Claude Opus 4.8等）を使ってコードベースをスキャンしパッチを提案。信頼できるセキュリティチームには脆弱性発見ツールも提供。
- Mythos-classモデル（セーフガード組み込み版）は「数週間以内」に全顧客へ展開予定と改めて言及。今後は重要インフラ事業者と重要OSSメンテナーを優先してプログラム拡大を継続。

## 自分への関連度: B

セキュリティ関心領域#3に直結。Claude Securityがコードベーススキャン製品として一般向けに展開されれば、Unityゲーム開発のセキュリティレビューに活用できる可能性がある。Mythos-classモデルの全顧客展開が近づいているため、Claude Code/APIの能力が近い将来大きく変わる前兆として押さえておく。

## 詳細

- **規模拡大**: Glasswing発足時の約50組織から合計約200組織へ。新参加の約150組織は15カ国以上に分布
- **新分野**: 電力・水道・医療・通信・ハードウェアセクター。大規模攻撃が1億人以上に影響しうると評価された組織を優先
- **発見実績**: 既存パートナーがMythos Previewでコードベースをスキャンし、高・重大脆弱性を10,000件超発見
- **Claude Security**: Claude Opus 4.8などの最新公開モデルを使ったコードスキャン＋パッチ提案製品。信頼されたセキュリティチームには脆弱性発見ツールも提供
- **Mythos-classモデル公開**: セーフガードを固めたMythos世代は数週間以内に全顧客展開予定（[既存エントリ参照](../02_read/2026-06-02-anthropic-mythos-preview-glasswing.md)）
- **今後**: 重要インフラ事業者と重要OSSメンテナーへ優先拡大、サイバー検証プログラムの拡大を継続

## 試すなら

1. Claude Security（https://www.anthropic.com/glasswing）の提供状況を確認し、申請可能なら申し込む
2. Mythos-classモデルの一般展開アナウンスを `/catch-up` でウォッチする
3. 自分のプロジェクトのコードベースに対してClaude Securityが使えるようになった際の適用を検討する

## ソース

- [Expanding Project Glasswing（Anthropic公式）](https://www.anthropic.com/news/expanding-project-glasswing)
- [Anthropic expands Project Glasswing to 150 organizations in more than 15 countries（Help Net Security）](https://www.helpnetsecurity.com/2026/06/03/anthropic-project-glasswing-expansion/)
- [Anthropic expands Glasswing as it promises public Mythos-class model releases（9to5Mac）](https://9to5mac.com/2026/06/02/anthropic-expands-glasswing-as-it-promises-public-claude-mythos-class-model-releases/)

---

## 感想・考察

### Q&A: 「Mythos へアクセスできる組織を150追加した」という理解は正しいか

結論として、その理解は誤り。今回の拡張で混同しやすい2つの軸を分けて整理する。

1. **Project Glasswing への参加組織（今回 +150）**
   - これは「Anthropic 側が Mythos Preview を使って、その組織のコードベースをスキャンしてあげる」防衛プログラムの対象が増えたという話。
   - 参加組織は Mythos の能力の「受益者」であって、組織自身が Mythos を操作・アクセスできるわけではない。
   - 対象は電力・水道・医療・通信・ハードウェアなどの重要インフラ系。

2. **Mythos-class モデルそのものの一般展開（別の話）**
   - セーフガードを固めた Mythos 世代は「数週間以内に全顧客へ展開予定」。
   - 「広く Mythos にアクセスできるようになる」のはこちらの軸。

つまり今回の +150 は「Anthropic が Mythos でコードスキャンする防衛対象が150組織増えた」であって、「Mythos を触れる組織が150増えた」ではない。一般顧客向けの Mythos アクセスは別軸で「数週間以内に全顧客展開」として予告されている。
