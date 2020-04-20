using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oilan
{
    public class DragDropQuest : Quest
    {
        //public GameObject cameraAnchor;
        //public float cameraTargetSize;
        
        private Vector3 cameraPosOriginal;
        private float cameraSizeOriginal;

        public GameObject bridgeUnsolved;
        public GameObject bridgeSolved;

        public List<DragDropObject> ddObjects;
        public List<DragDropTarget> ddTargets;

        public string text;

        public string audioName;
        public AudioClip audioClip;
        public SAudioManagerRef GameplayAudioHandler;

        private void Start()
        {
            isPreActivated = false;
            isActivated = false;
            isSolved = false;
        }

        public override void PreActivateQuest()
        {
            isPreActivated = true;
        }
        
        public override void ActivateQuest()
        {
            cameraPosOriginal = Camera.main.transform.position;
            cameraSizeOriginal = Camera.main.orthographicSize;

            GameplayManager.Instance.TurnPlayerControlsOnOff(false);

            GameplayManager.Instance.TurnAutoCamOnOff(false);
            GameplayManager.Instance.MoveCamera(cameraAnchor, cameraTargetSize);

            isActivated = true;
            
            bridgeSolved.SetActive(false);
            bridgeUnsolved.SetActive(true);

            foreach (DragDropObject ddObject in ddObjects)
            {
                ddObject.gameObject.SetActive(true);
                ddObject.OnPlaced += CheckSolved;
            }

            foreach (DragDropTarget ddTarget in ddTargets)
            {
                ddTarget.gameObject.SetActive(true);
            }

        }

        public override void CheckSolved()
        {
            bool isSolved = true;

            foreach (DragDropTarget ddTarget in ddTargets)
            {
                if (!ddTarget.isOccupied)
                {
                    isSolved = false;
                }
            }

            if (isSolved)
            {
                Solved();
            }
        }

        void playOwnAudioSource()
        {
            AudioSource l_audioSource = gameObject.GetComponent<AudioSource>();
            l_audioSource.Play();
            //GameplayAudioHandler.PlayAudioFromTimeline(audioName);
        }

        public override void Solved()
        {
            Debug.Log("Solved!");

            bridgeSolved.SetActive(true);
            bridgeUnsolved.SetActive(false);

            foreach (DragDropObject ddObject in ddObjects)
            {
                ddObject.OnPlaced = null;
                ddObject.gameObject.SetActive(false);
            }

            foreach (DragDropTarget ddTarget in ddTargets)
            {
                ddTarget.gameObject.SetActive(false);
            }
            GameplayManager.Instance.MoveCamera(cameraPosOriginal, cameraSizeOriginal);
            GameplayManager.Instance.TurnAutoCamOnOff(true);

            DialogueManager.Instance.currentText = text;
            DialogueManager.Instance.ShowDialogueGUI();
            
            Debug.Log("trying to play sound: " + audioName);
            AudioManager.Instance.PlaySound(audioName, true, true);
            playOwnAudioSource();
            StartCoroutine(HideDialogueCoroutine());
            WebGLMessageHandler.Instance.AddWebsiteStar();//Add 1 star when quest was finished
        }
        private IEnumerator HideDialogueCoroutine()
        {
            yield return new WaitForSeconds(audioClip.length);
            Debug.Log("HideDialogue");
            DialogueManager.Instance.HideDialogueGUI();

            //yield return new WaitForSeconds(time);

            //Debug.Log("ShowIndicator");
            //ShowHideGraphics(true);

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
    }
}
