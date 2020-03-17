using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Oilan
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(SortingGroup))]
    public class SortingGroupController : MonoBehaviour
    {

        public string sortingLayer = "Objects_Back";
        private string _sortingLayer = "Objects_Back";
        
        [Range(0, 1000)]
        public int sortingOrder = 0;
        private int _sortingOrder = 0;

        [SerializeField]
        private SortingGroup sortingGroup;

        private void Awake()
        {
            if (sortingGroup == null)
            {
                sortingGroup = GetComponent<SortingGroup>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (sortingLayer != _sortingLayer || sortingOrder != _sortingOrder)
            {
                _sortingLayer = sortingLayer;
                _sortingOrder = sortingOrder;

                sortingGroup.sortingLayerName = _sortingLayer;
                sortingGroup.sortingOrder = _sortingOrder;
            }

        }
    }
}
