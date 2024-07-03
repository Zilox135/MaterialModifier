using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Harmony.EntityAlive
{
    [HarmonyPatch(typeof(global::EntityAlive), nameof(global::EntityAlive.Init))]
    public class EntityTintMaterial_Patch
    {
        public static readonly string PropTintColor = "TintColor";
        public static readonly string PropTintMaterial = "TintMaterial";
        public static readonly string PropTintShaderProperties = "TintShaderProperties";

        private static readonly string _multiDigitMatchPattern = @"\d+$";
        private static readonly char[] _commaSeparator = new char[] { ',' };

        public static void Postfix(global::EntityAlive __instance)
        {
            DynamicProperties props = __instance.EntityClass?.Properties;

            if (!props.Values.ContainsKey(PropTintColor) && !props.Values.dic.Keys.Any(k => k.StartsWith(PropTintMaterial)))
            {
                return;
            }

            Renderer[] renderers = __instance.RootTransform?.GetComponentsInChildren<Renderer>();

            if (renderers == null || renderers.Length == 0)
            {
                return;
            }

            List<Material> materials = GetAllMaterials(renderers);
            string[] shaderProperties = ParseShaderProperties(props, _commaSeparator);

            foreach (KeyValuePair<string, string> prop in props.Values.dic)
            {
                if (string.IsNullOrEmpty(prop.Key) || !prop.Key.StartsWith(PropTintMaterial))
                {
                    continue;
                }

                Match multiDigitMatch = Regex.Match(prop.Key, _multiDigitMatchPattern);
                int materialIndex = StringParsers.ParseSInt32(multiDigitMatch.Value);

                props.ParseColorHex($"{PropTintMaterial}{materialIndex}", out Color tintMaterialColor, Color.white);

                if (tintMaterialColor != Color.white && materialIndex < materials.Count)
                {
                    for (int i = 0; i < shaderProperties.Length; i++)
                    {
                        Material currentMaterial = materials[materialIndex];

                        if (currentMaterial != null && currentMaterial.HasColor(shaderProperties[i]))
                        {
                            currentMaterial.SetColor(shaderProperties[i], tintMaterialColor);
                        }
                    }
                }
            }

            if (!props.Values.ContainsKey(PropTintColor))
            {
                return;
            }

            props.ParseColorHex(PropTintColor, out Color tintColor, Color.white);

            foreach (Material material in materials)
            {
                for (int i = 0; i < shaderProperties.Length; i++)
                {
                    if (material != null && material.HasColor(shaderProperties[i]))
                    {
                        material.SetColor(shaderProperties[i], tintColor);
                    }
                }
            }
        }

        private static List<Material> GetAllMaterials(Renderer[] renderers)
        {
            List<Material> materials = new List<Material>();
            foreach (Material material in renderers.SelectMany(r => r.materials))
            {
                materials.Add(material);
            }

            return materials;
        }

        private static string[] ParseShaderProperties(DynamicProperties props, char[] separator)
        {
            props.ParseString(PropTintShaderProperties, out string tintShaderProperties, string.Empty);

            return tintShaderProperties.Split(separator);
        }
    }
}