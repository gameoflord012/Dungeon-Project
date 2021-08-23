using Cinemachine;
using System.Collections;
using UnityEngine;

public class ScreenShakeFeedback : Feedback
{
    [SerializeField] float amplitude;
    [SerializeField] float frequency;
    [SerializeField] float shakeDuration;
    [SerializeField] CinemachineVirtualCamera cinemachineCameara;

    CinemachineBasicMultiChannelPerlin noise;

    private void Awake()
    {
        if (cinemachineCameara == null)
            cinemachineCameara = FindObjectOfType<CinemachineVirtualCamera>();

        noise = cinemachineCameara.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public override void CreateFeedback()
    {
        noise.m_AmplitudeGain = amplitude;
        noise.m_FrequencyGain = frequency;

        StartCoroutine(ScreenShakeRountine());
    }

    public override void ResetFeedback()
    {
        noise.m_AmplitudeGain = 0;
        StopAllCoroutines();
    }

    IEnumerator ScreenShakeRountine()
    {
        yield return new WaitForSeconds(shakeDuration);
        ResetFeedback();
    }
}