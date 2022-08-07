using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : Singleton<AudioManager>
{
    public Sound[] sounds;
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string Name)
    {
        Sound S = Array.Find(sounds, sound => sound.Name == Name);
        S.source.Play();
    }
}
