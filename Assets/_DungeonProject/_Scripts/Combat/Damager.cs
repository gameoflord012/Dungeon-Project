using UnityEngine;

public class Damager : MonoBehaviour
{
    public GameObject damageDealer;

    [field: SerializeField, Range(0, 100)]    
    public float Damage { get; private set; } = 10;

    [field: SerializeField]
    public float KnockbackStrength { get; private set; } = 0;

    [field: SerializeField]
    public float WaitTimeAfterKnockingback { get; private set; } = 0;

    private void Awake()
    {
        if (damageDealer == null)
            damageDealer = gameObject;
    }
}
