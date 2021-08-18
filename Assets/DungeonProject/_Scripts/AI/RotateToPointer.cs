using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToPointer : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 1;
    [SerializeField] float angleOffset = 10;

    public void RotateToPointerPosition(Vector2 pointerPosition)
    {
        StopAllCoroutines();
        StartCoroutine(RotateRoutine(pointerPosition));
    }

    public IEnumerator RotateRoutine(Vector2 pointerPosition)
    {
        Vector2 pointerDirection = pointerPosition - (Vector2)transform.position;
        while(Vector2.Angle(transform.right, pointerDirection) > angleOffset)
        {
            transform.right = Vector2.Lerp(transform.right, pointerDirection, rotateSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
