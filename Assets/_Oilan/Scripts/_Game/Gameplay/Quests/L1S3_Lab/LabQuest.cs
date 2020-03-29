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
     
        public PlayableAsset oakTimeline_start;
        public PlayableAsset oakTimeline_showProblems;
        public PlayableAsset oakTimeline_hideProblems;
        public PlayableAsset oakTimeline_endQuest;


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

            director.Play(oakTimeline_endQuest);
            
            yield return new WaitForSeconds((float)director.duration);

            ClearQuestCanvas();
            ClearQuestObjects();
     
            director.enabled = false;

            GameplayManager.Instance.TurnPlayerControlsOnOff(true);
            GameplayManager.Instance.TurnAutoCamOnOff(true);

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
            
            director.Play(oakTimeline_endQuest);

            yield return new WaitForSeconds((float)director.duration);

            director.enabled = false;           

            GameplayManager.Instance.TurnPlayerControlsOnOff(true);
            GameplayManager.Instance.TurnAutoCamOnOff(true);

            Character_Ali.Instance.backpack_Value = 1f;
            Character_Ali.Instance.equipment_Value = 0f;
            Character_Ali.Instance.hold_Value = 0f;
           
            yield return null;
        }

    }
}
