using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [field: SerializeField]
    [field: Range(0, 100)]
    public float Damage { get; private set; } = 10;
}
