using Panda;
using System.Collections;
using UnityEngine;

public class AttackTask : EnemyTaskBase
{
    [SerializeField] float attackRange = 1f;    

    [Task]
    public bool IsInAttackRange()
    {
        if (Task.isInspected)
        {
            Task.current.debugInfo = "Target: " + data.GetTargetPosition();
        }

        if (data.target == null) return false;        
        return ((Vector2)transform.position - data.GetTargetPosition()).sqrMagnitude < attackRange * attackRange;        
    }

    [Task]
    public bool AttackTarget()
    {
        if (data.target.TryGetComponent(out Health targetHealth))
        {
            inputEvents.OnPointerPositionChangedCallback(data.GetTargetPosition());
            movement.ResetMovement();

            targetHealth.TakeDamage(damager);
            return true;
        }
        return false;
    }
}