using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract void StartWeapon();
    public abstract void StopWeapon();

    public GameObject weaponOwner;
}
