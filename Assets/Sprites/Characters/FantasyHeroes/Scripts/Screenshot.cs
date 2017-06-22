using System;
using UnityEngine;

namespace Assets.FantasyHeroes.Scripts
{
    /// <summary>
    /// Take a screnshoot in play mode [S]
    /// </summary>
    public class Screenshot : MonoBehaviour
    {
        public int SuperSize = 1;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                var filename = Convert.ToString(DateTime.Now).Replace("/", "-").Replace(":", "-") + ".png";

                Application.CaptureScreenshot(filename, SuperSize);
                Debug.Log(filename);
            }
        }
    }
}