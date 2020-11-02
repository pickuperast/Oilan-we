using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oilan
{
    public class StepButton : MonoBehaviour
    {

        public string stepSceneID;

        public bool isUnlocked = false;

        public SpriteRenderer spriteActive;
        public SpriteRenderer spriteDisabled;
        public GameObject UIError;

        private void Start()
        {
            //UpdateState(false);
            UIError = GameObject.Find("UIerror blocked step");
        }

        public void UpdateState(bool isOn)
        {
            isUnlocked = isOn;
            spriteActive.gameObject.SetActive(isUnlocked);
            spriteDisabled.gameObject.SetActive(!isUnlocked);
            Debug.Log(gameObject.name);
        }

        public void OpenStep()
        {
            if (isUnlocked)
            {
                LevelsStepsManager.Instance.currentStepID = stepSceneID;
                GameStateParametersManager.Instance.SetBoolTrue("Bool_ShowNewGame");
            }
            else
            {
                UIError.SetActive(true);
            }
            
        }
    }
}