﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SUniversalEquipItemList
{
    public GameObject GOToShow;
    public bool isEquipped;
};

public class SUniversalEquipItemAnimation : MonoBehaviour
{

    public List<SUniversalEquipItemList> m_items;
    public bool isRequiredItemChecking = false;
    public Animator m_Anim;

    private void Start()    {
        m_Anim = gameObject.GetComponent<Animator>();
    }

    public void OnBool(string nameOfBool)    {
        m_Anim.SetBool(nameOfBool, true);
    }

    public void OffBool(string nameOfBool)    {
        m_Anim.SetBool(nameOfBool, false);
    }

    public void EquipItem(int ItemIdinList)    {
        m_items[ItemIdinList].isEquipped = true;
        isRequiredItemChecking = true;
    }

    public void UnEquipItem(int ItemIdinList)    {
        m_items[ItemIdinList].isEquipped = false;
        isRequiredItemChecking = isAnyItemEquipped();
    }

    [ExecuteAlways]
    bool isAnyItemEquipped()    {
        foreach (var item in m_items)
        {
            if (item.isEquipped) return true;
        }
        return false;
    }

    // Update is called once per frame
    void LateUpdate()    {
        if (isRequiredItemChecking) CheckRequiredItems();
        Debug.Log("showing item");
    }

    private void CheckRequiredItems() { //Делает проверку по листу m_items и переодевает персонажа
        for (int i = 0; i < m_items.Capacity; i++)
        {
            if (!m_items[i].isEquipped) continue;//пропускаем, если нет необходимости одеть вещь
            m_items[i].GOToShow.SetActive(true);
        }
    }
}
