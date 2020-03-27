using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Oilan
{

    public class SceneChanger : MonoBehaviour
    {
        public static SceneChanger Instance;

        public GameObject mainCameraPrefab;
        public GameObject mainCameraObj;


        [Header("MAIN")]
        [SerializeField]
        private string mainSceneName;
        [SerializeField]
        private Scene mainScene;

        public GameObject mainMenuCamera;
        //public AudioListener mainMenuAudioListener;
        //public GameObject mainMenuEventSystem;

        [Space(20), Header("Step")]
        public string stepSceneName;
        [SerializeField]
        private Scene stepScene;
        [SerializeField]
        private bool stepSceneIsLoaded;
        [SerializeField]
        private bool stepSceneIsUnloaded;
        
        void Awake()
        {
            Instance = this;

            mainScene = SceneManager.GetActiveScene();
            mainSceneName = mainScene.name;

            SetUpMainCamera();
        }

        private void SetUpMainCamera()
        {
            if (mainCameraObj == null)
            {
                mainCameraObj = Instantiate<GameObject>(mainCameraPrefab);
            }
        }

        // STEPS

        public void LoadStepScene(string newStepSceneName)
        {
            stepSceneName = newStepSceneName;

            stepSceneIsLoaded = false;
            stepSceneIsUnloaded = false;

            SceneManager.sceneLoaded += OnStepSceneLoaded;
            Debug.Log("Loading " + newStepSceneName);
            StartCoroutine(LoadStepSceneCoroutine());
        }

        private void OnStepSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            stepSceneIsLoaded = true;
            stepSceneIsUnloaded = false;

            stepScene = scene;

            SceneManager.sceneLoaded -= OnStepSceneLoaded;
        }

        private IEnumerator LoadStepSceneCoroutine()
        {
            //mainMenuAudioListener.enabled = false;
            //mainMenuEventSystem.SetActive(false);

            SceneManager.LoadScene(stepSceneName, LoadSceneMode.Additive);

            //Destroy(mainCameraObj);
            //mainCameraObj = null;
            mainMenuCamera.SetActive(false);

            while (!stepSceneIsLoaded)
            {
                yield return null;
            }
            //попробовать переместить аудиолистенер с мейн сцены в сцену степа и вернуть обратно когда выгружаем сцену

            if (stepSceneName.Substring(0,1) == "L")//means we are in step
            {
                AudioManager.Instance.ChangeAudioSource();
            }
                //GameplayStateParametersManager.Instance.SetTrigger("Start");

                CanvasFullscreenEffects.Instance.FadeIn();

            
            yield return null;
        }

        
        // UNLOAD

        public void UnloadGameScenes()
        {
            SceneManager.sceneUnloaded += OnGameSceneUnloaded;

            StartCoroutine(UnloadGameScenesCoroutine());
        }

        private void OnGameSceneUnloaded(Scene scene)
        {
            stepSceneIsUnloaded = true;
            stepSceneIsLoaded = false;

            SetUpMainCamera();

            SceneManager.sceneUnloaded -= OnGameSceneUnloaded;
        }

        private IEnumerator UnloadGameScenesCoroutine()
        {
            SceneManager.SetActiveScene(mainScene);

            mainMenuCamera.SetActive(true);
            //mainMenuAudioListener.enabled = true;
            //mainMenuEventSystem.SetActive(true);

            SceneManager.UnloadSceneAsync(stepScene);
            
            while (!stepSceneIsUnloaded)
            {
                yield return null;
            }
            
            GameStateParametersManager.Instance.SetBoolTrue("Bool_ToStepSelect");

            yield return null;
        }


    }
}