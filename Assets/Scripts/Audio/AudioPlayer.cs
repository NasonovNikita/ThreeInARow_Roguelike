using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour
    {
        public AudioEnum audioName;
  
        private AudioSource source;

        public void Awake()
        {
            source = GetComponent<AudioSource>();
        }

        public void Update()
        {
            source.volume = Globals.instance.volume / 100;
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
