using Panda;
using System.Collections;
using UnityEngine;

public class AttackTask : MonoBehaviour
{
    [SerializeField] float attackRange = 1f;

    ActorInputEvents inputEvents;
    ActorMovement movement;
    Damager damager;
    EnemyTaskData data;

    private void Awake()
    {
        damager = GetComponentInParent<Damager>();
        movement = GetComponentInParent<ActorMovement>();
        data = GetComponentInParent<EnemyTaskData>();
        inputEvents = GetComponentInParent<ActorInputEvents>();
    }

    [Task]
    public bool IsInAttackRange()
    {
        if (Task.isInspected)
        {
            Task.current.debugInfo = "Target: " + data.target;
        }

        if (data.target == null) return false;        
        return (transform.position - data.target.transform.position).sqrMagnitude < attackRange * attackRange;        
    }

    [Task]
    public bool AttackTarget()
    {
        if (data.target.TryGetComponent(out Health targetHealth))
        {
            inputEvents.OnPointerPositionChangedCallback(data.target.transform.position);
            movement.ResetMovement();

            targetHealth.TakeDamage(damager);
            return true;
        }
        return false;
    }
}