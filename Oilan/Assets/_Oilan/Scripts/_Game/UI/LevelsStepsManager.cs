using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oilan
{

    [Serializable]
    public class StepsPageElement
    {
        public string name;

        public GameObject prefab;
    }


    public class LevelsStepsManager : MonoBehaviour
    {
        public static LevelsStepsManager Instance;

        [Header("Containers")]
        public GameObject levelsPageContainer;
        public GameObject stepsPageContainer;
        private List<GameObject> stepsPageObjects = new List<GameObject>();

        [Header("Steps Pages Prefabs")]
        public List<StepsPageElement> stepsPageElements;

        public string currentLevelID;
        public string currentStepID;

        private void Awake()
        {
            Instance = this;
        }

        //called when level selection button is pressed, method creates prefab which contains all steps
        public void LoadStepsPage(string levelID)
        {
            currentLevelID = levelID;

            ClearStepsPage();

            for (int i = 0; i < stepsPageElements.Count; i++)//цикл для поиска подходящего префаба, в префабе уже созданы кнопки степов
            {
                if (stepsPageElements[i].name == levelID)//load level like in pressed button
                {
                    if (stepsPageElements[i].prefab != null)
                    {
                        //Creating
                        GameObject newStepsPage = Instantiate(stepsPageElements[i].prefab, stepsPageContainer.transform);
                        //blocking steps
                        //steps in db starts from 1, but in our list it starts from 0
                        if (i + 1 < SaveGameManager.Instance.mSaveData.level)//SaveGameManager.Instance.mSaveData.level = max opened level
                        {
                            foreach (var stepButton in newStepsPage.GetComponent<SStepsCheck>().steps)
                            {
                                stepButton.isUnlocked = true;
                                stepButton.UpdateState();
                            }
                        } else if (i + 1 == SaveGameManager.Instance.mSaveData.level)//if equals, we compare with mSaveData.step
                        {
                            for (int j = 0; j < newStepsPage.GetComponent<SStepsCheck>().steps.Capacity; j++)
                            {
                                if (SaveGameManager.Instance.mSaveData.step>=j+1)//compare max unlocked step
                                {
                                    //unlock step
                                    newStepsPage.GetComponent<SStepsCheck>().steps[j].isUnlocked = true;
                                    newStepsPage.GetComponent<SStepsCheck>().steps[j].UpdateState();
                                }
                            }
                        }
                        stepsPageObjects.Add(newStepsPage);
                    }
                    break;
                }
            }
        }

        private void ClearStepsPage()
        {
            foreach (GameObject item in stepsPageObjects)
            {
                Destroy(item);
            }
        }

    }
}