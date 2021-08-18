using Panda;
using System.Collections;
using UnityEngine;

public class AttackTask : EnemyTaskBase
{
    [SerializeField] float attackRange = 1f;

    [Task]
    public bool IsInAttackRange()
    {
        if (target == null) return false;
        return (transform.position - target.transform.position).sqrMagnitude < attackRange * attackRange;
    }
}