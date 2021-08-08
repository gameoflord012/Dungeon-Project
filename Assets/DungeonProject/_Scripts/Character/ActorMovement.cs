using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class ActorMovement : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed = 5, acceleration = 5, deacceleration = 8;

    public UnityEvent<bool> OnActorMoving;    

    private Vector2 direction;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void MoveWithDirection(Vector2 movementInput)
    {
        direction = movementInput.normalized;
    }

    private void FixedUpdate()
    {
        if(direction.sqrMagnitude > 0)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, direction * maxSpeed, acceleration * Time.fixedDeltaTime);
            OnActorMoving?.Invoke(true);
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, deacceleration * Time.fixedDeltaTime);
            OnActorMoving?.Invoke(false);
        }
    }
}
