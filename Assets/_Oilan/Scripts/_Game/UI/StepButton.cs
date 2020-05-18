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
            UpdateState();
        }

        public void UpdateState()
        {
            spriteActive.gameObject.SetActive(isUnlocked);
            spriteDisabled.gameObject.SetActive(!isUnlocked);
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