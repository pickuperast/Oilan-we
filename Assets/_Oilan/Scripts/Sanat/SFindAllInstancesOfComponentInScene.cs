using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFindAllInstancesOfComponentInScene : MonoBehaviour
{
    public AudioSource[] Refs;
    // Start is called before the first frame update
    void Start()
    {
        Refs = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
    }
}
