using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Oilan
{
    public class LabQuest : Quest
    {
        //public GameObject cameraAnchor;
        //public float cameraTargetSize;

        private Vector3 cameraPosOriginal;
        private float cameraSizeOriginal;

        public PlayableDirector director;

        public GameObject[] questCanvasList;

        public GameObject[] questObjectsList;

        public GameObject[] interactiveObjectsList;

        public GameObject buttonCheck;

        public ProblemAA_1_1_3[] problems;
        public float checkDelay = 0.5f;

        public List<DragDropObject> ddObjects;
        public List<DragDropTarget> ddTargets;

        public DragDropObject ddPlate;
        public DragDropObject ddLetter;
        
        public PlayableAsset oakTimeline_start;
        public PlayableAsset oakTimeline_showProblems;
        public PlayableAsset oakTimeline_hideProblems;
        public PlayableAsset oakTimeline_endQuest;
        public PlayableAsset oakTimeline_showSymbols;
        public PlayableAsset oakTimeline_hideSymbols;
        public PlayableAsset oakTimeline_showReward;


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

        public override void PreActivateQuest()
        {
            StartCoroutine(PreActivateQuestCoroutine());
        }

        private IEnumerator PreActivateQuestCoroutine()
        {
            ClearQuestCanvas();
            ClearQuestObjects();
            ClearInteractiveObjects();

            Character_Ali.Instance.ResetCharacterCutscenePosition();

            Character_Ali.Instance.SetSpriteVisibility(false);
            Character_Ali.Instance.SetCutsceneSpriteVisibility(true);

            GameplayManager.Instance.TurnPlayerControlsOnOff(false);

            director.Play(oakTimeline_start);

            yield return new WaitForSeconds((float)director.duration);

            isPreActivated = true;

            //GameplayManager.Instance.TurnPlayerControlsOnOff(true);

            yield return null;
        }


        public override void ActivateQuest()
        {
            cameraPosOriginal = Camera.main.transform.position;
            cameraSizeOriginal = Camera.main.orthographicSize;

            GameplayManager.Instance.TurnPlayerControlsOnOff(false);

            GameplayManager.Instance.TurnAutoCamOnOff(false);
            GameplayManager.Instance.MoveCamera(cameraAnchor, cameraTargetSize);

        }

        public void ActivateLockQuest()
        {
            StartCoroutine(ActivateQuestCoroutine());
        }

        private IEnumerator ActivateQuestCoroutine()
        {
            ClearQuestCanvas();

            problemValues newProblems = new ProblemAA().getAbacusSimple(problems.Length);

            for (int i = 0; i < problems.Length; i++)
            {
                problems[i].Init(newProblems.countsArr[i], newProblems.sumArr[i]);
            }

            director.Play(oakTimeline_showProblems);

            yield return new WaitForSeconds((float)director.duration);

            buttonCheck.SetActive(true);

            isActivated = true;

            yield return null;

        }

        public override void CheckSolved()
        {
            StartCoroutine(CheckSolvedCoroutine());
        }

        private IEnumerator CheckSolvedCoroutine()
        {

            foreach (ProblemAA_1_1_3 prblm in problems)
            {
                prblm.SetState(ProblemAA_1_1_3_State.IDLE);
            }

            bool _isSolved = true;

            foreach (ProblemAA_1_1_3 problem in problems)
            {
                if (problem.currentState == ProblemAA_1_1_3_State.IDLE)
                {
                    problem.CheckAnswer();

                    if (problem.currentState != ProblemAA_1_1_3_State.SOLVED)
                    {
                        _isSolved = false;
                    }

                    yield return new WaitForSeconds(checkDelay);
                }

            }

            if (_isSolved)
            {
                Solved();
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

            director.Play(oakTimeline_hideProblems);

            yield return new WaitForSeconds((float)director.duration);

            director.Play(oakTimeline_showSymbols);

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
            director.Play(oakTimeline_hideSymbols);

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
            
            director.Play(oakTimeline_showReward);
            yield return new WaitForSeconds((float)director.duration);

            Character_Ali.Instance.GetComponentInChildren<DragDropTarget>().isOccupied = false;

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
            
            director.Play(oakTimeline_endQuest);

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
