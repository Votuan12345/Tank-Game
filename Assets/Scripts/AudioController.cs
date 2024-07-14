using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    public AudioClip buttonClickSound;
    public AudioClip buyItemSound;

    private AudioSource sfxSource;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            sfxSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        sfxSource.volume = 0.2f;
        sfxSource.loop = false;
        sfxSource.playOnAwake = false;
    }

    public void PlaySfx(AudioClip sound)
    {
        sfxSource.PlayOneShot(sound);
    }
}
