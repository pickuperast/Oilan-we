using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Temirlan;
namespace Temirlan
{
    public enum CoroutineType
    {
        Check
    }
    public class Series : MonoBehaviour
    {
        [SerializeField]
        private GameObject trigger;
        [SerializeField]
        private Type _type;
        [SerializeField]
        private List<int> results;
        [SerializeField]
        private GameObject exercise;
        [HideInInspector]
        public Button check;
        private randomExerciseGenerator[] rand;
        private int ret = 0;
        [HideInInspector]
        public event Action onDestroy;
        private void Start()
        {
            rand = new randomExerciseGenerator[results.Count];
            check = transform.Find("ButtonCheck").gameObject.GetComponent<Button>();
            check.onClick.AddListener(() => StartEnumarator(CoroutineType.Check));
            Transform parent = transform.Find("Exercises");
            for(int i = 0; i < results.Count; i++) {
                rand[i] = Instantiate(exercise, parent).GetComponent<randomExerciseGenerator>();
                rand[i].OnStart(_type, results[i]);
            }
        }
        public void StartEnumarator(CoroutineType _type)
        {
            switch (_type) {
                case CoroutineType.Check:
                    StartCoroutine(Check(results => ret = results, ret));
                    break;
                default:
                    break;
            }
        }
        public IEnumerator Check(Action<int> val, int ret)
        {
            int sum = 0;
            for(int i = 0; i < results.Count; i++) {
                if (!rand[i].AnswerField.GetComponent<TMPro.TMP_InputField>().readOnly) {
                    sum += rand[i].Oncheck();
                    yield return new WaitForSeconds(.75f);
                }
            }
            sum += ret;
            val(sum);
            if (sum == results.Count) {
                onDestroy?.Invoke();
                trigger.SetActive(true);
                Destroy(gameObject);
            }
        }
        public void OnDestroyGameObject()
        {
            
        }

    }

}