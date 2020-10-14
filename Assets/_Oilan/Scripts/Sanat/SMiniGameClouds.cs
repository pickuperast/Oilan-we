using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Oilan;

public class SMiniGameClouds : MonoBehaviour
{
    public AudioSource _global_audio;
    public AudioClip _Au_igra_44;
    public AudioClip _Zv_36;
    public BoxCollider2D _CloudCollider;
    public GameObject UIContinue;

    private bool isTracking = true;
    void OnTriggerEnter2D(Collider2D collision)
    {//Не делаем проверку, потому что слой Interractable пересекается только с Али
        _global_audio.clip = _Au_igra_44;
        _global_audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTracking && Input.GetMouseButtonDown(0))
        {
            WebGLMessageHandler.Instance.ConsoleLog("Mouse clicked");
            RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            if (hit.collider == _CloudCollider)
            {
                StartCoroutine(CorrectAnswer());
            }

        }
    }

    IEnumerator CorrectAnswer()
    {
        _global_audio.clip = _Zv_36;
        _global_audio.Play();
        yield return new WaitForSeconds(3.8f);
        GameplayTheoryManager.Instance.openExternalTrainerString("abacus");
        //находим кнопку, которая будет запускать след. действие
        Button btn = UIContinue.transform.GetChild(0).GetComponent<Button>();
        btn.onClick.AddListener(WhenTrainerFinished);
        UIContinue.SetActive(true);
    }

    public void WhenTrainerFinished()
    {
        //После того, как Пользователь решит все задачи, Цифры лопаются как мыльный пузырь с Zv-5 и исчезают

    }
}
