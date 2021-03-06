using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour{

    public Sound[] sounds;
    public float timeScale;


    void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.source=gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play (string name,float delay)
    {
        Sound s =Array.Find(sounds, sound => sound.name == name);

        if (s == null) { Debug.LogWarning("Sound" + name + "was not found!"); return; }
        s.source.PlayDelayed(delay);
    }
}
