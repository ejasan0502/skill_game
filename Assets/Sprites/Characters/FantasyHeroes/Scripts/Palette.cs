using UnityEngine;
using UnityEngine.UI;

namespace Assets.FantasyHeroes.Scripts
{
    /// <summary>
    /// Palette used to change sprite color
    /// </summary>
    public class Palette : MonoBehaviour
    {
        /// <summary>
        /// Add color picker listeners in runtime
        /// </summary>
        public void Awake()
        {
            foreach (var button in GetComponentsInChildren<Button>(true))
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => FindObjectOfType<CharacterEditor>().PickColor(button.colors.normalColor));
            }
        }
    }
}