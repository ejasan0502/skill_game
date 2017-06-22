using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.FantasyHeroes.Scripts
{
    /// <summary>
    /// Defines editor's behaviour
    /// </summary>
    public class CharacterEditor : MonoBehaviour
    {
        public SpriteCollection SpriteCollection;
        public AnimationManager AnimationManager;
        public Character Dummy;

        [Header("UI")]
        public GameObject BodyTab;
        public GameObject EquipmentTab;
        public GameObject Editor;
        public GameObject CommonPalette;
        public GameObject SkinPalette;
        public Text HeadName;
        public Text EarsName;
        public Text HairName;
        public Text EyebrowsName;
        public Text EyesName;
        public Text MouthName;
        public Text BeardName;
        public Text BodyName;
        public Text HelmetName;
        public Text ArmorName;
        public Text MeleeWeapon1HName;
        public Text MeleeWeapon2HName;
        public Text ShieldName;
        public Text BowName;

        /// <summary>
        /// Called automatically on app start
        /// </summary>
        public void Start()
        {
            Refresh();
        }

        /// <summary>
        /// Change body / equipment tab
        /// </summary>
        /// <param name="tab">Pass 0 for body editor and 1 for equipment editor</param>
        public void SelectTab(int tab)
        {
            BodyTab.SetActive(tab == 0);
            EquipmentTab.SetActive(tab == 1);
        }

        /// <summary>
        /// Change head sprite
        /// </summary>
        /// <param name="direction">Pass 1 or -1 value to move next / previous</param>
        public void SetHead(int direction)
        {
            ReplaceSprite(ref Dummy.Head, SpriteCollection.Head, direction);
        }

        /// <summary>
        /// Change ear sprite
        /// </summary>
        /// <param name="direction">Pass 1 or -1 value to move next / previous</param>
        public void SetEar(int direction)
        {
            ReplaceSprite(ref Dummy.Ears, SpriteCollection.Ears, direction);
        }

        /// <summary>
        /// Change hair sprite
        /// </summary>
        /// <param name="direction">Pass 1 or -1 value to move next / previous</param>
        public void SetHair(int direction)
        {
            ReplaceSprite(ref Dummy.Hair, Dummy.Helmet == null ? SpriteCollection.Hair : SpriteCollection.HairShort, direction);
        }

        /// <summary>
        /// Change eyebrows sprite
        /// </summary>
        /// <param name="direction">Pass 1 or -1 value to move next / previous</param>
        public void SetEyebrows(int direction)
        {
            ReplaceSprite(ref Dummy.Eyebrows, SpriteCollection.Eyebrows, direction);
        }

        /// <summary>
        /// Change eyes sprite
        /// </summary>
        /// <param name="direction">Pass 1 or -1 value to move next / previous</param>
        public void SetEyes(int direction)
        {
            ReplaceSprite(ref Dummy.Eyes, SpriteCollection.Eyes, direction);
        }

        /// <summary>
        /// Change mouth sprite
        /// </summary>
        /// <param name="direction">Pass 1 or -1 value to move next / previous</param>
        public void SetMouth(int direction)
        {
            Dummy.Beard = null;
            ReplaceSprite(ref Dummy.Mouth, SpriteCollection.Mouth, direction);
        }

        /// <summary>
        /// Change beard sprite
        /// </summary>
        /// <param name="direction">Pass 1 or -1 value to move next / previous</param>
        public void SetBeard(int direction)
        {
            Dummy.Mouth = null;
            ReplaceSprite(ref Dummy.Beard, SpriteCollection.Beard, direction);
        }

        /// <summary>
        /// Change body sprite
        /// </summary>
        /// <param name="direction">Pass 1 or -1 value to move next / previous</param>
        public void SetBody(int direction)
        {
            ReplaceSprite(ref Dummy.Body, SpriteCollection.Body, direction);
        }

        /// <summary>
        /// Change helmet sprite
        /// </summary>
        /// <param name="direction">Pass 1 or -1 value to move next / previous</param>
        public void SetHelmet(int direction)
        {
            ReplaceSprite(ref Dummy.Helmet, SpriteCollection.Helmet, direction);
        }

        /// <summary>
        /// Change armor sprite
        /// </summary>
        /// <param name="direction">Pass 1 or -1 value to move next / previous</param>
        public void SetArmor(int direction)
        {
            ReplaceSprite(ref Dummy.Armor, SpriteCollection.Armor, direction);
        }

        /// <summary>
        /// Change one-handed melee weapon sprite
        /// </summary>
        /// <param name="direction">Pass 1 or -1 value to move next / previous</param>
        public void SetMeleeWeapon1H(int direction)
        {
            Dummy.WeaponType = WeaponType.Melee1H;
            ReplaceSprite(ref Dummy.Weapon, SpriteCollection.MeleeWeapon1H, direction);
            AnimationManager.Reset();
        }

        /// <summary>
        /// Change two-handed melee weapon sprite
        /// </summary>
        /// <param name="direction">Pass 1 or -1 value to move next / previous</param>
        public void SetMeleeWeapon2H(int direction)
        {
            Dummy.WeaponType = WeaponType.Melee2H;
            ReplaceSprite(ref Dummy.Weapon, SpriteCollection.MeleeWeapon2H, direction);
            AnimationManager.Reset();
        }

        /// <summary>
        /// Change bow sprite
        /// </summary>
        /// <param name="direction">Pass 1 or -1 value to move next / previous</param>
        public void SetBow(int direction)
        {
            Dummy.WeaponType = WeaponType.Bow;
            ReplaceSprite(ref Dummy.Bow, SpriteCollection.Bow, direction);
            AnimationManager.Reset();
        }

        /// <summary>
        /// Change shield sprite
        /// </summary>
        /// <param name="direction">Pass 1 or -1 value to move next / previous</param>
        public void SetShield(int direction)
        {
            Dummy.WeaponType = WeaponType.Melee1H;
            ReplaceSprite(ref Dummy.Shield, SpriteCollection.Shield, direction);
            AnimationManager.Reset();
        }

        private string _target;

        /// <summary>
        /// Open palette to change sprite color
        /// </summary>
        /// <param name="target">Pass one of the following values: Head, Ears, Body, Hair, Eyes, Mouth</param>
        public void OpenPalette(string target)
        {
            _target = target;
            Editor.SetActive(false);

            switch (_target)
            {
                case "Head":
                case "Ears":
                case "Body":
                    SkinPalette.SetActive(true);
                    break;
                case "Hair":
                case "Eyebrows":
                case "Eyes":
                case "Mouth":
                case "Beard":
                    CommonPalette.SetActive(true);
                    break;
            }
        }

        /// <summary>
        /// Close palette
        /// </summary>
        public void ClosePalette()
        {
            CommonPalette.SetActive(false);
            SkinPalette.SetActive(false);
            Editor.SetActive(true);
        }

        /// <summary>
        /// Pick color and apply to sprite
        /// </summary>
        /// <param name="color"></param>
        public void PickColor(Color color)
        {
            switch (_target)
            {
                case "Head":
                    Dummy.HeadRenderer.color = color;
                    break;
                case "Ears":
                    Dummy.EarsRenderer.color = color;
                    break;
                case "Hair":
                    Dummy.HairRenderer.color = color;
                    break;
                case "Eyebrows":
                    Dummy.EyebrowsRenderer.color = color;
                    break;
                case "Eyes":
                    Dummy.EyesRenderer.color = color;
                    break;
                case "Mouth":
                    Dummy.MouthRenderer.color = color;
                    break;
                case "Beard":
                    Dummy.BeardRenderer.color = color;
                    break;
                case "Body":
                    foreach (var part in Dummy.BodyRenderers)
                    {
                        part.color = color;
                    }
                    break;
            }
        }

        private void Refresh()
        {
            if (SpriteCollection.Hair.Contains(Dummy.Hair) && Dummy.Helmet != null)
            {
                Dummy.Hair = SpriteCollection.HairShort.SingleOrDefault(i => i.name == Dummy.Hair.name);
            }
            else if(SpriteCollection.HairShort.Contains(Dummy.Hair) && Dummy.Helmet == null)
            {
                Dummy.Hair = SpriteCollection.Hair.SingleOrDefault(i => i.name == Dummy.Hair.name);
            }

            Dummy.Initialize();
            HeadName.text = GetSpriteName(Dummy.Head);
            HairName.text = GetSpriteName(Dummy.Hair);
            EarsName.text = GetSpriteName(Dummy.Ears);
            EyebrowsName.text = GetSpriteName(Dummy.Eyebrows);
            EyesName.text = GetSpriteName(Dummy.Eyes);
            MouthName.text = GetSpriteName(Dummy.Mouth);
            BeardName.text = GetSpriteName(Dummy.Beard);
            BodyName.text = GetSpriteName(Dummy.Body);
            HelmetName.text = GetSpriteName(Dummy.Helmet);
            ArmorName.text = GetSpriteName(Dummy.Armor);
            MeleeWeapon1HName.text = Dummy.WeaponType == WeaponType.Melee1H ? GetSpriteName(Dummy.Weapon) : "-";
            MeleeWeapon2HName.text = Dummy.WeaponType == WeaponType.Melee2H ? GetSpriteName(Dummy.Weapon) : "-";
            ShieldName.text = Dummy.WeaponType == WeaponType.Melee1H ? GetSpriteName(Dummy.Shield) : "-";
            BowName.text = Dummy.WeaponType == WeaponType.Bow ? GetSpriteName(Dummy.Bow) : "-";
        }

        private void ReplaceSprite(ref Texture2D texture, List<Texture2D> collection, int direction)
        {
            var index = collection.IndexOf(texture) + direction;

            if (index == -1 || index == collection.Count)
            {
                texture = null;
            }
            else
            {
                if (index < 0)
                {
                    index = collection.Count - 1;
                }

                if (index >= collection.Count)
                {
                    index = 0;
                }

                texture = collection[index];
            }

            Refresh();
        }

        private static string GetSpriteName(Texture2D texture)
        {
            if (texture == null) return "-";
            if (texture.name.All(c => char.IsUpper(c))) return texture.name;

            return Regex.Replace(Regex.Replace(texture.name, "[A-Z]", " $0"), "([a-z])([1-9])", "$1 $2");
        }
    }
}