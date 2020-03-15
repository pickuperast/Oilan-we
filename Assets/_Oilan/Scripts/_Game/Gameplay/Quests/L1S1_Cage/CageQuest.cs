using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Oilan
{
    public class CageQuest : Quest
    {
        //public GameObject cameraAnchor;
        //public float cameraTargetSize;

        public GameObject cameraAnchorBags;
        public float cameraTargetSizeBags;
        
        private Vector3 cameraPosOriginal;
        private float cameraSizeOriginal;

        public PlayableDirector director;

        public GameObject[] questCanvasList;

        public GameObject[] questObjectsList;

        public GameObject[] interactiveObjectsList;
        
        public GameObject cage;

        public GameObject cageLock;

        public GameObject buttonCheck;

        public ProblemAA_1_1_3[] problems;
        public ProblemAA_1_1_3[] problemsBags;
        public float checkDelay = 0.5f;
                
        public DragDropObject ddLetter;

        public List<DragDropObject> ddObjects;
        public List<DragDropTarget> ddTargets;
        
        public PlayableAsset cageTimeline_closed;
        public PlayableAsset cageTimeline_showProblems;
        public PlayableAsset cageTimeline_hideProblems;
        public PlayableAsset cageTimeline_openCage;
        public PlayableAsset cageTimeline_showBagsProblems;
        public PlayableAsset cageTimeline_openBags;
        public PlayableAsset cageTimeline_hideBags;
        public PlayableAsset cageTimeline_letterOpen;
        public PlayableAsset cageTimeline_letterClose;
        public PlayableAsset cageTimeline_endQuest;

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
            
            Character_Ali.Instance.ResetCharacterCutscenePosition();

            Character_Ali.Instance.SetSpriteVisibility(false);
            Character_Ali.Instance.SetCutsceneSpriteVisibility(true);

            GameplayManager.Instance.TurnPlayerControlsOnOff(false);

            director.Play(cageTimeline_closed);

            yield return new WaitForSeconds((float)director.duration);

            isPreActivated = true;

            GameplayManager.Instance.TurnPlayerControlsOnOff(true);

            yield return null;
        }


        public override void ActivateQuest()
        {
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
            
            problemValues newProblems = new ProblemAA().getAbacusSimple(problems.Length);

            for (int i = 0; i < problems.Length; i++)
            {
                problems[i].Init(newProblems.countsArr[i], newProblems.sumArr[i]);
            }
            
            director.Play(cageTimeline_showProblems);

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

            director.Play(cageTimeline_hideProblems);

            yield return new WaitForSeconds((float)director.duration);

            GameplayManager.Instance.MoveCamera(cameraPosOriginal, cameraSizeOriginal);
            
            cageLock.SetActive(false);
            ClearQuestCanvas();

            director.Play(cageTimeline_openCage);

            yield return new WaitForSeconds((float)director.duration);
            
            GameplayManager.Instance.MoveCamera(cameraAnchorBags, cameraTargetSizeBags);

            
            
            problemValues newProblems = new ProblemAA().getAbacusSimple(problemsBags.Length);

            for (int i = 0; i < problemsBags.Length; i++)
            {
                problemsBags[i].Init(newProblems.countsArr[i], newProblems.sumArr[i]);
            }


            director.Play(cageTimeline_showBagsProblems);

            yield return new WaitForSeconds((float)director.duration);

            yield return null;
        }

        public void CheckBagsSolved()
        {
            StartCoroutine(CheckBagsSolvedCoroutine());
        }

        private IEnumerator CheckBagsSolvedCoroutine()
        {

            foreach (ProblemAA_1_1_3 prblm in problemsBags)
            {
                prblm.SetState(ProblemAA_1_1_3_State.IDLE);
            }

            bool _isSolved = true;

            foreach (ProblemAA_1_1_3 problem in problemsBags)
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
                BagsSolved();
            }

            yield return null;
        }

        public void BagsSolved()
        {
            StartCoroutine(BagsSolvedCoroutine());
        }

        private IEnumerator BagsSolvedCoroutine()
        {
            isSolved = true;

            director.Play(cageTimeline_openBags);

            foreach (DragDropObject ddFruit in ddObjects)
            {
                ddFruit.OnPlaced += CheckFruitsSolved;
            }

            foreach (DragDropTarget ddTarget in ddTargets)
            {
                ddTarget.isOccupied = false;
            }
            yield return new WaitForSeconds((float)director.duration + 0.1f);
            director.Stop();

            yield return null;
        }

        public void CheckFruitsSolved()
        {
            StartCoroutine(CheckFruitsSolvedCoroutine());
        }

        private IEnumerator CheckFruitsSolvedCoroutine()
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
                    foreach(DragDropObject ddFruit in ddObjects)
                    {
                        if (ddFruit.gameObject.activeInHierarchy && ddTarget.id == ddFruit.id)
                        {
                            ddFruit.gameObject.SetActive(false);
                        }
                    }
                }

            }

            if (isFruitsSolved)
            {
                FruitsSolved();
            }

            yield return null;
        }

        public void FruitsSolved()
        {
            StartCoroutine(FruitsSolvedCoroutine());
        }

        private IEnumerator FruitsSolvedCoroutine()
        {
            director.Play(cageTimeline_hideBags);

            yield return new WaitForSeconds((float)director.duration);

            GameplayManager.Instance.MoveCamera(cameraPosOriginal, cameraSizeOriginal);

            ddLetter.enabled = true;
            ddLetter.OnPlaced += OpenLetter;

            var cageLetter = ddLetter.gameObject.GetComponent<SortingGroupController>();
            cageLetter.sortingLayer = "Front";

            Character_Ali.Instance.GetComponentInChildren<DragDropTarget>().isOccupied = false;

            yield return null;
        }

        private void ShowReward()
        {
            //StartCoroutine(ShowRewardCoroutine());
        }

        
        private void OpenLetter()
        {
            StartCoroutine(OpenLetterCoroutine());
        }

        private IEnumerator OpenLetterCoroutine()
        {
            ClearQuestCanvas();

            ddLetter.gameObject.SetActive(false);

            director.Play(cageTimeline_letterOpen);

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
            director.Play(cageTimeline_letterClose);

            yield return new WaitForSeconds((float)director.duration);

            PostDeactivateQuest();

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
            
            director.Play(cageTimeline_endQuest);

            yield return new WaitForSeconds((float)director.duration);

            director.enabled = false;
            
            Character_Ali.Instance.SetSpriteVisibility(true);
            Character_Ali.Instance.SetCutsceneSpriteVisibility(false);

            GameplayManager.Instance.TurnPlayerControlsOnOff(true);
            GameplayManager.Instance.TurnAutoCamOnOff(true);

            //DialogueManager.Instance.currentText = text;
            //DialogueManager.Instance.ShowDialogueGUI();

            //AudioManager.Instance.PlaySound(audioName, SoundPriority.MEDIUM);
            //StartCoroutine(HideDialogueCoroutine());

            Character_Ali.Instance.backpack_Value = 1f;
            Character_Ali.Instance.equipment_Value = 0f;
            Character_Ali.Instance.hold_Value = 0f;
           
            yield return null;
        }

        private IEnumerator HideDialogueCoroutine()
        {
            //yield return new WaitForSeconds(audioClip.length);

            //Debug.Log("HideDialogue");
            //DialogueManager.Instance.HideDialogueGUI();

            yield return null;
        }


    }
}
