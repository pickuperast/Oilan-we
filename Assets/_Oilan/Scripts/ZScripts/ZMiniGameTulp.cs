using Oilan;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZMiniGameTulp : MonoBehaviour
{
    public float checkDelay;
    public AudioSource _global_audio;
    public AudioClip _Au_igra_46;
    public Oilan.ProblemFlashCardStairs[] problems;
    public GameObject btn;
    public GameObject _UINumbers;

    void Start()
    {
        Canvas _canvas = transform.GetChild(0).GetComponent<Canvas>();
        _canvas.worldCamera = Camera.main;
        _canvas.sortingLayerName = "UI";

        StartCoroutine(LaunchMiniGame());
    }

    public IEnumerator LaunchMiniGame()
    {
        _global_audio.clip = _Au_igra_46;
        _global_audio.Play();

        yield return new WaitForSeconds(_Au_igra_46.length);

        yield return new WaitForSeconds(0.2f);
        _UINumbers.SetActive(true);
        btn.SetActive(true);       
    }

    public void CheckSolved()
    {
        StartCoroutine(CheckSolvedCoroutine());
    }

    private IEnumerator CheckSolvedCoroutine()
    {
        bool _isSolved = true;

        for (int i = 0; i < problems.Length; i++)
        {
            if (problems[i].currentState != ProblemFlashCardState.SOLVED)
            {
                problems[i].CheckAnswer();

                if (problems[i].currentState == ProblemFlashCardState.SOLVED)
                    SAudioManagerRef.Instance.PlayAudioFromTimeline("Zv-3 (Характерный звук - издается в случае правильного ответа )");
                yield return new WaitForSeconds(checkDelay);

                if (problems[i].currentState != ProblemFlashCardState.SOLVED)
                {
                    _isSolved = false;
                }
                else
                {
                    problems[i].gameObject.SetActive(false);
                }
            }

            if (i == problems.Length - 1 && !_isSolved)
            {
                btn.SetActive(true);
            }
        }

        if (_isSolved)
        {
            Solved();
        }

        yield return null;
    }
    public void Solved()
    {
        _UINumbers.SetActive(false);
        gameObject.SetActive(false);
    }

}
