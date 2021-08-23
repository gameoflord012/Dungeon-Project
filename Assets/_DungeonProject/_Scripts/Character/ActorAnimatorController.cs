using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class ActorAnimatorController : MonoBehaviour
{
    public UnityEvent OnActorStepAnimationEvent;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetMovementParameter(bool isWalk)
    {
        animator.SetBool("isWalk", isWalk);
    }

    public void OnActorStepAnimationEventCallback()
    {
        OnActorStepAnimationEvent?.Invoke();
    }
}