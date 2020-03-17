using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class AudioRecord
{
    public string name;

    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(-3f, 3f)]
    public float pitch = 1f;
    public bool loop = false;
}

public class ObjectAudioLibrary : MonoBehaviour
{
    [Header ("Audio Records")]
    public List<AudioRecord> audioRecords;

    public AudioRecord GetAudioRecordParams(string inputName)
    {
        for (int i = 0; i < audioRecords.Count; i++)
        {
            if (audioRecords[i].name == inputName)
            {
                return audioRecords[i];
            }
        }    

        return new AudioRecord();
    }

    public AudioRecord GetAudioRecordParamsHash(int inputHash)
    {
        for (int i = 0; i < audioRecords.Count; i++)
        {
            if (Animator.StringToHash(audioRecords[i].name) == inputHash)
            {
                return audioRecords[i];
            }
        }

        return new AudioRecord();
    }
}
