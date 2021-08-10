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
        CompletePreviousFeedback();

        foreach (Feedback feedback in feedbacks)
        {
            feedback.CreateFeedback();
        }
    }

    private void CompletePreviousFeedback()
    {
        foreach (Feedback feedback in feedbacks)
        {
            feedback.ResetFeedback();
        }
    }
}