using UnityEngine;

public class WeaponAnimatorController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void TriggerMeleeAttackParameter()
    {
        animator.SetTrigger("Attack");
    }
}