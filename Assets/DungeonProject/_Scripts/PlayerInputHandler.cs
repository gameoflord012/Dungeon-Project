using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInputHandler : MonoBehaviour
{
    private Camera mainCamera;
    private bool isFireButtonPressed = false;
    private ActorInputEvents inputEvents;

    private void Awake()
    {
        mainCamera = Camera.main;
        inputEvents = GetComponent<ActorInputEvents>();
    }

    private void Update()
    {
        MovementInput();
        PointerInput();
        FireButtonInput();
    }

    private void PointerInput()
    {
        inputEvents.OnPointerPositionChanged?.Invoke(GetMousePosition());
    }

    private void MovementInput()
    {
        inputEvents.OnMovementKeyPressed?.Invoke(new Vector2(
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
                inputEvents.OnFireButtonPressed?.Invoke();
            }
        }
        else
        {
            if (isFireButtonPressed)
            {
                isFireButtonPressed = false;
                inputEvents.OnFireButtonReleased?.Invoke();
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
