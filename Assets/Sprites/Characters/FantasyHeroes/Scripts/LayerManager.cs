using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.FantasyHeroes.Scripts
{
    /// <summary>
    /// Helps to order layers (character parts)
    /// </summary>
    public class LayerManager : MonoBehaviour
    {
        public List<SpriteRenderer> Sprites;

        /// <summary>
        /// Set layers order
        /// </summary>
        public void SetLayerOrder()
        {
            for (var i = 0; i < Sprites.Count; i++)
            {
                Sprites[i].sortingOrder = 10 * i;
            }
        }

        /// <summary>
        /// Read ordered sprite list 
        /// </summary>
        public void ReadSpriteList()
        {
            Sprites = GetComponentsInChildren<SpriteLayout>(true).Select(i => i.GetComponent<SpriteRenderer>()).OrderBy(i => i.sortingOrder).ToList();
        }
    }
}