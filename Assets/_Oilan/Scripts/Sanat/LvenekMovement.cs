using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oilan
{
    enum AliState
    {
        Right,
        Left,
        Idle
    }

    public class LvenekMovement : MonoBehaviour
    {
        private bool isBeingPushed = false;
        private bool isRequiredToResetPush = true;
        public GameObject ali;
        private PlayerController playerController;
        public float speed;
        public float closeDistance = 3.0f;

        private Rigidbody2D rb;
        private BoxCollider2D bc2D;
        private CircleCollider2D cc2D;
        private Animator animator;
        private bool isLeft;
        private AliState aliState;
        void Start()
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
            bc2D = gameObject.GetComponent<BoxCollider2D>();
            cc2D = gameObject.GetComponent<CircleCollider2D>();
            playerController = ali.GetComponent<PlayerController>();
            animator = gameObject.GetComponent<Animator>();
        }

        void LateUpdate()
        {
            Vector3 offset = ali.transform.position - transform.position;
            float sqrLen = offset.sqrMagnitude;

            if (playerController.move.x > 0)
            {
                aliState = AliState.Left;
            }
            else if (playerController.move.x < 0)
            {
                aliState = AliState.Right;
            }
            else
            {
                aliState = AliState.Idle;
            }

            if (isBeingPushed)//pushes ali in front and plays walk animation
            {
                switch (aliState)
                {
                    case AliState.Left:
                        if (sqrLen > Mathf.Pow(closeDistance, 2))
                        {
                            bc2D.enabled = true;
                            cc2D.enabled = true;
                            rb.simulated = true;
                            transform.position += new Vector3(1, 0, 0) * Time.deltaTime * speed;
                        }
                        else
                        {
                            aliState = AliState.Idle;
                        }

                        break;
                    case AliState.Right:
                        if (sqrLen > Mathf.Pow(closeDistance, 2))
                        {
                            bc2D.enabled = true;
                            cc2D.enabled = true;
                            rb.simulated = true;
                            transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * speed;
                        }
                        else
                        {
                            aliState = AliState.Idle;
                        }

                        break;
                    case AliState.Idle:
                        bc2D.enabled = false;
                        cc2D.enabled = false;
                        rb.simulated = false;
                        StartCoroutine(ResetSpeed());
                        break;
                }

                isRequiredToResetPush = true;
            }
            else
            {
                if (isRequiredToResetPush)
                {
                    bc2D.enabled = false;
                    cc2D.enabled = false;
                    rb.simulated = false;
                    transform.position += new Vector3(0, 0, 0) * Time.deltaTime * speed;
                    // StartCoroutine(ResetSpeed());                 
                    isRequiredToResetPush = false;
                }
            }

            if (rb.simulated)
            {
                animator.Play("Lvenek_walk");
                //animator.SetBool("Lvenek_walk", true);
            }
            else
            {
                animator.Play("lvenek_idle");
                //animator.SetBool("Lvenek_walk", false);
            }
        }

        public void SetEmulateWalking(bool isOn)
        {
            isBeingPushed = isOn;
        }

        private IEnumerator ResetSpeed()
        {
            yield return new WaitForEndOfFrame();
            transform.position += new Vector3(0, 0, 0) * Time.deltaTime * speed;
        }
    }
}
