using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oilan;
public class OnPick : MonoBehaviour
{
    public TMiniGameLeafs sun;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if(collision.collider.tag == "Player" && sun.GetCollider()) {
        //    PlayerController.Instance.GetComponent<Animator>().SetTrigger("ali_take_star");
            
        //}
    }
}
