using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActorInput : MonoBehaviour
{
    public UnityEvent<Vector2> OnMovementKeyPressed;

    private void Update()
    {
        OnMovementKeyPressed?.Invoke(new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")));
    }
}
