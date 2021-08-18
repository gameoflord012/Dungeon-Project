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
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, pointerDirection) * Quaternion.Euler(0, 0, 90);

        while(Quaternion.Angle(transform.rotation, targetRotation) > angleOffset)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
