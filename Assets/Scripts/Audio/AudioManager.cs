using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Dictionary<AudioEnum, AudioPlayer> sounds;

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
        
        //Debug.unityLogger.Log($"Play {soundEnum}");
    }

    public void Stop(AudioEnum soundEnum)
    {
        AudioPlayer sound = GetAudio(soundEnum);
        
        sound.Stop();
        
        //Debug.unityLogger.Log($"Stop {soundEnum}");
    }

    public void StopAll()
    {
        foreach (AudioPlayer sound in sounds.Values)
        {
            sound.Stop();
        }
        
        //Debug.unityLogger.Log("StopAll");
    }

    private AudioPlayer GetAudio(AudioEnum soundEnum)
    {
        return sounds[soundEnum];
    }
}