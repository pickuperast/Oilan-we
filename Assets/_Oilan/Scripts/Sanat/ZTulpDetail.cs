using Oilan;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZTulpDetail : MonoBehaviour
{
    public UnityEvent uevent;
    public Oilan.ProblemAA_1_1_3[] problems;
    public GameObject _UINumbers;
    private Animator m_Anim;

    private float checkDelay = 0.5f;
    //Мини игра активируется, если сделать объект активным


    void Start()
    {
        //Чтобы UINumbers отображался поверх
        Canvas _canvas = transform.GetChild(0).GetComponent<Canvas>();
        _canvas.worldCamera = Camera.main;
        _canvas.sortingLayerName = "UI";
        m_Anim = gameObject.GetComponent<Animator>();
        GameplayManager.Instance.TurnPlayerControlsOnOff(true);
        StartCoroutine(LaunchMiniGame());
    }


    public IEnumerator LaunchMiniGame()
    {
        //вылетают уравнения
        m_Anim.SetTrigger("Born");
        Oilan.problemValues newProblems = new Oilan.ProblemAA().getAbacusSimple(problems.Length);
        for (int i = 0; i < problems.Length; i++)
        {
            problems[i].Init(newProblems.countsArr[i], newProblems.sumArr[i]);
        }
        yield return new WaitForSeconds(0.2f);//потому что по непонятной причине UINumbers становятся неактивными сами по себе

        _UINumbers.SetActive(true);
        //Пользователь вносит ответы, Система проверяет на соответствие форматов. Пользователь нажимает на кнопку «Проверить».
    }

    public void CheckSolved()
    {
        StartCoroutine(CheckSolvedCoroutine());
    }
    public void Solved()
    {
        GameplayManager.Instance.TurnPlayerControlsOnOff(false);

        //Действия если все ответы правильные
        m_Anim.SetTrigger("Die");

        //Скрываем кнопки ввода цифр
        _UINumbers.SetActive(false);

        //Проигрываем следующий таймлайн из списка таймлайнов в GameplayTimelineManager
        uevent.Invoke();
        //Даем 10 звезд
        Oilan.GameplayScoreManager.Instance.AddWebStars(10);

        //Выключаем игровой объект
        gameObject.SetActive(false);
    }

    private IEnumerator CheckSolvedCoroutine()
    {
        bool _isSolved = true;
        foreach (Oilan.ProblemAA_1_1_3 problem in problems)
        {
            if (problem.currentState != Oilan.ProblemAA_1_1_3_State.SOLVED)
            {
                problem.CheckAnswer();
                if (problem.currentState != Oilan.ProblemAA_1_1_3_State.SOLVED)
                {
                    _isSolved = false;
                }
                yield return new WaitForSeconds(checkDelay);
            }
        }
        if (_isSolved) Solved();
        yield return null;
    }
}
