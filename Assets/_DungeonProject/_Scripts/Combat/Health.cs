using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    [Range(0, 500)]
    private float currentHealth = 50, maxHealth = 50;

    public UnityEvent<Damager> OnActorTakeDamage;
    public UnityEvent<Vector2> OnActorTakeDamageImpactDirection;
    public UnityEvent OnActorHealthReachZero;

    public bool IsDead { get => Mathf.Approximately(CurrentHealth, 0f); }

    public float CurrentHealth
    {
        get => currentHealth;
        set
        {
            float newValue = Mathf.Clamp(value, 0, maxHealth);
            if (Mathf.Approximately(currentHealth, newValue)) return;
            currentHealth = newValue;

            if (IsDead)
            {
                OnActorHealthReachZero?.Invoke();
                return;
            }            
        }
    }

    public void TakeDamage(Damager damager)
    {
        if (IsDead) return;

        CurrentHealth -= damager.Damage;
        OnActorTakeDamage?.Invoke(damager);
        OnActorTakeDamageImpactDirection?.Invoke(transform.position - damager.transform.position);
        
        Debug.Log(damager + " Attack " + this);
    }
}
