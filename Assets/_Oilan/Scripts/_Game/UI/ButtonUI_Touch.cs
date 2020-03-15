using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oilan
{
    public class ButtonUI_Touch : MonoBehaviour
    {
        public bool isWorking = false;
        public bool isPressed = false;

        private void Update()
        {
            if (Input.touchCount == 1)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    // Construct a ray from the current touch coordinates
                    Vector2 coord = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                    RaycastHit2D hit = Physics2D.Raycast(coord, Vector3.forward, 10f);
                    // Create a particle if hit
                    if (hit.collider == GetComponent<Collider2D>())
                    {
                        isWorking = true;
                        isPressed = true;
                    }
                }
                
            }
            else
            {
                isPressed = false;
            }

            if (isWorking)
            {
                if (isPressed)
                {
                    PlayerController.Instance.move = new Vector2(1f, 0f);
                }
                else
                {
                    isWorking = false;
                    PlayerController.Instance.move = Vector2.zero;
                }
            }


        }

        private void OnEnable()
        {
            isWorking = false;
            isPressed = false;
            if (PlayerController.Instance != null)
            {
                PlayerController.Instance.move = Vector2.zero;
            }
        }

        private void OnDisable()
        {
            isWorking = false;
            isPressed = false;
            PlayerController.Instance.move = Vector2.zero;
        }

    }
}
