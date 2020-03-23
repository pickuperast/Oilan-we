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

        public List<DragDropObject> ddObjects;//drag & drop
        public List<DragDropTarget> ddTargets;

        public DragDropObject ddPlate;
        public DragDropObject ddLetter;

        public PlayableAsset mTimeline_start;
        public PlayableAsset mTimeline_showProblems;
        public PlayableAsset mTimeline_hideProblems;
        public PlayableAsset mTimeline_showSymbols;
        public PlayableAsset mTimeline_hideSymbols;
        public PlayableAsset mTimeline_showReward;
        public PlayableAsset mTimeline_letterOpen;
        public PlayableAsset mTimeline_letterClose;
        public PlayableAsset mTimeline_endQuest;

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
            ClearQuestCanvas();

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

            director.Play(mTimeline_hideProblems);

            yield return new WaitForSeconds((float)director.duration);

            director.Play(mTimeline_showSymbols);

            foreach (DragDropObject ddSymbol in ddObjects)
            {
                ddSymbol.OnPlaced += CheckSymbolsSolved;
            }

            foreach (DragDropTarget ddTarget in ddTargets)
            {
                ddTarget.isOccupied = false;
            }
            yield return new WaitForSeconds((float)director.duration + 0.1f);
            director.Stop();

            yield return null;
        }

        public void CheckSymbolsSolved()
        {
            StartCoroutine(CheckSymbolsSolvedCoroutine());
        }

        private IEnumerator CheckSymbolsSolvedCoroutine()
        {

            bool isFruitsSolved = true;

            foreach (DragDropTarget ddTarget in ddTargets)
            {
                if (!ddTarget.isOccupied)
                {
                    isFruitsSolved = false;
                }
                else
                {
                    foreach (DragDropObject ddSymbol in ddObjects)
                    {
                        if (ddSymbol.gameObject.activeInHierarchy && ddTarget.id == ddSymbol.id)
                        {
                            ddSymbol.GetComponent<DragDropObject>().enabled = false;
                        }
                    }
                }

            }

            if (isFruitsSolved)
            {
                SymbolsSolved();
            }

            yield return null;
        }

        public void SymbolsSolved()
        {
            StartCoroutine(SymbolsSolvedCoroutine());
        }

        private IEnumerator SymbolsSolvedCoroutine()
        {
            director.Play(mTimeline_hideSymbols);

            yield return new WaitForSeconds((float)director.duration);

            GameplayManager.Instance.MoveCamera(cameraPosOriginal, cameraSizeOriginal);

            Character_Ali.Instance.GetComponentInChildren<DragDropTarget>().isOccupied = false;

            foreach (DragDropObject ddSymbol in ddObjects)
            {
                ddSymbol.GetComponent<Collider2D>().enabled = false;
            }

            foreach (DragDropTarget ddTarget in ddTargets)
            {
                ddTarget.GetComponent<Collider2D>().enabled = false;
            }


            ddPlate.enabled = true;
            ddPlate.OnPlaced += ShowReward;

            //var sortPlate = ddPlate.gameObject.GetComponent<CageLetter>();
            //sortPlate.sortingLayer = "Front";

            yield return null;
        }

        private void ShowReward()
        {
            StartCoroutine(ShowRewardCoroutine());
        }

        private IEnumerator ShowRewardCoroutine()
        {
            ClearQuestCanvas();

            ddPlate.gameObject.SetActive(false);

            director.Play(mTimeline_showReward);
            yield return new WaitForSeconds((float)director.duration);

            ddLetter.OnPlaced += OpenLetter;
            Character_Ali.Instance.GetComponentInChildren<DragDropTarget>().isOccupied = false;

            //var sortLetter = ddLetter.gameObject.GetComponent<CageLetter>();
            //sortLetter.sortingLayer = "Front";

            yield return null;
        }

        private void OpenLetter()
        {
            StartCoroutine(OpenLetterCoroutine());
        }

        private IEnumerator OpenLetterCoroutine()
        {
            ClearQuestCanvas();

            ddLetter.gameObject.SetActive(false);

            director.Play(mTimeline_letterOpen);

            yield return new WaitForSeconds((float)director.duration);

            yield return null;
        }

        public void ButtonCloseLetter()
        {
            CloseLetter();
        }

        private void CloseLetter()
        {
            StartCoroutine(CloseLetterCoroutine());
        }

        private IEnumerator CloseLetterCoroutine()
        {
            director.Play(mTimeline_letterClose);

            yield return new WaitForSeconds((float)director.duration);

            PostDeactivateQuest();
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

            director.Play(mTimeline_endQuest);

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

