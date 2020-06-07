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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && pickableType == PickableType.STAR && !isPicked)
            {
                isPicked = true;
                WebGLMessageHandler.Instance.AddWebsiteStar();
                AudioManager.Instance.PlaySound("Zv-9 (Волшебный звук для звезды (отлетают на табло в меню “Награды”))");

                Destroy(this.gameObject);
            }
        }

    }
}
