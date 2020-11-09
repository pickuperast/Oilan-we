using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;
    public Animator m_Anim;

    private void Start()
    {
        StartCoroutine(testCor());
    }

    IEnumerator testCor()
    {
        yield return new WaitForSeconds(4f);
        m_Anim.SetTrigger("zoom_for_minigame_clouds");
    }
}
