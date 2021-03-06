using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : GoapActionBase
{
    [field: SerializeField] public override float Cost { get; set; } = 1;

    [SerializeField] float attackRange = 1f;
    [SerializeField] float timeBetweenAttacks = .3f;
    float timeSinceLastAttack = Mathf.Infinity;
    Health health;
    Damager damager;

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
        yield return new KeyValuePair<string, object>("Running", true);
    }

    public override bool checkProceduralPrecondition(GoapAgent agent)
    {
        return timeSinceLastAttack > timeBetweenAttacks;
    }

    public override bool isInRange()
    {
        if (data.Target == null) return true;
        return (data.Target.transform.position - transform.position).LengthSmalllerThan(attackRange);
    }

    public override IEnumerator<PerformState> perform(GoapAgent agent)
    {
        if (data.Target == null) yield return PerformState.falied;

        if (data.Target.TryGetComponent(out Health targetHealth))
        {
            AttackBehaviour(targetHealth);
            yield break;
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
    }

    public override Vector3 GetTargetPosition()
    {
        if (data.Target == null) return transform.position;
        return data.Target.transform.position;
    }
}