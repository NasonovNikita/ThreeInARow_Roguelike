using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        private Dictionary<AudioEnum, AudioPlayer> _sounds;
        public static AudioManager Instance { get; private set; }

        public void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);

            _sounds = new Dictionary<AudioEnum, AudioPlayer>();
            foreach (var snd in GetComponentsInChildren<AudioPlayer>())
                _sounds[snd.audioName] = snd;
        }

        /// <summary>
        ///     Plays chosen by ID music
        /// </summary>
        /// <param name="soundEnum">A sound's <b>key</b> (ID)</param>
        public void Play(AudioEnum soundEnum)
        {
            var sound = GetAudio(soundEnum);

            sound.Play();
        }

        public void Stop(AudioEnum soundEnum)
        {
            var sound = GetAudio(soundEnum);

            sound.Stop();
        }

        public void StopAll()
        {
            foreach (var sound in _sounds.Values) sound.Stop();
        }

        private AudioPlayer GetAudio(AudioEnum soundEnum)
        {
            return _sounds[soundEnum];
        }
    }
}