using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Oilan
{
    public class ChestQuest : Quest
    {
        //public GameObject cameraAnchor;
        //public float cameraTargetSize;
        private Vector3 cameraPosOriginal;
        private float cameraSizeOriginal;

        public PlayableDirector director;

        public GameObject[] questCanvasList;

        public GameObject[] questObjectsList;

        public GameObject[] interactiveObjectsList;
        
        public GameObject chest;

        public GameObject buttonCheck;

        public ProblemAA_1_1_3[] problems;
        public float checkDelay = 0.5f;

        public DragDropObject ddLetter;
        public DragDropObject ddBackpack;

        public PlayableAsset chestTimeline_closed;
        public PlayableAsset chestTimeline_showProblems;
        public PlayableAsset chestTimeline_hideProblems;
        public PlayableAsset chestTimeline_open;
        public PlayableAsset chestTimeline_letterOpen;
        public PlayableAsset chestTimeline_letterClose;
        public PlayableAsset chestTimeline_showReward;
        public PlayableAsset chestTimeline_endQuest;

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
            ClearInteractiveObjects();

            Character_Ali.Instance.ResetCharacterCutscenePosition();

            GameplayManager.Instance.TurnPlayerControlsOnOff(false);

            director.Play(chestTimeline_closed);

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

            director.Play(chestTimeline_showProblems);

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
                if (prblm.isSolved) {
                    //hide it
                    prblm.StartHideTimer = true;
                }
                else
                {
                    prblm.SetState(ProblemAA_1_1_3_State.IDLE);
                }
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
            WebGLMessageHandler.Instance.ConsoleLog("Everything is solved...");
            isSolved = true;

            WebGLMessageHandler.Instance.ConsoleLog("Deactivating button...");
            buttonCheck.SetActive(false);

            WebGLMessageHandler.Instance.ConsoleLog("director.Play(chestTimeline_hideProblems);");
            director.Play(chestTimeline_hideProblems);

            WebGLMessageHandler.Instance.ConsoleLog("yield return new WaitForSeconds((float)director.duration);");
            yield return new WaitForSeconds((float)director.duration);

            WebGLMessageHandler.Instance.ConsoleLog("director.Play(chestTimeline_open);");
            director.Play(chestTimeline_open);

            WebGLMessageHandler.Instance.ConsoleLog("GameplayManager.Instance.MoveCamera(cameraPosOriginal, cameraSizeOriginal);");
            GameplayManager.Instance.MoveCamera(cameraPosOriginal, cameraSizeOriginal);

            ddLetter.OnPlaced += OpenLetter;
            Character_Ali.Instance.GetComponentInChildren<DragDropTarget>().isOccupied = false;

            yield return new WaitForSeconds((float)director.duration);

            yield return null;
        }

        private void ShowReward()
        {
            StartCoroutine(ShowRewardCoroutine());
        }

        private IEnumerator ShowRewardCoroutine()
        {

            ClearQuestCanvas();

            director.Play(chestTimeline_showReward);

            ddBackpack.OnPlaced += PostDeactivateQuest;
            Character_Ali.Instance.GetComponentInChildren<DragDropTarget>().isOccupied = false;

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

            director.Play(chestTimeline_letterOpen);

            yield return new WaitForSeconds((float)director.duration);

            yield return null;
        }

        public void ButtonCloseLetter(int newFontID)//look for SUIFontChanger
        {
            StartCoroutine(CloseLetterCoroutine(newFontID));
            //CloseLetter();
        }

        /*private void CloseLetter()
        {
            StartCoroutine(CloseLetterCoroutine());
        }*/

        private IEnumerator CloseLetterCoroutine(int newFontID)
        {
            director.Play(chestTimeline_letterClose);

            yield return new WaitForSeconds((float)director.duration);
            ShowReward();
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
            
            director.Play(chestTimeline_endQuest);

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
