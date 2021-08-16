using UnityEngine;

public class ParticleOrientation : MonoBehaviour
{
    public void SetParticleDirection(Vector2 facingDirection)
    {
        transform.right = facingDirection;
    }
}
