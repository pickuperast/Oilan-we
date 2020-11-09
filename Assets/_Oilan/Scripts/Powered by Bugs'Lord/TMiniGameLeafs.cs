using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Oilan;

public class TMiniGameLeafs : MonoBehaviour
{
    [SerializeField] GameObject _UI_Leafs_game;
    [SerializeField] AudioSource _global_audio;
    [SerializeField] AudioClip Au_igra_45;
    [SerializeField] AudioClip _Au_P3_55;
    [SerializeField] GameObject UIContinue;
    [SerializeField] GameObject exercises;
    [SerializeField] Sprite sunSprite;
    private bool sun = false;
    float fadeTime = .4f;
    GameObject GOLeafs = null;
    Toggle[] TOtoggles;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "sun") {
            Destroy(collision.gameObject);
        }
        if(collision.tag == "Player") {
            PlayerController.Instance.TurnPlayerControllsOnOff(false);
            StartCoroutine(PlayAudio());
            _UI_Leafs_game.SetActive(true);
            _UI_Leafs_game.transform.Find("Check").GetComponent<Button>().onClick.AddListener(Check);
            GetComponent<BoxCollider2D>().enabled = false;
        }
        if(collision.tag == "sun") {
            sun = true;
        }
    }

    void Start()
    {
        GOLeafs = _UI_Leafs_game.transform.Find("leafs").gameObject;
        TOtoggles = GOLeafs.GetComponentsInChildren<Toggle>();
        gameObject.SetActive(false);
    }
    //висит на кнопке
    void Check()
    {
        int sum = 0;
        foreach( var toggle in TOtoggles) {
            if(toggle.isOn && toggle.gameObject.name.Contains("Green")) {
                sum++;
            }
        }
        Debug.Log(sum);
        if(sum == 5) {
            _UI_Leafs_game.transform.Find("Check").GetComponent<Button>().onClick.RemoveListener(Check);
            StartCoroutine(onDestroy(TOtoggles));
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
        float frameTime = 1f / 60f;
        float fadeAmount = 0.0166f;// fadeTime / frameTime;
        foreach(var toggle in toggles) {
            if (!toggle.isOn && toggle.gameObject.name.Contains("Yellow")) {
                AudioManager.Instance.PlaySound("Zv-38 (Хруст-шелест листьев)");
                CanvasGroup _canvasGroupToggle = toggle.GetComponent<CanvasGroup>();
                while (_canvasGroupToggle.alpha >= fadeAmount) {
                    _canvasGroupToggle.alpha -= 2*fadeAmount;
                    yield return new WaitForSeconds(frameTime);
                }
                toggle.enabled = false;
            }
        }
        GameplayTheoryManager.Instance.openExternalTrainerString("fleshCart");
        _UI_Leafs_game.transform.Find("Check").GetComponent<Button>().onClick.AddListener(() => StartCoroutine(OpenStars()));

        /*
        exercises.SetActive(true);
        exercises.GetComponent<Temirlan.Series>().onDestroy +=  () => StartCoroutine(OpenStars());
        */
    }
    public IEnumerator OpenStars (){
        int i = 0;
        float fadeTime = 0.5f;
        float fadeAmount = 0.0166f / fadeTime;//0.0166 - frame time
        foreach (var toggle in TOtoggles) {
            if (toggle.gameObject.name.Contains("Green")) {
                i++;
                if(i == 3) {
                    GameObject obj = toggle.gameObject;
                    Destroy(toggle);

                    obj.transform.GetChild(0).gameObject.SetActive(false);
                    obj.AddComponent<Image>().sprite = sunSprite;
                    continue;
                } else {
                    CanvasGroup _canvas = toggle.GetComponent<CanvasGroup>();
                    while (_canvas.alpha >= fadeAmount) {
                        _canvas.alpha -= fadeAmount;
                        yield return new WaitForEndOfFrame();
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
        Character_Ali.Instance.m_Anim.SetBool("ali_eyes_right", false);
        Character_Ali.Instance.m_Anim.SetBool("ali_eyes_front", true);
        _UI_Leafs_game.SetActive(false);
        //do camera zoom
        Character_Ali.Instance.m_Anim.SetBool("isSunCrystalEquipped", true);
        //do camera zoom out
        Character_Ali.Instance.m_Anim.SetTrigger("ali_put_plate_in_backpack(useThis)");
        yield return new WaitForSeconds(1.5f);//Время через которое рука будет у рюкзака
        //отрисовываем одетую плиту
        Character_Ali.Instance.m_Anim.SetBool("isPlateWithStarEquipped", true);
        yield return new WaitForSeconds(1.72f);//опускаем руку вниз
        //надеваем камень на плиту
        Character_Ali.Instance.m_Anim.SetTrigger("ali_r37_svodit_vmeste ruki");
        yield return new WaitForSeconds(0.566f);//время необходимое для сведения руки и плиты
        //скрываем кристал
        Character_Ali.Instance.m_Anim.SetBool("isSunCrystalEquipped", false);
        yield return new WaitForSeconds(0.534f);//время необходимое для сведения руки и плиты
        Character_Ali.Instance.m_Anim.SetTrigger("ali_put_plate_in_backpack(useThis)");
        yield return new WaitForSeconds(1.5f);//Время через которое рука будет у рюкзака
        Character_Ali.Instance.m_Anim.SetBool("isPlateWithStarEquipped", false);
        Character_Ali.Instance.m_Anim.SetBool("Talk", true);
        _global_audio.clip = _Au_P3_55;
        _global_audio.Play();
        yield return new WaitForSeconds(8.0f);
        Character_Ali.Instance.SetAnimatorAli_r78_Bool_Talk(false);
        PlayerController.Instance.TurnPlayerControllsOnOff(true);
        _UI_Leafs_game.SetActive(false);
    }

    public bool GetCollider()
    {
        return sun;
    }
}
