using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class ActorMovement : MonoBehaviour
{
    [SerializeField]
    private MovementDataSO movementData;

    [SerializeField]
    public float velocityThreshold = 6f;

    [SerializeField]
    bool inThresoldCheck;

    public UnityEvent<Vector2> OnDirectionChange;
    public UnityEvent<bool> OnActorMoving;
    public UnityEvent OnVelocityChangeSuddenly;

    private Vector2 direction;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void MoveWithDirection(Vector2 movementInput)
    {
        Vector2 newDirection = movementInput.normalized;

        if ((newDirection - direction).sqrMagnitude > 0)
        {
            OnDirectionChange?.Invoke(newDirection);
            direction = newDirection;
        }
    }

    private void FixedUpdate()
    {
        Vector2 newVelocity;
        if (direction.sqrMagnitude > 0)
        {
            newVelocity = direction * movementData.maxSpeed;
            rb.velocity = Vector2.Lerp(rb.velocity, newVelocity, movementData.acceleration * Time.fixedDeltaTime);
            OnActorMoving?.Invoke(true);
        }
        else
        {
            newVelocity = Vector2.zero;
            rb.velocity = Vector2.Lerp(rb.velocity, newVelocity, movementData.deacceleration * Time.fixedDeltaTime);
            OnActorMoving?.Invoke(false);
        }
        ThresoldCheck(newVelocity);
    }

    private void ThresoldCheck(Vector2 newVelocity)
    {
        float thresoldMagnitude = (rb.velocity - newVelocity).magnitude;
        if (thresoldMagnitude > velocityThreshold)
        {
            if (!inThresoldCheck)
            {
                inThresoldCheck = true;
                OnVelocityChangeSuddenly?.Invoke();
            }
        }
        else
        {
            inThresoldCheck = false;
        }
    }
}
