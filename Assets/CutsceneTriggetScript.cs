using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTriggetScript : MonoBehaviour
{
    [SerializeField] PlayableDirector director;

    private void OnTriggerEnter(Collider colider)
    {
        if(colider.tag.Equals("Player"))
        {
            director.Play();
        }
    }
}
