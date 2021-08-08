using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    [SerializeField]
    int weaponOwnerSortingOrder = 0;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void RotateToPointerPosition(Vector2 pointerPosition)
    {
        Vector2 direction = pointerPosition - (Vector2)transform.position;
        float zAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(zAngle, Vector3.forward);

        FacingWeaponAlignedWithRotation(zAngle);
        AdjustSortingOrder(zAngle);
    }

    private void FacingWeaponAlignedWithRotation(float angle)
    {
        spriteRenderer.flipY = angle > 90 || angle < -90;
    }

    private void AdjustSortingOrder(float angle)
    {
        if(angle > 0 && angle < 180)
        {
            spriteRenderer.sortingOrder = weaponOwnerSortingOrder - 1;
        }
        else
        {
            spriteRenderer.sortingOrder = weaponOwnerSortingOrder + 1;
        }
    }
}
