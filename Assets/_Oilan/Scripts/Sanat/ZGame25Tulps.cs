using Oilan;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZGame25Tulps : MonoBehaviour
{

    public UnityEvent uevent;
    public AudioSource _global_audio;
    public AudioClip _Au_igra_47;
    public GameObject btn;
    public GameObject circle;
    public GameObject startPoint;
    public bool isFinish;
    void Start()
    {
        StartCoroutine(LaunchMiniGame());
    }

    public IEnumerator LaunchMiniGame()
    {
         isFinish = false;
         GameplayManager.Instance.TurnPlayerControlsOnOff(true);
        _global_audio.clip = _Au_igra_47;
        _global_audio.Play();

        yield return new WaitForSeconds(_Au_igra_47.length);

        btn.SetActive(true);
    }

    public void CheckSolved()
    {
        StartCoroutine(CheckSolvedCoroutine());
    }

    public void IsFinishOn()
    {
        isFinish = true;
    }

    public void IsFinishOff()
    {
        isFinish = false;
    }
    private IEnumerator CheckSolvedCoroutine()
    {           

        if (isFinish)
        {
            Solved();
        }
        else
        {
            circle.transform.position = startPoint.transform.position;
            _global_audio.clip = _Au_igra_47;
            _global_audio.Play();
        }

        yield return null;
    }
    public void Solved()
    {
        GameplayManager.Instance.TurnPlayerControlsOnOff(false);
        uevent.Invoke();
        gameObject.SetActive(false);
    }
}
