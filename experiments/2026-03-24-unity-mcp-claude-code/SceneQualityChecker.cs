// SceneQualityChecker.cs
// Unity MCP カスタムツール: シーンの品質チェック
// 配置先: Assets/Editor/MCP/SceneQualityChecker.cs
//
// [McpTool] 属性を付けることで Unity MCP に自動登録される（手動登録不要）。
// 依存: Unity.AI.MCP.Editor アセンブリ（com.unity.ai.assistant パッケージに含まれる）

#if UNITY_EDITOR
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.AI.MCP.Editor.ToolRegistry;
using UnityEngine;
using UnityEngine.Rendering;

namespace UnityMcpExample.MCP
{
    [McpTool("scene_quality_check",
        "シーン内のGameObjectを品質チェックし、問題点（コリジョン漏れ・デフォルトマテリアル・スケール異常・ライティング不足・未命名オブジェクト）を報告する",
        EnabledByDefault = true,
        Groups = new[] { "scene" })]
    public class SceneQualityChecker : IUnityMcpTool
    {
        public Task<object> ExecuteAsync(object parameters)
        {
            var issues = new List<string>();
            var warnings = new List<string>();

            CheckMissingColliders(issues);
            CheckDefaultMaterials(issues, warnings);
            CheckAbnormalScales(issues, warnings);
            CheckLighting(issues, warnings);
            CheckUnnamedObjects(warnings);

            return Task.FromResult<object>(new
            {
                issueCount = issues.Count,
                warningCount = warnings.Count,
                issues,
                warnings,
                summary = issues.Count == 0 && warnings.Count == 0
                    ? "問題は見つかりませんでした。"
                    : $"Issues: {issues.Count}件、Warnings: {warnings.Count}件"
            });
        }

        // -------------------------------------------------------------------
        // 各チェック
        // -------------------------------------------------------------------

        static void CheckMissingColliders(List<string> issues)
        {
            var renderers = Object.FindObjectsByType<Renderer>(FindObjectsSortMode.None);
            foreach (var r in renderers)
            {
                if (r.GetComponentInParent<Canvas>() != null) continue;
                if (r is ParticleSystemRenderer) continue;
                if (r.GetComponentInParent<Collider>() == null)
                    issues.Add($"[MissingCollider] {GetPath(r.gameObject)} — Renderer はあるが Collider なし");
            }
        }

        static void CheckDefaultMaterials(List<string> issues, List<string> warnings)
        {
            var renderers = Object.FindObjectsByType<Renderer>(FindObjectsSortMode.None);
            foreach (var r in renderers)
            {
                foreach (var mat in r.sharedMaterials)
                {
                    if (mat == null)
                    {
                        issues.Add($"[NullMaterial] {GetPath(r.gameObject)} — null マテリアルスロットあり");
                        continue;
                    }
                    var n = mat.name.ToLower();
                    if (n is "default-material" or "default diffuse" or "defaultmaterial")
                        warnings.Add($"[DefaultMaterial] {GetPath(r.gameObject)} — デフォルトマテリアル使用中: {mat.name}");
                }
            }
        }

        static void CheckAbnormalScales(List<string> issues, List<string> warnings)
        {
            const float maxScale = 100f;
            foreach (var t in Object.FindObjectsByType<Transform>(FindObjectsSortMode.None))
            {
                var s = t.localScale;
                if (s.x == 0 || s.y == 0 || s.z == 0)
                    issues.Add($"[ZeroScale] {GetPath(t.gameObject)} — スケールにゼロ成分: {s}");
                else if (Mathf.Abs(s.x) > maxScale || Mathf.Abs(s.y) > maxScale || Mathf.Abs(s.z) > maxScale)
                    warnings.Add($"[LargeScale] {GetPath(t.gameObject)} — スケールが {maxScale} 超: {s}");
            }
        }

        static void CheckLighting(List<string> issues, List<string> warnings)
        {
            if (Object.FindObjectsByType<Light>(FindObjectsSortMode.None).Length == 0)
                issues.Add("[NoLight] シーン内に Light が存在しない");

            if (RenderSettings.ambientMode == AmbientMode.Flat &&
                RenderSettings.ambientLight == Color.black)
                warnings.Add("[DarkAmbient] アンビエントライトが完全に黒 (Color.black)");
        }

        static void CheckUnnamedObjects(List<string> warnings)
        {
            foreach (var t in Object.FindObjectsByType<Transform>(FindObjectsSortMode.None))
            {
                if (t.name == "GameObject")
                    warnings.Add($"[UnnamedObject] {GetPath(t.gameObject)} — デフォルト名のまま");
            }
        }

        // -------------------------------------------------------------------
        // ユーティリティ
        // -------------------------------------------------------------------

        static string GetPath(GameObject go)
        {
            var path = go.name;
            var parent = go.transform.parent;
            while (parent != null)
            {
                path = parent.name + "/" + path;
                parent = parent.parent;
            }
            return path;
        }
    }
}
#endif
