using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Oilan
{
    public class SSaveGameToServer : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Save Game");
            GameplayManager.Instance.WhenStepWasFinished();
        }
    }
}