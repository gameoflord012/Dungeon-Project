using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField]
    int weaponOwnerSortingOrder = 0;

    [SerializeField]
    private Weapon currentWeapon_;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        EquipWeapon(GetComponentInChildren<Weapon>());
    }

    private void EquipWeapon(Weapon weaponToEquip)
    {
        currentWeapon_ = weaponToEquip;
        if(currentWeapon_ != null)
            currentWeapon_.WeaponOwner = GetWeaponOwner();
    }

    private GameObject GetWeaponOwner()
    {
        return transform.root.GetComponentInChildren<ActorMovement>().gameObject;
    }

    public void UseCurrentWeapon()
    {
        if(currentWeapon_ != null) currentWeapon_.StartWeapon();
    }

    public void StopCurrentWeapon()
    {
        if (currentWeapon_ != null) currentWeapon_.StopWeapon();
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
        if(angle > 90 || angle < -90)
            transform.rotation *= Quaternion.AngleAxis(180, Vector3.right);
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
