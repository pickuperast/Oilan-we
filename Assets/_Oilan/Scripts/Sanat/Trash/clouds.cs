using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clouds : MonoBehaviour
{
    public GameObject obj;
    public bool isDrop;
    void Update()
    {
        if (obj != null && isDrop) {
            float step = 0.5f * Time.deltaTime; // calculate distance to move
            obj.transform.Translate( -(obj.transform.position - transform.position) * Time.deltaTime * 0.5f);
            Debug.Log(Vector3.Distance(obj.transform.position, Vector3.zero));
            if(Vector3.Distance(obj.transform.position, Vector3.zero) <= 2.5f) {
                obj.GetComponent<SpriteRenderer>().sortingLayerName = "Objects_back";
                obj.GetComponent<SpriteRenderer>().sortingOrder = 10;
                obj.GetComponent<Rigidbody2D>().gravityScale = 1f;
            }
        }
    }
}
