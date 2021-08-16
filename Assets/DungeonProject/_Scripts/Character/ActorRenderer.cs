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

        if (pointerDirection.x < 0)
            spriteRenderer.flipX = true;

        else if (pointerDirection.x > 0)
            spriteRenderer.flipX = false;
    }
}
