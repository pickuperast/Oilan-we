using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Oilan
{
    public enum ProblemAA_1_1_3_State
    {
        IDLE,
        INACTIVE,
        WRONG,
        SOLVED
    }
    
    public class ProblemAA_1_1_3: MonoBehaviour
    {
        public ProblemAA_1_1_3_State currentState = ProblemAA_1_1_3_State.IDLE;

        public UnityEvent correctAnswerEvent;

        public int val1 = 0;
        public int val2 = 0;
        public int val3 = 0;

        public int answer = -1;
        public int answerUser = -1;

        public int rewardMax = 1;
        public int reward = 1;
        
        public int counterWrongAnswers = 0;

        public AudioSource audioSource;

        public AudioClip clipSolved;
        public AudioClip clipWrong;
        
        public Color colorIdle;
        public Color colorWrong;
        public Color colorSolved;

        public TMP_InputField valField1;
        public TMP_InputField valField2;
        public TMP_InputField valField3;
        
        public Image inputFieldImage;
        public TMP_InputField inputField;

        public bool isSolved = false;
        public CanvasGroup m_CanvasGroup;
        public bool StartHideTimer = false;
        float HideTimer = 1f;

        void Update()
        {
            Hide();
        }

        void Hide()
        {
            if (StartHideTimer && HideTimer > 0)
            {
                HideTimer -= Time.deltaTime;
                m_CanvasGroup.alpha = HideTimer;
            }
        }

        private void Awake()
        {
            if (audioSource == null)
            {
                audioSource = GetComponent<AudioSource>();
            }
        }

        public void Init(List<int> vals, int sum)
        {
            val1 = vals[0];
            val2 = vals[1];
            val3 = vals[2];

            answer = sum;

            valField1.text = val1.ToString();
            valField2.text = val2.ToString();
            valField3.text = val3.ToString();

            SetState(ProblemAA_1_1_3_State.IDLE);
        }

        public void SetState(ProblemAA_1_1_3_State newState)
        {
            currentState = newState;

            switch (newState)
            {
                case ProblemAA_1_1_3_State.IDLE:
                    
                    
                    inputFieldImage.color = colorIdle;

                    break;
                case ProblemAA_1_1_3_State.INACTIVE:

                    answerUser = answer;

                    inputFieldImage.color = colorIdle;
                    inputField.text = answer.ToString();

                    break;
                case ProblemAA_1_1_3_State.WRONG:
                    
                    inputFieldImage.color = colorWrong;
                    audioSource.PlayOneShot(clipWrong);

                    break;
                case ProblemAA_1_1_3_State.SOLVED:

                    inputFieldImage.color = colorSolved;
                    audioSource.PlayOneShot(clipSolved);

                    if (correctAnswerEvent != null)
                    {
                        correctAnswerEvent.Invoke();
                    }
                    
                    break;
                default:
                    break;
            }
        }

        public void CheckAnswer()
        {
            string newAnswer = inputField.text;

            if (audioSource != null)
            {
                audioSource.Stop();
            }
            
            if (!String.IsNullOrEmpty(newAnswer) && int.TryParse(newAnswer, out answerUser))
            {
                if (answerUser == answer)
                {
                    SetState(ProblemAA_1_1_3_State.SOLVED);
                    isSolved = true;
                    StartHideTimer = true;
                }
                else
                {
                    SetState(ProblemAA_1_1_3_State.WRONG);
                }
            }
            else
            {
                answerUser = -1;
                SetState(ProblemAA_1_1_3_State.WRONG);
            }
            
        }

        

     }
}