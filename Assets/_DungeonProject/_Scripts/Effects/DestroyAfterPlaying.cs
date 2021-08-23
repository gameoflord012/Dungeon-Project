using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestroyAfterPlaying : MonoBehaviour
{
    ParticleSystem particle;
    AudioSource audioSource;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();        
    }

    private void Update()
    {
        if (particle != null && !particle.IsAlive())
            Destroy(gameObject);

        if (audioSource != null && !audioSource.isPlaying)
            Destroy(gameObject);
    }
}
