using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOrientation : MonoBehaviour
{
    public void FacingToPointerPosition(Vector2 pointerPosition)
    {
        Vector2 facingDirection = pointerPosition - (Vector2)transform.position;
        if (facingDirection.x < 0)
            transform.right = Vector2.left;
        else if (facingDirection.x > 0)
            transform.right = Vector2.right;
    }
}
