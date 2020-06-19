using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sCharacterCutSceneForTimeline : MonoBehaviour
{
    public GameObject Hero;

    public void MoveToHero() {
        transform.position = Hero.transform.position;
    }

    public void Destroy_Obj() {
        Destroy(gameObject);
    }
        
}
