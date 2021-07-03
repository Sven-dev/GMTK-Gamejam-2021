using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private Sound[] Sounds;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        foreach (Sound sound in Sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.clip;

            sound.Source.volume = sound.Volume;
            sound.Source.pitch = sound.Pitch;
            sound.Source.playOnAwake = sound.PlayOnAwake;
            sound.Source.loop = sound.Loop;
        }
    }

    public void Play(string name)
    {
        Sound sound = FindSound(name);
        sound.Source.Play();
    }

    public void Stop(string name)
    {
        Sound sound = FindSound(name);
        sound.Source.Stop();
    }

    public void SetPitch(string name, float pitch)
    {
        Sound sound = FindSound(name);
        sound.Source.pitch = pitch;
    }

    public void SetLoop(string name, bool loop)
    {
        Sound sound = FindSound(name);
        sound.Source.loop = loop;
    }

    private Sound FindSound(string name)
    {
        Sound sound = Array.Find(Sounds, sound => sound.Name == name);

        if (sound == null)
        {
            Debug.LogWarning("Warning: Sound " + name + " was not found!");
        }

        return sound;
    }
}