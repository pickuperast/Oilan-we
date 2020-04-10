using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class parts//Вещи которые во время игры могут быть одеты
{
    public string name;
    public SpriteRenderer sprite;
    public int defaultOrder;
};

[ExecuteInEditMode]
public class SIsometricSpriteRenderer : MonoBehaviour
{
    public List<parts> Parts;
    public bool isOn = false;

    // Update is called once per frame
    void Update()
    {
        if (isOn) { 
            foreach (var pt in Parts)
            {
                pt.sprite.sortingOrder = (int)(pt.defaultOrder + transform.position.y * -10);
            }
        }
    }

    public void Generate()
    {
        for (var i = 0; i < Parts.Count; i++)
        {
            Parts[i].defaultOrder = Parts[i].sprite.sortingOrder;
            Parts[i].name = Parts[i].sprite.ToString();
        }
    }
}
