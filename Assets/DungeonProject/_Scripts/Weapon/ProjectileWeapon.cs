using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [SerializeField]
    Bullet bulletPrefab;

    [SerializeField]
    Transform gunMuzzle;

    public override void UseWeapon()
    {
        Bullet bullet = Instantiate(bulletPrefab, gunMuzzle.position, gunMuzzle.rotation);
        bullet.Direction = transform.right;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(gunMuzzle.position, gunMuzzle.transform.right);
    }
}
