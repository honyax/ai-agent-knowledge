---
date: 2026-07-01
status: read
relevance: A
tags: [anthropic-news, korea, nexon, ゲーム開発, claude-code, パートナーシップ]
source_urls:
  - https://www.anthropic.com/news/seoul-office-partnerships-korean-ai-ecosystem
  - https://www.anthropic.com/news/seoul-becomes-third-anthropic-office-in-asia-pacific
  - https://www.cdomagazine.tech/aiml/anthropic-expands-south-korea-presence-with-seoul-office-and-new-ai-partnerships
  - https://www.benzinga.com/markets/tech/26/06/53267847/anthropic-eyes-south-korea-expansion-ahead-of-ipo-with-seoul-office-and-partnerships
experiment_dir: null
---

# Anthropic ソウルオフィス開設、Nexon が「live-service ゲーム開発」に Claude Code 採用

## 3行要約

- 6/17、Anthropic は東京・ベンガルールに続くアジア太平洋 3 拠点目としてソウルオフィスを正式開設。代表は元 Snowflake Korea GM の KiYoung Choi。韓国科学技術情報通信部 (MSIT) と AI 安全性・サイバーセキュリティ・韓国語モデル評価で MoU 締結。
- 企業導入: NAVER（全エンジニアリング組織で Claude Code）、Samsung SDS（Samsung Electronics で Claude Cowork / Claude Code）、LG CNS（LG Group 全体で Claude）、**Nexon（live-service ゲーム開発で Claude Code）**、Hanwha Solutions（AWS Bedrock 経由、リージョン内データ管理）、Channel Corp（23 万社の Channel Talk 基盤）。
- 学術連携: KAIST / 高麗大 / 延世大 / POSTECH の National AI Research Lab 60 名の研究者に Claude を提供。Anthropic Economic Index で「韓国は Claude.ai 利用が世界トップ 12 圏内」（技術・クリエイティブ分野が中心）。

## 自分への関連度: A

[[user_role]] / CLAUDE.md にあるとおりゲーム開発 20 年・Unity/C# 主領域なので、**Nexon が live-service ゲーム開発に Claude Code を採用した事例**は実務に直結する。具体的なワークフロー詳細が出てくれば [[2026-03-31-unity-ai-features]] / [[2026-03-31-unity-ai-guiding-principles]] の延長で追跡したい。Nexon は MapleStory / Dungeon&Fighter など長期運用タイトルで、自分の Unity ゲーム運用との重なりが多い。

## 詳細

### オフィス・MoU

- **オフィス**: ソウル、AP 3 拠点目（東京、ベンガルール、ソウル）。代表 KiYoung Choi（元 Snowflake Korea GM）。
- **MSIT との MoU**: AI 安全性、サイバーセキュリティ、韓国語モデル評価で Korea AI Safety Institute と協業。
- **IPO 文脈**: Benzinga は「IPO を控えた Anthropic の韓国市場への布石」と分析（[[2026-06-02-anthropic-ipo-confidential-filing]] とつなぐ）。

### 企業パートナーシップ詳細

| 企業 | 内容 |
|------|------|
| NAVER | 全エンジニアリング組織で Claude Code 採用 |
| Samsung SDS | Samsung Electronics で Claude Cowork + Claude Code |
| LG CNS | LG Group 全体で Claude |
| **Nexon** | **live-service ゲーム開発に Claude Code** |
| Hanwha Solutions | AWS Bedrock 経由 Claude、リージョン内データ管理 |
| Channel Corp | 23 万社が使う Channel Talk プラットフォームに Claude |

### Nexon の意味合い

- 「live-service ゲーム」= MapleStory（22 年運用）、Dungeon&Fighter（20 年運用）、Maple M、ブルーアーカイブなど、長期運用前提のタイトル群。
- live-service は機能追加・バグ修正・イベント実装が日常茶飯事で、AI コーディング支援のフィット感が高い領域。
- 詳細なワークフローは未公開（NAVER のような「全社員」レベルの数字は出ていない）が、ゲーム業界での Claude Code 公式採用事例としては最大級。
- 自分が想定する Unity → UE5 移植 ([[project_unity_to_ue5_migration]]) のような長期運用 + 大規模リファクタ案件にも示唆。

### 学術連携

- KAIST、Korea University、Yonsei、POSTECH の National AI Research Lab consortium。
- 約 60 名の研究者に Claude を無償または優待提供。
- 韓国語モデル評価で MSIT と協業するため、Korean benchmark の整備が進む可能性。

### Anthropic Economic Index

- 韓国は Claude.ai 利用が「世界トップ 12 ヶ国」。
- 利用の中心は「技術・クリエイティブ」分野（=ゲーム/コンテンツ産業との親和性が高い）。

