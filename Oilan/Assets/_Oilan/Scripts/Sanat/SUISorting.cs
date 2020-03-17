using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class SUISorting : MonoBehaviour
{
    public Canvas mCanvas;

    [Range(0, 1000)]
    public int sortingOrder = 0;
    private int _sortingOrder = 0;

    private void Start()
    {
        mCanvas = gameObject.GetComponent<Canvas>();
    }

    void Update()
    {
        if (sortingOrder != _sortingOrder)
        {
            _sortingOrder = sortingOrder;
            mCanvas.sortingOrder = sortingOrder;
        }

    }

}
