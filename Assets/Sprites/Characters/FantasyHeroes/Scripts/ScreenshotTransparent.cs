using System;
using UnityEngine;

namespace Assets.FantasyHeroes.Scripts
{
    /// <summary>
    /// Take a screnshoot with transparent background in play mode [S]
    /// </summary>
    public class ScreenshotTransparent : MonoBehaviour
    {
        public int Width = 1920;
        public int Height = 1280;

        public static string ScreenShotName(int width, int height)
        {
            return string.Format("{0}/Screenshot_{1}x{2}_{3}.png", Application.dataPath, width, height, DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
        }

        public void Update()
        {
            #if !UNITY_WEBPLAYER

            if (Input.GetKeyDown(KeyCode.S))
            {
                var renderTexture = new RenderTexture(Width, Height, 24);
                var texture2D = new Texture2D(Width, Height, TextureFormat.ARGB32, false);

                GetComponent<Camera>().targetTexture = renderTexture;
                GetComponent<Camera>().Render();
                RenderTexture.active = renderTexture;
                texture2D.ReadPixels(new Rect(0, 0, Width, Height), 0, 0);
                GetComponent<Camera>().targetTexture = null;
                RenderTexture.active = null;
                Destroy(renderTexture);
                
                var bytes = texture2D.EncodeToPNG();
                var filename = ScreenShotName(Width, Height);

                System.IO.File.WriteAllBytes(filename, bytes);
                Debug.Log(string.Format("Took screenshot to: {0}", filename));
            }

            #endif
        }
    }
}