using Core;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour
    {
        /// Audio ID
        public AudioEnum audioName;

        [SerializeField] private float volumeRatio;

        private AudioSource _source;

        public void Awake()
        {
            _source = GetComponent<AudioSource>();
        }

        public void Update()
        {
            _source.volume = Globals.Instance.volume * volumeRatio / 100;
        }

        public void Play()
        {
            _source.Play();
        }

        public void Stop()
        {
            _source.Stop();
        }
    }
}