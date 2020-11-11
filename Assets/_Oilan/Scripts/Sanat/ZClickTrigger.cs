using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZClickTrigger : MonoBehaviour
{

    UnityEvent uEvent;

    private void OnMouseDown()
    {
        uEvent.Invoke();
    }
}
