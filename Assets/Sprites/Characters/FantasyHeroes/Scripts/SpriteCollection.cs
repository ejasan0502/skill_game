using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Assets.FantasyHeroes.Scripts
{
    /// <summary>
    /// Collect sprites from specified path
    /// </summary>
    [ExecuteInEditMode]
    public class SpriteCollection : MonoBehaviour
    {
        public Object SpritePath;

        [Header("Body Parts")]
        public List<Texture2D> Head;
        public List<Texture2D> Ears;
        public List<Texture2D> Hair;
        public List<Texture2D> HairShort;
        public List<Texture2D> Eyebrows;
        public List<Texture2D> Eyes;
        public List<Texture2D> Mouth;
        public List<Texture2D> Beard;
        public List<Texture2D> Body;

        [Header("Equipment")]
        public List<Texture2D> Helmet;
        public List<Texture2D> Armor;
        public List<Texture2D> MeleeWeapon1H;
        public List<Texture2D> MeleeWeapon2H;
        public List<Texture2D> Shield;
        public List<Texture2D> Bow;

        #if UNITY_EDITOR

        /// <summary>
        /// Called automatically when something was changed
        /// </summary>
        public void OnValidate()
        {
            Refresh();
        }

        /// <summary>
        /// Read all sprites from specified path again
        /// </summary>
        public void Refresh()
        {
            var path = UnityEditor.AssetDatabase.GetAssetPath(SpritePath);

            Head = LoadSprites(path + "/BodyParts/Head");
            Ears = LoadSprites(path + "/BodyParts/Ears");
            Hair = LoadSprites(path + "/BodyParts/Hair");
            HairShort = LoadSprites(path + "/BodyParts/HairShort");
            Eyebrows = LoadSprites(path + "/BodyParts/Eyebrows");
            Eyes = LoadSprites(path + "/BodyParts/Eyes");
            Mouth = LoadSprites(path + "/BodyParts/Mouth");
            Beard = LoadSprites(path + "/BodyParts/Beard");
            Body = LoadSprites(path + "/BodyParts/Body");
            Helmet = LoadSprites(path + "/Equipment/Helmet");
            Armor = LoadSprites(path + "/Equipment/Armor");
            MeleeWeapon1H = LoadSprites(path + "/Equipment/MeleeWeapon1H");
            MeleeWeapon2H = LoadSprites(path + "/Equipment/MeleeWeapon2H");
            Shield = LoadSprites(path + "/Equipment/Shield");
            Bow = LoadSprites(path + "/Equipment/Bow");
        }

        private static List<Texture2D> LoadSprites(string path)
        {
            return Directory.GetFiles(path, "*.png", SearchOption.AllDirectories).Select(i => UnityEditor.AssetDatabase.LoadAssetAtPath(i, typeof(Texture2D))).Cast<Texture2D>().ToList();
        }

        #endif
    }
}