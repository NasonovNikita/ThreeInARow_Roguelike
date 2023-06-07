using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;

        private Dictionary<AudioEnum, AudioPlayer> _sounds;

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

            _sounds = new Dictionary<AudioEnum, AudioPlayer>();
            foreach (AudioPlayer snd in GetComponentsInChildren<AudioPlayer>())
            {
                _sounds[snd.audioName] = snd;
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
            foreach (AudioPlayer sound in _sounds.Values)
            {
                sound.Stop();
            }
        
            //Debug.unityLogger.Log("StopAll");
        }

        private AudioPlayer GetAudio(AudioEnum soundEnum)
        {
            return _sounds[soundEnum];
        }
    }
}