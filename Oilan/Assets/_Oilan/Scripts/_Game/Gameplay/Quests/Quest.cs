using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oilan
{
    public abstract class Quest : MonoBehaviour
    {
        public GameObject cameraAnchor;
        public float cameraTargetSize;

        public bool isPreActivated;
        public bool isActivated;
        public bool isSolved;
        public bool isDeactivated;
        public bool isPostDeactivated;

        public abstract void PreActivateQuest();
        
        public abstract void ActivateQuest();

        public abstract void CheckSolved();

        public abstract void Solved();

        public abstract void DeactivateQuest();

        public abstract void PostDeactivateQuest();

    }
}
