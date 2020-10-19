﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Playables;

namespace Oilan
{
    public class MillQuest : MonoBehaviour
    {
        public GameObject cameraAnchor;
        public float cameraTargetSize;

        public GameObject[] stackProblem;
        public GameObject[] buttonCheck;

        private int numProb = 0;
        public ProblemFlashCardStairs[] allProblems;
        public float checkDelay = 0.5f; //Задержка между проверками ответов

        public GameObject flashSpinner;
        public GameObject[] mills;
        public GameObject _ali;
        public GameObject _UINumbers;

        private Character_Ali character_Ali;

        public AudioSource _global_audio;

        public AudioClip Au_igra_42;
        public AudioClip _Au_igra_43;

        public AnimationClip rotate_120_degree;

        private Animator animFlash;
       // private Animator mill_Anim;
        private Animator ali_Anim;

        public GameObject spinnerMill;
        public int rotationDirection = -1; // -1 for clockwise
                                          //  1 for anti-clockwise
        public int rotationStep = 10;    // should be less than 90

        private Vector3 currentRotation, targetRotation;

        //Мини игра активируется, если сделать объект активным
        void Start()
        {
           // mill_Anim = mills[0].GetComponent<Animator>();
            ali_Anim = _ali.GetComponent<Animator>();
            character_Ali = _ali.GetComponent<Character_Ali>();
            animFlash = flashSpinner.GetComponent<Animator>();  

            foreach (var btn in buttonCheck)
            {
                btn.SetActive(false);
            }

            StartCoroutine(LaunchMiniGame());

        }

        public IEnumerator LaunchMiniGame()
        {
          //  CameraZoom(); //перемещение камеры

            _ali.GetComponent<Character_Ali>().SetAnimatorAli_r78_Bool_Talk(true);
            ali_Anim.SetBool("talk", true);
            _global_audio.clip = Au_igra_42;
            _global_audio.Play();

            //ждем пока не проиграется звук
            yield return new WaitForSeconds(Au_igra_42.length);

            _ali.GetComponent<Character_Ali>().SetAnimatorAli_r78_Bool_Talk(false);

            stackProblem[0].SetActive(true);
            buttonCheck[0].SetActive(true);
            _UINumbers.SetActive(true);
            //Пользователь вносит ответы, Система проверяет на соответствие форматов. Пользователь нажимает на кнопку «Проверить».
        }

        public void CheckSolved(int numBtn)
        {
            StartCoroutine(CheckSolvedCoroutine(numBtn));
        }

        private IEnumerator CheckSolvedCoroutine(int numCarts)
        {
            List<ProblemFlashCardStairs> problems = new List<ProblemFlashCardStairs>();

            for (int numProb = numCarts * 3; numProb < numCarts * 3 + 3; numProb++)
            {
                problems.Add(allProblems[numProb]);
            }

            buttonCheck[numCarts].SetActive(false);

            bool _isSolved = true;

            for (int i = 0; i < problems.Count; i++)
            {
                if (problems[i].gameObject.activeSelf)
                {
                    if (problems[i].currentState != ProblemFlashCardState.SOLVED)
                    {
                        problems[i].CheckAnswer();

                        yield return new WaitForSeconds(checkDelay);

                        if (problems[i].currentState != ProblemFlashCardState.SOLVED)
                        {
                            _isSolved = false;
                        }
                        else
                        {
                            problems[i].gameObject.SetActive(false);                     
                        }
                    }
                }
            }

            if (_isSolved)
            {
                stackProblem[numCarts].SetActive(false);
                buttonCheck[numCarts].SetActive(false);

                if (numCarts != 2)
                {                  
                    //mill_Anim.SetTrigger("Rotate_120_degree");
                    //animFlash.enabled = true;
                    // animFlash.SetTrigger("Rotate_120_degree");
                    rotateObject(spinnerMill);
                    rotateObject(flashSpinner);
                    yield return new WaitForSeconds(rotate_120_degree.length);
                    //animFlash.enabled = false;

                    // flashSpinner.transform.Rotate(0, 0, flashSpinner.transform.rotation.z - 120);
                    stackProblem[numCarts + 1].SetActive(true);
                    buttonCheck[numCarts + 1].SetActive(true);
                }
                else
                {                             
                    Solved();
                }

            }
            else
            {
                buttonCheck[numCarts].SetActive(true);
            }

            yield return null;
        }

        public void Solved()
        {
            _UINumbers.SetActive(false);
            StartCoroutine(AudioCoroutine(_Au_igra_43));

            //foreach (var mill in mills)
            //{
            //    mill.GetComponent<Animator>().enabled = true;
            //    mill.GetComponent<Animator>().SetBool("Sleep", false);
            //}

            //Проигрываем следующий таймлайн из списка таймлайнов в GameplayTimelineManager
            Oilan.GameplayTimelineManager.Instance.PlayNextTimeline();

            //Даем 10 звезд
            Oilan.GameplayScoreManager.Instance.AddWebStars(9);

            //Выключаем игровой объект
            gameObject.SetActive(false);
        }

        IEnumerator AudioCoroutine(AudioClip audioClip)
        {
            character_Ali.SetAnimatorTalkTrigger(true);
            _global_audio.clip = audioClip;
            _global_audio.Play();
            yield return new WaitForSeconds(audioClip.length);
            character_Ali.SetAnimatorTalkTrigger(false);
        }

        public void CameraZoom()
        {
            GameplayManager.Instance.TurnPlayerControlsOnOff(false);
            GameplayManager.Instance.TurnAutoCamOnOff(false);
            GameplayManager.Instance.MoveCamera(cameraAnchor, cameraTargetSize);
        }

        private void rotateObject(GameObject gameObj)
        {
            currentRotation = gameObj.transform.eulerAngles;
            targetRotation.z = (currentRotation.z + (120 * rotationDirection));
            StartCoroutine(objectRotationAnimation(gameObj));
        }

        IEnumerator objectRotationAnimation(GameObject gameObj)
        {
            // add rotation step to current rotation.
            currentRotation.z += (rotationStep * rotationDirection);
            gameObj.transform.eulerAngles = currentRotation;

            yield return new WaitForSeconds(0);

            if (((int)currentRotation.z >
   (int)targetRotation.z && rotationDirection < 0) ||  // for clockwise
         ((int)currentRotation.z < (int)targetRotation.z && rotationDirection > 0)) // for anti-clockwise
            {
                StartCoroutine(objectRotationAnimation(gameObj));
            }
            else
            {
                gameObj.transform.eulerAngles = targetRotation;
            }
        }
    }
}

