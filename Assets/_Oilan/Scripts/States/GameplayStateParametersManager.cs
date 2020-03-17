using UnityEngine;
using System.Collections;

namespace Oilan
{

    public class GameplayStateParametersManager : MonoBehaviour
    {

        public static GameplayStateParametersManager Instance;

        public Animator gameplayStateAnimator;

        void Awake()
        {
            Instance = this;

            if (gameplayStateAnimator == null)
            {
                gameplayStateAnimator = GetComponent<Animator>();
            }

        }

        public void SetBool(string name, bool value)
        {
            gameplayStateAnimator.SetBool(name, value);
        }

        public void SetBoolTrue(string name)
        {
            SetBool(name, true);
        }

        public void SetIntegerDefault(string name)
        {
            gameplayStateAnimator.SetInteger(name, 0);
        }

        public void SetInteger1(string name)
        {
            gameplayStateAnimator.SetInteger(name, 1);
        }

        public void SetInteger2(string name)
        {
            gameplayStateAnimator.SetInteger(name, 2);
        }

        public void SetInteger3(string name)
        {
            gameplayStateAnimator.SetInteger(name, 3);
        }

        public void SetTrigger(string name)
        {
            gameplayStateAnimator.SetTrigger(name);
        }
    }
}