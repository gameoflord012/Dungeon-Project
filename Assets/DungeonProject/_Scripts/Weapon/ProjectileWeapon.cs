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

    [SerializeField]
    public bool isWeaponAutoFire = false;

    [SerializeField]
    private bool isAutoFireOn_ = false;


    private bool waitForNextFire = false;

    private void Update()
    {
        if (isAutoFireOn_ && !waitForNextFire)
        {
            FireWeapon();
            StartCoroutine(WaitNextFireRoutine());
        }
    }    

    public override void StartWeapon()
    {
        if (isWeaponAutoFire)
            isAutoFireOn_ = true;
        else
            FireWeapon();
    }

    public override void StopWeapon()
    {
        if (isWeaponAutoFire)
            isAutoFireOn_ = false;
    }

    private void FireWeapon()
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

    IEnumerator WaitNextFireRoutine()
    {
        waitForNextFire = true;
        yield return new WaitForSeconds(timeBetweenFire);
        waitForNextFire = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(gunMuzzle.position, gunMuzzle.transform.right);
    }
}
