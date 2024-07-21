using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        private Dictionary<AudioEnum, AudioPlayer> sounds;
        public static AudioManager Instance { get; private set; }

        public void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);

            sounds = new Dictionary<AudioEnum, AudioPlayer>();
            foreach (AudioPlayer snd in GetComponentsInChildren<AudioPlayer>())
                sounds[snd.audioName] = snd;
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

        public void StopAll()
        {
            foreach (AudioPlayer sound in sounds.Values) sound.Stop();
        }

        private AudioPlayer GetAudio(AudioEnum soundEnum)
        {
            return sounds[soundEnum];
        }
    }
}