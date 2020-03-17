using UnityEngine;
using System.Collections;
using System;

namespace Oilan
{
    /// <summary>
    /// Class is charge to set and get PlayerPrefs values
    /// </summary>
    public class PrefsCenter : MonoBehaviour
    {
        public static void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
        }



        /// <summary>
        /// Prefs, which shows: Is sound on?
        /// </summary>
        public static bool IsSoundOn
        {
            get { return PlayerPrefsX.GetBool("IsSoundOn"); }
            set { PlayerPrefsX.SetBool("IsSoundOn", value); PlayerPrefs.Save(); }
        }

        /// <summary>
        /// Prefs, which shows: Is music muted?
        /// </summary>
        public static string MuteMusic
        {
            get { return PlayerPrefs.GetString("MuteMusic"); }
            set { PlayerPrefs.SetString("MuteMusic", value); PlayerPrefs.Save(); }
        }
        
        /// <summary>
        /// Prefs, which shows: Is SFX muted?
        /// </summary>
        public static string MuteSFX
        {
            get { return PlayerPrefs.GetString("MuteSFX"); }
            set { PlayerPrefs.SetString("MuteSFX", value); PlayerPrefs.Save(); }
        }

        /// <summary>
        /// Prefs, which shows language
        /// </summary>
        public static int Language
        {
            get { return PlayerPrefs.GetInt("Language"); }
            set { PlayerPrefs.SetInt("Language", value); PlayerPrefs.Save(); }
        }


        /// <summary>
        /// Prefs, which shows GamesCompleteQTY
        /// </summary>
        public static int GamesCompleteQTY
        {
            get { return PlayerPrefs.GetInt("GamesCompleteQTY"); }
            set { PlayerPrefs.SetInt("GamesCompleteQTY", value); PlayerPrefs.Save(); }
        }
        
        
        
        /// <summary>
        /// Prefs, which shows Coins
        /// </summary>
        public static int Coins
        {
            get { return PlayerPrefs.GetInt("Coins"); }
            set { PlayerPrefs.SetInt("Coins", value); PlayerPrefs.Save(); }
        }



        /// <summary>
        /// Prefs, which shows count of game started
        /// </summary>
        public static int GameStartCount
        {
            get { return PlayerPrefs.GetInt("GameStartCount"); }
            set { PlayerPrefs.SetInt("GameStartCount", value); PlayerPrefs.Save(); }
        }

        /// <summary>
        /// Prefs, which shows count of app launched
        /// </summary>
        public static int AppStartCount
        {
            get { return PlayerPrefs.GetInt("AppStartCount"); }
            set { PlayerPrefs.SetInt("AppStartCount", value); PlayerPrefs.Save(); }
        }

        /// <summary>
        /// Parse from string to enum
        /// </summary>
        /// <param name="value">parsed string</param>
        private static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }


        public static bool GetIsItemUnlocked(string ballType)
        {
            return PlayerPrefsX.GetBool(ballType + "Unlocked");
        }

        public static void SetIsItemUnlocked(string ballType, bool value)
        {
            PlayerPrefsX.SetBool(ballType + "Unlocked", value);
            PlayerPrefs.Save();
        }

        public static bool GetIsItemInUse(string ballType)
        {
            return PlayerPrefsX.GetBool(ballType + "InUse");
        }

        public static void SetIsItemInUse(string ballType, bool value)
        {
            PlayerPrefsX.SetBool(ballType + "InUse", value);
            PlayerPrefs.Save();
        }

    }
}