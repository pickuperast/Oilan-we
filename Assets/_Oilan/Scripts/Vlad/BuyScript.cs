using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject BuyButton;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IntemOnClick()
    {
        gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        BuyButton.SetActive(true);
    }
    public void ItemDeselected()
    {
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        BuyButton.SetActive(false);
    }
}
