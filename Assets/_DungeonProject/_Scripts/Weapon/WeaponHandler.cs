using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField]
    int weaponOwnerSortingOrder = 0;

    [SerializeField]
    private WeaponBase currentWeapon_;

    private SpriteRenderer weaponSpriteRenderer;

    private void Awake()
    {
        weaponSpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (currentWeapon_ == null)
            EquipWeapon(GetComponentInChildren<WeaponBase>());
        else
            EquipWeapon(currentWeapon_);
    }

    private void EquipWeapon(WeaponBase weaponToEquip)
    {
        if (weaponToEquip == null) return;

        currentWeapon_ = weaponToEquip;        
        currentWeapon_.WeaponOwner = GetWeaponOwner();

        foreach (var damager in currentWeapon_.GetComponentsInChildren<Damager>())
            damager.damageDealer = GetWeaponOwner();
    }

    private GameObject GetWeaponOwner()
    {
        return transform.root.GetComponentInChildren<ActorMovement>().gameObject;
    }

    public void UseCurrentWeapon()
    {
        if (currentWeapon_ != null) currentWeapon_.StartWeapon();
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
        if (angle > 90 || angle < -90)
            transform.rotation *= Quaternion.AngleAxis(180, Vector3.right);
    }

    private void AdjustSortingOrder(float angle)
    {
        if (weaponSpriteRenderer == null) return;

        if (angle > 0 && angle < 180)
        {
            weaponSpriteRenderer.sortingOrder = weaponOwnerSortingOrder - 1;
        }
        else
        {
            weaponSpriteRenderer.sortingOrder = weaponOwnerSortingOrder + 1;
        }
    }
}
