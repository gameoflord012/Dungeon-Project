using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActorInput : MonoBehaviour
{
    public UnityEvent<Vector2> OnMovementKeyPressed;
    public UnityEvent<Vector2> OnPointerPositionChanged;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        OnMovementKeyPressed?.Invoke(new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"))
            );

        OnPointerPositionChanged?.Invoke(GetMousePosition());
    }

    private Vector2 GetMousePosition()
    {
        return  mainCamera.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            mainCamera.nearClipPlane
            ));
    }
}
