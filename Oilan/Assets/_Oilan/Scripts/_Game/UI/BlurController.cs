using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Oilan
{
    [ExecuteAlways]
    public class BlurController : MonoBehaviour
    {
        [Range(0f, 1f)]
        public float opacity = 0f;
        private float _opacity = 0f;
        [Range(0f, 10f)]
        public float size = 0f;
        private float _size = 0f;

        public Image blurImage;

        private void Awake()
        {
            if (blurImage == null)
            {
                blurImage = GetComponent<Image>();
            }
        }
        
        // Update is called once per frame
        void Update()
        {
            
            if (size != _size || opacity != _opacity)
            {
                _size = size;
                _opacity = opacity;

                blurImage.material.SetFloat("_Size", _size);
                blurImage.material.SetFloat("_Opacity", _opacity);
                blurImage.material.SetInt("_Radius", Mathf.FloorToInt(_size * 5f));
            }


        }
    }
}