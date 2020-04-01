using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oilan
{
    public class SPickableEquipment : MonoBehaviour
    {
        public ChestQuest Quest0;
        public int QuestID = 0;

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log(gameObject.name + " is collided with " + other.gameObject.name);
            AudioManager.Instance.PlaySound("Zv-3 (Характерный звук - издается в случае правильного ответа )");
            gameObject.SetActive(false);
            switch (QuestID)
            {
                case 0:
                    Quest0.OpenLetter();
                    break;
                case 1:
                    Quest0.PostDeactivateQuest();
                    break;
                default:
                    break;
            }
        }
    }
}
