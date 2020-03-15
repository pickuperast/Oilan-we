using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SButterfly : MonoBehaviour
{
    public Animator m_anim;
           Vector2 startPos;
    public Vector2 endPos;
    public float speed = 0.1f;
    public SpriteRenderer part_body;
    public SpriteRenderer part_wing_front;
    public SpriteRenderer part_wing_back;
    public bool isDirectionFront = true;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        transform.position = startPos;
        m_anim.Play("Butterfly_fly");
    }

    // Update is called once per frame
    void Update()
    {
        if (speed == 0f) return;
        if (isDirectionFront)
        {
            transform.Translate(speed * Vector3.right * Time.deltaTime);
            if (transform.position.x > endPos.x)
            {
                isDirectionFront = false;
                part_body.flipX = part_wing_front.flipX = part_wing_back.flipX = true;
            }
        }
        else
        {
            transform.Translate(speed * Vector3.left * Time.deltaTime);
            if (transform.position.x < startPos.x)
            {
                isDirectionFront = true;
                part_body.flipX = part_wing_front.flipX = part_wing_back.flipX = false;
            }
        }

    }
}
