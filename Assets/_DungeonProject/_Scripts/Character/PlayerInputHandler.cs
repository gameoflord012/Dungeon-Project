using UnityEngine;

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
        inputEvents.OnPointerPositionChangedCallback(GetMousePosition());
    }

    private void MovementInput()
    {
        inputEvents.OnMovementKeyPressedCallback(new Vector2(
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
                inputEvents.OnFireButtonPressedCallback();
            }
        }
        else
        {
            if (isFireButtonPressed)
            {
                isFireButtonPressed = false;
                inputEvents.OnFireButtonReleasedCallback();
            }
        }
    }

    private Vector2 GetMousePosition()
    {
        return mainCamera.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            mainCamera.nearClipPlane
            ));
    }
}
