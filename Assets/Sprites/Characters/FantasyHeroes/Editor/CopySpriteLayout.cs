using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Assets.FantasyHeroes.Editor
{
    /// <summary>
    /// Copy rects and pivots for single and multiple sprites
    /// </summary>
    public class CopySpriteLayout : EditorWindow
    {
        public Object CopyFrom;
        public Object CopyTo;

        [MenuItem("Window/Copy Sprite Layout")]
        public static void Init()
        {
            var window = (CopySpriteLayout) GetWindow(typeof (CopySpriteLayout));

            window.Show();
        }

        public void OnGUI()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Copy from:", EditorStyles.boldLabel);
            CopyFrom = EditorGUILayout.ObjectField(CopyFrom, typeof (Texture2D), false, GUILayout.Width(220));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Copy to:", EditorStyles.boldLabel);
            CopyTo = EditorGUILayout.ObjectField(CopyTo, typeof (Texture2D), false, GUILayout.Width(220));
            GUILayout.EndHorizontal();
            GUILayout.Space(25f);

            if (GUILayout.Button("Copy pivots and slices"))
            {
                CopyPivotsAndSlices();
            }
        }

        private void CopyPivotsAndSlices()
        {
            if (!CopyFrom || !CopyTo)
            {
                Debug.Log("Missing one object");
                return;
            }

            if (CopyFrom.GetType() != typeof (Texture2D) || CopyTo.GetType() != typeof (Texture2D))
            {
                Debug.Log("Cant convert from: " + CopyFrom.GetType() + "to: " + CopyTo.GetType() + ". Needs two Texture2D objects!");
                return;
            }

            var copyFromPath = AssetDatabase.GetAssetPath(CopyFrom);
            var ti1 = (TextureImporter) AssetImporter.GetAtPath(copyFromPath);

            ti1.isReadable = true;

            var copyToPath = AssetDatabase.GetAssetPath(CopyTo);
            var ti2 = (TextureImporter) AssetImporter.GetAtPath(copyToPath);

            ti2.isReadable = true;
            ti2.spriteImportMode = SpriteImportMode.Multiple;

            Debug.Log("Amount of slices found: " + ti1.spritesheet.Length);

            var ratio = (CopyFrom as Texture).width / (CopyTo as Texture).width;

            Debug.Log("Ratio = " + ratio);

            var spritesheet = ti1.spritesheet.ToArray();

            for (var i = 0; i < spritesheet.Length; i++)
            {
                var meta = spritesheet[i];

                meta.rect.min /= ratio;
                meta.rect.max /= ratio;

                spritesheet[i] = meta;
            }

            ti2.spritesheet = spritesheet;

            AssetDatabase.ImportAsset(copyToPath, ImportAssetOptions.ForceUpdate);
        }
    }
}