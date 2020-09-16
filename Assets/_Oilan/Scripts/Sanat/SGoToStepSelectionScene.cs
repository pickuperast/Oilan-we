using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Oilan
{
    public class SGoToStepSelectionScene : MonoBehaviour
    {

        //Вызывается при прохождении степа, откидывает игру на стартовую сцену и прогружает определенный level
        public void GoToStepSelection()
        {
            //LevelsStepsManager.Instance.currentStepID = "Main";
            //SceneChanger.Instance.LoadStepScene(LevelsStepsManager.Instance.currentStepID);
            //LevelsStepsManager.Instance.LoadStepsPage("L"+level.ToString());
            SceneChanger.Instance.UnloadGameScenes();
            //SceneManager.UnloadScene(1);
        }

        public void StepFinish()
        {
            //GameplayManager.Instance.WhenStepWasFinished();
        }
    }
}
