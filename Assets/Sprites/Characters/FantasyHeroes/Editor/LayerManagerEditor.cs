#if UNITY_EDITOR

using Assets.FantasyHeroes.Scripts;
using UnityEditor;
using UnityEngine;

namespace Assets.FantasyHeroes.Editor
{
    /// <summary>
    /// Add action buttons to LayerManager script
    /// </summary>
    [CustomEditor(typeof(LayerManager))]
    public class LayerManagerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var script = (LayerManager) target;

            if (GUILayout.Button("Read Sprite List"))
            {
                script.ReadSpriteList();
            }

            if (GUILayout.Button("Set Layer Order"))
            {
                script.SetLayerOrder();
            }
        }
    }
}

#endif