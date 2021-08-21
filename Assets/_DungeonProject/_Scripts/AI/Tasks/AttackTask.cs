using Panda;
using System.Collections;
using UnityEngine;

public class AttackTask : EnemyTaskBase
{
    [SerializeField] float attackRange = 1f;
    [SerializeField] float timeBetweenAttacks = .3f;

    private float timeSinceLastAttack = Mathf.Infinity;

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
    public void AttackTarget()
    {
        if(timeSinceLastAttack > timeBetweenAttacks)
        {
            if (data.target.TryGetComponent(out Health targetHealth))
            {
                AttackBehaviour(targetHealth);
                Task.current.Succeed();
            }
            else
            {
                Task.current.Fail();
            }
        }        
    }

    private void AttackBehaviour(Health targetHealth)
    {
        timeSinceLastAttack = 0;

        inputEvents.OnPointerPositionChangedCallback(data.GetTargetPosition());
        movement.StopMoving();

        targetHealth.TakeDamage(damager);
    }

    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
    }
}