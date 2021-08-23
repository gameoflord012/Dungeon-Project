using UnityEngine;
using UnityEngine.Events;

public class ActorInputEvents : MonoBehaviour
{
    [field: SerializeField]
    private UnityEvent<Vector2> OnMovementKeyPressed { get; set; }
    [field: SerializeField]
    private UnityEvent<Vector2> OnPointerPositionChanged { get; set; }
    [field: SerializeField]
    private UnityEvent OnFireButtonPressed { get; set; }
    [field: SerializeField]
    private UnityEvent OnFireButtonReleased { get; set; }

    private Vector2 lastPointerPosition;

    public void OnMovementKeyPressedCallback(Vector2 inputAxis)
    {
        if (this.enabled == false) return;
        OnMovementKeyPressed?.Invoke(inputAxis);
    }

    public void OnPointerPositionChangedCallback(Vector2 pointerPosition)
    {
        if (this.enabled == false) return;
        lastPointerPosition = pointerPosition;
        OnPointerPositionChanged?.Invoke(pointerPosition);
    }

    public void OnFireButtonPressedCallback()
    {
        if (this.enabled == false) return;
        OnFireButtonPressed?.Invoke();
    }

    public void OnFireButtonReleasedCallback()
    {
        if (this.enabled == false) return;
        OnFireButtonReleased?.Invoke();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(lastPointerPosition, .2f);
    }

    private void Update()
    {
        
    }
}