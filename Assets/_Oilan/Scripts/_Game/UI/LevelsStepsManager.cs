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

        [Header("Error UI when lvl/step closed")]
        public GameObject UIError;

        [Header("Для этих юзеров степы не будут блокироваться")]
        public List<int> AdminUserId;
        private void Awake()
        {
            Instance = this;
        }

        //блокируем все предыдущие степы для игрока. Запрос от 2020 10 23
        void BlockStepsFromSecondApproach(GameObject _newStepsPage)
        {
            foreach (int id in AdminUserId)//Если ид в списке админов, то не блокируем степы для игрока
                if (SaveGameManager.Instance.mSaveData.id == id) return;

            SStepsCheck _SStepsCheck = _newStepsPage.GetComponent<SStepsCheck>();
            foreach (var stepButton in _SStepsCheck.steps)
            {
                if (SaveGameManager.Instance.mSaveData.level == _SStepsCheck.this_level && SaveGameManager.Instance.mSaveData.step == int.Parse(stepButton.stepSceneID.Substring(2, 1))) continue;
                stepButton.isUnlocked = false;
                stepButton.UpdateState();
            }
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
                                if (j + 1 <= SaveGameManager.Instance.mSaveData.step)//compare max unlocked step
                                {
                                    //unlock step
                                    newStepsPage.GetComponent<SStepsCheck>().steps[j].isUnlocked = true;
                                    newStepsPage.GetComponent<SStepsCheck>().steps[j].UpdateState();

                                    //блокируем все предыдущие степы для игрока. Запрос от 2020 10 23
                                    BlockStepsFromSecondApproach(newStepsPage);
                                }
                            }
                        }

                     //   newStepsPage.GetComponent<StepButton>().UIError = UIError;//add link to error ui from existing main scene
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