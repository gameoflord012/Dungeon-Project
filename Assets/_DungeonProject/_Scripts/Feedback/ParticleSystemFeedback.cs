using UnityEngine;

public class ParticleSystemFeedback : Feedback
{
    private ParticleSystem particle;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    public override void CreateFeedback()
    {
        particle.Play();
    }

    public override void ResetFeedback()
    {
        particle.Stop();
    }
}
