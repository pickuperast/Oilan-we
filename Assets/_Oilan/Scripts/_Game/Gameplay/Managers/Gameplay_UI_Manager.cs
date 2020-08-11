using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Oilan.Utils;

namespace Oilan
{

    public enum Gameplay_UI_State
    {
        Gameplay,
        Cutscene,
        Theory,
        Trainer,
        Video
    }

    public class Gameplay_UI_Manager : MonoBehaviour
    {
        public static Gameplay_UI_Manager Instance;

        public Gameplay_UI_State currentState;

        public GameObject gameplayGUI;
        public GameObject controlsGUI;
        public GameObject dialogueGUI;
        public GameObject theoryGUI;
        public GameObject videoGUI;
        
        //public Animator gameplayStateController;


        private void Awake()
        {
            Instance = this;
        }


        public void ButtonPausePressed()
        {
            //GameplayManager.Instance.PauseGame();
            //GameplayStateParametersManager.Instance.SetTrigger("ShowPause");
        }

        public void ButtonSFXPressed()
        {

        }

        public void ButtonMusicPressed()
        {

        }

        public void ButtonRestartPressed()
        {
            
        }

        public void ButtonHomePressed()
        {

        }

        public void ToStepPage(string stepPage)
        {
            LevelsStepsManager.Instance.LoadStepsPage(stepPage);
            WebGLMessageHandler.Instance.ConsoleLog("button на карту мира was pressed");
        }
        public void ToMainMenu()
        {
            SceneChanger.Instance.UnloadGameScenes();
        }

        public void ChangeActiveUI(string stateName)
        {
            List<GameObject> ui_all = new List<GameObject>();

            ui_all.Add(gameplayGUI);
            //ui_all.Add(controlsGUI);
            ui_all.Add(dialogueGUI);
            ui_all.Add(theoryGUI);
            ui_all.Add(videoGUI);

            List<GameObject> ui_active = new List<GameObject>();

            // choose active ui elements
            switch (stateName)
            {
                case "Gameplay":
                    currentState = Gameplay_UI_State.Gameplay;

                    ui_active.Add(gameplayGUI);
                    //ui_active.Add(controlsGUI);

                    TurnUIOnOff(ui_all, ui_active);

                    break;

                case "Cutscene":
                    currentState = Gameplay_UI_State.Cutscene;

                    ui_active.Add(gameplayGUI);

                    TurnUIOnOff(ui_all, ui_active);

                    break;

                case "Theory":
                    currentState = Gameplay_UI_State.Theory;

                    ui_active.Add(gameplayGUI);
                    ui_active.Add(theoryGUI);

                    TurnUIOnOff(ui_all, ui_active);

                    break;

                case "Trainer":
                    currentState = Gameplay_UI_State.Trainer;

                    ui_active.Add(gameplayGUI);
                    ui_active.Add(theoryGUI);

                    TurnUIOnOff(ui_all, ui_active);

                    GameplayManager.Instance.StartGame();

                    break;

                case "Video":
                    currentState = Gameplay_UI_State.Video;

                    ui_active.Add(gameplayGUI);
                    ui_active.Add(videoGUI);

                    TurnUIOnOff(ui_all, ui_active);


                    break;

            }

            //ButtonToggleAudio[] buttonsAudio = FindObjectsOfType<ButtonToggleAudio>();

            //foreach (ButtonToggleAudio bttn in buttonsAudio)
            //{
            //    if (bttn.gameObject.activeInHierarchy)
            //        bttn.Refresh();
            //}

        }

        void TurnUIOnOff(List<GameObject> allUI, List<GameObject> activeUI)
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
            switch (currentState)
            {
                case Gameplay_UI_State.Gameplay:
                    //menuPause.GetComponent<Animator>().SetTrigger("Hide");
                    break;


            }

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

