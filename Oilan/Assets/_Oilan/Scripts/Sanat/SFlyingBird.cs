using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFlyingBird : MonoBehaviour
{
    public Animator m_anim;
    public Vector2 startPos;
    public Vector2 endPos;
    public float speed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = startPos;
        m_anim.Play("Bird_Fly");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Vector3.right * Time.deltaTime);
        if (transform.position.x > endPos.x)
        {
            transform.position = startPos;
        }
    }
}
