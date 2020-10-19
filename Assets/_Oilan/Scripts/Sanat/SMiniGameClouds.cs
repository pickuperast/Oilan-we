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
    public AudioClip _Zv_36;
    public AudioClip _Au_P3_55;
    public BoxCollider2D _CloudCollider;
    public GameObject UIContinue;
    public List<Animator> _CloudAnims;
    public GameObject _GrayBG;
    public GameObject _DroppableItem;

    private bool isDroppableDrop = false;
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

        //Если есть предмет над облаками, то он падает на землю
        if (_DroppableItem != null && isDroppableDrop)
        {
            if (_DroppableItem.transform.position != Vector3.zero)
            {
                float step = 0.5f * Time.deltaTime; // calculate distance to move
                transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, step);

                // Check if the position of the cube and sphere are approximately equal.
                if (Vector3.Distance(transform.position, Vector3.zero) < 0.001f)
                {
                    StartCoroutine(CorWhenPubDroppableItemInPlate());
                }
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

        }
    }

    IEnumerator CorWhenPubDroppableItemInPlate()
    {
        isDroppableDrop = false;
        _DroppableItem.SetActive(false);
        _CharacterAli.GetComponent<Animator>().SetBool("isSunCrystalEquipped", true);
        _CharacterAli.GetComponent<Animator>().SetTrigger("ali_r37_svodit_vmeste ruki");
        yield return new WaitForSeconds(0.4f);
        _CharacterAli.GetComponent<Animator>().SetBool("isSunCrystalEquipped", false);
        //Персонаж радостный держит жёлтый деталь: кристалл солнца, достаёт из рюкзака деталь: плиту и крепит деталь: кристалл солнца на пустое место со звуком Zv-18и кладет плиту в рюкзак (ali_r11) 
        //Персонаж говорит(ali_r78): Au - P3 - 55
        _global_audio.clip = _Au_P3_55;
        _global_audio.Play();
        yield return new WaitForSeconds(8.0f);
        _CharacterAli.GetComponent<PlayerController>().TurnPlayerControllsOnOff(true);
    }
}