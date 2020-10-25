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

    public Color ActiveColor = new Color32(25, 110, 0, 255);
    public Color InactiveColor = new Color32(25, 110, 0, 100);
    // Start is called before the first frame update
    void Start()
    {
        ActiveColor = new Color32(25, 110, 0, 255);
        InactiveColor = new Color32(25, 110, 0, 100);
    }
    public void SetPanelActive(GameObject firstPnl, GameObject secondPnl, GameObject thirdPnl)
    {
        firstPnl.SetActive(true);
        secondPnl.SetActive(false);
        thirdPnl.SetActive(false);
        firstPnl.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        secondPnl.transform.localScale = Vector3.one;
        thirdPnl.transform.localScale = Vector3.one;
    }
  
    public void SetClothesPanelActive()
    {
        SetPanelActive(ClothesPnl, PetsPnl, EffectsPnl);
        
       /* gameObject.GetComponent<Renderer>().material.color = ActiveColor;
        SecondBtn.GetComponent<Renderer>().material.color = UnactiveColor;
        ThirdBtn.GetComponent<Renderer>().material.color = UnactiveColor;*/
    }
    public void SetPetsPanelActive()
    {
        SetPanelActive(PetsPnl, ClothesPnl, EffectsPnl);
        /*gameObject.GetComponent<Renderer>().material.color = ActiveColor;
        SecondBtn.GetComponent<Renderer>().material.color = UnactiveColor;
        ThirdBtn.GetComponent<Renderer>().material.color = UnactiveColor;*/
    }
    public void SetEffectsPanelActive()
    {
        SetPanelActive(EffectsPnl, PetsPnl, ClothesPnl);
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
