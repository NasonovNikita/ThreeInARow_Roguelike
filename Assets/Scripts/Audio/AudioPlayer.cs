using Core;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour
    {
        public AudioEnum audioName;
  
        private AudioSource source;

        [SerializeField] private float volumeRatio = 1;

        public void Awake()
        {
            source = GetComponent<AudioSource>();
        }

        public void Update()
        {
            source.volume = Globals.instance.volume * volumeRatio / 100;
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
