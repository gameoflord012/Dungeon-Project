using Cinemachine;
using System.Collections;
using UnityEngine;

public class ScreenShakeFeedback : Feedback
{
    [SerializeField] float amplitude;
    [SerializeField] float frequency;
    [SerializeField] float shakeDuration;    

    [SerializeField] ScreenShakeManager manager;

    private void Awake()
    {
        if (manager == null)
            manager = FindObjectOfType<ScreenShakeManager>();
    }

    public override void CreateFeedback()
    {
        manager.ShakeScreen(shakeDuration, amplitude, frequency);
    }

    public override void ResetFeedback()
    {
        manager.StopCurrentScreenShake();
    }
}