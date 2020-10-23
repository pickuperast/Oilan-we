using System;
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

        public GameObject[] flashSpinner;
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

        public GameObject[] spinnerMill;
        public int rotationDirection = -1; // -1 for clockwise
                                           //  1 for anti-clockwise
        public int rotationStep = 10;    // should be less than 90

        private Vector3 currentRotation, targetRotation;

        public GameObject cameraRig;
        public int interpolationFramesCount = 45; // Number of frames to completely interpolate between the 2 positions
        int elapsedFrames = 0;

        //Мини игра активируется, если сделать объект активным
        void Start()
        {
                          // mill_Anim = mills[0].GetComponent<Animator>();
            ali_Anim = _ali.GetComponent<Animator>();
            character_Ali = _ali.GetComponent<Character_Ali>();

            StartCoroutine(LaunchMiniGame());

        }

        void Update()
        {
            float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;

            Vector3 interpolatedPosition = Vector3.Lerp(cameraRig.transform.position, cameraAnchor.transform.position, interpolationRatio);

            elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);  // reset elapsedFrames to zero after it reached (interpolationFramesCount + 1)

        }

        public IEnumerator LaunchMiniGame()
        {

            _ali.GetComponent<Character_Ali>().SetAnimatorAli_r78_Bool_Talk(true);
            ali_Anim.SetBool("talk", true);
            _global_audio.clip = Au_igra_42;
            _global_audio.Play();

            //ждем пока не проиграется звук
            yield return new WaitForSeconds(Au_igra_42.length);

            _ali.GetComponent<Character_Ali>().SetAnimatorAli_r78_Bool_Talk(false);

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

            bool endSolve = false;

            for (int numProb = numCarts * 3; numProb < numCarts * 3 + 3; numProb++)
            {
                if (allProblems[numProb].currentState != ProblemFlashCardState.SOLVED)
                {
                    problems.Add(allProblems[numProb]);
                    if (numProb == 2 || numProb == 5 || numProb == 8) endSolve = true;
                    break;
                }
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
                buttonCheck[numCarts].SetActive(false);

                if (!endSolve)
                {
                    //mill_Anim.SetTrigger("Rotate_120_degree");
                    //animFlash.enabled = true;
                    // animFlash.SetTrigger("Rotate_120_degree");
                    rotateObject(spinnerMill[numCarts]);
                    rotateObject(flashSpinner[numCarts]);
                    yield return new WaitForSeconds(rotate_120_degree.length);
                    //animFlash.enabled = false;

                    // flashSpinner.transform.Rotate(0, 0, flashSpinner.transform.rotation.z - 120);
                    buttonCheck[numCarts].SetActive(true);
                }
                else
                {
                    foreach (var problem in allProblems)
                    {
                        if (problem.currentState != ProblemFlashCardState.SOLVED) endSolve = false;

                    }

                    if (endSolve) Solved();
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

