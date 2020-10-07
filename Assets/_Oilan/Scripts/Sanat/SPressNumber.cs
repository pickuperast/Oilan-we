using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SPressNumber : MonoBehaviour
{
    public int numberValue = 1;
    public Button[] ButtonsNumbers;

    //Handle the onClick event
    void CustomButton_onClick(TMP_InputField SelectedField, int val)
    {
        SelectedField.text = val.ToString();
        Debug.Log(val.ToString());
    }

    //Крепим поле на котором курсор через Unity сцену
    public void SetSelectedField(TMP_InputField InSceneSelectedField)
    {
        Debug.Log("selected");
        for (int i = 0; i < ButtonsNumbers.Length; i++)        {
            ButtonsNumbers[i].onClick.AddListener(() => CustomButton_onClick(InSceneSelectedField, i+1));
        }
    }
}
