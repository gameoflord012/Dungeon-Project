using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damager))]
[RequireComponent(typeof(Collider2D))]
public class MeleeWeapon : Weapon
{
    [SerializeField]
    Collider attackArea;

    [SerializeField]
    bool SpreadToMultipleTargets;

    Damager damager;

    List<Health> attackTargets = new List<Health>();

    private void Awake()
    {
        damager = GetComponent<Damager>();
    }

    public override void StartWeapon()
    {
        if (SpreadToMultipleTargets)
        {
            foreach (Health attackTarget in attackTargets)
                attackTarget.TakeDamage(damager);
        }
        else if (attackTargets.Count > 0)
            attackTargets[0].TakeDamage(damager);
    }

    public override void StopWeapon()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsAttackTarget(collision))
            attackTargets.Add(collision.GetComponent<Health>());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsAttackTarget(collision))
            attackTargets.Remove(collision.GetComponent<Health>());
    }

    bool IsAttackTarget(Collider2D collision)
    {
        if (LayerMaskManager.IsAttackable(collision.gameObject.layer))
        {
            if (collision.TryGetComponent(out Health damageTarget))
            {
                damageTarget.CurrentHealth -= damager.Damage;
                return true;
            }
        }
        return false;
    }
}
