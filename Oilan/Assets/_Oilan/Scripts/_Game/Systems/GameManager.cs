using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Oilan.Utils;
using DG.Tweening;
using UnityStandardAssets.Cameras;

namespace Oilan
{

    public enum GameState
    {
        MENU,
        GAMEPLAY
    }

    public class GameManager : MonoBehaviour
    {

        public static GameManager Instance;

        public Camera mainCamera;
        public AutoCam autoCam;
        private float mainCameraOriginalSize;

        public Canvas mainCanvas;
        public CanvasScaler mainCanvasScaler;

        private Rect screenRect;

        public GameObject controlsUI;

        [Space(20)]
        public GameState gameState;

        public InitData InitDataObj { get { return initData; } }
        private InitData initData;

        void Awake()
        {
            Instance = this;

            if (mainCamera == null)
                mainCamera = Camera.main;

            mainCameraOriginalSize = mainCamera.orthographicSize;
        }

        void Start()
        {
            SetUpScreen();

            Initialize();
        }


        private void Initialize()
        {

            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            DOTween.Init();

            initData = new InitData();

            AudioManager.Instance.SetMasterVolume();
        }

        void SetUpScreen()
        {
            Camera cam = mainCamera;
            Canvas canv = mainCanvas;
            //CanvasScaler canvasScaler = Instance.mainCanvasScaler;

            float height = 2f * cam.orthographicSize;
            float width = height * cam.aspect;

            // Screen Rect sizes with normalized proportions
            float nHeight = height;
            float nWidth = width;

            //// GAMEFIELD

            //// minimal gamefield height and width with this screen Height
            //float mHeight = height * maxGamefieldHeight;
            //float mWidth = ((numberOfColumns + minimalGap) / (numberOfVisibleRows + minimalGap)) * mHeight;

            //if (width >= mWidth)
            //{
            //    nHeight = height;
            //    nWidth = mWidth;
            //}
            //else
            //{
            //    float factor = width / mWidth;

            //    nWidth = mWidth * factor;
            //    nHeight = height;// * factor;
            //}

            screenRect = new Rect(cam.transform.position.x - nWidth / 2, cam.transform.position.y - nHeight / 2, nWidth, nHeight);

            //float scaleFactor = (nWidth * canvasScaler.referencePixelsPerUnit) / canvasScaler.referenceResolution.x;
            Vector2 canvasSize = canv.GetComponent<RectTransform>().sizeDelta;
            // should be scaled with constant height

            Vector3 scaleFactor = new Vector3(canvasSize.y / screenRect.height, canvasSize.y / screenRect.height, 1f);
        }

        public Rect GetScreenRect()
        {
            return screenRect;
        }


        public void DeleteProgress()
        {
            //PrefsCenter.ClearPrefs();

            //ScoreManager.Instance.ClearCoins();

            //PrefsCenter.AppStartCount = 1;

        }

        public void ChangeState(GameState newState)
        {
            gameState = newState;

            ScoreManager.Instance.UpdateAllStats();
        }

        public GameState GetCurrentGameState()
        {
            return gameState;
        }

        public void StartANewGame()
        {
            ChangeState(GameState.GAMEPLAY);

            //if (!(PrefsCenter.GameStartCount >= 1))
            //{
            //    PrefsCenter.GameStartCount = 0;
            //}

            SceneChanger.Instance.LoadStepScene(LevelsStepsManager.Instance.currentStepID);


            //PrefsCenter.GameStartCount += 1;

        }

        public void PauseGame()
        {
            GameplayManager.Instance.PauseGame();
        }

        public void UnPauseGame()
        {
            GameplayManager.Instance.UnPauseGame();

        }

        //public void SaveGame()
        //{

        //    GamePlayManager.Instance.SaveGame();

        //}

        //public void LoadGame()
        //{

        //    GamePlayManager.Instance.LoadGame();

        //}

        //public void RemoveSavedGame()
        //{

        //    GamePlayManager.Instance.RemoveSavedGame();

        //}


        public void RemovePlayerPrefs()
        {
            // TODO
            PrefsCenter.ClearPrefs();
        }

    }
}
