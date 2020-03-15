using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Oilan.Utils;

namespace Oilan
{

    public class ScoreManager : MonoBehaviour
    {

        public static ScoreManager Instance;

        public int coins
        {
            get
            {
                return _coins;
            }
            set
            {
                if (_coins != value)
                {
                    _coins = Mathf.Max(0, value);

                    PrefsCenter.Coins = _coins;

                    UpdateAllStats();
                }
            }
        }
        [SerializeField]
        private int _coins;

        [Space(20)]
        [Header("GAMEPLAY")]
        public TextMeshProUGUI coinsTextMainMenu;

        void Awake()
        {
            Instance = this;

        }

        void Start()
        {
            ResetAllStats();
            coins = 0;
        }

        public void UpdateValues()
        {
            //
        }

        public void ResetAllStats()
        {
            // COINS
            coins = PrefsCenter.Coins;

            UpdateAllStats();
        }

        public void UpdateAllStats()
        {
            UpdateValues();

            //COINS
            coinsTextMainMenu.GetComponent<UI_CurveNumberChanger>().SetNumber(_coins);

        }


        // COINS

        public void ClearCoins()
        {
            PrefsCenter.Coins = 0;

            ResetAllStats();
        }

        public void AddCoins(int value = 1)
        {
            coins += value;
        }


        // SPEND COINS

        public bool SpendCoins(int value)
        {
            bool success = false;
            if (CanSpend(value))
            {
                coins -= value;
                success = true;
            }
            else
                Quasarlog.Log("Cannot spend so much!");

            return success;
        }

        public bool CanSpend(int value)
        {
            return (value <= _coins);
        }


    }
}