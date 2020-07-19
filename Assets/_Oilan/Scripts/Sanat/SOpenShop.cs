using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOpenShop : MonoBehaviour
{
    public void OpenShop()
    {
        Oilan.WebGLMessageHandler.Instance.PubOpenShop();
    }
}
