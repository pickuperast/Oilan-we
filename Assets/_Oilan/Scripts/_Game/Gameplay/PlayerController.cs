using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oilan
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;

        private PlayerControls controls;
        public List<GameObject> UIControlButtons = new List<GameObject>();
        public Vector2 move;
        public bool m_Jump;
        public bool canJump;

        [Header("if player is AFK")]
        public float idleTimer = 0f;
        public bool idlePause = false;
        public bool isFirstAFKWasPlayed = false;
        public float FirstAFKMax = 10f;
        public float SecondAFKMax = 20f;
        public string AudioIdleFirst = "au_t_15";
        public float AudioIdleFirstLength = 2.743f;
        public List<string> AudioIdleSecondAFK;
        public List<float> AudioIdleSecondAFKLength;
        private Character_Ali m_Character;


        public void PauseAFK_Routine(bool isOn)
        {
            idlePause = isOn;
        }

        private void Update()
        {
            if (!idlePause)
            {
                idleTimer += Time.deltaTime;
            }
            if (idleTimer > FirstAFKMax && !isFirstAFKWasPlayed)//"Nu chto stoim, vpered spasat' mir play audio au_t_15
            {
                isFirstAFKWasPlayed = true;
                AudioManager.Instance.PlaySound(AudioIdleFirst, false, true);
                Character_Ali.Instance.SetAnimatorAli_r83_Bool(true);
                StartCoroutine(CoroutineStopR83());
            }
            if (idleTimer > SecondAFKMax)// every 60 sec play audio au_t_ZEVOK
            {             
                int random = Random.Range(0, AudioIdleSecondAFK.Capacity);
                StartCoroutine(CoroutineStopZEVOK(random));
                AudioManager.Instance.PlaySound(AudioIdleSecondAFK[random], false, false);//play 1 of 3 zevok audios
                Character_Ali.Instance.SetAnimatorAli_r20_Trigger();
                idleTimer = 0f;
            }
            if (move != Vector2.zero)//Reset
            {
                idleTimer = 0f;
                isFirstAFKWasPlayed = false;
            }
        }

        IEnumerator CoroutineStopZEVOK(int random)
        {
            Gameplay_UI_Manager.Instance.ChangeActiveUI("Cutscene");
            controls.Disable();
            yield return new WaitForSeconds(AudioIdleSecondAFKLength[random]);//Wait for ending sound
            Gameplay_UI_Manager.Instance.ChangeActiveUI("Gameplay");
            controls.Enable();
        }

        IEnumerator CoroutineStopR83()
        {
            Gameplay_UI_Manager.Instance.ChangeActiveUI("Cutscene");
            controls.Disable();
            yield return new WaitForSeconds(AudioIdleFirstLength);//Wait for ending sound
            Gameplay_UI_Manager.Instance.ChangeActiveUI("Gameplay");
            controls.Enable();
            Character_Ali.Instance.SetAnimatorAli_r83_Bool(false);
        }

        // Start is called before the first frame update
        void Awake()
        {
            Instance = this;

            m_Character = GetComponent<Character_Ali>();

            controls = new PlayerControls();
            UIControlButtons.Add(GameObject.Find("ButtonLeft"));
            UIControlButtons.Add(GameObject.Find("ButtonRight"));
            UIControlButtons.Add(GameObject.Find("ButtonUp"));
            controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
            controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;
            if (canJump) { 
                controls.Gameplay.Jump.started += ctx => m_Jump = true;
            }
        }

        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = move.x;
            // Pass all parameters to the character control script.
            m_Character.Move(h, crouch, m_Jump);
            m_Jump = false;
        }

        public void TurnPlayerControllsOnOff(bool newValue)
        {
            if (newValue)
            {
                controls.Enable();
                idlePause = false;
                GameplayManager.Instance.TurnAutoCamOnOff(true);
                //GameplayManager.Instance.gameplayAutoCam.GetComponent<Animator>().enabled = false;
            }
            else
            {
                controls.Disable();
                idlePause = true;
                GameplayManager.Instance.TurnAutoCamOnOff(false);
            }
            foreach (var btn in UIControlButtons)
            {
                if (btn.GetComponent<ButtonUI_Mouse>().isShown)
                {
                    btn.SetActive(newValue);
                }
            }
            PauseAFK_Routine(!newValue);
        }

        private void OnEnable()
        {
            controls.Enable();
        }

        private void OnDisable()
        {
            controls.Disable();
        }

    }
}
