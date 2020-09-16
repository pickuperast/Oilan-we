using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using System.Runtime.InteropServices;
using System;

namespace Oilan
{
    [Serializable]
    public class GameLocation
    {
        public int level;
        public int step;
        public int part;
    }

    [Serializable]
    public class GameLocationStats
    {
        public GameLocation gameLocation;

        public bool isUnlocked;

        public bool isPurchased;

        public int starsMax;
        public int starsCollected;

    }

    public class SaveGameManager : MonoBehaviour
    {
        public static SaveGameManager Instance;
        //'{"level":1,"timeElapsed":47.5,"playerName":"Dr Charles Francis"}'
        //{"userID":18,"starsTotal":95,"level":1,"step":1,"part":1}
        [SerializeField]
        private int userID;

        [SerializeField]
        private string progress;

        [SerializeField]
        private int starsTotal;

        [SerializeField]
        private GameLocation locationUnlockedMax;
        private GameLocation locationLast;

        [SerializeField]
        [Header("Must be in order of gameplay")]
        public List<GameLocationStats> gameLocationsStats;

        public TextMeshProUGUI TextSavedStats;
        public Text MainMenuStarsScore;

        [SerializeField]
        public SaveData mSaveData;
        public List<LevelButton> levelButtons;


        void Start()
        {
            //need to turn off if want to show in github
            PullProgress();
            //PushProgress();
        }

        void PullProgress()
        {
            Instance = this;
            mSaveData = WebGLMessageHandler.Instance.GetData();
            //If we send level 2 it means level 2 is unlocked and should be active
            //------------Unlock levels
            //mSaveData.level starts from 1 == id 0 in list
            //total 10 levels
            for (int i = 0; i < 10; i++)
            {
                if (i <= mSaveData.count_level)
                {
                    levelButtons[i].isBought = true;
                }
                if (i < mSaveData.level)
                {
                    levelButtons[i].isUnlocked = true;
                    levelButtons[i].UpdateState();
                }
                //-----------Unlock steps in levels
            }
            for (int i = 0; i < mSaveData.level; i++)//mSaveData.level starts from 1 == id 0 in list
            {
                levelButtons[i].isUnlocked = true;
                levelButtons[i].UpdateState();
                //-----------Unlock steps in levels
            }
            UpdateStarsScoreText();
            string log = "Loaded: ";
            log += "id: " + mSaveData.id + "; ";
            log += "user_id: " + mSaveData.user_id + "; ";
            log += "level: " + mSaveData.level + "; ";
            log += "step: " + mSaveData.step + "; ";
            log += "part: " + mSaveData.part + "; ";
            log += "stars: " + mSaveData.stars + "; ";
            WebGLMessageHandler.Instance.ConsoleLog(log);
            //TextSavedStats.text = log;
        }

        void UpdateStarsScoreText()
        {
            MainMenuStarsScore.text = mSaveData.stars.ToString();
        }

        public void SetPartFinished(int i_level, int i_step, int i_part)
        {
            if (i_part > mSaveData.part) {
                mSaveData.part = i_part;
            }
        }
        //Этот метод вызывается в конце степа и сохраняет прогресс, если уровень или пройденный степ больше того, что было
        //Необходимо отправлять не номер текущего степа, а номер следующего открывшегося степа
        //например если пройден последний степ 1 уровня, отправить i_level = 2, i_step = 1
        public void SaveProgress(int i_level, int i_step)
        {
            //if (i_level > mSaveData.level || i_step > mSaveData.step)
            //{
                mSaveData.level = i_level;
                mSaveData.step = i_step;
                mSaveData.stars = ScoreManager.Instance.coins;//отправляем только заработанное количество звезд
                PushProgress();
            //}
            
        }

        void PushProgress()
        {
            string SaveDataInJSON = JsonUtility.ToJson(mSaveData);
            WebGLMessageHandler.Instance.SetData(SaveDataInJSON);
        }

        public int GetUserID()
        {
            return userID;
        }

        /*
        public int GetStarsTotal()
        {
            return starsTotal;
        }

        public void SetStarsTotal(int newValue)
        {
            if (newValue > starsTotal)
            {
                starsTotal = newValue;
            }
        }
        */
        public GameLocation GetLastLocation()
        {
            return locationLast;
        }

        public GameLocation GetMaxUnlockedLocation()
        {
            return locationUnlockedMax;
        }

        public void SetLocationVisited(GameLocation newLocation)
        {
            locationLast = newLocation;

            SetLocationUnlocked(newLocation);
        }

        public void SetLocationUnlocked(GameLocation newLocation)
        {
            for (int i = 0; i < gameLocationsStats.Count - 1; i++)
            {
                if (CheckLocationsEqual(newLocation, gameLocationsStats[i].gameLocation))
                {
                    if (!gameLocationsStats[i].isUnlocked)
                    {
                        gameLocationsStats[i].isUnlocked = true;

                        if (CheckLocationsEqual(newLocation, locationUnlockedMax))
                        {
                            locationUnlockedMax = newLocation;
                        }

                        for (int j = 0; j < i; j++)
                        {
                            gameLocationsStats[j].isUnlocked = true;
                        }

                        break;
                    }
                }

            }
        }

        public bool CheckLocationsEqual(GameLocation location1, GameLocation location2)
        {
            bool returnValue = false;

            if ((location1.level == location2.level)
                && (location1.step == location2.step)
                && (location1.part == location2.part))
            {
                returnValue = true;
            }

            return returnValue;
        }


    }
}
