using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SUICloseErrorStepClosed : MonoBehaviour
{
    public float timeout = 3f;

    private bool timerLeft = false;
    private float elapsedTime = 0f;

    private void Start()
    {
        
    }

    void Update()
    {
        if (!timerLeft)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > timeout)
            {
                timerLeft = true;
                gameObject.SetActive(false);
            }
        }
    }
}
