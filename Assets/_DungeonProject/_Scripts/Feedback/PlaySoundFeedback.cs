using System.Collections;
using UnityEngine;

public class PlaySoundFeedback : Feedback
{
    [SerializeField] AudioSource audioSource;

    private void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public override void CreateFeedback()
    {
        audioSource.Play();
    }

    public override void ResetFeedback()
    {
        audioSource.Stop();
    }
}