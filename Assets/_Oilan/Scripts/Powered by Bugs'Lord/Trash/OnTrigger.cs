using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using Oilan;
public class OnTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject game;
    [SerializeField]
    private AudioSource _global_audio;
    [SerializeField]
    private AudioClip Au_igra_45;
    [SerializeField]
    private AudioClip _Au_P3_55;
    [SerializeField]
    private GameObject UIContinue;
    [SerializeField]
    private GameObject exercises;
    [SerializeField]
    private Sprite sunSprite;
    private bool sun = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player") {
            PlayerController.Instance.TurnPlayerControllsOnOff(false);
            if(Au_igra_45 != null)
                StartCoroutine(PlayAudio());
            game.SetActive(true);
            game.transform.Find("Check").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(Check);
            GetComponent<BoxCollider2D>().enabled = false;
        }
        if(collision.tag == "sun") {
            sun = true;
        }
    }
    void Check()
    {
        GameObject leafs = game.transform.Find("leafs").gameObject;
        Toggle[] toggles = leafs.GetComponentsInChildren<Toggle>();
        int sum = 0;
        foreach( var toggle in toggles) {
            if(toggle.isOn && toggle.gameObject.name.Contains("Green")) {
                sum++;
            }
        }
        Debug.Log(sum);
        if(sum == 5) {
            StartCoroutine(onDestroy(toggles));
        }
    }
    IEnumerator PlayAudio()
    {
        _global_audio.clip = Au_igra_45;
        _global_audio.Play();
        yield return new WaitForSeconds(Au_igra_45.length);
    }

    IEnumerator onDestroy(Toggle[] toggles)
    {
        foreach(var toggle in toggles) {
            if (!toggle.isOn && toggle.gameObject.name.Contains("Yellow")) {
                while (toggle.GetComponent<CanvasGroup>().alpha >= .005f) {
                    toggle.GetComponent<CanvasGroup>().alpha -= toggle.GetComponent<CanvasGroup>().alpha / 10;
                    yield return new WaitForSeconds(.02f);
                }
                toggle.enabled = false;
            }
        }
        game.transform.Find("Check").gameObject.SetActive(false);
        exercises.SetActive(true);
        exercises.GetComponent<Temirlan.Series>().onDestroy +=  () => StartCoroutine(OpenStars());
    }
    public IEnumerator OpenStars (){
        GameObject leafs = game.transform.Find("leafs").gameObject;
        Toggle[] toggles = leafs.GetComponentsInChildren<Toggle>();
        int i = 0;
        foreach(var toggle in toggles) {
            if (toggle.gameObject.name.Contains("Green")) {
                i++;
                Debug.LogError(i);
                if(i == 3) {
                    GameObject obj = toggle.gameObject;
                    Destroy(toggle);

                    obj.transform.GetChild(0).gameObject.SetActive(false);
                    obj.AddComponent<Image>().sprite = sunSprite;
                    continue;
                } else {
                    while (toggle.GetComponent<CanvasGroup>().alpha >= .005f) {
                        toggle.GetComponent<CanvasGroup>().alpha -= toggle.GetComponent<CanvasGroup>().alpha / 10;
                        yield return new WaitForSeconds(.02f);
                    }
                    GameplayScoreManager.Instance.AddWebStars();
                    AudioManager.Instance.PlaySound("Zv-9 (Волшебный звук для звезды (отлетают на табло в меню “Награды”))");
                }
            }
        }
        StartCoroutine(CorWhenPubDroppableItemInPlate());
    }

    public IEnumerator CorWhenPubDroppableItemInPlate()
    {
        game.SetActive(false);
        GameObject _CharacterAli = PlayerController.Instance.GetComponent<Character_Ali>().gameObject;
        yield return new WaitForSeconds(4f);
        _CharacterAli.GetComponent<Animator>().SetBool("isSunCrystalEquipped", true);
        _CharacterAli.GetComponent<Animator>().SetTrigger("ali_r37_svodit_vmeste ruki");
        yield return new WaitForSeconds(0.4f);
        _CharacterAli.GetComponent<Animator>().SetBool("isSunCrystalEquipped", false);
        //Персонаж радостный держит жёлтый деталь: кристалл солнца, достаёт из рюкзака деталь: плиту и крепит деталь: кристалл солнца на пустое место со звуком Zv-18и кладет плиту в рюкзак (ali_r11) 
        //Персонаж говорит(ali_r78): Au - P3 - 55
        _CharacterAli.GetComponent<Animator>().SetBool("Talk", true);
        _global_audio.clip = _Au_P3_55;
        _global_audio.Play();
        yield return new WaitForSeconds(8.0f);
        _CharacterAli.GetComponent<Animator>().SetBool("Talk", false);
        _CharacterAli.GetComponent<PlayerController>().TurnPlayerControllsOnOff(true);
    }

    void Continue()
    {
        game.SetActive(false);
        
        PlayerController.Instance.TurnPlayerControllsOnOff(true);
    }
    public bool GetCollider()
    {
        return sun;
    }
}
