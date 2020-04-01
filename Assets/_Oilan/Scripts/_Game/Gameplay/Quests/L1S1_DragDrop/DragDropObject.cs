using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Oilan
{
    [RequireComponent(typeof(Collider2D), typeof(SortingGroup))]
    public class DragDropObject : MonoBehaviour
    {
        public Animator animator;
        private SortingGroup sortingGroup;
        public int itemID = 0;
        public string id;
        public GameObject WhatToDestroy;
        [SerializeField]
        private float fingerCompensation = 3f;

        public bool attractionIsActive = false;
        public Vector3 attractionPoint = Vector3.zero;
        public DragDropTarget attractionTarget = null;

        [SerializeField]
        private bool isPressed = false;
        [SerializeField]
        private float isPressedTime = 0f;
        [SerializeField]
        private bool isDragging = false;

        [SerializeField]
        private string originalSortingLayerName;

        private Vector3 initialPosition;

        private Vector2 initialMousePosition;

        public float minDragDistance = 0.15f; // minimal distance to decide it's a drag
        public float minDragTime = 1f; // minimal time to decide it's a drag
        public float releaseTime;

        public Action OnPlaced;
        public Action OnWrongPlaced;
        public Action OnDestroyed;

        private void Awake()
        {
            if (!(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer))
            {
                fingerCompensation = 0f;
            }

            initialPosition = transform.position;

            if (animator == null)
            {
                animator = GetComponent<Animator>();
            }

            if (sortingGroup == null)
            {
                sortingGroup = GetComponent<SortingGroup>();
            }

            originalSortingLayerName = sortingGroup.sortingLayerName;
        }

        // PRIVATE
        private void Update()
        {

            if (isPressed && GameManager.Instance.GetCurrentGameState() == GameState.GAMEPLAY)
            {
                isPressedTime += Time.deltaTime;

                Vector2 mousePos = GameplayManager.Instance.gameplayCamera.ScreenToWorldPoint(Input.mousePosition);

                Rect screenRect = GameplayManager.Instance.GetScreenRect();

                // mouse relative to screen height position [0..1]
                float screenHeightPos = (mousePos.y - screenRect.yMin) / screenRect.height;

                if (!isDragging)
                {
                    if ((Vector2.Distance(mousePos, initialMousePosition) > minDragDistance)
                        ||
                        (isPressedTime > minDragTime))
                    {
                        isDragging = true;

                        DragModeOn();

                        UpdateSorting();

                    }
                }

                if (isDragging)
                {
                    if (animator != null)
                    {
                        animator.enabled = false;
                    }

                    Vector3 desirePosition = mousePos + (fingerCompensation * Mathf.Sqrt(screenHeightPos)) * Vector2.up;


                    CanPlaceObject();

                    if (attractionIsActive && id == attractionTarget.id)
                    {
                        transform.position = Vector3.Lerp(desirePosition,
                                                          attractionPoint,
                                                          1 - (Vector3.Distance(desirePosition, attractionPoint))
                                                          );

                    }
                    else
                    {
                        transform.position = desirePosition;
                    }

                }

            }

        }



        private void OnMouseDown()
        {
            if (GameManager.Instance.GetCurrentGameState() == GameState.GAMEPLAY)
            {
                isPressed = true;
                isPressedTime = 0f;
                isDragging = false;

                initialMousePosition = GameplayManager.Instance.gameplayCamera.ScreenToWorldPoint(Input.mousePosition);
            }
            
            ResetSorting();
        }

        private void OnMouseUp()
        {
            if (isPressed && GameManager.Instance.GetCurrentGameState() == GameState.GAMEPLAY)
            {
                isPressed = false;
                isPressedTime = 0f;

                if (isDragging)
                {
                    TryPlaceObject();
                }

                isDragging = false;


            }
            else if (isPressed && GameManager.Instance.GetCurrentGameState() != GameState.GAMEPLAY)
            {
                isPressed = false;
                isPressedTime = 0f;
                isDragging = false;
            }

            UpdateSorting();

            if (animator != null)
            {
                animator.enabled = true;
            }
        }

        public void ResetSorting()
        {
            originalSortingLayerName = sortingGroup.sortingLayerName;

            UpdateSorting();
        }
        
        public void UpdateSorting()
        {
            if (isDragging)
            {
                sortingGroup.sortingLayerName = "Front_Front";
            }
            else
            {
                sortingGroup.sortingLayerName = originalSortingLayerName;
            }

        }

        private void DragModeOn()
        {
            //transform.localScale = new Vector3(gridScale,
            //                                   gridScale,
            //                                   1f);
        }


        // Placing
        private void CanPlaceObject()
        {
            bool canPlace = false;
            DragDropTarget ddTarget = null;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward, 1f, ~((1 << 10) | (1 << 0)));

            if (hit.collider != null)
            {
                ddTarget = hit.collider.GetComponent<DragDropTarget>();

                if (ddTarget != null)
                {
                    if (!ddTarget.isOccupied)
                    {
                        canPlace = true;
                    }

                }

            }

            if (canPlace && ddTarget != null)
            {
                attractionPoint = ddTarget.transform.position;
                attractionTarget = ddTarget;
                attractionIsActive = true;
            }
            else
            {
                attractionPoint = Vector3.zero;
                attractionTarget = null;
                attractionIsActive = false;
            }
        }

        private void TryPlaceObject()
        {
            if (attractionIsActive)
            {
                bool canPlace = !attractionTarget.checkID || (attractionTarget.checkID && attractionTarget.id == id);

                if (attractionIsActive && canPlace)
                {
                    PlaceObject();
                }
                else
                {
                    WrongPlaceObject();
                }
            }
            else
            {
                ReturnToInitialPosition();
            }

        }

        private void PlaceObject()
        {
            attractionTarget.isOccupied = true;
            
            if (OnPlaced != null)
            {
                OnPlaced();
            }

            AudioManager.Instance.PlaySound("Zv-3 (Характерный звук - издается в случае правильного ответа )");
            if (id == "Ali")
            {
                Character_Ali.Instance.m_items[itemID].isEquipped = true;
                Character_Ali.Instance.CheckRequiredItems();
                if (itemID != 2)
                {
                    Debug.Log("Playing show hand + item animation. ItemID = " + itemID);
                    //Character_Ali.Instance.SetAnimatorShowItemTalk(true);
                }
                else//backpack
                {
                    Character_Ali.Instance.SetAnimatorPointOnChest(false);
                    Character_Ali.Instance.SetAnimatorTalkTrigger(false);
                    Character_Ali.Instance.SetAnimatorIdleTrigger();
                }
            }
            transform.DOMove(attractionPoint, 0.1f);
            if (WhatToDestroy != null) { 
                WhatToDestroy.SetActive(false);
            }
            else
            {
                gameObject.SetActive(false);
            }

        }

        private void WrongPlaceObject()
        {

            if (OnWrongPlaced != null)
            {
                OnWrongPlaced();
            }

            AudioManager.Instance.PlaySound("Zv-2 (Характерный звук - издается в случае неправильного ответа)");

            ReturnToInitialPosition();
        }

        // PUBLIC
        public void ReturnToInitialPosition()
        {
            transform.DOMove(initialPosition, 0.5f);
        }

        public bool IsPressedOrDragging()
        {
            return (isPressed || isDragging);
        }



    }

}

