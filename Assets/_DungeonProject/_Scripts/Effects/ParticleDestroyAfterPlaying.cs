using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyAfterPlaying : MonoBehaviour
{
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
