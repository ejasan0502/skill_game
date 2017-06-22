using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.FantasyHeroes.Scripts
{
    /// <summary>
    /// Play animation from character editor
    /// </summary>
    public class AnimationManager : MonoBehaviour
    {
        public Character Dummy;

        [Header("UI")]
        public Text ClipName;

        private readonly List<string> _animationClips = new List<string> { "Stand", "Alert", "Attack", "Walk", "Run", "Jump" };
        private int _animationClipIndex;

        /// <summary>
        /// Called automatically on app start
        /// </summary>
        public void Start()
        {
            Reset();
        }

        /// <summary>
        /// Reset animation on start and weapon type change
        /// </summary>
        public void Reset()
        {
            PlayAnimation(0);
        }

        /// <summary>
        /// Change animation and play it
        /// </summary>
        /// <param name="direction">Pass 1 or -1 value to play forward / reverse</param>
        public void PlayAnimation(int direction)
        {
            _animationClipIndex += direction;

            if (_animationClipIndex < 0)
            {
                _animationClipIndex = _animationClips.Count - 1;
            }

            if (_animationClipIndex >= _animationClips.Count)
            {
                _animationClipIndex = 0;
            }

            var clipName = _animationClips[_animationClipIndex];

            clipName = ResolveAnimatiobClip(clipName);

            Dummy.Animator.SetTrigger("LoopAll");
            Dummy.Animator.Play(clipName);
            ClipName.text = clipName;
        }

        private string ResolveAnimatiobClip(string clipName)
        {
            switch (clipName)
            {
                case "Stand":
                case "Walk":
                case "Run":
                case "Jump": return clipName;
                case "Alert":
                    switch (Dummy.WeaponType)
                    {
                        case WeaponType.Melee1H:
                        case WeaponType.Bow: return "Alert1H";
                        case WeaponType.Melee2H: return "Alert1H";
                        default: throw new NotImplementedException();
                    }
                case "Attack":
                    switch (Dummy.WeaponType)
                    {
                        case WeaponType.Melee1H: return "Attack1H";
                        case WeaponType.Melee2H: return "Attack2H";
                        case WeaponType.Bow: return "Shot";
                        default: throw new NotImplementedException();
                    }
                default: throw new NotImplementedException();
            }
        }
    }
}