using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private Dictionary<AudioEnum, AudioPlayer> sounds;

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

        sounds = new Dictionary<AudioEnum, AudioPlayer>();
        foreach (AudioPlayer snd in GetComponentsInChildren<AudioPlayer>())
        {
            sounds[snd.audioName] = snd;
        }
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
        return sounds[soundEnum];
    }
}