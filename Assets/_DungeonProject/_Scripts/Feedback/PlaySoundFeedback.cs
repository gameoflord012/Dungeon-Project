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

    public override void CreateFeedback()
    {        
        if (audioSource == null && !TryGetComponent(out audioSource))
            audioSource = CreateAudioSource(audioClip);

        audioSource.Play();
    }

    public override void ResetFeedback()
    {
        if(audioSource != null)
            audioSource.Stop();
    }

    private AudioSource CreateAudioSource(AudioClip clip)
    {
        var result = new GameObject().AddComponent<DestroyAfterPlaying>().gameObject.AddComponent<AudioSource>();        

        result.transform.position = transform.position;
        result.clip = clip;
        result.volume = volume;
        result.spatialBlend = 0;

        return result;
    }
}