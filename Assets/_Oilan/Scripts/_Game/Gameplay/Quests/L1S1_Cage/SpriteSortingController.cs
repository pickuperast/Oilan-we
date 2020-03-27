using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oilan
{
    [ExecuteInEditMode]
    public class SpriteSortingController : MonoBehaviour
    {

        public string sortingLayer = "Objects_Back";

        public string[] arr_layers = new string[] {"Objects_Back", "Actors", "Objects_Front", "Actors_Front", "Front", "Front_Front", "UI"};
        [Range(0, 1000)]
        public int sortingOrder = 0;
        private int _sortingOrder = 0;

        [Range(0, 5)]
        public int NewsortingLayer = 0;
        private int _NewsortingLayer = 0;

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
            if (NewsortingLayer != _NewsortingLayer || sortingOrder != _sortingOrder)
            {
                _NewsortingLayer = NewsortingLayer;
                _sortingOrder = sortingOrder;

                sRenderer.sortingLayerName = arr_layers[NewsortingLayer];
                sRenderer.sortingOrder = _sortingOrder;
                sortingLayer = arr_layers[NewsortingLayer];
            }

        }
    }
}
