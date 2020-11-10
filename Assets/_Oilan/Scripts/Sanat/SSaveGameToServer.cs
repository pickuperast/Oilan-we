using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Oilan
{
    public class SSaveGameToServer : MonoBehaviour
    {
        void Start()
        {
            transform.localPosition = Vector3.zero;
            gameObject.SetActive(false);
            GameplayManager.Instance.FinalUI = gameObject;
            WebGLMessageHandler.Instance.ConsoleLog("FinalUI setted up");
        }

        private void OnEnable()
        {
            StartCoroutine(SaveGame());
        }

        IEnumerator SaveGame()
        {
            yield return new WaitForSeconds(.5f);//Да топорно, но работает =)
            if (gameObject.active) { 
                WebGLMessageHandler.Instance.ConsoleLog("calling when step was finished");
                GameplayManager.Instance.WhenStepWasFinished();
            }
        }
    }
}