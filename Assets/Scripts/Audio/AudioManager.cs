using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private AudioPlayer[] sounds;
    
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);

        sounds = GetComponentsInChildren<AudioPlayer>();
    }

    public void Play(AudioEnum soundEnum)
    {
        AudioPlayer sound = GetAudio(soundEnum);
        
        sound.Play();
    }

    public void Stop(AudioEnum soundEnum)
    {
        AudioPlayer sound = GetAudio(soundEnum);
        
        sound.Stop();
    }

    private AudioPlayer GetAudio(AudioEnum soundEnum)
    {
        return Array.Find(sounds, (snd) => snd.audioName == soundEnum);
    }
}