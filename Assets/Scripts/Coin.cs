using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponentInChildren<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(GameManager.instance != null)
            {
                GameManager.instance.CoinCount++;
                Prefs.playerCoins++;

                animator.SetTrigger("Collected");
                if(audioSource != null) audioSource.Play();
                gameObject.GetComponent<Collider2D>().enabled = false;
            }
        }
    }

}
