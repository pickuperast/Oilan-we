using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Oilan
{  
    public class ProblemFlashCardStairs : MonoBehaviour
    {
        public ProblemFlashCardState currentState = ProblemFlashCardState.IDLE;

        public UnityEvent correctAnswerEvent;
        
        public int answer = -1;
        public int answerUser = -1;

        public int rewardMax = 1;
        public int reward = 1;
        
        public int counterWrongAnswers = 0;

        [HideInInspector] public bool isFirstTime = true; 

        public AudioSource audioSource;

        public AudioClip clipSolved;
        public AudioClip clipWrong;
        
        public Color colorIdle;
        public Color colorWrong;
        public Color colorSolved;
        
        public Image inputFieldImage;
        public TMP_InputField inputField;

        private void Awake()
        {
            if (audioSource == null)
            {
                audioSource = GetComponent<AudioSource>();
            }
        }

        public void Init()
        {
            SetState(currentState);
        }

        public void SetState(ProblemFlashCardState newState)
        {
            currentState = newState;

            switch (newState)
            {
                case ProblemFlashCardState.IDLE:

                    inputFieldImage.color = colorIdle;

                    break;
                case ProblemFlashCardState.INACTIVE:

                    answerUser = answer;

                    inputFieldImage.color = colorIdle;
                    inputField.text = answer.ToString();

                    break;
                case ProblemFlashCardState.WRONG:

                    inputFieldImage.color = colorWrong;
                    audioSource.PlayOneShot(clipWrong);

                    break;
                case ProblemFlashCardState.SOLVED:

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
                    GameplayScoreManager.Instance.AddWebStars(1);
                    SetState(ProblemFlashCardState.SOLVED);
                }
                else
                {
                    SetState(ProblemFlashCardState.WRONG);
                    isFirstTime = false;
                }
            }
            else
            {
                answerUser = -1;
                SetState(ProblemFlashCardState.WRONG);            
            }
            
        }
            

    }
}