using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SUILetterTextAndFontChanger : MonoBehaviour
{
    [Serializable]
    public class FontsList
    {
        public string name;
        public Font m_font;
    };

    public Text LetterText;//need to setup in prefab
    public List<FontsList> g_font;//need to setup in prefab

    public void SendLetterEvil(string txt)
    {
        //0 - good, 1 - evil
        LetterText.text = txt;
        ChangeFont(1);
    }

    public void SendLetterGood(string txt)
    {
        //0 - good, 1 - evil
        LetterText.text = txt;
        ChangeFont(0);
    }

    public void ChangeFont(int id)
    {
        LetterText.font = g_font[id].m_font;
    }
}