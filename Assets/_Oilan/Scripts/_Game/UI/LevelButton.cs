using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oilan
{
    public class LevelButton : MonoBehaviour
    {

        public string levelID;

        public bool isUnlocked = false;
        public bool isBought = false;

        public SpriteRenderer spriteActive;
        public SpriteRenderer spriteDisabled;
        public GameObject UIError;
        public GameObject UIBuyLevel;

        private void Start()
        {
            UpdateState();
        }

        public void UpdateState()
        {
            spriteActive.gameObject.SetActive(isUnlocked);
            spriteDisabled.gameObject.SetActive(!isUnlocked);
        }

        public void OpenLevel()
        {
            //Debug.Log("button is clicked!"+gameObject.name);
            if (isUnlocked)
            {
                if (isBought) {
                    LevelsStepsManager.Instance.LoadStepsPage(levelID);
                    GameStateParametersManager.Instance.SetTrigger("ShowStepSelect");
                } else {
                    UIBuyLevel.SetActive(true);
                }
            } else {
                UIError.SetActive(true);
            }
        }

    }
}