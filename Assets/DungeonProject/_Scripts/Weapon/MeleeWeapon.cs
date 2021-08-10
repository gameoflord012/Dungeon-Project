using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Damager))]
[RequireComponent(typeof(Collider2D))]
public class MeleeWeapon : Weapon
{
    [SerializeField]
    UnityEvent OnWeaponStartAttacking;

    [SerializeField]
    bool SpreadToMultipleTargets = true;

    Damager damager;

    List<Health> attackTargets = new List<Health>();

    public override GameObject WeaponOwner
    {
        get => base.WeaponOwner;
        set
        {
            base.WeaponOwner = value;
            gameObject.layer = AttackLayer;
        }
    }

    private void Awake()
    {
        damager = GetComponent<Damager>();
    }

    public override void StartWeapon()
    {
        OnWeaponStartAttacking?.Invoke();
        
    }

    public override void StopWeapon()
    {

    }

    public void TriggerAttackArea()
    {
        if (SpreadToMultipleTargets)
        {
            foreach (Health attackTarget in attackTargets)
                attackTarget.TakeDamage(damager);
        }
        else if (attackTargets.Count > 0)
            attackTargets[0].TakeDamage(damager);
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
                return true;
            }
        }
        return false;
    }
}
