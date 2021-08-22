using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(WeaponCallback))]
public class WeaponBase : MonoBehaviour
{
    protected WeaponCallback weaponCallback;

    public virtual void StartWeapon() { weaponCallback.OnWeaponStart?.Invoke(); }
    public virtual void StopWeapon() { weaponCallback.OnWeaponStop?.Invoke(); }

    public LayerMask AttackLayer { get; private set; }

    private GameObject weaponOwner;

    public virtual GameObject WeaponOwner
    {
        get => weaponOwner;
        set
        {
            weaponOwner = value;
            AttackLayer = LayerMaskManager.GetAttackLayer(weaponOwner.layer);
        }
    }

    protected virtual void Awake()
    {
        weaponCallback = GetComponent<WeaponCallback>();
    }
}
