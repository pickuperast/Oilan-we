using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Oilan
{

    //[ExecuteInEditMode]
    public class GameplayCanvasFullscreenEffects : MonoBehaviour
    {
        public static GameplayCanvasFullscreenEffects Instance;

        // FadeInOut
        [Header("Fade In Out")]
        public Image fadeInOutImage;
        [Range(0f, 1f)]
        public float fadeInAlpha = 0f;
        [Range(0f, 1f)]
        public float fadeOutAlpha = 1f;
        public float fadeInOutDuration = 0.75f;

        private void Awake()
        {
            Instance = this;
            SetFadeInOutImageEnabled(true);
            fadeInOutImage.ColorSetAlpha(1f);
        }

        // FADE IN OUT

        private void SetFadeInOutImageEnabled(bool isEnabled = true)
        {
            fadeInOutImage.enabled = isEnabled;
        }

        public void FadeIn()
        {
            SetFadeInOutImageEnabled(true);
            DOTween.ToAlpha(() => fadeInOutImage.color, x => fadeInOutImage.color = x, fadeInAlpha, fadeInOutDuration).OnComplete(() => SetFadeInOutImageEnabled(false));
        }

        public void FadeOut()
        {
            SetFadeInOutImageEnabled(true);
            DOTween.ToAlpha(() => fadeInOutImage.color, x => fadeInOutImage.color = x, fadeOutAlpha, fadeInOutDuration).OnComplete(() => SetFadeInOutImageEnabled(true));
        }

    }
}