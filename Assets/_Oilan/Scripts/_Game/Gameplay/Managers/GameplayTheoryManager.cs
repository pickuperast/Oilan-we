using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oilan
{
    public class GameplayTheoryManager : MonoBehaviour
    {
        public static GameplayTheoryManager Instance;

        public List<GameObject> theoryScreens;
        public string TrainerType;
        public int level;
        public int step;

        void Awake()
        {
            Instance = this;
        }

        public void CleanupTheoryScreens()
        {
            foreach (var item in theoryScreens)
            {
                item.SetActive(false);
            }
        }

        //ТУ – "multiplicationTable"
        //АА – "abacus"
        //ФК – "fleshCart"
        //МА – "mental"
        public void openExternalTrainer()
        {
            WebGLMessageHandler.Instance.PubOpenTrainer(TrainerType, level, step);
        }

        public void openExternalTrainerString(string l_type)
        {
            WebGLMessageHandler.Instance.PubOpenTrainer(l_type, level, step);
        }
    }
}