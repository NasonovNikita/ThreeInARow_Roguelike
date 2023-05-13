using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioEnum audioName;
  
    private AudioSource source;

    public void Awake()
    {
        source = GetComponent<AudioSource>();
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
