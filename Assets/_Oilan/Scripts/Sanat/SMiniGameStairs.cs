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
    
    private void OnTriggerEnter2D(Collider2D collision)
    {//Не делаем проверку, потому что слой Interractable пересекается только с Али
        GetComponent<BoxCollider2D>().enabled = false;
        GameplayTheoryManager.Instance.openExternalTrainerString("fleshCart");
        GameplayManager.Instance.TurnPlayerControlsOnOff(false);
        PlayerController.Instance.PauseAFK_Routine(true);

        //находим кнопку, которая будет запускать след. действие
        
        Button btn = _buttonNextPart.transform.GetChild(0).GetComponent<Button>();
        btn.onClick.AddListener(WhenTrainerFinished);
        _buttonNextPart.SetActive(true);
    }
    //Вызывается нажатием кнопки
    public void WhenTrainerFinished() { StartCoroutine(BuildStairs()); }


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
