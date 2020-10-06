using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SPressNumber : MonoBehaviour
{
    public int numberValue = 1;
    public TMP_InputField SelectedField;

    void Start()    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(CustomButton_onClick); //subscribe to the onClick event
    }

    //Handle the onClick event
    void CustomButton_onClick()    {
        Debug.Log(numberValue);
    }

    //Крепим поле на котором курсор через Unity сцену
    public void SetSelectedField(TMP_InputField InSceneSelectedField)    {
        SelectedField = InSceneSelectedField;
    }
}
