using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oilan
{
    [ExecuteInEditMode]
    public class SpriteSortingController : MonoBehaviour
    {

        public string sortingLayer = "Objects_Back";
        private string _sortingLayer = "Objects_Back";
        
        [Range(0, 1000)]
        public int sortingOrder = 0;
        private int _sortingOrder = 0;

        public SpriteRenderer sRenderer;

        private void Awake()
        {
            if (sRenderer == null)
            {
                sRenderer = GetComponent<SpriteRenderer>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (sortingLayer != _sortingLayer || sortingOrder != _sortingOrder)
            {
                _sortingLayer = sortingLayer;
                _sortingOrder = sortingOrder;

                sRenderer.sortingLayerName = _sortingLayer;
                sRenderer.sortingOrder = _sortingOrder;
            }

        }
    }
}
