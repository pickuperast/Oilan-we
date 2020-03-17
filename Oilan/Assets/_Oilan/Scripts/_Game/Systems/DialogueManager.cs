using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Oilan.Utils;

namespace Oilan
{

    public class DialogueManager : MonoBehaviour
    {

        public static DialogueManager Instance;


        public string currentText;

        [Space(20)]
        [Header("DIALOGUE TEXT")]
        public GameObject dialogueGUI;
        public TextMeshProUGUI dialogueTMP;

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            currentText = string.Empty;
            HideDialogueGUI();
        }

        public void ResetAllStats()
        {
            currentText = string.Empty;
            HideDialogueGUI();
        }

        public void ShowDialogueGUI()
        {
            dialogueGUI.SetActive(true);
            dialogueTMP.text = currentText;
        }
        public void HideDialogueGUI()
        {
            dialogueGUI.SetActive(false);
            dialogueTMP.text = string.Empty;
        }

    }
}