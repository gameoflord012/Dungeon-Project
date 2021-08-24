using Cinemachine;
using System.Collections;
using UnityEngine;

public class ScreenShakeFeedback : Feedback
{
    [SerializeField] float amplitude = 1;
    [SerializeField] float frequency = 1;
    [SerializeField] float shakeDuration = .2f;    

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