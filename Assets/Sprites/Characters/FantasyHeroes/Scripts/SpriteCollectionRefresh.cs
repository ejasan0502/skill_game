#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace Assets.FantasyHeroes.Scripts
{
    /// <summary>
    /// Refresh the main sprite collection when importing new sprite bundles
    /// </summary>
    public class SpriteCollectionRefresh : AssetPostprocessor
    {
        public static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            var spriteCollection = Object.FindObjectOfType<SpriteCollection>();

            if (spriteCollection != null)
            {
                Object.FindObjectOfType<SpriteCollection>().Refresh();
            }
        }
    }
}

#endif