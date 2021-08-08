using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorRenderer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public void FacingToPointerPosition(Vector2 pointerPosition)
    {
        Vector2 pointerDirection = pointerPosition - (Vector2)transform.position;

        if (pointerPosition.x < 0)
            spriteRenderer.flipX = true;

        else if (pointerPosition.x > 0)
            spriteRenderer.flipX = false;
    }
}
