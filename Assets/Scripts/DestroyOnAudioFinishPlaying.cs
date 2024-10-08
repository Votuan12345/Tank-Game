using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnAudioFinishPlaying : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(WaitCoutine());
    }

    IEnumerator WaitCoutine()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(gameObject);
    }
}
