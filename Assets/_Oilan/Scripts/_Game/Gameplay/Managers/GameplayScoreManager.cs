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

        public void AddCoins(int value = 1)
        {
            ScoreManager.Instance.coins += value;

            UpdateAllStats();
        }

        public void AddWebStars(int HowMuch = 1) {
            for (int i = 0; i < HowMuch; i++)
                WebGLMessageHandler.Instance.AddWebsiteStar();
        }
    }
}