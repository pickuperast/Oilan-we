using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sDialogTrigger : MonoBehaviour
{
    [TextArea(3, 5)]
    public string text;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Oilan.DialogueManager.Instance.currentText = text;
            Oilan.DialogueManager.Instance.ShowDialogueGUI();
            StartCoroutine(PlaySound());

            //Oilan.AudioManager.Instance.PlaySound(audioName, SoundPriority.MEDIUM);

            //ShowHideGraphics(false);

            Oilan.Character_Ali.Instance.SetAnimatorTalkTrigger(true);

            Oilan.GameplayManager.Instance.TurnPlayerControlsOnOff(false);

            //StartCoroutine(HideDialogueCoroutine(6f));
            gameObject.GetComponent<BoxCollider2D>().enabled = false;//Отключаем триггер

        }
    }

    IEnumerator PlaySound()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);//Wait for ending sound
        Oilan.Character_Ali.Instance.SetAnimatorTalkTrigger(false);
        Oilan.Character_Ali.Instance.SetAnimatorTrigger("Idle");//reset animation
        Oilan.GameplayManager.Instance.TurnPlayerControlsOnOff(true);//turn controls on
        //audio.clip = otherClip;
        //audio.Play();
        //yield return new WaitForSeconds(audio.clip.length);
    }

    //скрывает маркер триггера во время игры
    private void Awake()
    {
        var graphics = GetComponentInChildren<SpriteRenderer>();
        if (graphics != null) {
            graphics.enabled = false;// && showGizmo;
        }
    }
}
