using UnityEngine;
using UnityEngine.Events;

class FeedbackPlayer : MonoBehaviour
{
    public UnityEvent OnFeedbackPlayed;

    Feedback[] feedbacks;

    private void Awake()
    {
        feedbacks = GetComponents<Feedback>();
    }

    public void PlayFeedbacks()
    {
        OnFeedbackPlayed?.Invoke();

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