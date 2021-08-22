using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileWeapon : WeaponBase
{
    public UnityEvent OnWeaponFireProjectile;

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
        base.StartWeapon();

        if (isWeaponAutoFire)
            isAutoFireOn_ = true;
        else
            FireWeapon();
    }

    public override void StopWeapon()
    {
        base.StopWeapon();

        if (isWeaponAutoFire)
            isAutoFireOn_ = false;
    }

    private void FireWeapon()
    {
        OnWeaponFireProjectile?.Invoke();
        SpawnBullet(gunMuzzle.rotation * GetAccuracyAngle());
    }

    private void SpawnBullet(Quaternion accuracyAngle)
    {
        Bullet bullet = Instantiate(bulletPrefab, gunMuzzle.position, accuracyAngle);
        bullet.Init(accuracyAngle, AttackLayer);
    }

    private Quaternion GetAccuracyAngle()
    {
        return Quaternion.AngleAxis(
            UnityEngine.Random.Range(-accuracyAngle, accuracyAngle),
            Vector3.forward);
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
