using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sMainSceneAnimation : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("ali_r24",true);
    }
}
