using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Oilan.Utils;

namespace Oilan
{

    public class GameplayScoreManager : MonoBehaviour
    {

        public static GameplayScoreManager Instance;

        [Space(20)]
        [Header("GAMEPLAY")]
        public TextMeshProUGUI coinsTextGameplay;

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            ResetAllStats();
        }

        public void ResetAllStats()
        {
            UpdateAllStats();
        }

        public void UpdateAllStats()
        {
            //COINS
            coinsTextGameplay.GetComponent<UI_CurveNumberChanger>().SetNumber(ScoreManager.Instance.coins);
        }

        public void AddWebStars(int HowMuch = 1) {
            if (SaveGameManager.Instance.mSaveData.level > GameplayManager.Instance.next_level_num) return;
            if (SaveGameManager.Instance.mSaveData.step >= GameplayManager.Instance.next_step_num) return;
            WebGLMessageHandler.Instance.AddWebsiteStar(HowMuch);
            ScoreManager.Instance.coins += HowMuch;
            UpdateAllStats();
        }
    }
}