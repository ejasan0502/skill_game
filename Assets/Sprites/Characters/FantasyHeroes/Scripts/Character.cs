using System.Collections.Generic;
using UnityEngine;

namespace Assets.FantasyHeroes.Scripts
{
    /// <summary>
    /// Character presentation in editor
    /// </summary>
    [ExecuteInEditMode]
    public class Character : MonoBehaviour
    {
        [Header("Body")]
        public Texture2D Head;
        public Texture2D Ears;
        public Texture2D Hair;
        public Texture2D Eyebrows;
        public Texture2D Eyes;
        public Texture2D Mouth;
        public Texture2D Beard;
        public Texture2D Body;

        [Header("Equipment")]
        public Texture2D Helmet;
        public Texture2D Weapon;
        public Texture2D Armor;
        public Texture2D Shield;
        public Texture2D Bow;
        
        [Header("Renderers")]
        public SpriteRenderer HeadRenderer;
        public SpriteRenderer EarsRenderer;
        public SpriteRenderer HairRenderer;
        public SpriteRenderer EyebrowsRenderer;
        public SpriteRenderer EyesRenderer;
        public SpriteRenderer MouthRenderer;
        public SpriteRenderer BeardRenderer;
        public SpriteRenderer[] BodyRenderers;
        public SpriteRenderer HelmetRenderer;
        public SpriteRenderer WeaponRenderer;
        public SpriteRenderer[] ArmorRenderers;
        public SpriteRenderer[] BowRenderers;
        public SpriteRenderer ShieldRenderer;

        [Header("Animation")]
        public Animator Animator;
        public WeaponType WeaponType;

        /// <summary>
        /// Called automatically when something was changed
        /// </summary>
        public void OnValidate()
        {
            if (Head == null) return;

            Initialize();
        }

        /// <summary>
        /// Initialize character renderers with selected sprites
        /// </summary>
        public void Initialize()
        {
            ReplaceSprite(HeadRenderer, Head);
            ReplaceSprite(EarsRenderer, Ears);
            ReplaceSprite(HairRenderer, Hair);
            ReplaceSprite(EyebrowsRenderer, Eyebrows);
            ReplaceSprite(EyesRenderer, Eyes);
            ReplaceSprite(MouthRenderer, Mouth);
            ReplaceSprite(BeardRenderer, Beard);
            ReplaceSprite(HelmetRenderer, Helmet);
            ReplaceSprite(WeaponRenderer, Weapon);
            ReplaceTexture(BodyRenderers, Body);
            ReplaceTexture(ArmorRenderers, Armor);
            ReplaceTexture(BowRenderers, Bow);
            ReplaceSprite(ShieldRenderer, Shield);

            WeaponRenderer.enabled = WeaponType == WeaponType.Melee1H || WeaponType == WeaponType.Melee2H;
            ShieldRenderer.enabled = WeaponType == WeaponType.Melee1H;

            foreach (var part in BowRenderers)
            {
                part.enabled = WeaponType == WeaponType.Bow;
            }
        }

        private static void ReplaceSprite(SpriteRenderer part, Texture2D texture)
        {
            if (texture == null)
            {
                part.sprite = null;
                return;
            }

            var layout = part.GetComponent<SpriteLayout>();
            var pivot = new Vector2(layout.Pivot.x / layout.Rect.width, layout.Pivot.y / layout.Rect.height);

            part.sprite = Sprite.Create(texture, layout.Rect, pivot, 100, 2, SpriteMeshType.Tight);
            part.sprite.name = "Dynamic";
        }

        private static void ReplaceTexture(IEnumerable<SpriteRenderer> parts, Texture2D texture)
        {
            foreach (var part in parts)
            {
                ReplaceSprite(part, texture);
            }
        }
    }
}