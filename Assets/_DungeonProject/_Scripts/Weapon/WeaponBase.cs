using UnityEngine;
using UnityEngine.Events;

public class WeaponBase : MonoBehaviour
{
    [SerializeField]
    UnityEvent OnWeaponStart;
    [SerializeField]
    UnityEvent OnWeaponStop;

    public virtual void StartWeapon() { OnWeaponStart?.Invoke(); }
    public virtual void StopWeapon() { OnWeaponStop?.Invoke(); }

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
}
