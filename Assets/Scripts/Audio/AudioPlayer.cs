using Core;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour
    {
        public AudioEnum audioName;

        [SerializeField] private float volumeRatio = 1;

        private AudioSource source;

        public void Awake()
        {
            source = GetComponent<AudioSource>();
        }

        public void Update()
        {
            source.volume = Globals.Instance.volume * volumeRatio / 100;
        }

        public void Play()
        {
            source.Play();
        }

        public void Stop()
        {
            source.Stop();
        }
    }
}