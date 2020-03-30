using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;


namespace Oilan
{
    [SerializeField]
    public class SaveData
    {
        public int id;
        public int user_id;
        public int level;
        public int step;
        public int part;
        public int stars;

        public SaveData(int id, int level, int step, int part, int stars)
        {
             id = 0;//it is user id
             level = 0;
             step = 0;
             part = 0;
             stars = 0;
        }
    }

    public class WebGLMessageHandler : MonoBehaviour
    {
        public static WebGLMessageHandler Instance;

        public TextMeshProUGUI TextSavedStats;

#if UNITY_WEBGL
        /*
        [DllImport("__Internal")]
        private static extern void TestVoid();

        [DllImport("__Internal")]
        private static extern void TestInt(int value);

        [DllImport("__Internal")]
        private static extern void TestString(string value);

        [DllImport("__Internal")]
        private static extern int TestReturnInt();

        [DllImport("__Internal")]
        private static extern string TestReturnString();
        */
        [DllImport("__Internal")]//Assets\Plugins\Oilan\Utils\WebGL\JSManager
        private static extern int OpenTrainer(string TrainerType, int level, int step);

        [DllImport("__Internal")]//Assets\Plugins\Oilan\Utils\WebGL\JSManager
        private static extern int GetUserID();

        [DllImport("__Internal")]//Assets\Plugins\Oilan\Utils\WebGL\JSManager
        private static extern string GetProgress();

        [DllImport("__Internal")]//Assets\Plugins\Oilan\Utils\WebGL\JSManager
        private static extern void SetProgress(string JSONinput);

        [DllImport("__Internal")]//Assets\Plugins\Oilan\Utils\WebGL\JSManager
        private static extern void LibConsoleWriter(string JSONinput);
        //answer is: {&quot;1&quot;:
        //                   {&quot;id&quot;:2,&quot;user_id&quot;:9,&quot;level&quot;:1,&quot;step&quot;:1,&quot;part&quot;:1,&quot;starts&quot;:0}}
        // Given JSON input should be:
        // {"name":"Dr Charles","lives":3,"health":0.8}
        // this example will return a PlayerInfo object with
        // name == "Dr Charles", lives == 3, and health == 0.8f.
        /*
        [DllImport("__Internal")]
        private static extern int GetStarsTotal(int userID);

        [DllImport("__Internal")]
        private static extern void SetStarsTotal(int userID, int value);

        [DllImport("__Internal")]
        private static extern int GetStarsCollectedInLocationStep(int userID, int level, int step);
        
        [DllImport("__Internal")]
        private static extern int GetStarsCollectedInLocationPart(int userID, int level, int step, int part);

        [DllImport("__Internal")]
        private static extern void SetStarsCollectedInLocationStep(int userID, int level, int step, int value);

        [DllImport("__Internal")]
        private static extern void SetStarsCollectedInLocationPart(int userID, int level, int step, int part, int value);

        [DllImport("__Internal")]
        private static extern bool GetLocationIsUnlocked(int userID, int level, int step);
        
        [DllImport("__Internal")]
        private static extern void SetLocationIsUnlocked(int userID, int level, int step, bool value);

        [DllImport("__Internal")]
        private static extern int GetMaxUnlockedStepInLevel(int userID, int level);
        
        [DllImport("__Internal")]
        private static extern string GetLocationMax(int userID);

        [DllImport("__Internal")]
        private static extern string GetLocationLast(int userID);

        [DllImport("__Internal")]
        private static extern int GetLocationLastLevel(int userID);

        [DllImport("__Internal")]
        private static extern int GetLocationLastStep(int userID);

        [DllImport("__Internal")]
        private static extern int GetLocationLastPart(int userID);

        [DllImport("__Internal")]
        private static extern void SetLocationLast(int userID, int level, int step, int part);

        [DllImport("__Internal")]
        private static extern void ShowTrainer(int userID, int level, int step, string type);
        */
#endif

        private void Awake()
        {
            Instance = this;
            //var gameInstance = UnityLoader.instantiate("gameContainer", "Build/webgl.json");
        }

        public void RecieveMessageInt(int message)
        {
            //Debug.Log("MessageRecieved: " + message.ToString());
        }

        public void RecieveMessageString(string message)
        {
            //Debug.Log("MessageRecieved: " + message);
        }
        /*
        public void PullSaveDataFromJSON(string message)
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(message);

            StringBuilder saveDataString = new StringBuilder();

            saveDataString.AppendLine("level: " + saveData.level);
            saveDataString.AppendLine("stars: " + saveData.stars);

            Debug.Log(saveDataString.ToString());

        }*/
        public void ConsoleLog(string txt)
        {
           if (WebPlatform())
            LibConsoleWriter(txt);
        }
        public void ReturnFromTrainer(bool success, int starsEarned)
        {
            //Debug.Log("Returned from Trainer. Success: " + success.ToString() + " StarsEarned: " + starsEarned.ToString());
        }

        public void TestOpenTrainer()
        {
            PubOpenTrainer("abacus", 1, 1);
        }

