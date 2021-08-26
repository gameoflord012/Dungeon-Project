using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : GoapActionBase
{
    [field: SerializeField] public override GameObject Target { get; set; }
    [field: SerializeField] public override float Cost { get; set; } = 1;

    [SerializeField] float attackRange = 1f;
    [SerializeField] float timeBetweenAttacks = .3f;
    float timeSinceLastAttack = Mathf.Infinity;
    Health health;
    Damager damager;

    bool isAttacked;

    protected override void Awake()
    {
        base.Awake();
        health = GetComponentInParent<Health>();
        damager = GetComponentInParent<Damager>();
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
        yield return new KeyValuePair<string, object>("HasTarget", true);
    }

    public override void OnTargetChanged(GameObject target)
    {
        base.OnTargetChanged(target);
        Target = target;
    }

    public override bool checkProceduralPrecondition(GameObject agent)
    {
        return timeSinceLastAttack > timeBetweenAttacks;
    }

    public override bool isInRange()
    {
        return (Target.transform.position - transform.position).sqrMagnitude < attackRange * attackRange;
    }

    public override IEnumerator<PerformState> perform(GameObject agent)
    {
        if (Target == null) yield return PerformState.falied;

        if (Target.TryGetComponent(out Health targetHealth))
        {
            AttackBehaviour(targetHealth);
            yield return PerformState.succeed;
        }
        else
            yield return PerformState.falied;
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