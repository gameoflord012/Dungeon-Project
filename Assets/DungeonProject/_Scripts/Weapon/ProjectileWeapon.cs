using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [SerializeField]
    Bullet bulletPrefab;

    [SerializeField]
    Transform gunMuzzle;

    [SerializeField]
    float accuracyAngle = 10;

    public override void UseWeapon()
    {
        SpawnBullet(gunMuzzle.rotation * GetAccuracyAngle());
    }

    private void SpawnBullet(Quaternion accuracyAngle)
    {
        Bullet bullet = Instantiate(bulletPrefab, gunMuzzle.position, accuracyAngle);
        bullet.Direction = accuracyAngle * Vector3.right;
    }

    private Quaternion GetAccuracyAngle()
    {
        return Quaternion.AngleAxis(UnityEngine.Random.Range(-accuracyAngle, accuracyAngle), Vector3.forward);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(gunMuzzle.position, gunMuzzle.transform.right);
    }
}
