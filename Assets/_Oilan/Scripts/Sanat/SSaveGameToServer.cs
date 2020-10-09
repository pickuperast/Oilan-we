using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Oilan
{
    public class SSaveGameToServer : MonoBehaviour
    {
        public GameplayManager _GameplayManager; 
        // Start is called before the first frame update
        void Start()
        {
            _GameplayManager.WhenStepWasFinished();
            //GameplayManager.Instance.WhenStepWasFinished();
        }
    }
}