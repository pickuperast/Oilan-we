using Oilan;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oilan
{
    [Serializable]
    public class EquippableItem//���� ������� �� ����� ���� ����� ���� �����
    {
        public string name;
        public Sprite m_sprite;
        public GameObject Backpack;
        public bool isEquipped;
        //===start=== Only one possible
        public bool isForLeftHand;
        public bool isForBack;
        public bool isForFoot;
        //===end===
    };

    public class Character_Ali : MonoBehaviour
    {
        public static Character_Ali Instance;

        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private SpriteRenderer m_SpriteRenderer;
        
        public bool m_FacingRight = true;  // For determining which way the player is currently facing.

        [Header("CUTSCENES")]
        public GameObject characterCutscene;

        [Header("ANIMATION BEHAVIOUR")]
        public float backpack_Value = 0f;
        private float _backpack_Value = 0f;
        public float equipment_Value = 0f;
        private float _equipment_Value = 0f;
        public float hold_Value = 0f;
        private float _hold_Value = 0f;


        [Header("Map initialization")]
        public GameObject Chest;


        [Header("EQUIPMENT")]
        public List<EquippableItem> m_items;
        public SpriteRenderer LeftHand;
        public SpriteRenderer Back;
        public SpriteRenderer FootLeft;
        public SpriteRenderer FootRight;

        public bool isBeingPushed = false;
        private bool isRequiredToResetPush = true;
        /*
        public Vector2 StartPosition;
        public float timer;
        bool positioned;
        
        void MoveToStart() {
            transform.position = new Vector3(StartPosition.x, StartPosition.y, 1f);
        }
        
        private void Update()
        {
            if (!positioned) {
                if (gameObject.GetComponent<PlayerController>().enabled)
                    gameObject.GetComponent<PlayerController>().enabled = false;
                if (timer > 0f) { 
                    timer -= Time.deltaTime;
                } else {
                    MoveToStart();
                    gameObject.GetComponent<PlayerController>().enabled = true;
                }
            }
        }
        */
        [ExecuteAlways]
        public void EquipItem(int ItemIdinList)
        {
            m_items[ItemIdinList].isEquipped = true;
            CheckRequiredItems();
            Debug.Log("Called EquipItem("+ ItemIdinList+")");
        }
        public void UnEquipItem(int ItemIdinList)
        {
            m_items[ItemIdinList].isEquipped = false;
            CheckRequiredItems();
            Debug.Log("Called UnEquipItem(" + ItemIdinList + ")");
            if (ItemIdinList != 2)
            {
                Character_Ali.Instance.SetAnimatorShowItemTalk(false);
            }
        }

        private void LateUpdate()
        {
            CheckRequiredItems();
        }

        public void CheckRequiredItems()//������ �������� �� ����� m_items � ����������� ���������
        {
            //check left hand items
            foreach (var item in m_items) {
                Debug.Log("Checking item: " + item.name);
                if (item.isForLeftHand && item.isEquipped) {
                    LeftHand.sprite = item.m_sprite;
                    Debug.Log("showing left hand item. Sprite: " + LeftHand.sprite);
                    break;
                }
            }

            //check backpack
            foreach (var item in m_items) {
                if (item.isForBack) {
                    item.Backpack.SetActive(item.isEquipped);//Back of character has only one item - blue backpack, so i will just turn it on when need
                }
            }

            //check foots
            foreach (var item in m_items) {
                if (item.isForFoot && item.isEquipped) {
                    FootLeft.sprite = item.m_sprite;
                    FootRight.sprite = item.m_sprite;
                }
            }
            Debug.Log("hand item sprite: " + LeftHand.sprite);
        }
        
        private void Awake()
        {
            Instance = this;
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            m_Grounded = false;
            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++) {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
            }
            m_Anim.SetBool("Ground", m_Grounded);


            if (isBeingPushed)//pushes ali in front and plays walk animation
            {
                gameObject.GetComponent<PlayerController>().move = new Vector2(1f, 0f);
                isRequiredToResetPush = true;
            }
            else
            {
                if (isRequiredToResetPush)
                {
                    StartCoroutine(ResetSpeed());
                    isRequiredToResetPush = false;
                }
            }


            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
            if (m_Grounded) {// && Mathf.Abs(m_Rigidbody2D.velocity.y) < 0.1f)
                m_Anim.SetBool("Jump", false);
            }
                //Debug.Log("hand item sprite: " + LeftHand.sprite);
            //CheckRequiredItems();
            /*
            if (_backpack_Value != backpack_Value
                || _equipment_Value != equipment_Value
                || _hold_Value != hold_Value)
            {
                _backpack_Value = backpack_Value;
                _equipment_Value = equipment_Value;
                _hold_Value = hold_Value;

                //m_Anim.SetFloat("Backpack_Value", _backpack_Value);
                //m_Anim.SetFloat("Equipment_Value", _equipment_Value);
                //m_Anim.SetFloat("Hold_Value", _hold_Value);
            }*/

        }

        public void SetAnimatorTrigger(string triggerName)
        {
            m_Anim.SetTrigger(triggerName);
        }

        public void SetAnimatorBool(string boolName, bool newValue = false)
        {
            m_Anim.SetBool(boolName, newValue);
        }
        public void SetEmulateWalking(bool isOn)
        {
            isBeingPushed = isOn;
        }
        public void SetEyesFront(bool isOn)
        {
            SetAnimatorBool("ali_eyes_front", isOn);
        }
        public void SetAnimatorAli_r83_Bool(bool isOn)
        {
            SetAnimatorBool("ali_r83_ukazyvaet_vpered", isOn);
        }
            public void SetAnimatorAli_r48_Bool(bool isOn)
        {
            SetAnimatorBool("ali_r48_point_in_front", isOn);
        }
        public void SetAnimatorAli_r19_Trigger()
        {
            SetAnimatorTrigger("ali_r19_plate_from_backpack");
        }
        public void SetAnimatorAli_r20_Trigger()
        {
            SetAnimatorTrigger("ali_r20_zevaet");
        }
        public void SetAnimatorAli_r37_Trigger()
        {
            SetAnimatorTrigger("ali_r37_applause");
        }
        public void SetAnimatorAli_r45_Trigger()
        {
            SetAnimatorTrigger("ali_r45_talk_look_in_front");
        }
        public void SetAnimatorAli_r47_Trigger()
        {
            SetAnimatorTrigger("ali_r47_surprise_no_smile");
        }
        public void SetAnimatorAli_r59_Trigger()
        {
            SetAnimatorTrigger("ali_r59_put_plate_in_backpack");
        }
        public void SetAnimatorAli_r76_Trigger()
        {
            SetAnimatorTrigger("ali_r76_happy_talk");
        }
        public void SetAnimatorAli_r80_Trigger()
        {
            SetAnimatorTrigger("ali_r80_look_down");
        }
        public void SetAnimatorTalkTrigger(bool isOn){
            SetAnimatorBool("Talk",isOn);
        }
        public void SetAnimatorPointOnChest(bool isOn)
        {
            SetAnimatorBool("ali_b14_point_on_chest",isOn);
        }
        public void SetAnimatorIdleTrigger(){
            SetAnimatorTrigger("Idle");
        }
        public void SetAnimatorShowItemTalk(bool isOn){
            SetAnimatorBool("Lift_up_left_hand", isOn);
        }

        public void SetAnimator_r24_MashetRukoi()
        {
            SetAnimatorTrigger("ali_r24_mashet_levoi_rukoi");
        }

        public void SetAnimatorAli_r40_look_aroundTrigger()
        {
            SetAnimatorTrigger("ali_r40_look_around");
        }
        public void SetAnimatorAli_r58_Trigger(bool isOn)
        {
            SetAnimatorBool("ali_r58_walk", isOn);
        }

        public void SetAnimatorAli_r46_stay_Trigger(bool isOn)
        {
            SetAnimatorBool("ali_r46_stay", isOn);
        }

        

        public void SetSpriteVisibility(bool newVal)
        {
            //Chest.SetActive(newVal);
            //GetComponent<SpriteRenderer>().enabled = newVal;
        }
        
        public void SetCutsceneSpriteVisibility(bool newVal)
        {
            characterCutscene.SetActive(newVal);
        }
        
        public void ResetCharacterCutscenePosition()
        {
            characterCutscene.transform.position = transform.position;
        }
        
        public void SetPositionToCutsceneSprite()
        {
            transform.position = characterCutscene.transform.position;
        }
        
        public void SetCutsceneMode(bool isCutscene)
        {
            StartCoroutine(SetCutsceneModeCoroutine(isCutscene));
        }
        
        private IEnumerator SetCutsceneModeCoroutine(bool isCutscene)
        {
            if (isCutscene)
            {
               // SetCutsceneSpriteVisibility(true);

                //m_Rigidbody2D.simulated = false;
                //m_Rigidbody2D.isKinematic = true;
                Chest.SetActive(false);
                //m_SpriteRenderer.enabled = false;
            }
            else
            {
                //SetCutsceneSpriteVisibility(false);
               // ResetCharacterCutscenePosition();

                //m_Anim.enabled = false;
                
                ResetPosition();
                //m_Rigidbody2D.isKinematic = false;
                //m_Rigidbody2D.simulated = true;

                yield return new WaitForEndOfFrame();

                //m_Anim.enabled = true;
                
                m_Anim.SetTrigger("Idle");
                Chest.SetActive(true);
                //m_SpriteRenderer.enabled = true;
            }

            GameplayManager.Instance.TurnPlayerControlsOnOff(!isCutscene);
            GameplayManager.Instance.TurnAutoCamOnOff(!isCutscene);

            yield return null;
        }

        public void ResetPosition()
        {
            transform.position = GameplayManager.Instance.characterStartAnchor.transform.position;

            m_Rigidbody2D.position = transform.position;
            m_Rigidbody2D.velocity = Vector3.zero;
            m_Rigidbody2D.angularVelocity = 0f;
            gameObject.GetComponent<PlayerController>().move = new Vector2(0.01f, 0f);
            StartCoroutine(ResetSpeed());
        }

        private IEnumerator ResetSpeed()
        {
            yield return new WaitForEndOfFrame();
            gameObject.GetComponent<PlayerController>().move = new Vector2(0f, 0f);
        }

        public void Move(float move, bool crouch, bool jump)
        {
            //// If crouching, check to see if the character can stand up
            //if (!crouch && m_Anim.GetBool("Crouch"))
            //{
            //    // If the character has a ceiling preventing them from standing up, keep them crouching
            //    if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            //    {
            //        crouch = true;
            //    }
            //}

            //// Set whether or not the character is crouching in the animator
            //m_Anim.SetBool("Crouch", crouch);

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move*m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                //Debug.Log(new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y));
                m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
            if (m_Grounded && jump && m_Anim.GetBool("Ground"))
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
