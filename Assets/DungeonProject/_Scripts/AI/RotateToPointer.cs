using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToPointer : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 1f;

    public void RotateToPointerPosition(Vector2 pointerPosition)
    {
        Vector2 pointerDirection = pointerPosition - (Vector2)transform.position;
        transform.right = Vector2.Lerp(transform.right, pointerDirection, rotateSpeed * Time.deltaTime);
    }
}
