using UnityEngine;

public class ParticleSystemFeedback : Feedback
{
    [SerializeField] ParticleSystem particlePrefab;

    public override void CreateFeedback()
    {
        ParticleSystem particleSystem = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        if (!particleSystem.main.playOnAwake)
            particleSystem.Play();
    }

    public override void ResetFeedback()
    {
        
    }
}
