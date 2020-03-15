using UnityEngine;
using System;

public class StatePlaySoundObj : StateMachineBehaviour
{
    [Header("Leave empty to find by State Name")]
    public string clipName;

    private string _clipName = "";
    private AudioClip clip = null;
    private float time = 0f;
    private float volume = 1f;
    private float pitch = 1f;
    private bool loop = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AudioSource source = animator.gameObject.GetComponent<AudioSource>();

        ObjectAudioLibrary audioLibrary = animator.gameObject.GetComponent<ObjectAudioLibrary>();

        time = stateInfo.length * stateInfo.normalizedTime;

        source.Stop();
        source.clip = null;
        source.volume = 1f;
        source.pitch = 1f;
        source.loop = false;

        _clipName = "";

        if (!clipName.Equals(""))
        {
            _clipName = clipName;
        }

        AudioRecord audioRecord = new AudioRecord();

        if(audioLibrary != null)
        {
            if (_clipName != "")
            {
                audioRecord = audioLibrary.GetAudioRecordParams(_clipName);
            }
            else
            {
                audioRecord = audioLibrary.GetAudioRecordParamsHash(stateInfo.shortNameHash);
            }
        }
        else
        {
            Debug.Log("Add 'ObjectAudioLibrary component to gameObject'", animator.gameObject);
        }

        if (audioRecord != null)
        {
            clip = audioRecord.clip;
            volume = audioRecord.volume;
            pitch = audioRecord.pitch;
            loop = audioRecord.loop;
        }

        if (clip == null)
        {
            //throw new Exception("AudioClip is empty!");
            Debug.Log("AudioClip is empty! Check Object's AudioLibrary", animator.gameObject);
        }
        else
        {
            source.clip = clip;
            source.volume = volume;
            source.pitch = pitch;
            source.loop = loop;
        }

        if (source.clip != null && source.clip.length > 0f)
        {
            source.timeSamples = Mathf.FloorToInt(time * ((float)source.clip.samples / source.clip.length));
            source.Play();
        }
    }
}