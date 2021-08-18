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

    public void OnMovementKeyPressedCallback(Vector2 inputAxis)
    {
        if (this.enabled == false) return;
        OnMovementKeyPressed?.Invoke(inputAxis);
    }

    public void OnPointerPositionChangedCallback(Vector2 pointerPosition)
    {
        if (this.enabled == false) return;
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
}