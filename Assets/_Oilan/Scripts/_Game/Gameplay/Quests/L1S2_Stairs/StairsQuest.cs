using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Oilan
{
    public class StairsQuest : Quest
    {
        //public GameObject cameraAnchor;
        //public float cameraTargetSize;

        private Vector3 cameraPosOriginal;
        private float cameraSizeOriginal;

        public GameObject star;

        public PlayableDirector director;

        public GameObject[] questCanvasList;

        public GameObject[] questObjectsList;

        public GameObject[] interactiveObjectsList;

        //public GameObject m;

        public GameObject buttonCheck;

        public ProblemFlashCard[] problems;
        public float checkDelay = 0.5f;//Задержка между проверками ответов

        public PlayableAsset mTimeline_start;
        public PlayableAsset mTimeline_showProblems;
        public PlayableAsset mTimeline_hideProblemsAndEndQuest;

        private void Start()
        {
            isPreActivated = false;
            isActivated = false;
            isSolved = false;
            isDeactivated = false;
            isPostDeactivated = false;

            buttonCheck.SetActive(false);
        }

        public void ClearQuestCanvas()
        {
            foreach (GameObject canvas in questCanvasList)
            {
                canvas.SetActive(false);
            }
        }

        public void ClearQuestObjects()
        {
            foreach (GameObject go in questObjectsList)
            {
                go.SetActive(false);
            }
        }

        public void ClearInteractiveObjects()
        {
            foreach (GameObject go in interactiveObjectsList)
            {
                go.SetActive(false);
            }
        }

        public override void PreActivateQuest()//Вызывается из Quest_Trigger
        {
            StartCoroutine(PreActivateQuestCoroutine());
        }

        private IEnumerator PreActivateQuestCoroutine()
        {
            ClearQuestCanvas();
            ClearQuestObjects();
            ClearInteractiveObjects();
            ////=====визуальное включение копии игрока (для анимаций разговоров)
            //Character_Ali.Instance.ResetCharacterCutscenePosition();
            //Character_Ali.Instance.SetSpriteVisibility(false);
            //Character_Ali.Instance.SetCutsceneSpriteVisibility(true);
            ////=====визуальное включение копии игрока (для анимаций разговоров)

            GameplayManager.Instance.TurnPlayerControlsOnOff(false);//отключение контроля

            director.Play(mTimeline_start);

            yield return new WaitForSeconds((float)director.duration);

            isPreActivated = true;

            //GameplayManager.Instance.TurnPlayerControlsOnOff(true);

            yield return null;
        }

        public override void ActivateQuest()//это флеш игра
        {
            //перемещение камеры
            cameraPosOriginal = Camera.main.transform.position;
            cameraSizeOriginal = Camera.main.orthographicSize;

            GameplayManager.Instance.TurnPlayerControlsOnOff(false);

            GameplayManager.Instance.TurnAutoCamOnOff(false);
            GameplayManager.Instance.MoveCamera(cameraAnchor, cameraTargetSize);

            StartCoroutine(ActivateQuestCoroutine());
        }

        private IEnumerator ActivateQuestCoroutine()
        {
            for (int i = 0; i < problems.Length; i++)
            {
               problems[i].Init();
            }

            director.Play(mTimeline_showProblems);

            yield return new WaitForSeconds((float)director.duration);

            buttonCheck.SetActive(true);

            isActivated = true;

            yield return null;

        }

        public override void CheckSolved()
        {
            StopAllCoroutines();
            StartCoroutine(CheckSolvedCoroutine());
        }

        private IEnumerator CheckSolvedCoroutine()
        {
            buttonCheck.SetActive(false);

            foreach (ProblemFlashCard prblm in problems)
            {
                prblm.SetState(ProblemFlashCardState.IDLE);
            }

            bool _isSolved = true;

            for (int i = 0; i < problems.Length; i++)
            {
                if (problems[i].gameObject.activeSelf)
                {
                    if (problems[i].currentState == ProblemFlashCardState.IDLE)
                    {
                        problems[i].CheckAnswer();

                        yield return new WaitForSeconds(checkDelay);

                        if (problems[i].currentState != ProblemFlashCardState.SOLVED)
                        {
                            _isSolved = false;
                        }
                        else
                        {

                            problems[i].currentState = ProblemFlashCardState.SOLVED;
                            problems[i].gameObject.SetActive(false);
                            questObjectsList[i].GetComponent<Animator>().enabled = true;

                            yield return new WaitForSeconds(1);
                            if (problems[i].isFirstTime)
                            {
                                GameObject starObject = Instantiate(star, new Vector3(questObjectsList[i].transform.position.x, questObjectsList[i].transform.position.y + 1, questObjectsList[i].transform.position.z),
                                    Quaternion.identity, questObjectsList[i].transform) as GameObject;
                                starObject.GetComponentInChildren<SpriteRenderer>().sortingLayerName = "Objects_Back";
                                starObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 11 + i;
                            }
                            SAudioManagerRef.Instance.PlayAudioFromTimeline("Zv-32 (Звук крепления ступеньки (сбор лестницы))");
                        }
                    }
                }
            }

            if (_isSolved)
            {
                GameplayScoreManager.Instance.AddWebStars(10);
                Solved();
            }
            else
            {
                buttonCheck.SetActive(true);
            }

            yield return null;
        }

        public override void Solved()
        {         
            StartCoroutine(SolvedCoroutine());
        }

        private IEnumerator SolvedCoroutine()
        {
            isSolved = true;

            buttonCheck.SetActive(false);

            ClearQuestCanvas();
            ClearQuestObjects();

            GameplayManager.Instance.TurnPlayerControlsOnOff(true);
            GameplayManager.Instance.TurnAutoCamOnOff(true);

            director.Play(mTimeline_hideProblemsAndEndQuest);

            yield return new WaitForSeconds((float)director.duration);

            director.enabled = false;
   
            Character_Ali.Instance.backpack_Value = 1f;
            Character_Ali.Instance.equipment_Value = 0f;
            Character_Ali.Instance.hold_Value = 0f;

            yield return null;
        }

        public override void DeactivateQuest()
        {
            //StartCoroutine(DeactivateQuestCoroutine());
        }

        public override void PostDeactivateQuest()
        {
            StartCoroutine(PostDeactivateQuestCoroutine());
        }

        private IEnumerator PostDeactivateQuestCoroutine()
        {
            ClearQuestCanvas();
            ClearQuestObjects();

         //   director.Play(mTimeline_endQuest);

            yield return new WaitForSeconds((float)director.duration);

            director.enabled = false;

            Character_Ali.Instance.SetSpriteVisibility(true);
            Character_Ali.Instance.SetCutsceneSpriteVisibility(false);

            GameplayManager.Instance.TurnPlayerControlsOnOff(true);
            GameplayManager.Instance.TurnAutoCamOnOff(true);

            Character_Ali.Instance.backpack_Value = 1f;
            Character_Ali.Instance.equipment_Value = 0f;
            Character_Ali.Instance.hold_Value = 0f;

            yield return null;
        }

    }
}

