using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oilan
{
    public class QuestTrigger : MonoBehaviour
    {
        public Quest quest;

        public bool active = true;
        public bool showGizmo = true;

        public bool preActivateQuest = false;

        public bool playDialogue = false;

        public string text;

        public string audioName;
        public AudioClip audioClip;

        //Used when need to play some thinking animation before speech
        [Header("Pre Animation before speech")]
        public bool isplayPreAnimation;
        public string TriggerName;
        public float PreAnimationTime;

        private void Awake()
        {
            SetTriggerActive(true);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && active == true)
            {
                SetTriggerActive(false);
                
                StartCoroutine(ActivateQuestCoroutine());
            }
        }

        private IEnumerator ActivateQuestCoroutine()
        {
            if (preActivateQuest)
            {
                quest.PreActivateQuest();
                
                while (!quest.isPreActivated)
                {
                    yield return null;
                }
            }

            if(playDialogue)
            {
                GameplayManager.Instance.TurnPlayerControlsOnOff(false);
                
                if (isplayPreAnimation)
                {
                    Character_Ali.Instance.SetAnimatorTrigger(TriggerName);
                    yield return new WaitForSeconds(PreAnimationTime);
                }

                DialogueManager.Instance.currentText = text;
                DialogueManager.Instance.ShowDialogueGUI();

                Character_Ali.Instance.SetAnimatorTalkTrigger(true);

                AudioManager.Instance.PlaySound(audioName);
                yield return new WaitForSeconds(audioClip.length);

                Character_Ali.Instance.SetAnimatorTalkTrigger(false);
                Character_Ali.Instance.SetAnimatorIdleTrigger();

                WebGLMessageHandler.Instance.ConsoleLog("HideDialogue");
                DialogueManager.Instance.HideDialogueGUI();

                GameplayManager.Instance.TurnPlayerControlsOnOff(true);

            }

            quest.ActivateQuest();

            yield return null;
        }


        private void SetTriggerActive(bool newValue)
        {
            active = newValue;

            var graphics = GetComponentInChildren<SpriteRenderer>();
            if (graphics != null)
            {
                graphics.enabled = newValue && showGizmo;
            }

        }


    }

}