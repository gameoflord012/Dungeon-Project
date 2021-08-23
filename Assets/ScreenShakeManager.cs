using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeManager : MonoBehaviour
{
    CinemachineVirtualCamera cinemachineCameara;
    CinemachineBasicMultiChannelPerlin noise;

    private void Awake()
    {
        cinemachineCameara = GetComponent<CinemachineVirtualCamera>();
        noise = cinemachineCameara.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeScreen(float duration, float amplitude, float frequency)
    {
        StopAllCoroutines();

        noise.m_AmplitudeGain = amplitude;
        noise.m_FrequencyGain = frequency;

        StartCoroutine(ScreenShakeRountine(duration));
    }    

    IEnumerator ScreenShakeRountine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        StopCurrentScreenShake();
    }

    public void StopCurrentScreenShake()
    {
        noise.m_AmplitudeGain = 0;
    }
}
