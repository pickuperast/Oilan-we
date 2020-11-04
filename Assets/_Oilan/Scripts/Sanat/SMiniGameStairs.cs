using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Oilan;

public class SMiniGameStairs : MonoBehaviour
{
    public Animator _Stairs;
    public GameObject _StarsPlacer;
    public GameObject _buttonNextPart;
    [SerializeField]
    private AudioClip Au_igra_15;
    [SerializeField]
    private AudioSource _global_audio;

    private void OnTriggerEnter2D(Collider2D collision)
    {//Не делаем проверку, потому что слой Interractable пересекается только с Али
        GetComponent<BoxCollider2D>().enabled = false;
        GameplayManager.Instance.TurnPlayerControlsOnOff(false);
        PlayerController.Instance.PauseAFK_Routine(true);
        StartCoroutine(IntroToGame());

        //находим кнопку, которая будет запускать след. действие
        
        Button btn = _buttonNextPart.transform.GetChild(0).GetComponent<Button>();
        btn.onClick.AddListener(WhenTrainerFinished);
        _buttonNextPart.SetActive(true);
    }
    //Вызывается нажатием кнопки
    public void WhenTrainerFinished() { StartCoroutine(BuildStairs()); }

    IEnumerator IntroToGame()
    {
        _global_audio.clip = Au_igra_15;
        _global_audio.Play();
        PlayerController.Instance.gameObject.GetComponent<Animator>().SetBool("Talk", true);
        yield return new WaitForEndOfFrame();
        //yield return new WaitForSeconds(Au_igra_15.length);
        GameplayTheoryManager.Instance.openExternalTrainerString("fleshCart");
        yield return new WaitForSeconds(Au_igra_15.length);
        PlayerController.Instance.gameObject.GetComponent<Animator>().SetBool("Talk", false);
    }

    IEnumerator BuildStairs()
    {
        _buttonNextPart.SetActive(false);
        _Stairs.SetTrigger("born");
        //Ждем пока не отыграет анимация рождения
        yield return new WaitForSeconds(1.667f);

        _Stairs.SetBool("idle", true);
        _StarsPlacer.SetActive(true);
        GameplayManager.Instance.TurnPlayerControlsOnOff(true);
        PlayerController.Instance.PauseAFK_Routine(false);
    }
}
