using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using Temirlan;
namespace Temirlan
{
    public enum Type
    {
        mental,
        abacus,
        fleshCart,
        multiplicationTable
    }

    public struct Cell
    {
        public List<int[]> numbers;
        public int result;
        public Cell(Type _type, int _result)
        {
            this.result = _result;
            numbers = new List<int[]>();
            switch (_type) {
                case Type.fleshCart:
                    while(_result != 0) {
                        int[] Arr = {(_result % 10) - 5 >= 0 ? 1 : 0, (_result % 10) - 5 >= 0 ? (_result % 10) - 5 : _result % 10 };
                        numbers.Add(Arr);
                        _result = _result / 10;
                    }
                    break;
                default:
                    break;
            }
        }
    };
    public class randomExerciseGenerator : MonoBehaviour
    {
        [SerializeField]
        private Type _type;
        [SerializeField]
        private int answer;
        private Cell cell;
        [HideInInspector]
        private GameObject answerField;
        public GameObject AnswerField {
            get { return answerField; }
            private set {}
        }


        #region FLESHCART
        [ConditionallyHide("_type", Type.fleshCart), SerializeField]
        private Transform[,] bone;
        [ConditionallyHide("_type", Type.fleshCart), SerializeField]
        private GameObject backGround;

        #endregion


        private void Awake()
        {
            answerField = gameObject.transform.Find("answerField").gameObject;
        }
        public void OnStart(Type _type_, int result)
        {
            _type = _type_;
            answer = result;
            switch (_type) {
                case Type.fleshCart:
                    FleshCart();
                    break;
                default:
                    break;
            }
        }
        private void FleshCart()
        {
            cell = new Cell(_type, answer);
            Transform panels = gameObject.transform.Find("Panels");
            RectTransform rect = panels.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(rect.rect.width * cell.numbers.Count, 500);
            bone = new Transform[cell.numbers.Count, 5];
            for (int i = cell.numbers.Count - 1; i >= 0; i--) {
                Transform bones = Instantiate(backGround, panels).transform;
                panels.parent.GetComponent<RectTransform>().sizeDelta = panels.GetComponent<RectTransform>().sizeDelta;
                for (int j = 0; j < bones.childCount; j++) {
                    bone[i, j] = bones.GetChild(j);
                }
                if (cell.numbers[i][0] == 1)
                    bone[i, 0].gameObject.SetActive(true);
                for (int j = 1; j <= cell.numbers[i][1]; j++) {
                    bone[i, j].gameObject.SetActive(true);
                }
            }
        }

        public int Oncheck()
        {
            int UserResult = 0;
            TMPro.TMP_InputField inputField = answerField.GetComponent<TMPro.TMP_InputField>();
            int.TryParse(inputField.text, out UserResult);
            if(UserResult == cell.result) {
                inputField.GetComponent<Image>().color = new Color32(194, 255, 129, 255);
                inputField.readOnly = true;
                StartCoroutine(Disable());
                return 1;
            } else {
                inputField.image.color = new Color32(255, 168, 168, 255);
            }
            return 0;
        }

        private  IEnumerator Disable()
        {
            while(GetComponent<CanvasGroup>().alpha >= .005f) {
                GetComponent<CanvasGroup>().alpha -= GetComponent<CanvasGroup>().alpha / 10;
                yield return new WaitForSeconds(.02f);
            }

        }

        private void OnEnable()
        {
        }
        private void OnDisable()
        {
            Destroy(gameObject);
        }

    }
}
