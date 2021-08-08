using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActorInput : MonoBehaviour
{
    public UnityEvent<Vector2> OnMovementKeyPressed;
    public UnityEvent<Vector2> OnPointerPositionChanged;

    public UnityEvent OnFireButtonPressed;
    public UnityEvent OnFireButtonReleased;

    public bool isFireButtonPressed = false;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        MovementInput();
        PointerInput();
        FireButtonInput();
    }

    private void PointerInput()
    {
        OnPointerPositionChanged?.Invoke(GetMousePosition());
    }

    private void MovementInput()
    {
        OnMovementKeyPressed?.Invoke(new Vector2(
                    Input.GetAxisRaw("Horizontal"),
                    Input.GetAxisRaw("Vertical"))
                    );
    }

    private void FireButtonInput()
    {
        if (Input.GetAxisRaw("Fire1") > 0)
        {
            if (!isFireButtonPressed)
            {
                isFireButtonPressed = true;
                OnFireButtonPressed?.Invoke();
            }
        }
        else
        {
            if (isFireButtonPressed)
            {
                isFireButtonPressed = false;
                OnFireButtonReleased?.Invoke();
            }
        }
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
