using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LvenekMovement : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * speed;
    }
}
