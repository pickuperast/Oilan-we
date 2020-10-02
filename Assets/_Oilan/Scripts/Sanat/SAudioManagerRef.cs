using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Collections;
using Oilan;

public class SAudioManagerRef : MonoBehaviour
{
    public static SAudioManagerRef Instance;
    public GameObject GOAudioManager;
    public AudioSource l_audioSource;
    public AudioClip clip;
    private AudioSource bgsound;
    //public List<string> l_audioName;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        bgsound = GameObject.Find("SoundBackgroundHandler").GetComponent<AudioSource>();
        //bgsound.clip = (AudioClip)Resources.Load("Media/Audio/Sounds/" + clip.name, typeof(AudioClip));
        //bgsound.Play();
        GOAudioManager = GameObject.Find("AudioHandler");
        l_audioSource = GOAudioManager.GetComponent<AudioSource>();
        l_audioSource.clip = null;
        //musicSource.enabled = false;
        //Destroy(musicSource);
        //GameObject GOAudioManager;
        //GOAudioManager = GameObject.Find("GameplayAudioHandler");
        //musicSource = GOAudioManager.GetComponent<AudioSource>();
    }
    /*
    public void SwitchAudioSourceOnOff(bool isOn)
    {
        l_audioSource.enabled = isOn ? true : false;
    }
    */

    public void PauseBGSound()
    {
        bgsound.Pause();
    }

    public void StartBGSound()
    {
        bgsound.Play();
    }

    public void TurnDownBGSound()
    {
        bgsound.volume = 0.2f;
    }

    public void TurnUpBGSound()
    {
        bgsound.volume = 1f;
    }

    public void StopAudio()
    {
        l_audioSource.Stop();
    }

    public void PlayAudio()
    {
        l_audioSource.Play();
    }

    [ExecuteAlways]
    public void PlayAudioFromTimeline(string audioName)
    {
        WebGLMessageHandler.Instance.ConsoleLog("Trying to play " + audioName + " sound");
        if (GOAudioManager != null) { 
        GOAudioManager.GetComponent<AudioManager>().PlaySound(audioName);
        }
        else
        {
            AudioClip clip = (AudioClip)Resources.Load("Media/Audio/Sounds/" + audioName, typeof(AudioClip));
            if (clip == null)
            {
                throw new Exception("Couldn't find AudioClip with name '" + audioName + "'. Are you sure the file is in a folder named 'Resources'?");
            }
            else
            {
                l_audioSource.clip = clip;
            }
            l_audioSource.Play();
            WebGLMessageHandler.Instance.ConsoleLog("Playing " + audioName + " sound with l_audioSource");
        }
    }
}
