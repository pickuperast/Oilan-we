using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMiniGameTriDereva : MonoBehaviour
{
    public List<GameObject> _Trees;
    public int _correctTreeNum = 2;//0,1,2
    public Animator _ali;
    public Animator _starik;
    public SAudioManagerRef _audioManager;
    public string _audioCorrect = "Zv-3 (Характерный звук - издается в случае правильного ответа )";
    public string _audioWrong = "Zv-2 (Характерный звук - издается в случае неправильного ответа)";
    public GameObject UIContinue;


    private bool isTracking = true;

    // Update is called once per frame
    void Update()
    {
        if (isTracking && Input.GetMouseButtonDown(0))
        {
            Oilan.WebGLMessageHandler.Instance.ConsoleLog("Mouse clicked");
            RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            if (hit.collider.gameObject == _Trees[_correctTreeNum])
            {
                _ali.SetBool("Talk", false);
                Oilan.WebGLMessageHandler.Instance.ConsoleLog("Correct tree!");
                StartCoroutine(CorrectAnswer());
            }
            else if(hit.collider.gameObject == _Trees[0] || hit.collider.gameObject == _Trees[1] || hit.collider.gameObject == _Trees[2])
            {
                _ali.SetBool("Talk", false);
                Oilan.WebGLMessageHandler.Instance.ConsoleLog("Wrong tree!");
                _audioManager.PlayAudioFromTimeline(_audioWrong);
            }
        }
    }

    IEnumerator CorrectAnswer()
    {
        isTracking = false;
        _audioManager.PlayAudioFromTimeline(_audioCorrect);
        yield return new WaitForSeconds(1f);
        Oilan.GameplayTheoryManager.Instance.openExternalTrainerString("fleshCart");
        UIContinue.SetActive(true);
    }

    public void PubContinueAfterTrainerFinished()
    {
        StartCoroutine(ContinueAfterTrainerFinished());//Потому что WebGl не может с кнопки вызывать курутину
    }

    IEnumerator ContinueAfterTrainerFinished()
    {
        Oilan.WebGLMessageHandler.Instance.ConsoleLog("Continue button pressed");
        UIContinue.SetActive(false);
        //play activate crystal on starik
        _starik.SetTrigger("activate_krystall");
        _audioManager.PlayAudioFromTimeline("Zv-20 (Звук волшебства)");
        yield return new WaitForSeconds(1f);
        _ali.SetTrigger("ali_r47_surprise_no_smile");
        yield return new WaitForSeconds(3.667f + 1.35f);
        Oilan.GameplayTimelineManager.Instance.PlayNextTimeline();
        gameObject.SetActive(false);
    }
}
