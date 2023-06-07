using UnityEngine;

namespace Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        public AudioEnum audioName;
  
        private AudioSource _source;

        public void Awake()
        {
            _source = GetComponent<AudioSource>();
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
