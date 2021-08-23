using System.Collections;
using UnityEngine;

public class PlaySoundFeedback : Feedback
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioClip;    
    
    [Header("If AudioSource is not specified")]
    [Space(10)]
    [SerializeField] float volume = 1f;
    [SerializeField, Range(0, 1)] float spatialBlend = 0;

    private void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if(!TryGetComponent(out audioSource))
            audioSource = CreateAudioSource(audioClip);
    }

    public override void CreateFeedback()
    {
        audioSource.Play();
    }

    public override void ResetFeedback()
    {
        audioSource.Stop();
    }

    private AudioSource CreateAudioSource(AudioClip clip)
    {
        var result = new GameObject().AddComponent<AudioSource>();

        result.transform.position = transform.position;
        result.clip = clip;
        result.spatialBlend = 0;

        return result;
    }
}