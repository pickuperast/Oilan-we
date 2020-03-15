using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oilan
{
    public class LevelButton : MonoBehaviour
    {

        public string levelID;

        public bool isUnlocked = false;

        public SpriteRenderer spriteActive;
        public SpriteRenderer spriteDisabled;

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
                LevelsStepsManager.Instance.LoadStepsPage(levelID);

                GameStateParametersManager.Instance.SetTrigger("ShowStepSelect");
            }
        }

    }
}