---
date: 2026-07-04
status: read
relevance: B
tags: [claude-api, apple, foundation-models, swift, ios27, on-device]
source_urls:
  - https://claude.com/blog/claude-for-foundation-models
  - https://platform.claude.com/docs/en/cli-sdks-libraries/libraries/apple-foundation-models
  - https://github.com/anthropics/ClaudeForFoundationModels
  - https://mjtsai.com/blog/2026/06/16/apple-foundation-models-in-appleos-27/
experiment_dir: null
---

# Claude が Apple Foundation Models framework に対応、Swift の同一 API でオンデバイスとクラウドを切替

## 3行要約

- Anthropic が **ClaudeForFoundationModels**（Swift パッケージ、ベータ）を公開。Apple の Foundation Models framework（iOS 27 / iPadOS 27 / macOS 27 / visionOS 27 / watchOS 27）の **server-side language model** として Claude を組み込める。
- framework の `LanguageModel` プロトコルに Claude を準拠させる設計で、Apple のオンデバイスモデルと **同じ `LanguageModelSession` API**（`respond(to:)`、ストリーミング、guided generation、tool calling）がそのまま使える。オンデバイスで足りない処理だけ Claude に投げる「ハイブリッド」構成が書きやすい。
- リクエストはアプリから Claude API へ直接送信され、**Apple は経路に入らない**（プロンプトも応答も見えない）。課金は通常の Anthropic API 価格。OS 27 ベータの server-side LLM API が対象で、GA までに API 変更の可能性あり。

## 自分への関連度: B

Swift ネイティブ開発は自分の主戦場ではない（Unity / UE5.8 が中心）ため直接は使わないが、CLAUDE.md 関心領域 5（Claude API の変更・ゲーム内 AI 統合の可能性）の文脈で知っておく価値がある。「オンデバイスの小型モデルで一次処理し、重い推論だけクラウドの Claude に投げる」というハイブリッドパターンは、ゲーム内 AI 統合を考える際のアーキテクチャ参考になる。Unity の iOS ビルドから直接使うのは（Swift API のため）ブリッジが必要で現実的でない点も押さえておく。

## 詳細

### 何ができるか

- Swift パッケージを追加し、Anthropic API キーでサインイン
- Apple のオンデバイスモデルと同じ `LanguageModelSession` API で Claude を呼べる:
  - `respond(to:)` / ストリーミング / guided generation（型付き出力）/ tool calling
- **オンデバイス → Claude の連携**: オンデバイスパスの型付き出力を、そのまま Claude リクエストに渡せる。SwiftUI ビューへのストリーミング反映もパッケージが処理

### プライバシーと課金

- リクエストは **アプリ → Claude API 直行**。Apple のサーバー（Private Cloud Compute 含む）は経路に入らない
- プロンプト・レスポンスを Apple は見られない
- 課金は開発者の Anthropic アカウントに通常 API 価格で計上

### 位置づけ

- Apple は OS 27 の Foundation Models framework に「server-side language model」の差し込み口を用意し、Claude と Gemini が初期対応（WWDC 2026 の流れ）
- Xcode 27 の Claude 統合と合わせ、Apple エコシステムへの Anthropic の本格進出
- ベータであり、OS 27 GA までに API 変更の可能性が明記されている

### ゲーム開発視点での読み方

- パターンとしては「軽い判定・分類はオンデバイス（無料・低遅延・オフライン可）、複雑な生成・推論はクラウド Claude（高能力・従量課金）」の切替が 1 つの API で書ける、というのが本質
- Unity / UE から直接使うものではないが、ゲーム内 AI のコスト設計（全部クラウドに投げない）の参考モデルになる

## 試すなら

1. GitHub の [ClaudeForFoundationModels](https://github.com/anthropics/ClaudeForFoundationModels) の README とサンプルを読む（Swift 環境がなくても設計は追える）。
2. Mac を macOS 27 ベータにできる環境があれば、Xcode で最小サンプル（オンデバイス → Claude フォールバック）を動かす。
3. ゲーム内 AI 統合の設計メモに「オンデバイス一次処理 + クラウド二次処理」パターンとして記録し、Unity での類似構成（ONNX ローカル推論 + Claude API）を考える材料にする。

## ソース

- [Claude support for Apple's Foundation Models framework (Anthropic 公式)](https://claude.com/blog/claude-for-foundation-models)
- [Apple Foundation Models (Claude Platform Docs)](https://platform.claude.com/docs/en/cli-sdks-libraries/libraries/apple-foundation-models)
- [anthropics/ClaudeForFoundationModels (GitHub)](https://github.com/anthropics/ClaudeForFoundationModels)
- [Apple Foundation Models in appleOS 27 (Michael Tsai)](https://mjtsai.com/blog/2026/06/16/apple-foundation-models-in-appleos-27/)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->

### Android との比較（2026-07-08 会話メモ）

Android にも似た発想の仕組みはあるが、閉じたエコシステムである点が Apple と異なる。

- Google は ML Kit GenAI API / AICore 経由で **Gemini Nano**（オンデバイス）を提供。2026年4月には Firebase AI Logic に **hybrid inference**（実験的機能）が追加され、オンデバイスの Gemini Nano とクラウド版 Gemini を動的に切り替えられるようになった。構造的には Apple の Foundation Models framework とほぼ同じ発想。
- ただしこの hybrid inference は **Gemini 専用**で、Claude のような第三者モデルを差し込む口はない。
- 一方 Apple 側は WWDC 2026 で Foundation Models framework を**サードパーティモデル（Claude・Gemini）に開放**し、今夏には**オープンソース化**も予定していると発表されている。
- Android Studio（IDE）には Claude を含む第三者モデルを選べる「Model Provider」設定があるが、これは開発者向け AI アシスタント機能であり、アプリ内のオンデバイス+クラウド切替パターンとは別物。

→ 「OS が用意したオンデバイス/クラウド切替 API に Claude を挿せる」という点では、現状 Apple 側にしかない優位性。Android が同種の口を第三者モデルに開放するかは今後の観測ポイント。
