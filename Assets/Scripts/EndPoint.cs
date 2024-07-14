using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    [SerializeField] private List<Transform> transforms;

    private int randomIndex;
    private AudioSource m_AudioSource;

    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if(transforms == null || transforms.Count == 0)
        {
            return;
        }
        randomIndex = Random.Range(0, transforms.Count);
        transform.position = transforms[randomIndex].position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UIManager.instance.NextLevel();
            if(m_AudioSource != null)
            {
                m_AudioSource.Play();
            }
        }
    }
}
