using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract void StartWeapon();
    public abstract void StopWeapon();

    public LayerMask AttackLayer { get; private set; }

    private GameObject weaponOwner;

    public virtual GameObject WeaponOwner { 
        get => weaponOwner; 
        set
        {
            weaponOwner = value;
            AttackLayer = LayerMaskManager.GetAttackLayer(weaponOwner.layer);
        }
    }
}
