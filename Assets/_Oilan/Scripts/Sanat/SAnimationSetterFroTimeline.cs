using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SAnimationSetterFroTimeline : MonoBehaviour
{
    Animator m_anim;
    public List<string> AnimationNames;
    public int currentAnimation = 2;
    private int _currentAnimation = 2;
    public List<string> AnimationBools;
    public int currentAnimationBool = -1;
    private int _currentAnimationBool = -1;
    // Start is called before the first frame update
    void Start()
    {
        m_anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentAnimation != currentAnimation)
        {
            _currentAnimation = currentAnimation;
            m_anim.Play(AnimationNames[_currentAnimation], 0, .0f);
        }
        if (_currentAnimationBool != currentAnimationBool)
        {
            _currentAnimationBool = currentAnimationBool;
            m_anim.SetBool(AnimationBools[_currentAnimationBool], true);
        }
    }
}
