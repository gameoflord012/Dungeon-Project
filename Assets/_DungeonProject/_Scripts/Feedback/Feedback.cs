using UnityEngine;

[RequireComponent(typeof(FeedbackPlayer))]
public abstract class Feedback : MonoBehaviour
{
    public abstract void CreateFeedback();
    public abstract void ResetFeedback();
}