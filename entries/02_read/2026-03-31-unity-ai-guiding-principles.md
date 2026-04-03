---
date: 2026-03-31
status: read
relevance: A
tags: [unity, unity-ai, policy, copyright, data, ethics]
source_urls:
  - https://unity.com/ja/legal/unityai-guiding-principles
  - https://digitalproduction.com/2025/08/22/unity-6-2-welcomes-ai-but-pace-caution-user-liability-on-copyright/
experiment_dir: null
---

# Unity AI ガイディングプリンシプル — データ利用・著作権・開発者責任の整理

## 3行要約

- **デフォルトでデータ学習はオフ**。「Improve Unity AI」設定を明示的にオンにしない限り、プロンプトやコードはモデル改善に使われない
- 生成アセットに **「Unity AI」メタデータが自動付与**され、プロジェクト内の追跡・監査・削除が可能
- **著作権・権利侵害の最終責任は開発者**。Unity はIP侵害を防ぐフィルターを設けているが、生成物のライセンス適合性確認はユーザー側の義務

## 自分への関連度: A

商用ゲームに Unity AI 生成アセットを使う場合、権利関係の理解は必須。
特に「Improve Unity AI」のオプトイン設定と、生成アセットのメタデータ追跡は実際に使う前に把握しておく必要がある。

## 詳細

### データ利用ポリシー

| 設定 | デフォルト | 内容 |
|------|-----------|------|
| Improve Unity AI | **OFF** | プロンプト・応答・コード等を Unity AI 改善に使用することを許可 |
| カスタムモデル学習 | 明示的アップロードのみ | 自組織専用の再学習モデルを作成可能。アップロードデータは Partner Models の改善には使われない |

- ランタイムアプリケーションやユーザー制作物（メッシュ・オーディオ等）はモデル学習には**使用されない**
- カスタムモデルは組織内のみで利用可能

### 著作権・IP への対応

- Partner Models は**テキストマッチング・コンテキスト分析・ビジョンモデル**等を使い、IP侵害の可能性が高いプロンプトをブロック
- 生成アセットに「Unity AI」メタデータを付与し、プロジェクト内で検索・追跡・削除が容易
- **最終的な適法性の確認義務は開発者側**にある（Unity は免責）

### 開発者への実務的影響

- 商用リリース前に AI 生成アセットの洗い出しが必要（メタデータで検索可能）
- 「Improve Unity AI」を ON にする場合、社内の情報セキュリティポリシーとの整合を確認する
- AI 生成コンテンツのライセンス条項（利用規約）を定期的にチェックする必要がある

## 試すなら

1. Unity Dashboard で「Improve Unity AI」設定が OFF になっていることを確認
2. Generators で生成したアセットのメタデータ（「Unity AI」タグ）を Project ウィンドウで検索確認
3. 商用プロジェクトで使う前に Unity AI Guiding Principles の最新版（unity.com/legal/unityai-guiding-principles）を一読する

## ソース

- [Unity AI Guiding Principles（公式）](https://unity.com/ja/legal/unityai-guiding-principles)
- [Unity 6.2 Welcomes AI, But Pace Caution—User Liability on Copyright（Digital Production）](https://digitalproduction.com/2025/08/22/unity-6-2-welcomes-ai-but-pace-caution-user-liability-on-copyright/)
- [Using AI-generated assets responsibly（Unity Learn）](https://learn.unity.com/course/prototype-a-scene-with-unity-ai/tutorial/using-ai-generated-assets-responsibly)

---

## 感想・考察

エントリ内容をざっくり確認。Unity AI を商用利用する際のデータポリシー・著作権責任の所在をまとめたガイドラインが存在するという理解。「Improve Unity AI」はデフォルトOFF、生成アセットにメタデータが付与される、最終的な権利確認は開発者責任、という3点が要点。実際に Unity AI を使い始めるタイミングで公式ガイドライン原文を一読する。
