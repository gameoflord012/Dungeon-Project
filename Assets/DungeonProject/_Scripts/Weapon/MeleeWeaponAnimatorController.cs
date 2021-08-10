using System.Collections;
using UnityEngine;

public class MeleeWeaponAnimatorController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerMeleeAttackParameter()
    {
        animator.SetTrigger("Attack");
    }
}