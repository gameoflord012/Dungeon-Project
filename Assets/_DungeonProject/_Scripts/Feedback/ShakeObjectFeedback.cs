using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeObjectFeedback : Feedback
{
    [SerializeField] Transform shakeTarget;
    [SerializeField] float duration = .2f;
    [SerializeField] float strength = 1f;

    public override void CreateFeedback()
    {
        shakeTarget.transform.DOShakePosition(duration, strength);
    }

    public override void ResetFeedback()
    {
        shakeTarget.transform.DOComplete();
    }
}
