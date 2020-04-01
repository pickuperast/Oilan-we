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
    //public List<string> l_audioName;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        GOAudioManager = GameObject.Find("AudioHandler");
        l_audioSource = GOAudioManager.GetComponent<AudioSource>();
        
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
    public void PlayAudioFromTimeline(string audioName)
    {
        Debug.Log("Trying to play " + audioName + " sound");
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
        }
    }
}