        public void TestSetProgress()
        {
            SetData("{id:18, level:1,step:4,part:1,stars:0}");
        }

//#if UNITY_WEBGL
        public void SetData(string mJSONinput)
        {
            //LibConsoleWriter("UnityLog: Setting progress to: " + mJSONinput);
            //TextSavedStats.text += "\nUnityLog: Setting progress to: " + mJSONinput;
            //Debug.Log("Setting progress to: "+ mJSONinput);
            //SetProgress(mJSONinput);
        }

        public void PubOpenTrainer(string TrainerType, int level, int step)
        {
            //LibConsoleWriter("UnityLog: Setting progress to: " + mJSONinput);
            //TextSavedStats.text += "\nUnityLog: Setting progress to: " + mJSONinput;
            //Debug.Log("Setting progress to: "+ mJSONinput);
             OpenTrainer(TrainerType, level, step); 
        }
//#elif UNITY_EDITOR
//#endif

        bool WebPlatform()
        {
            return Application.platform != RuntimePlatform.WebGLPlayer;//!= for testing in pickuperast.github.io and oilan.kz
        }

        public SaveData GetData()
        {
            SaveData mSaveData = new SaveData(0, 0, 0, 0, 0);
            if (WebPlatform())
            {
                string progress = GetProgress(); 
                //LibConsoleWriter("UnityLog: call GetProgress(), answer: " + progress);
                //TextSavedStats.text += "\nUnityLog: call GetProgress(), answer: " + progress;
                progress = GetProgress().Replace("&quot;", @"""");
                //LibConsoleWriter("UnityLog: Replacing quots, answer: " + progress);
                //TextSavedStats.text += "\nUnityLog: Replacing quots, answer: " + progress;
                string pattern = @"{.*?\}";
                string input = progress;
                //LibConsoleWriter("UnityLog: setting string input, answer: input = " + progress);
                string output = "";
                //string input = @"[{&quot;id&quot;:18,&quot;level&quot;:1,&quot;step&quot;:1,&quot;part&quot;:1,&quot;stars&quot;:0}]";
                RegexOptions options = RegexOptions.Multiline;

                foreach (Match m in Regex.Matches(input, pattern, options))
                {
                    output = m.Value;
                    //LibConsoleWriter("UnityLog: Regex matching, answer: m.Value = " + m.Value);
                    //TextSavedStats.text += "\nUnityLog: Regex matching, answer: m.Value = " + m.Value;
                    //Debug.Log(m.Value);
                    //Debug.Log("'{0}' found at index {1}." + m.Value + m.Index);
                }
                //output = output.Substring(1, output.Length - 1);//{"id":2,"level":1,"step":1,"part":1,"stars":0}
                mSaveData = JsonUtility.FromJson<SaveData>(output);
                return mSaveData;
                /*
                //string pattern = @"(?:\{.*?\:)(.*?\})";
                SaveGameManager.Instance.mSetProgress(progress);
                Debug.Log("Debug.log - Getting progress: " + progress);
                TextSavedStats.text = progress;
                SaveGameManager.Instance.SetUserID(GetUserID());

                //SaveGameManager.Instance.SetStarsTotal(GetStarsTotal(SaveGameManager.Instance.GetUserID())); //DELETED

                GameLocation newLocation = new GameLocation();

                //string MaxLocation = GetLocationMax(SaveGameManager.Instance.GetUserID());

                //SaveGameManager.Instance.SetLocationUnlocked();
                */
            }
            else
            {
                string GetProgress = @"[{&quot;id&quot;:18,&quot;level&quot;:1,&quot;step&quot;:1,&quot;part&quot;:1,&quot;stars&quot;:0}]";
                string progress = GetProgress.Replace("&quot;", @"""");
                string pattern = @"{.*?\}";
                string input = progress;
                string output = "";
                //string input = @"{&quot;1&quot;:{&quot;id&quot;:2,&quot;user_id&quot;:9,&quot;level&quot;:1,&quot;step&quot;:1,&quot;part&quot;:1,&quot;starts&quot;:0}}";
                RegexOptions options = RegexOptions.Multiline;

                foreach (Match m in Regex.Matches(input, pattern, options))
                {
                    output = m.Value;
                    Debug.Log(m.Value);
                    Debug.Log("'{0}' found at index {1}." + m.Value + m.Index);
                }
                //output = output.Substring(1, output.Length - 1);//{"id":2,"user_id":9,"level":1,"step":1,"part":1,"starts":0}
                mSaveData = JsonUtility.FromJson<SaveData>(output);
                return mSaveData;
            }
            return mSaveData;
            //UpdateConnectionTestData();
        }

        public void UpdateConnectionTestData()
        {
            TextSavedStats.text = "Saved Stats" + "\nUserID: " + SaveGameManager.Instance.GetUserID()
                                    + "\n StarsTotal: " //+ SaveGameManager.Instance.GetStarsTotal()
                                    + "\n LocationUnlockedMax: " //+ SaveGameManager.Instance.GetMaxUnlockedLocation()
                                    + "\n LocationLast: "; //+ SaveGameManager.Instance.GetLastLocation();

        }

    }
}