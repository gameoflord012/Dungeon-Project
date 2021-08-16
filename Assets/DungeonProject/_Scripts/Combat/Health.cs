using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    [Range(0, 500)]
    private float currentHealth = 50, maxHealth = 50;

    public float CurrentHealth
    {
        get => currentHealth;
        set
        {
            float newValue = Mathf.Clamp(value, 0, maxHealth);
            if (Mathf.Approximately(currentHealth, newValue)) return;
            currentHealth = newValue;
        }
    }

    public void TakeDamage(Damager damager)
    {
        CurrentHealth -= damager.Damage;
        Debug.Log(damager + " Attack " + this);
    }

    private void Update()
    {
        if (Mathf.Approximately(CurrentHealth, 0f))
            Destroy(gameObject);
    }
}