## 試すなら

1. Nexon Korea / Nexon Games のテックブログ・採用情報を定期チェックし、Claude Code 採用後のワークフロー記事が出たら拾う（韓国語のため翻訳経由でも）。
2. [[2026-03-31-unity-ai-features]] / [[2026-03-31-unity-ai-guiding-principles]] と並べ、live-service 文脈での AI コーディング採用パターンを整理。
3. 韓国語 Claude モデル評価の進捗を [[2026-06-12-anthropic-public-record-survey]] と合わせて追跡（多言語性能向上は日本語にも波及しうる）。
4. Anthropic Economic Index の韓国データ更新を観察、技術職での利用パターンが日本（自分の環境）と似ているか比較。
5. NAVER の全エンジニア導入は、[[2026-06-06-claude-code-team-claude-md-design]] と同じく大規模組織での CLAUDE.md 設計の事例として注目（記事化されれば拾う）。

## ソース

- [Anthropic opens Seoul office and announces new partnerships (Anthropic 公式)](https://www.anthropic.com/news/seoul-office-partnerships-korean-ai-ecosystem)
- [Seoul becomes Anthropic's third office in Asia-Pacific (Anthropic 公式)](https://www.anthropic.com/news/seoul-becomes-third-anthropic-office-in-asia-pacific)
- [Anthropic Expands South Korea Presence (CDO Magazine)](https://www.cdomagazine.tech/aiml/anthropic-expands-south-korea-presence-with-seoul-office-and-new-ai-partnerships)
- [Anthropic Eyes South Korea Growth Ahead of IPO (Benzinga)](https://www.benzinga.com/markets/tech/26/06/53267847/anthropic-eyes-south-korea-expansion-ahead-of-ipo-with-seoul-office-and-partnerships)

---

## 感想・考察

### 「パートナーシップ」の実態（2026-07-03）

Anthropic 側が「パートナーシップ」と一括りにしている中には、実は **3 種類の異なる関係** が混ざっている。同じ Seoul 発表パッケージでも位置づけがまったく違うので分解しておく。

#### 1. 企業導入（実質は大口顧客契約）

NAVER / Samsung SDS / LG CNS / **Nexon** / Hanwha / Channel Corp の 6 社。

- 実体は **Enterprise / Team プランのライセンス契約**（有償利用）
- 「戦略的パートナーシップ」と呼ぶ理由:
  - 導入規模が大きい（全エンジニアリング組織単位など）
  - 相互に PR 価値がある（Anthropic は導入事例、企業側は「AI 先進企業」ブランディング）
  - 導入支援チームが Anthropic 側から張り付く可能性
- Nexon の場合、公開情報は「live-service ゲーム開発に Claude Code を採用」だけ。金額・座席数・共同開発の有無は非公開。
- **含まれない**: Anthropic が Nexon に出資、共同で製品を作って外販する、といった資本・共同事業関係ではない。

#### 2. 政府 MoU（覚書、無償の技術協力）

**韓国科学技術情報通信部 (MSIT) との MoU**。

- MoU = 法的拘束力の薄い協力覚書
- 内容: AI 安全性、サイバーセキュリティ、韓国語モデル評価
- Korea AI Safety Institute との共同評価手法作り程度
- 金銭のやり取りは通常発生しない。相互の技術情報共有と共同研究。

#### 3. 学術支援（研究者への提供）

**KAIST / 高麗大 / 延世大 / POSTECH の研究者 60 名**。

- Claude を無償または優待価格で提供
- 見返りは「Claude を使った研究論文の産出」＝ Anthropic のマーケティング/評価材料
- [[2026-07-01-claude-science-ai-workbench]] の $30k 助成×50 件と似た構造（学術優待）

#### 分類表

| タイプ | 実態 | 金銭の流れ |
|--------|------|----------|
| 企業パートナー | 大口顧客ライセンス契約 | 企業 → Anthropic |
| 政府 MoU | 技術協力覚書 | なし |
| 学術支援 | 優待/無償提供 | Anthropic → 研究者（クレジット） |

#### Nexon 事案の正しい読み方

「Anthropic と Nexon が資本や事業レベルで結合した」わけではなく、**Nexon が Claude Code のエンタープライズ顧客になった** というのが実態。PR 上「パートナーシップ」と呼ぶことで、単なる「顧客」より強い戦略的関係に見せている。

自分の Unity 案件で「Nexon の Claude Code 導入事例」を参照する際は、この解像度で見ておく必要がある（=詳細ワークフローが仮に出てきても、それは「共同で作った新製品」ではなく「Nexon 社内での運用ノウハウ」）。

<!-- /try 実行時に自動生成 -->
