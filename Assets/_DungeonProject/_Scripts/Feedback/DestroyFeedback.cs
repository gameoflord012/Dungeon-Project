using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFeedback : Feedback
{
    [SerializeField] GameObject destroyTarget;
    [SerializeField] float destroyDelay;

    public override void CreateFeedback()
    {
        Destroy(destroyTarget, destroyDelay);
    }

    public override void ResetFeedback()
    {
        
    }
}
