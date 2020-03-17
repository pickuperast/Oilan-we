using UnityEngine;
using System.Collections;
using System;

namespace Oilan
{
    public class AppInputManager : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.GetCurrentGameState() == GameState.MENU)
            {
                UI_Manager.Instance.ReactOnButton("ESCAPE");
            }

        }
    }
}

