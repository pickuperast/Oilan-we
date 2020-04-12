using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SIsometricSpriteRenderer))]
public class SIsometricSortingEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SIsometricSpriteRenderer myScript = (SIsometricSpriteRenderer)target;
        if (GUILayout.Button("Generate sprite orders"))
            myScript.Generate();
    }
}
