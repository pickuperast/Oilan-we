using Oilan;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZObjectEvent : MonoBehaviour
{
    public DragDropObject dropObject;
    void Start()
    {
        dropObject.OnPlaced += DropObject;
    }

    // Update is called once per frame
    void DropObject()
    {
        StartCoroutine(DropObjectCoroutine());
    }

    private IEnumerator DropObjectCoroutine()
    {
        GameplayTimelineManager.Instance.PlayNextTimeline();
        yield return null;
    }
}
