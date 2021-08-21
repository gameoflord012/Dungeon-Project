using UnityEngine;

class FeedbackPlayer : MonoBehaviour
{
    Feedback[] feedbacks;

    private void Awake()
    {
        feedbacks = GetComponents<Feedback>();
    }

    public void PlayFeedbacks()
    {
        CompleteRunningFeedback();

        foreach (Feedback feedback in feedbacks)
        {
            feedback.CreateFeedback();
        }
    }

    public void CompleteRunningFeedback()
    {
        foreach (Feedback feedback in feedbacks)
        {
            feedback.ResetFeedback();
        }
    }
}