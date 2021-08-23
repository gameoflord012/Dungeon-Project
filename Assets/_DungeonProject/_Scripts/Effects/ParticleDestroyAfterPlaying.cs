using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParticleDestroyAfterPlaying : MonoBehaviour
{
    public UnityEvent OnParticleDestroyed;

    ParticleSystem particle;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (!particle.IsAlive())
            Destroy(gameObject);
    }
}
