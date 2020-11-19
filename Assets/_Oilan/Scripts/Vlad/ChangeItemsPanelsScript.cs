using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeItemsPanelsScript : MonoBehaviour
{
    public GameObject SecondBtn;
    public GameObject ThirdBtn;
    public GameObject ClothesPnl;
    public GameObject PetsPnl;
    public GameObject EffectsPnl;
    private Color ActiveColor;
    private Color UnactiveColor;
    // Start is called before the first frame update
    void Start()
    {
        ActiveColor = new Color32(25, 110, 0, 255);
        UnactiveColor = new Color32(25, 110, 0, 100);
    }

  
    public void SetClothesPanelActive()
    {
        ClothesPnl.SetActive(true);
        PetsPnl.SetActive(false);
        EffectsPnl.SetActive(false);
        gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        SecondBtn.transform.localScale = new Vector3(1, 1, 1);
        ThirdBtn.transform.localScale = new Vector3(1, 1, 1);

       /* gameObject.GetComponent<Renderer>().material.color = ActiveColor;
        SecondBtn.GetComponent<Renderer>().material.color = UnactiveColor;
        ThirdBtn.GetComponent<Renderer>().material.color = UnactiveColor;*/
    }
    public void SetPetsPanelActive()
    {
        ClothesPnl.SetActive(false);
        PetsPnl.SetActive(true);
        EffectsPnl.SetActive(false);
        gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        SecondBtn.transform.localScale = new Vector3(1, 1, 1);
        ThirdBtn.transform.localScale = new Vector3(1, 1, 1);
        /*gameObject.GetComponent<Renderer>().material.color = ActiveColor;
        SecondBtn.GetComponent<Renderer>().material.color = UnactiveColor;
        ThirdBtn.GetComponent<Renderer>().material.color = UnactiveColor;*/
    }
    public void SetEffectsPanelActive()
    {
        ClothesPnl.SetActive(false);
        PetsPnl.SetActive(false);
        EffectsPnl.SetActive(true);
        gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        SecondBtn.transform.localScale = new Vector3(1, 1, 1);
        ThirdBtn.transform.localScale = new Vector3(1, 1, 1);
      /*  gameObject.GetComponent<Renderer>().material.color = ActiveColor;
        SecondBtn.GetComponent<Renderer>().material.color = UnactiveColor;
        ThirdBtn.GetComponent<Renderer>().material.color = UnactiveColor;*/
    }
    public void ChangePanels()
    {
        if (gameObject.tag == "ClothesButton")
        {
            ClothesPnl.SetActive(true);
            PetsPnl.SetActive(false);
            EffectsPnl.SetActive(false);
            gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
          
        }
        if (gameObject.tag == "PetsButton")
        {
            ClothesPnl.SetActive(false);
            PetsPnl.SetActive(true);
            EffectsPnl.SetActive(false);
            gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        }
        if (gameObject.tag == "EffectsButton")
        {
            ClothesPnl.SetActive(false);
            PetsPnl.SetActive(false);
            EffectsPnl.SetActive(true);
            gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        }
    }


}
