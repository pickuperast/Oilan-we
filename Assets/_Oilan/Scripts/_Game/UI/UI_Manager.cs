using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Oilan.Utils;

namespace Oilan
{

    public enum UI_State
    {
        MenuLoading,
        MenuMain,
        MenuLevelSelect,
        MenuStepSelect,
        MenuSettings,
        MenuQuit,
        GameplayMain,
        QuitConfirm,
        MenuConnectionTest
    }


    public class UI_Manager : MonoBehaviour
    {

        // SINGLETON
        public static UI_Manager Instance;

        public UI_State currentState;

        [Header("BG Objects")]
        public GameObject bgLoading;
        public GameObject bgMain;
        public GameObject bgLevelSelect;
        public GameObject bgStepSelect;
        
        [Header("UI Menu")]
        public GameObject menuLoading;
        public GameObject menuMain;
        public GameObject menuLevelSelect;
        public GameObject menuStepSelect;
        public GameObject menuSettings;
        public GameObject menuQuit;

        public GameObject menuConnectionTest;
        
        public Animator gameStateController;


        void Awake()
        {
            Instance = this;
        }

        public void ChangeUIByGameMode()
        {

        }

        public void ChangeActiveUI(string stateName)
        {
            // UI Objects
            List<GameObject> obj_all = new List<GameObject>();

            obj_all.Add(menuLoading);
            obj_all.Add(menuMain);
            obj_all.Add(menuLevelSelect);
            obj_all.Add(menuStepSelect);
            obj_all.Add(menuSettings);
            obj_all.Add(menuQuit);

            obj_all.Add(menuConnectionTest);

            obj_all.Add(bgLoading);
            obj_all.Add(bgMain);
            obj_all.Add(bgLevelSelect);
            obj_all.Add(bgStepSelect);

            List<GameObject> obj_active = new List<GameObject>();

            // choose active ui and bg elements
            switch (stateName)
            {
                case "MenuLoading":
                    currentState = UI_State.MenuLoading;

                    obj_active.Add(menuLoading);
                    obj_active.Add(bgLoading);

                    TurnUIAndBGOnOff(obj_all, obj_active);

                    GameManager.Instance.ChangeState(GameState.MENU);

                    break;

                case "MenuMain":
                    currentState = UI_State.MenuMain;

                    obj_active.Add(menuMain);
                    obj_active.Add(bgMain);

                    TurnUIAndBGOnOff(obj_all, obj_active);

                    GameManager.Instance.ChangeState(GameState.MENU);

                    break;

                case "MenuLevelSelect":
                    currentState = UI_State.MenuLevelSelect;

                    obj_active.Add(menuLevelSelect);
                    obj_active.Add(bgLevelSelect);

                    TurnUIAndBGOnOff(obj_all, obj_active);

                    GameManager.Instance.ChangeState(GameState.MENU);

                    break;

                case "MenuStepSelect":
                    currentState = UI_State.MenuStepSelect;

                    obj_active.Add(menuStepSelect);
                    obj_active.Add(bgStepSelect);

                    TurnUIAndBGOnOff(obj_all, obj_active);

                    GameManager.Instance.ChangeState(GameState.MENU);

                    break;

                case "MenuSettings":
                    currentState = UI_State.MenuSettings;

                    obj_active.Add(menuMain);
                    obj_active.Add(menuSettings);
                    obj_active.Add(bgMain);

                    TurnUIAndBGOnOff(obj_all, obj_active);

                    GameManager.Instance.ChangeState(GameState.MENU);

                    break;

                case "MenuQuit":
                    currentState = UI_State.MenuQuit;

                    obj_active.Add(menuMain);
                    obj_active.Add(menuQuit);
                    obj_active.Add(bgMain);

                    TurnUIAndBGOnOff(obj_all, obj_active);

                    GameManager.Instance.ChangeState(GameState.MENU);

                    break;

                case "MenuConnectionTest":
                    currentState = UI_State.MenuConnectionTest;

                    obj_active.Add(menuMain);
                    obj_active.Add(menuConnectionTest);
                    obj_active.Add(bgMain);

                    TurnUIAndBGOnOff(obj_all, obj_active);

                    GameManager.Instance.ChangeState(GameState.MENU);

                    break;

                case "GameplayMain":

                    currentState = UI_State.GameplayMain;

                    TurnUIAndBGOnOff(obj_all, obj_active);

                    GameManager.Instance.StartANewGame();

                    break;

                default:
                    break;
            }

            ButtonToggleAudio[] buttonsAudio = FindObjectsOfType<ButtonToggleAudio>();

            foreach (ButtonToggleAudio bttn in buttonsAudio)
            {
                if (bttn.gameObject.activeInHierarchy)
                    bttn.Refresh();
            }

        }

        void TurnUIAndBGOnOff(List<GameObject> allUI, List<GameObject> activeUI)
        {
            foreach (GameObject ui in allUI)
            {
                bool shouldBeActive = activeUI.Contains(ui);

                bool reactivate = shouldBeActive && !ui.activeInHierarchy;

                ui.SetActive(shouldBeActive);

                if (reactivate)
                {
                    Animator anim = ui.GetComponent<Animator>();
                    if (anim != null)
                    {
                        anim.SetTrigger("Reset");
                    }

                }

            }
        }

        void BackButtonAction()
        {
            //switch (currentState)
            //{
            //    case UI_State.MenuMain:
            //        gameStateController.SetTrigger("ShowQuit");
            //        break;

            //    case UI_State.MenuLevelSelect:
            //        menuLevelSelect.GetComponent<Animator>().SetTrigger("Hide");
            //        break;

            //    case UI_State.MenuStepSelect:
            //        menuStepSelect.GetComponent<Animator>().SetTrigger("Hide");
            //        break;

            //    case UI_State.MenuSettings:
            //        menuSettings.GetComponent<Animator>().SetTrigger("Hide");
            //        break;

            //    case UI_State.MenuConnectionTest:
            //        menuConnectionTest.GetComponent<Animator>().SetTrigger("Hide");
            //        break;

            //    case UI_State.GameplayMain:
            //        gameStateController.SetTrigger("ShowPause");
            //        break;

            //     case UI_State.MenuQuit:
            //        menuQuit.GetComponent<Animator>().SetTrigger("Hide");
            //        break;

            //    default:
            //        break;
            //}

        }

        public void ReactOnButton(string buttonName)
        {
            switch (buttonName)
            {
                case "ESCAPE":
                    BackButtonAction();
                    break;
                default:
                    Quasarlog.Log("Button action not found!");
                    break;
            }

        }


    }
}