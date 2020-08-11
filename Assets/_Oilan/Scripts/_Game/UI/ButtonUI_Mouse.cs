using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oilan
{
    public class ButtonUI_Mouse : MonoBehaviour
    {
        public bool isShown = true;
        public bool isWorking = false;
        public bool isPressed = false;

        public Vector2 moveVector = Vector2.right;

        public bool jumpBool = false;

        private void OnMouseDown()
        {
            isWorking = true;
            isPressed = true;
        }

        private void OnMouseUp()
        {
            isWorking = false;
            isPressed = false;
            PlayerController.Instance.move = Vector2.zero;
        }
        private void OnMouseExit()
        {
            isWorking = false;
            isPressed = false;
            PlayerController.Instance.move = Vector2.zero;
        }
        private void Start()
        {
            if (!isShown)
                gameObject.SetActive(false);
        }
        private void FixedUpdate()
        {
            if (isWorking)
            {
                if (isPressed)
                {
                    PlayerController.Instance.move = moveVector;
                    if (jumpBool)
                    {
                        PlayerController.Instance.m_Jump = true;
                        
                        isPressed = false;
                    }
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
