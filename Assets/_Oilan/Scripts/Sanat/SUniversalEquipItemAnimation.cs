using System.Collections;
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

    public void EquipItem(int ItemIdinList)
    {
        m_items[ItemIdinList].isEquipped = true;
        isRequiredItemChecking = true;
    }

    public void UnEquipItem(int ItemIdinList)
    {
        m_items[ItemIdinList].isEquipped = false;
        isRequiredItemChecking = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (isRequiredItemChecking) CheckRequiredItems();
    }

    private void CheckRequiredItems()//Делает проверку по листу m_items и переодевает персонажа
    {
        for (int i = 0; i < m_items.Capacity; i++)
        {
            if (!m_items[i].isEquipped) continue;//пропускаем, если нет необходимости одеть вещь
            m_items[i].GOToShow.SetActive(true);
        }
    }
}
