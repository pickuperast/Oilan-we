using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZDialogPhraseTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(PlaySound());

            Oilan.Character_Ali.Instance.SetAnimatorTalkTrigger(true);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;//Отключаем триггер

        }
    }

    IEnumerator PlaySound()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);//Wait for ending sound
        Oilan.Character_Ali.Instance.SetAnimatorTalkTrigger(false);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;//Включаем триггер
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
