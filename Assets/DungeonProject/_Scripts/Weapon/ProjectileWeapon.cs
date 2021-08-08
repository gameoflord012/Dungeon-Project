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

    [SerializeField]
    float timeBetweenFire = 0.1f;

    [field: SerializeField]
    public bool isAutoFireOn { get; set; } = false;

    private bool waitForNextFire = false;

    private void Update()
    {
        if (isAutoFireOn && !waitForNextFire)
        {            
            UseWeapon();
            StartCoroutine(WaitNextFireRoutine());
        }
    }

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

    IEnumerator WaitNextFireRoutine()
    {
        waitForNextFire = true;
        yield return new WaitForSeconds(timeBetweenFire);
        waitForNextFire = false;
    }
}
