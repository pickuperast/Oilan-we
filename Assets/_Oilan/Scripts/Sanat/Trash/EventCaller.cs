using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum GameType
{
    Stairs, Clouds
}
namespace Temirlan
{
    public class EventCaller : MonoBehaviour
    {
        public GameType gameType;
        string str;
        [ConditionallyHide("gameType", GameType.Stairs), SerializeField]
        private GameObject exercises;
        public event Action onDestroy;
        private void Start()
        {
            exercises.SetActive(true);
            exercises.GetComponent<Series>().onDestroy += StartAnimation;
        }
        public void StartAnimation()
        {
            Debug.Log("Hello");
        }
    }
}
