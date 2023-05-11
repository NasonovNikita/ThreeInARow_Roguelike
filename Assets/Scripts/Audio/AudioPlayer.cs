using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip sound;

    [SerializeField] private bool loop;
    
    public AudioEnum audioName;
  
    private AudioSource source;

    public void Awake()
    {
        source = GetComponent<AudioSource>();

        source.clip = sound;
        source.loop = loop;
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
