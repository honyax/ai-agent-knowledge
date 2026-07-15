---
date: 2026-07-16
status: read
relevance: A
tags: [claude-api, deprecation, fast-mode, opus-4-7, opus-4-1, api-key, rate-limit]
source_urls:
  - https://platform.claude.com/docs/en/release-notes/overview
  - https://byteiota.com/claude-api-july-2026-rate-limits-key-expiry/
  - https://platform.claude.com/docs/en/build-with-claude/fast-mode
  - https://code.claude.com/docs/en/fast-mode
experiment_dir: null
---

# Claude API 7月の期限つき変更: Opus 4.7 fast mode 廃止（7/24）、Opus 4.1 引退（8/5）、API キー有効期限

## 3行要約

- **Opus 4.7 の fast mode が 7/24 に削除**（6/25 に非推奨化済み）。以降 `claude-opus-4-7` に `speed: "fast"` を付けたリクエストは**エラーになる**（Opus 4.6 と違い標準速度へのフォールバックなし）。モデル自体は標準速度で存続。fast mode を使い続けるなら Opus 4.8 へ移行（$10/$50 per Mtok で、Opus 4.7 fast の $30/$150 より大幅に安い）。
- **Claude Opus 4.1 が非推奨化、8/5 に API から引退**。旧モデル指定が残っているコードは要移行。
- **API キーに有効期限を設定可能に**（Console）: プリセット（3時間/1日/7日/30日）、カスタム、Never から選択。7 日以上のキーは期限前に作成者へメール通知。rate limit の引き上げも同時期に実施（byteiota 報道）。

## 自分への関連度: A

[[feedback_model_preference]] のとおり自分のフォールバックモデルは **Opus 4.7**（4.8 には戻らない方針）なので、4.7 関連の機能削減は直接関係する。ただし削除されるのは fast mode（API の `speed: "fast"` / Claude Code の `/fast`）のみで、**標準速度の Opus 4.7 は存続**するため、現状の使い方への実害はなし。「4.7 の機能が削られ始めた」こと自体が、いずれ来る 4.7 本体の非推奨化の前兆として要ウォッチ。API キー有効期限は WIF ([[2026-07-01-workload-identity-federation-ga]]) と同じ「キー漏洩対策」の流れの軽量版で、個人でも使える。

## 詳細

### Opus 4.7 fast mode 削除（7/24）

- タイムライン: 6/25 非推奨化 → **7/24 削除**
- 削除後: `claude-opus-4-7` + `speed: "fast"` は**エラー**。Opus 4.6 のときのような標準速度への自動フォールバックは**ない**（breaking change）
- モデル本体（標準速度）は存続
- 移行先: Opus 4.8 fast mode（$10/$50 per Mtok。Opus 4.7 fast の $30/$150 から大幅値下げ）
- fast mode の rate limit は Opus 4.8 / 4.7 で共有プール、標準 Opus とは別枠

### Opus 4.1 引退（8/5）

- Claude Opus 4.1（2025 年のモデル）が非推奨化、**8/5 に Claude API から引退**
- 古いスクリプト・設定にモデル ID が残っている場合は要更新

### API キー有効期限（Console）

- API キー / Admin API キー作成時に有効期限を設定可能に
- プリセット: 3 時間 / 1 日 / 7 日 / 30 日、カスタム期間、または Never
- **7 日以上の寿命のキーは期限前に作成者へメール通知**
- 用途: 実験用の短命キー発行、secrets manager 未導入環境での漏洩リスク軽減。WIF ([[2026-07-01-workload-identity-federation-ga]]) ほどの構成変更なしで使える手軽な対策

### その他（同時期の Platform 変更）

- rate limit 引き上げ（byteiota 報道タイトルより）
- `agent-memory-2026-07-22` ベータヘッダー追加（メモリ一覧 API の挙動変更。Managed Agents 利用者向けで自分には当面関係薄）
- 各言語 SDK（Python 0.116.0 / TypeScript 0.110.0 等）がメモリ関連呼び出しの新ヘッダーに対応

## 試すなら

1. 自分のスクリプト・設定に `claude-opus-4-1` や `speed: "fast"` + Opus 4.7 の指定が残っていないか grep（実験コード / experiments ディレクトリも含む）。
2. Claude Console で既存 API キーを確認し、実験用キーに有効期限（30 日など）を設定し直す運用に切り替える。
3. Claude Code の `/fast` を使う習慣がある場合、対象モデルが Opus 4.8 になっている（4.7 で使っていない）ことを確認。
4. Opus 4.7 本体の非推奨化アナウンスが出ないか、今後の catch-up で継続ウォッチ（出たら Sonnet 5 常用への一本化を判断）。

## ソース

- [Claude Platform release notes (公式)](https://platform.claude.com/docs/en/release-notes/overview)
- [Claude API July 2026: Rate Limits Up, Keys Expire, Act by July 24 (byteiota)](https://byteiota.com/claude-api-july-2026-rate-limits-key-expiry/)
- [Fast mode (research preview) - Claude Platform Docs](https://platform.claude.com/docs/en/build-with-claude/fast-mode)
- [Speed up responses with fast mode - Claude Code Docs](https://code.claude.com/docs/en/fast-mode)

---

## 感想・考察

### エントリの構成整理

- 「モデルの扱いの話か」というと半分そうで半分違う。3トピック中2つがモデルのライフサイクル(Opus 4.7 fast mode 削除、Opus 4.1 引退)、1つは API 運用(キー有効期限)の話。
- Opus 4.7 fast mode 削除はモデル本体ではなくオプションの削除だが、`speed: "fast"` 指定が自動フォールバックなしでエラーになる breaking change である点に注意。Opus 4.1 引退は正真正銘のモデル引退。
- 自分への実害は現状なし(fast mode は使っておらず、標準速度の Opus 4.7 は存続)。ただし「4.7 の機能が削られ始めた」のは、通例モデル本体の非推奨化が数ヶ月以内に続く前兆パターン。4.7 本体の非推奨アナウンスが出たら Sonnet 5 常用への一本化を判断する、が最大のウォッチ事項。

### grep 確認結果(2026-07-16)

- experiments ディレクトリを `claude-opus-4-1` / `speed + fast` で grep した結果、ヒットは 2026-03-25-opus-46-api-updates の実験メモ内の説明文1行のみ。実行コードに引退対象モデルや fast mode 指定は残っておらず、対応不要。
