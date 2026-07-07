---
date: 2026-07-04
status: read
relevance: A
tags: [mcp, spec, stateless, release-candidate, mcp-apps, tasks-extension, protocol]
source_urls:
  - https://blog.modelcontextprotocol.io/posts/2026-07-28-release-candidate/
  - https://mcp.directory/blog/mcp-2026-07-28-release-candidate
  - https://stacktr.ee/blog/mcp-2026-spec-changes
  - https://workos.com/blog/mcp-2026-spec-agent-authentication
experiment_dir: null
---

# MCP 仕様 2026-07-28 RC: プロトコルコアの stateless 化、セッションヘッダー廃止

## 3行要約

- MCP の次期仕様（2026-07-28 確定予定）の Release Candidate が公開。最大の変更は **プロトコルコアの stateless 化**: `Mcp-Session-Id` ヘッダーとプロトコルレベルのセッション概念が削除され、任意のリクエストが任意のサーバーインスタンスに着地できるようになる。
- 運用面のインパクト: 従来リモート MCP サーバーに必要だった sticky session・共有セッションストア・ゲートウェイでの deep packet inspection が不要になり、**普通のラウンドロビン LB の後ろで動かせる**。`tools/list` レスポンスは `ttlMs` の範囲でクライアントキャッシュ可能に。
- その他: **MCP Apps**（サーバーがサンドボックス化 iframe で描画される HTML UI を配布）、**Tasks 拡張**（長時間ジョブ。2025-11-25 で実験的コア機能だったが、本番運用の知見を受けて拡張に再設計）、OAuth/OIDC への整合強化、正式な deprecation ポリシー。SDK メンテナ向けに約 10 週間の検証期間が設けられている。

## 自分への関連度: A

CLAUDE.md 関心領域 6（MCP 関連のアップデート）に直結。自分は Unity MCP / Blender MCP / Godot MCP を利用し、elicitation デモサーバーを自作した経験もある（`experiments/2026-03-25-mcp-elicitation`）。stateless 化はサーバー実装の書き方を変える破壊的変更を含むため、自作サーバーを今後も書くなら新旧の差分を押さえておく必要がある。ローカル stdio 利用が中心の自分には当面の実害は小さいが、リモート MCP（[[2026-05-22-claude-managed-agents-sandbox-mcp-tunnels]] の MCP tunnels 等）が普及するほど効いてくる。

## 詳細

### Stateless コア（最大の変更）

- **削除されるもの**: `Mcp-Session-Id` ヘッダー、プロトコルレベルのセッション、初期化ハンドシェイク（の必須性）
- **可能になること**:
  - 任意の MCP リクエストが任意のサーバーインスタンスに着地できる（sticky routing 不要）
  - 共有セッションストアがプロトコルレイヤーでは不要
  - `Mcp-Method` ヘッダーでのルーティング
  - `tools/list` レスポンスのクライアント側キャッシュ（サーバーの `ttlMs` 指定に従う）
- **動機**: リモート MCP サーバーを「普通の HTTP インフラ」（ラウンドロビン LB、CDN、serverless）で運用できるようにする。従来はセッションが状態を持つため水平スケールが面倒だった。

### MCP Apps（UI 拡張）

- サーバーが **インタラクティブな HTML UI** を配布し、ホストがサンドボックス化 iframe で描画。
- ツールが UI テンプレートを事前宣言 → ホストが prefetch / キャッシュ / セキュリティレビュー可能。
- 描画された UI は、MCP の他の部分と同じ JSON-RPC ベースプロトコルでホストと通信。
- [[2026-03-26-mcp-apps-extension]] で追った MCP Apps の流れが本仕様に正式合流する形。

### Tasks 拡張（長時間ジョブ）

- 2025-11-25 仕様で実験的コア機能として入ったが、本番運用で再設計が必要と判明し、**コア仕様から外れて拡張（extension）へ**。
- stateless モデルに合わせてライフサイクルを再構成。

### その他

