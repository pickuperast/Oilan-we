using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMiniGameTrost : MonoBehaviour
{
    public GameObject _starik;
    public GameObject _ali;
    public AnimationClip _starik_biet_zemlu;
    public AnimationClip _starik_ukazivaet;
    public AnimationClip _mini_game_trost;
    public AudioSource _starik_audio;
    public AudioSource _global_audio;
    public AudioClip _Au_igra_38;
    public AudioClip _Au_igra_39;
    public Oilan.ProblemAA_1_1_3[] problems;

    private Animator starik_Anim;
    private Animator m_Anim;
    private float checkDelay = 0.5f;
    //Мини игра активируется, если сделать объект активным
    void Start(){
        starik_Anim = _starik.GetComponent<Animator>();
        m_Anim = gameObject.GetComponent<Animator>();
        LaunchMiniGame();
        StartCoroutine(ActivateQuestCoroutine());
    }
    

    public void LaunchMiniGame()    {
        //Старец взмахивает тростью (starik_biet_zemlu, starik_ukazivaet) со звуком Zv-11 Звук проигрывается в самой анимации
        starik_Anim.SetTrigger("starik_biet_zemlu");

        //таймер сна
        StartCoroutine(Sleep(_starik_biet_zemlu.length + _starik_ukazivaet.length));

        //в этот момент Старец говорит (starik_neitral) Au_igra_38: «Цифры раз, цифры два, все явитесь к нам сюда!»
        starik_Anim.SetBool("talk", true);
        _global_audio.clip = _Au_igra_38;
        _global_audio.Play();

        //ждем пока не проиграется звук
        StartCoroutine(Sleep(_Au_igra_38.length));
        starik_Anim.SetBool("talk", false);

        //Из трости появляется свет деталь: свечение из трости, и из трости вылетают уравнения с Zv-36 с полем ответа, которые собираются на небе. Звук проигрывается в самой анимации
        starik_Anim.SetTrigger("mini_game_trost");

        //ждем пока не проиграется анимацифя
        StartCoroutine(Sleep(_mini_game_trost.length));

        //Старец говорит (starik_neitral) Au_igra_39: «Нужно зарядить его цифрами, и он снова засияет! Реши задачи с помощью Абакуса.». 
        starik_Anim.SetBool("talk", true);
        _global_audio.clip = _Au_igra_39;
        _global_audio.Play();

        //ждем пока не проиграется звук
        StartCoroutine(Sleep(_Au_igra_39.length));
        starik_Anim.SetBool("talk", false);

        //Пользователь вносит ответы, Система проверяет на соответствие форматов. Пользователь нажимает на кнопку «Проверить».
        m_Anim.SetTrigger("Born");
    }

    public void CheckSolved()    {
        StartCoroutine(CheckSolvedCoroutine());
    }
    public void Solved()    {

        //Действия если все ответы правильные
        m_Anim.SetTrigger("Die");

        //Проигрываем следующий таймлайн из списка таймлайнов в GameplayTimelineManager
        Oilan.GameplayTimelineManager.Instance.PlayNextTimeline();
    }
    private IEnumerator ActivateQuestCoroutine()    {
        Oilan.problemValues newProblems = new Oilan.ProblemAA().getAbacusSimple(problems.Length);
        for (int i = 0; i < problems.Length; i++)        {
            problems[i].Init(newProblems.countsArr[i], newProblems.sumArr[i]);
        }
        yield return null;

    }
    private IEnumerator CheckSolvedCoroutine()    {
        bool _isSolved = true;
        foreach (Oilan.ProblemAA_1_1_3 problem in problems)        {
            if (problem.currentState != Oilan.ProblemAA_1_1_3_State.SOLVED)            {
                problem.CheckAnswer();
                if (problem.currentState != Oilan.ProblemAA_1_1_3_State.SOLVED)                {
                    _isSolved = false;
                }
                yield return new WaitForSeconds(checkDelay);
            }
        }
        if (_isSolved) Solved();
        yield return null;
    }

    IEnumerator Sleep(float HowMany) { 
        yield return new WaitForSeconds(HowMany);//Wait
    }
}
