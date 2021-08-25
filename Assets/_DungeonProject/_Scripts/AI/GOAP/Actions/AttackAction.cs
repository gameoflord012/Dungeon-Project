using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : GoapActionBase
{
    [field: SerializeField] public override GameObject Target { get; set; }
    [field: SerializeField] public override float Cost { get; set; } = 0;

    [SerializeField] float attackRange = 1f;
    [SerializeField] float timeBetweenAttacks = .3f;
    float timeSinceLastAttack = Mathf.Infinity;
    Health health;

    bool isAttacked;

    protected override void Awake()
    {
        base.Awake();
        health = GetComponentInParent<Health>();        
    }

    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
    }

    public override IEnumerable<KeyValuePair<string, object>> GetEffects()
    {
        yield return new KeyValuePair<string, object>("AttackTarget", true);
    }

    public override IEnumerable<KeyValuePair<string, object>> GetPreconditions()
    {
        yield return new KeyValuePair<string, object>("InAttackRange", true);
    }

    public override void OnTargetChanged(GameObject target)
    {
        base.OnTargetChanged(target);
        Target = target;
    }

    public override bool checkProceduralPrecondition(GameObject agent)
    {
        return
            timeSinceLastAttack > timeBetweenAttacks &&
            Target != null;        
    }    

    public override bool isDone()
    {
        return isAttacked;
    }

    public override bool isInRange()
    {
        return (Target.transform.position - transform.position).sqrMagnitude < attackRange * attackRange;
    }

    public override bool perform(GameObject agent)
    {
        if (data.Target.TryGetComponent(out Health targetHealth))
        {
            AttackBehaviour(targetHealth);
            return true;
        }

        return false;
    }

    private void AttackBehaviour(Health targetHealth)
    {
        if (health != null && health.IsDead) return;

        timeSinceLastAttack = 0;

        inputEvents.OnPointerPositionChangedCallback(data.GetTargetPosition());
        movement.StopMoving();

        targetHealth.TakeDamage(damager);
        isAttacked = true;
    }
}