- **認可**: OAuth / OpenID Connect のデプロイ実態に合わせた整合強化（Enterprise-Managed Authorization の流れとも接続）。
- **Deprecation ポリシー**: 正式な廃止プロセスが仕様に入る。プロトコルとしての「大人化」。
- **タイムライン**: RC は公開済み、最終仕様は 2026-07-28。SDK メンテナ・クライアント実装者向けに約 10 週間の検証窓。

### 破壊的変更への備え

- ChatForest の記事は「6 つの breaking changes」を挙げ、本番 MCP サーバーは 7/28 までに対応せよと警告。
- 既存サーバーは当面旧プロトコルバージョンをネゴシエートできる見込みだが、クライアント側（Claude Code 等）の対応バージョン打ち切りタイミングに注意。

## 試すなら

1. RC 本文（blog.modelcontextprotocol.io）を読み、自作 elicitation デモサーバー（`experiments/2026-03-25-mcp-elicitation`）に影響する変更（初期化ハンドシェイク、セッション）を特定する。
2. 使用中の MCP サーバー（Unity MCP / Blender MCP / Godot MCP）のリポジトリで、2026-07-28 仕様対応の Issue / PR が立っているか確認する。
3. Python / TypeScript の公式 MCP SDK の RC 対応ブランチを覗き、stateless 化でサーバー実装の boilerplate がどう変わるか比較する。
4. 7/28 の最終仕様確定後、Claude Code 側の対応バージョン（changelog）を追跡し、旧仕様サーバーがいつまで動くか把握する。

## ソース

- [The 2026-07-28 MCP Specification Release Candidate (公式ブログ)](https://blog.modelcontextprotocol.io/posts/2026-07-28-release-candidate/)
- [MCP 2026-07-28: The Stateless Release Candidate, Explained (MCP.Directory)](https://mcp.directory/blog/mcp-2026-07-28-release-candidate)
- [MCP 2026-07-28 spec: what changed, what breaks (Stacktree)](https://stacktr.ee/blog/mcp-2026-spec-changes)
- [The biggest MCP spec update ships July 28 (WorkOS)](https://workos.com/blog/mcp-2026-spec-agent-authentication)

---

## 感想・考察

<!-- /try 実行時に自動生成 -->

### 会話メモ（2026-07-08）: statelessの仕組みを深掘り

- **旧仕様がステートフルだった理由**: stdio接続はそもそも1対1プロセスなので問題にならないが、リモート用の Streamable HTTP transport では `initialize` ハンドシェイクで一度だけ negotiate した capabilities や、SSEストリームの接続先、購読状態などを**サーバー実装がプロセス内メモリに保持**していたのが原因。これがあるインスタンスにしか存在しないため、複数インスタンスへのスケールに sticky routing か共有セッションストアが必要だった。
- **capabilities の扱いの変化**: `initialize`/`initialized` ハンドシェイク自体が廃止（SEP-2575）。今後はプロトコルバージョン・クライアント情報・capabilities を**毎リクエストの `_meta` に自己申告**する形になり、サーバー側がキャッシュを持たなくても処理できるようになる。
- **認証はもともとstatelessに近かった**: MCPの認可はOAuth 2.1のリソースサーバーモデルで、アクセストークンをリクエストごとに検証する設計。`Mcp-Session-Id` には元々依存していない。今回のRCには別途OAuth/OIDC整合強化の6 SEPが含まれるが、これはstateless化の直接の帰結ではなく並行した改善。
- **JWTの仕組み**: JWTは暗号化ではなく署名付き平文（ペイロードは誰でもBase64デコードで読める）。サーバー側がやるのは「復号」ではなく、ASの公開鍵(JWKS)を使った**署名検証**。公開鍵は秘密ではないため、どのサーバーインスタンスも独立に取得・キャッシュして検証でき、これがstateless設計と噛み合う。ただし署名検証が通っても `aud` クレームで「自分宛てか」を別途チェックしないと token passthrough のリスクがある。
- **stateless化の本質**: 「通信データに処理に必要な情報が全部入っている」というより正確には「**クライアントごとの会話状態をインスタンス間で共有する必要がなくなった**」という理解が正しい。JWKSのような共有参照データはサーバー側に残るが、これは全インスタンスが独立に持てる非セッション依存のデータなので、statelessの原則を壊さない。
