using UnityEngine;

public class ParticleSystemFeedback : Feedback
{
    [SerializeField] ParticleSystem particlePrefab;
    [SerializeField] bool destroyAfterPlaying = true;

    public override void CreateFeedback()
    {
        ParticleSystem particleSystem = Instantiate(particlePrefab, transform.position, transform.rotation);
        if (destroyAfterPlaying) particleSystem.gameObject.AddComponent<DestroyAfterPlaying>();

        if (!particleSystem.main.playOnAwake)
            particleSystem.Play();
    }

    public override void ResetFeedback()
    {
        
    }
}
