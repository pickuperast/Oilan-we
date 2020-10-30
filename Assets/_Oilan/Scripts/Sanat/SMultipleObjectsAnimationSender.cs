using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMultipleObjectsAnimationSender : MonoBehaviour
{
    public List<Animator> m_anim = new List<Animator>();

    public void SetTrigger(string TriggerName)
    {
        foreach (var m_Animator in m_anim)
            m_Animator.SetTrigger(TriggerName);
    }

    public void OnBool(string BoolName)
    {
        foreach (var m_Animator in m_anim)
            m_Animator.SetBool(BoolName, true);
    }

    public void OffBool(string BoolName)
    {
        foreach (var m_Animator in m_anim)
            m_Animator.SetBool(BoolName, false);
    }
}
