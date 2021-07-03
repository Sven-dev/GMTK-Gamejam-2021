using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string Name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float Volume;   
    [Range(0f, 3f)]
    public float Pitch;
    public bool PlayOnAwake;
    public bool Loop;

    [HideInInspector]
    public AudioSource Source;
}