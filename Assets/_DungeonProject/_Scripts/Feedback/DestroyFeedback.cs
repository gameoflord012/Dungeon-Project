using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFeedback : Feedback
{
    [SerializeField] GameObject destroyTarget;
    [SerializeField] float destroyDelay;

    public override void CreateFeedback()
    {
        StartCoroutine(DestroyFeedbackRoutine());
    }

    public override void ResetFeedback()
    {
        
    }

    IEnumerator DestroyFeedbackRoutine()
    {
        yield return new WaitForSeconds(destroyDelay);

        var feedbackPlayers = destroyTarget.GetComponentsInChildren<FeedbackPlayer>();
        foreach (FeedbackPlayer feedbackPlayer in feedbackPlayers)
            feedbackPlayer.CompleteRunningFeedback();

        Destroy(destroyTarget);
    }
}
