using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Oilan;

public class SMiniGameClouds : MonoBehaviour
{
    public GameObject _CharacterAli;
    public AudioSource _global_audio;
    public AudioClip _Au_igra_44;
    public AudioClip _Au_P3_53;
    public AudioClip _Zv_36;
    public AudioClip _Au_P3_55;
    public BoxCollider2D _CloudCollider;
    public GameObject UIContinue;
    public List<Animator> _CloudAnims;
    public GameObject _GrayBG;
    public GameObject _DroppableItem;
    public GameObject bush;
    private bool isDroppableDrop = false;
    private bool isTracking = true;
    void OnTriggerEnter2D(Collider2D collision)
    {//Не делаем проверку, потому что слой Interractable пересекается только с Али
        StartCoroutine(Sounding());
        GetComponent<BoxCollider2D>().enabled = false;
        PlayerController.Instance.TurnPlayerControllsOnOff(false);
        
    }
    private IEnumerator Sounding()
    {
        _CharacterAli.GetComponent<Animator>().SetTrigger("ali_r39");
        //yield return new WaitForSeconds(5f);
        _CharacterAli.GetComponent<Animator>().SetBool("Talk", true);
        _global_audio.clip = _Au_P3_53;
        _global_audio.Play();
        yield return new WaitForSeconds(_Au_P3_53.length);
        _global_audio.clip = _Au_igra_44;
        _global_audio.Play();
        yield return new WaitForSeconds(_Au_igra_44.length);
        _CharacterAli.GetComponent<Animator>().SetBool("Talk", false);
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
                _CloudCollider.enabled = false;

                StartCoroutine(CorrectAnswer());
            }
        }

        //Если есть предмет над облаками, то он падает на землю
        if (_DroppableItem != null && isDroppableDrop)
        {
            if (_DroppableItem.transform.position.x > 1.5f) {

                float step = 0.5f * Time.deltaTime; // calculate distance to move
                _DroppableItem.transform.Translate(-(_DroppableItem.transform.position - transform.position) * Time.deltaTime * 0.5f);

                // Check if the position of the cube and sphere are approximately equal.
                if (Vector3.Distance(_DroppableItem.transform.position, Vector3.zero) <= 84f) {
                    _DroppableItem.GetComponent<SpriteRenderer>().sortingLayerName = "Objects_Back";
                    _DroppableItem.GetComponent<SpriteRenderer>().sortingOrder = 10;
                    _DroppableItem.GetComponent<Rigidbody2D>().gravityScale = 1f;
                    isDroppableDrop = false;
                    bush.GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }/*
        if (_DroppableItem.GetComponent<Rigidbody2D>().gravityScale > 0) {
            if (bush.transform.position.x - _DroppableItem.transform.position.x > 1) {
                _DroppableItem.transform.Translate(Vector3.right * Time.deltaTime * 1.5f);
            } else {
                _DroppableItem.SetActive(false);
            }
        }*/
    }
    IEnumerator CorrectAnswer()
    {
        _global_audio.clip = _Zv_36;
        _global_audio.Play();
        PlayerController.Instance.TurnPlayerControllsOnOff(false);
        yield return new WaitForSeconds(3.8f);
        GameplayTheoryManager.Instance.openExternalTrainerString("abacus");
        //находим кнопку, которая будет запускать след. действие
        Button btn = UIContinue.transform.GetChild(0).GetComponent<Button>();
        btn.onClick.AddListener(WhenTrainerFinished);
        UIContinue.SetActive(true);
    }

    public void WhenTrainerFinished()
    {
        UIContinue.SetActive(false);
        StartCoroutine(CorWhenTrainerFinished());
    }

    IEnumerator CorWhenTrainerFinished()
    {
        //После того, как Пользователь решит все задачи, Цифры лопаются как мыльный пузырь с Zv-5 и исчезают
        foreach (var l_Animator in _CloudAnims)
        {
            l_Animator.SetTrigger("die");
            _GrayBG.SetActive(false);
        }
        yield return new WaitForSeconds(1.0f);

        //Если есть предмет над облаками, то он падает на землю
        if (_DroppableItem != null)
        {
            isDroppableDrop = true;
        }
    }
}