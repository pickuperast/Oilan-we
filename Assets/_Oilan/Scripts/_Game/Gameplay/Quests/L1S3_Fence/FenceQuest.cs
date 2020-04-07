using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace Oilan
{
    public class FenceQuest : Quest
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

        public GameObject buttonCheck;

        public ProblemFlashCard[] problems;
        public float checkDelay = 0.5f;

        public PlayableAsset oakTimeline_showProblems;
        public PlayableAsset oakTimeline_hideProblems;



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
           // StartCoroutine(PreActivateQuestCoroutine());
        }

        public override void ActivateQuest()
        {
            cameraPosOriginal = Camera.main.transform.position;
            cameraSizeOriginal = Camera.main.orthographicSize;

            director.Play(oakTimeline_showProblems);

            GameplayManager.Instance.TurnPlayerControlsOnOff(false);

            GameplayManager.Instance.TurnAutoCamOnOff(false);
          //  GameplayManager.Instance.MoveCamera(cameraAnchor, cameraTargetSize);

        }

        public override void CheckSolved()
        {
            StartCoroutine(CheckSolvedCoroutine());
        }

        private IEnumerator CheckSolvedCoroutine()
        {

            foreach (ProblemFlashCard prblm in problems)
            {
                prblm.SetState(ProblemFlashCardState.IDLE);
            }

            bool _isSolved = true;

            for (int i = 0; i< problems.Length;i++) 
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
                        if (problems[i].isFirstTime)
                        {
                            GameObject starObject = Instantiate(star, new Vector3(problems[i].transform.position.x, problems[i].transform.position.y + 1, problems[i].transform.position.z),
                            Quaternion.identity, problems[i].transform) as GameObject;            
                            starObject.AddComponent<Image>().sprite = starObject.GetComponentInChildren<SpriteRenderer>().sprite;
                            yield return new WaitForSeconds(0.5f);
                            GameplayScoreManager.Instance.AddCoins(1);
                            SAudioManagerRef.Instance.PlayAudioFromTimeline("Zv-9 (Волшебный звук для звезды (отлетают на табло в меню “Награды”))");
                            Destroy(starObject);
                        }
                      
                    }
                 
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

            ClearQuestCanvas();
            ClearQuestObjects();
            GameplayManager.Instance.TurnPlayerControlsOnOff(true);
            GameplayManager.Instance.TurnAutoCamOnOff(true);
            director.enabled = false;    
            yield return null;
        }

        public override void DeactivateQuest()
        {
            //StartCoroutine(DeactivateQuestCoroutine());
        }
        
        public override void PostDeactivateQuest()
        {
            //StartCoroutine(PostDeactivateQuestCoroutine());
        }

        private IEnumerator PostDeactivateQuestCoroutine()
        {

              yield return null;
        }

    }
}
