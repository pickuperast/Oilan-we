using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SArrowManagement : MonoBehaviour
{

    public GameObject Arrow;
    public float WhenShowArrow = 3f;//after WhenShowArrow secs we will see blinking arrow

    private bool timerLeft = false;
    private float elapsedTime = 0f;

    private void Start()
    {
        Arrow.SetActive(false);
    }
    void Update()
    {
        if (!timerLeft)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > WhenShowArrow)
            {
                timerLeft = true;
                Arrow.SetActive(true);
            }
        }
    }
}
