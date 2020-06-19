using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oilan
{
    public class DialogueTrigger : MonoBehaviour
    {

        public bool active = true;

        public bool showGizmo = true;

        [TextArea(3,5)]
        public string text;

        public string audioName;
        public AudioClip audioClip;
        public GameObject ali_cutscene;

        private void Awake()
        {
            ShowHideGraphics(true);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && active == true)
            {
                DialogueManager.Instance.currentText = text;
                DialogueManager.Instance.ShowDialogueGUI();

                AudioManager.Instance.PlaySound(audioName, true, true);

                ShowHideGraphics(false);

                Character_Ali.Instance.SetAnimatorTalkTrigger(true);
                Character_Ali.Instance.SetAnimatorPointOnChest(true);
                //Debug.Log("class DialogueTrigger called Character_Ali.Instance.SetAnimatorTalkTrigger();");
                //GameplayManager.Instance.TurnPlayerControlsOnOff(false);

                StartCoroutine(HideDialogueCoroutine(audioClip.length));
                Destroy(ali_cutscene);
            }
        }

        private void ShowHideGraphics(bool show)
        {
            active = show;

            var graphics = GetComponentInChildren<SpriteRenderer>();
            if (graphics != null)
            {
                graphics.enabled = show && showGizmo;
            }

        }

        private IEnumerator HideDialogueCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
            
            //Debug.Log("HideDialogue");
            DialogueManager.Instance.HideDialogueGUI();
            Character_Ali.Instance.SetAnimatorTalkTrigger(false);
            Character_Ali.Instance.SetAnimatorPointOnChest(false);

            //Character_Ali.Instance.SetAnimatorIdleTrigger();
            //GameplayManager.Instance.TurnPlayerControlsOnOff(true);

        }

    }

}