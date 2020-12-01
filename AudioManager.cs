using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private void Awake()
    {
        if (FindObjectsOfType<AudioManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        foreach (Sound s in sounds)
        {
         s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
       FindObjectOfType<AudioManager>().Play("Background");
        DontDestroyOnLoad(gameObject);
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError(s.name + "is missing");
            return;
        }
        s.source.Play();

    }
}
