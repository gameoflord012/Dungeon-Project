using UnityEngine;

[RequireComponent(typeof(FeedbackPlayer))]
public abstract class Feedback : MonoBehaviour
{
    [SerializeField] bool resetWhenDestroyed = true;

    public abstract void CreateFeedback();
    public abstract void ResetFeedback();

    protected virtual void OnDestroy()
    {
        if(resetWhenDestroyed)
            ResetFeedback();
    }
}