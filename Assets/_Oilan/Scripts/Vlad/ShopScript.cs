using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ShopScript : MonoBehaviour
{
    public GameObject MapButton;
    public GameObject ShopButton;

    public void OpenShop()
    {
        //ShopButton.SetActive(false);
        gameObject.SetActive(true);
        MapButton.SetActive(false);
        ShopButton.SetActive(false);
    }
    public void CloseShop()
    {
        gameObject.SetActive(false);
        MapButton.SetActive(true); 
        ShopButton.SetActive(true);
    }

}
