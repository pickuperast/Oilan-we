using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System;

namespace Oilan.Editor
{
    public class SettingsWindow : EditorWindow
    {
        #region Private fields
        private static SettingsWindow window;
        private Vector2 scrollViewVector;
        private int selected = 0;

        private string[] toolbarStrings = new string[] { "Links" };
        #endregion
        [MenuItem("Oilan/Settings")]
        public static void Init()
        {
            window = (SettingsWindow)EditorWindow.GetWindow(typeof(SettingsWindow));
            window.titleContent.text = "Settings";
            window.Show();
        }

        void OnGUI()
        {
            int oldSelected = selected;
            if (oldSelected != selected)
                scrollViewVector = Vector2.zero;

            selected = GUILayout.Toolbar(selected, toolbarStrings, new GUILayoutOption[] { GUILayout.Width(400) });
            scrollViewVector = GUI.BeginScrollView(new Rect(25, 25, position.width - 30, position.height), scrollViewVector, new Rect(0, 0, 400, 1600));

            switch (selected)
            {
                case 0:
                    LinksTab.GetInstance().TabShow();
                    break;
            }
            GUI.EndScrollView();
        }
    }
}