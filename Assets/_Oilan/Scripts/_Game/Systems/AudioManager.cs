using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Collections;

namespace Oilan
{

    public enum SoundPriority
    {
        LOW,
        MEDIUM,
        HIGH
    }

    [System.Serializable]
    public class SoundItem
    {
        public string name;

        [Range(0f, 1f)]
        public float volume = 1f;
        public AudioClip clip;
    }

    public class AudioManager : MonoBehaviour
    {

        public static AudioManager Instance;

        public AudioMixer mainMixer;

        public AudioSource musicSource;

        [Space(20)]
        [Header("SAMPLES")]
        public bool samplingEnabled = false;

        private float[] musicSamples = new float[64];
        public float[] normSamples = new float[6];
        public float sampleSpeedUp = 1f;
        public float sampleSpeedDown = 1f;

        [Space(20)]
        [Header("MUSIC")]
        public AudioClip[] musicTracks;
        public bool randomTrack = false;
        public int lastTrackID = -1;

        [Space(20)]
        [Header("MUTE")]
        public bool muteMusic;
        public bool muteSFX;

        void Awake()
        {
            Instance = this;
        }

        // Use this for initialization
        void Start()
        {

            //PlayMusic();
        }

        public void ChangeAudioSource()//because unity webgl cant play with 2 audiosources
        {
            //musicSource.enabled = false;
            //Destroy(musicSource);
            //GameObject GOAudioManager;
            //GOAudioManager = GameObject.Find("GameplayAudioHandler");
            //musicSource = GOAudioManager.GetComponent<AudioSource>();
        }
        /*
        void Update()
        {
            // MUSIC
            if (!muteMusic && !musicSource.isPlaying)
                PlayMusic();

            if (samplingEnabled)
            {
                musicSource.GetSpectrumData(musicSamples, 0, FFTWindow.Rectangular);
                // 64 = 2 - 4 - 8 - 16 - 32 - 64
                // 0-1
                // 2-3
                // 4-7
                // 8-15
                // 16-31
                // 32-63

                float[] tempSamples = new float[normSamples.Length];

                int j = 0;
                for (int i = 0; i < musicSamples.Length; i++)
                {
                    if (i > Mathf.Pow(2, j + 1) - 1)
                    {
                        j++;
                    }

                    tempSamples[j] += musicSamples[i];
                }

                float deltaTime = Time.deltaTime;
                for (int i = 0; i < normSamples.Length; i++)
                {
                    if (tempSamples[i] > normSamples[i])
                    {
                        normSamples[i] += Mathf.Clamp(tempSamples[i] - normSamples[i], 0, sampleSpeedUp * deltaTime);
                    }
                    else
                    {
                        normSamples[i] += Mathf.Clamp(tempSamples[i] - normSamples[i], -sampleSpeedDown * deltaTime, 0);
                    }
                }
            }

            if (!muteSFX)
            {
                
            }
        }
        */
        // PUBLIC INTERFACE

        public void ToggleMusic()
        {

            muteMusic = !muteMusic;

            if (muteMusic && musicSource.isPlaying)
            {
                StopMusic();
            }

            if (muteMusic)
                PrefsCenter.MuteMusic = "ON";
            else
                PrefsCenter.MuteMusic = "OFF";

            PlayMusic();
        }


        public void PlaySound(string soundName, bool isLockControl = false, bool isTalkAnimation = false, float volume = 1f, float pitch = 1f, bool loop = false)
        {
            if (!muteSFX)
            {
                StartCoroutine(CoroutinePlaySound(soundName, isLockControl, isTalkAnimation, volume, pitch, loop));
                if (isLockControl)
                {
                    GameplayManager.Instance.TurnPlayerControlsOnOff(false);
                }
                if (isTalkAnimation)
                {
                    Character_Ali.Instance.SetAnimatorTalkTrigger(true);
                }
            }
        }

        IEnumerator CoroutinePlaySound(string soundName, bool isLockControl, bool isTalkAnimation, float volume = 1f, float pitch = 1f, bool loop = false)
        {
            Debug.Log("Playing sound: " + soundName);
            AudioTrack trackH = new AudioTrack();
            trackH.SetClip(soundName);
            trackH.volume = volume;
            trackH.pitch = pitch;
            trackH.loop = loop;
            musicSource.Play();
            yield return new WaitForSeconds(musicSource.clip.length);//Wait for ending sound
            if (isLockControl)
            {
                GameplayManager.Instance.TurnPlayerControlsOnOff(true);//turn controls on
            }
            if (isTalkAnimation)
            {
                Character_Ali.Instance.SetAnimatorTalkTrigger(false);
                Character_Ali.Instance.SetAnimatorTrigger("Idle");//reset animation
            }
            //audio.clip = otherClip;
            //audio.Play();
        }

        public void PlayMusic()
        {

            if (!muteMusic && !musicSource.isPlaying && musicTracks.Length > 0)
            {
                int nextTrackID;

                if (randomTrack)
                {
                    nextTrackID = UnityEngine.Random.Range(0, musicTracks.Length - 1);

                    if (nextTrackID >= lastTrackID)
                    {
                        nextTrackID++;
                    }
                }
                else
                {
                    nextTrackID = lastTrackID + 1;
                }

                if (nextTrackID >= musicTracks.Length)
                {
                    nextTrackID = 0;
                }

                musicSource.clip = musicTracks[nextTrackID];
                musicSource.Play();
                lastTrackID = nextTrackID;
            }

        }

        public void StopMusic()
        {
            musicSource.Stop();
        }


        public void SetMasterVolume(float newVolume = 1f)
        {
            mainMixer.SetFloat("MasterVolume", Mathf.Log10(Mathf.Clamp(newVolume, 0.0001f, 1f)) * 20);

            //  TODO 
            // For Snapshots

            //if (newVolume == 1f)
            //{
            //    mainMixer.ClearFloat("MasterVolume");
            //}
        }

        // GET

    }
}