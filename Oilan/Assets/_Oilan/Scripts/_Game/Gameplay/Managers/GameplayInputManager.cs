using UnityEngine;
using System.Collections;
using System;

namespace Oilan
{
    public class GameplayInputManager : MonoBehaviour
    {

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.GetCurrentGameState() == GameState.GAMEPLAY)
            {
                Gameplay_UI_Manager.Instance.ReactOnButton("ESCAPE");
            }
        }
    }

    }

