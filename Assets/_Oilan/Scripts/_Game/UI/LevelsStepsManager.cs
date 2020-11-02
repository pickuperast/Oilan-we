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

        private void Awake()  {Instance = this;}

        //called when level selection button is pressed, method creates prefab which contains all steps
        public void LoadStepsPage(string levelID)
        {
            currentLevelID = levelID;
            SStepsCheck _SStepsCheck = null;
            ClearStepsPage();
            bool isAdmin = false;

            foreach (int id in AdminUserId)//Если ид в списке админов, то не блокируем степы для игрока
                if (SaveGameManager.Instance.mSaveData.id == id) isAdmin = true;

            //если открыт уровень на котором прогресс:
            for (int i = 0; i < stepsPageElements.Count; i++)//цикл для поиска подходящего префаба, в префабе уже созданы кнопки степов
            {
                if (stepsPageElements[i].name == levelID)//load level like in pressed button
                {
                    if (stepsPageElements[i].prefab != null)
                    {
                        //Creating
                        GameObject newStepsPage = Instantiate(stepsPageElements[i].prefab, stepsPageContainer.transform);
                        _SStepsCheck = newStepsPage.GetComponent<SStepsCheck>();
                        //blocking steps
                        //steps in db starts from 1, but in our list it starts from 0
                        //Если level который открываем меньше max level прогресса, то открываем все степы
                        if (i + 1 < SaveGameManager.Instance.mSaveData.level)//SaveGameManager.Instance.mSaveData.level = max opened level
                        {
                            foreach (var stepButton in _SStepsCheck.steps)
                                stepButton.UpdateState(true);

                        }//Если level который открываем равен max level прогресса, то открываем степы до max степ прогресса (включительно)
                        else if (i + 1 == SaveGameManager.Instance.mSaveData.level)//if equals, we compare with mSaveData.step
                        {
                            ////блокируем все предыдущие степы для игрока. Запрос от 2020 10 23
                            if (isAdmin)
                            {
                                for (int j = 0; j < _SStepsCheck.steps.Capacity; j++)
                                {
                                    if (j + 1 <= SaveGameManager.Instance.mSaveData.step)//compare max unlocked step
                                        _SStepsCheck.steps[j].UpdateState(true);//unlock step
                                }
                            }
                            else
                                _SStepsCheck.steps[SaveGameManager.Instance.mSaveData.step - 1].UpdateState(true);//unlock step, потому что степ в БД(1), в листе(0)
                            //открываем все предыдущие степы для игрока
                            //for (int j = 0; j < _SStepsCheck.steps.Capacity; j++)
                            //{
                            //    if (j + 1 <= SaveGameManager.Instance.mSaveData.step)//compare max unlocked step
                            //        _SStepsCheck.steps[j].UpdateState(true);//unlock step
                            //    else
                            //        break;
                            //}
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