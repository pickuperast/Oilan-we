using UnityEngine;
using System.Collections;
using Oilan.Utils;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityStandardAssets.Cameras;
using DG.Tweening;
using System.Runtime.InteropServices;

namespace Oilan
{
    public enum GameplayState
    {
        Ready,
        Playing,
        Cutscene,
        Paused,
        GameOver
    }

    public class GameplayManager : MonoBehaviour
    {
        public static GameplayManager Instance;

        [SerializeField]
        bool isInitialized = false;

        [Header("GAME STATE")]
        public GameplayState currentGamePlayState;

        public string stepID;
        public int next_level_num;//значение устанавливается в сцене, такие значения запишутся в прогресс при прохождении уровня
        public int next_step_num;//значение устанавливается в сцене, такие значения запишутся в прогресс при прохождении уровня

        public bool startFromFirstTimeline = false;

        [Space(20)]
        [Header("GAMEPLAY OBJECTS")]
        public Camera gameplayCamera;
        public AutoCam gameplayAutoCam;
        private Transform gameplayCameraTransform;
        private float gameplayCameraOriginalSize;

        public Canvas gameplayCanvas;
        public CanvasScaler gameplayCanvasScaler;

        private Rect gameplayScreenRect;

        public GameObject characterStartAnchor;

        public GameObject controlsUI;

        // DEBUG
        public bool showSaveGameDebugLog = false;








        // STANDARD FUNCTIONS
        private void Awake()
        {
            Instance = this;

            if (gameplayCamera == null)
            {
                gameplayCamera = Camera.main;
            }

            if (gameplayCameraTransform == null)
            {
                gameplayCameraTransform = gameplayCamera.transform;
            }

            gameplayCameraOriginalSize = gameplayCamera.orthographicSize;

            isInitialized = false;
        }

        private void Start()
        {
            SetUpScreen();
            Init();
        }

        void SetUpScreen()
        {
            Camera cam = gameplayCamera;
            Canvas canv = gameplayCanvas;
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

            gameplayScreenRect = new Rect(cam.transform.position.x - nWidth / 2, cam.transform.position.y - nHeight / 2, nWidth, nHeight);

            //float scaleFactor = (nWidth * canvasScaler.referencePixelsPerUnit) / canvasScaler.referenceResolution.x;
            Vector2 canvasSize = canv.GetComponent<RectTransform>().sizeDelta;
            // should be scaled with constant height

            Vector3 scaleFactor = new Vector3(canvasSize.y / gameplayScreenRect.height, canvasSize.y / gameplayScreenRect.height, 1f);
        }

        public Rect GetScreenRect()
        {
            return gameplayScreenRect;
        }

        public void LateUpdate()
        {
            if (currentGamePlayState == GameplayState.Playing)
            {
                
                // CAMERA AND HELPERS POSITION

            }
        }


        // INIT
        public void Init()
        {


            if (startFromFirstTimeline)
            {
                GameplayTimelineManager.Instance.PlayFirstTimeline();
            }

            isInitialized = true;

            
        }


        // UPDATES


        // SETUP
        private void SetStartProperties()
        {
            // APPLICATION

            
            // GAMEPLAY

            GameplayScoreManager.Instance.ResetAllStats();
            
            
            // OBJECTS


        }


        // GAME STATE
        public void ChangeGamePlayState(GameplayState newState)
        {
            currentGamePlayState = newState;

            UpdateState();

            GameplayScoreManager.Instance.UpdateAllStats();
        }

        public void UpdateState()
        {
            // Time Scale control


            switch (currentGamePlayState)
            {
                case GameplayState.Ready:

                    PrepareANewGame();

                    break;

                case GameplayState.Playing:

                    GameplayCanvasFullscreenEffects.Instance.FadeIn();

                    break;

                case GameplayState.Paused:

                    break;

                case GameplayState.GameOver:

                    GameplayStateParametersManager.Instance.SetTrigger("ShowGameOver");
                    
                    break;
            }
        }

        public void PrepareANewGame()
        {
            SetStartProperties();
        }

        public void StartGame()
        {
            if (currentGamePlayState == GameplayState.Paused)
            {
                UnPauseGame();
            }
            else
            {
                ChangeGamePlayState(GameplayState.Playing);

            }


            PrefsCenter.GameStartCount += 1;
        }

        private void GameOver()
        {
            // game state protection from double coroutines
            if (currentGamePlayState == GameplayState.Playing)
            {
                ChangeGamePlayState(GameplayState.GameOver);
            }
        }

        public void GameOverActions()
        {
            
            PrefsCenter.GamesCompleteQTY += 1;
        }


        public void PauseGame()
        {
            // game state protection from double coroutines
            if (currentGamePlayState == GameplayState.Playing)
            {
                ChangeGamePlayState(GameplayState.Paused);

                GameplayStateParametersManager.Instance.SetTrigger("ShowPause");
            }
            
        }

        public void UnPauseGame()
        {
            // game state protection from double coroutines
            if (currentGamePlayState == GameplayState.Paused)
            {
                // if we did nothing and stil waiting for gameover
                ChangeGamePlayState(GameplayState.Playing);
            }
        }

        public void WhenPartWasFinished(int part)
        {
            SaveGameManager.Instance.SetPartFinished(next_level_num, next_step_num, part);
        }

        public void WhenStepWasFinished()
        {
            SaveGameManager.Instance.SaveProgress(next_level_num, next_step_num);
        }
        // GET PARAMS

        public GameplayState GetCurrentGameplayState()
        {
            return currentGamePlayState;
        }

        // SET PARAMS
        public void AddCoins(int addValue)
        {
            GameplayScoreManager.Instance.AddCoins(addValue);
        }

        // OBJECTS CREATIONS


        public void TurnPlayerControlsOnOff(bool newValue)
        {

            PlayerController.Instance.move = Vector2.zero;

            PlayerController.Instance.TurnPlayerControllsOnOff(newValue);

            controlsUI.SetActive(newValue);
        }

        public void TurnAutoCamOnOff(bool newValue)
        {
            gameplayAutoCam.enabled = newValue;

            if (newValue)
            {
                gameplayCamera.DOOrthoSize(gameplayCameraOriginalSize, 0.5f);
            }
        }

        public void MoveCamera(GameObject anchor, float targetSize)
        {
            gameplayCamera.transform.DOMove(new Vector3(anchor.transform.position.x, anchor.transform.position.y, gameplayCamera.transform.position.z), 0.5f);

            gameplayCamera.DOOrthoSize(targetSize, 0.5f);
        }

        public void MoveCamera(Vector3 newPos, float targetSize)
        {
            gameplayCamera.transform.DOMove(new Vector3(newPos.x, newPos.y, gameplayCamera.transform.position.z), 0.5f);

            gameplayCamera.DOOrthoSize(targetSize, 0.5f);
        }
    }

}