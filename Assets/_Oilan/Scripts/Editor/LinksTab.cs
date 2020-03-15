using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

namespace Oilan.Editor
{
    public class LinksTab
    {
        private LinksData loadedLinksData;
        private TextAsset txtAsset;

        private string urlAndroid;
        private int cycleCount = 0;

        static LinksTab instance;
        public static LinksTab GetInstance()
        {
            if (instance == null)
                instance = new LinksTab();
            return instance;
        }

        public void TabShow()
        {
            CheckForLoadingData();
            EditorGUIUtility.labelWidth = 160F;
            
            urlAndroid = EditorGUILayout.TextField("URL Android", urlAndroid, GUILayout.Width(550));

            SaveBtn();
        }

        private void SaveBtn()
        {
            GUIStyle style = new GUIStyle(GUI.skin.button);
            style.normal.textColor = new Color32(0, 128, 0, 255);
            if (GUILayout.Button("Save", style, new GUILayoutOption[] { GUILayout.Width(75) }))
            {
                LinksData links = new LinksData();
                links.urlAndroid = urlAndroid;

                string generalPath = Application.dataPath + Constants._resources + Constants._pathLinksData + ".txt";
                string saveJSON = JsonUtility.ToJson(links, true);
                StreamWriter sw = new StreamWriter(generalPath);
                sw.Write(saveJSON);
                sw.Close();
                AssetDatabase.Refresh();

                Debug.Log("Links data is saved".StrColored(DebugColors.green));
            }
        }
        private void CheckForLoadingData()
        {
            if (loadedLinksData == null)
            {
                cycleCount++;
                txtAsset = Resources.Load(Constants._pathLinksData) as TextAsset;
                if (txtAsset != null)
                {
                    loadedLinksData = JsonUtility.FromJson<LinksData>(txtAsset.text);
                }
                else
                {
                    Debug.LogError("Count find file at path AppResources/" + Constants._pathLinksData);
                }

                if (loadedLinksData != null)
                {
                    urlAndroid = loadedLinksData.urlAndroid;
                }

            }
        }
    }
}