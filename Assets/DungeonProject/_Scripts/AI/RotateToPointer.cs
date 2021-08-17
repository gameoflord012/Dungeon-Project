using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToPointer : MonoBehaviour
{
    [SerializeField] float rotateSpeed;

    public void RotateToPointerPosition(Vector2 pointerPosition)
    {
        transform.right = Vector2.Lerp(transform.right, pointerPosition, rotateSpeed * Time.deltaTime);
    }
}
