#if UNITY_EDITOR

using Assets.FantasyHeroes.Scripts;
using UnityEditor;
using UnityEngine;

namespace Assets.FantasyHeroes.Editor
{
    /// <summary>
    /// Add "Refresh" button to SpriteCollection script
    /// </summary>
    [CustomEditor(typeof(SpriteCollection))]
    public class SpriteCollectionEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var script = (SpriteCollection) target;

            if (GUILayout.Button("Refresh"))
            {
                script.Refresh();
            }
        }
    }
}

#endif