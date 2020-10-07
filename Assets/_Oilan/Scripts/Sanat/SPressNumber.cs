using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class SPressNumber : MonoBehaviour
{
    public Button[] ButtonsNumbers;
    private void Start()
    {
        //Для корректной сортировки кнопок
        StartCoroutine(SortButtons());
    }
    IEnumerator SortButtons()
    {
        //Для корректной сортировки кнопок
        gameObject.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        gameObject.SetActive(false);
    }
    //Handle the onClick event
    void CustomButton_onClick(TMP_InputField SelectedField, int val)
    {
        SelectedField.text = val.ToString();
    }

    //Крепим поле на котором курсор через Unity сцену
    public void SetSelectedField(TMP_InputField InSceneSelectedField)
    {
        for (int i = 0; i < ButtonsNumbers.Length; i++)
        {
            int j = i;//Почему? Потому что docs.microsoft.com/ru-ru/archive/blogs/ericlippert/closing-over-the-loop-variable-considered-harmful и answers.unity.com/questions/912202/buttononclicaddlistenermethodobject-wrong-object-s.html
            ButtonsNumbers[i].onClick.RemoveAllListeners();
            //ButtonsNumbers[i].onClick.AddListener(() => CustomButton_onClick(InSceneSelectedField, j));//всем кнопкам присвается один и тот же параметр "9"
            ButtonsNumbers[i].onClick.AddListener(delegate { CustomButton_onClick(InSceneSelectedField, j); });
        }
    }
}
