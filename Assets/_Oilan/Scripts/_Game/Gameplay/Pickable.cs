using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oilan
{
    public enum PickableType
    {
        STAR
    }

    public class Pickable : MonoBehaviour
    {
        public PickableType pickableType = PickableType.STAR;

        [SerializeField]
        private bool isPicked = false;

        private void Awake()
        {
            isPicked = false;
        }

        private void Start()
        {
            if (!WebGLMessageHandler.Instance.UnityPlatform()) {
                if (SaveGameManager.Instance.mSaveData.level > GameplayManager.Instance.next_level_num) Destroy(gameObject);
                if (SaveGameManager.Instance.mSaveData.step >= GameplayManager.Instance.next_step_num) Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //if (collision.CompareTag("Player") && pickableType == PickableType.STAR && !isPicked)
            isPicked = true;
            GameplayScoreManager.Instance.AddWebStars();
            GetComponent<AudioSource>().Play();//Zv-9 (Волшебный звук для звезды (отлетают на табло в меню “Награды”
            Destroy(this.gameObject);
        }

    }
}
