using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMakeTransparent : MonoBehaviour
{
    SpriteRenderer sp;
    public void Transparent()
    {
        sp = GetComponent<SpriteRenderer>();
        StartCoroutine(NewCoroutine());
    }
    IEnumerator NewCoroutine()
    {
        while(sp.color.a > 0.1) {

            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, sp.color.a / 1.1f);
            yield return new WaitForEndOfFrame();
        }
    }
}
