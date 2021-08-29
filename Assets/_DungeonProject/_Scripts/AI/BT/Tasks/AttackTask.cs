//using Panda;
//using System.Collections;
//using UnityEngine;

//public class AttackTask : EnemyTaskBase
//{
//    [SerializeField] float attackRange = 1f;
//    [SerializeField] float timeBetweenAttacks = .3f;

//    float timeSinceLastAttack = Mathf.Infinity;
//    Health health;

//    protected override void Awake()
//    {
//        base.Awake();
//        health = GetComponentInParent<Health>();
//    }

//    [Task]
//    public bool IsInAttackRange()
//    {
//        if (Task.isInspected)
//        {
//            Task.current.debugInfo = "Target: " + data.GetTargetPosition();
//        }

//        if (data.Target == null) return false;        
//        return ((Vector2)transform.position - data.GetTargetPosition()).sqrMagnitude < attackRange * attackRange;        
//    }

//    [Task]
//    public void AttackTarget()
//    {
//        if(timeSinceLastAttack > timeBetweenAttacks)
//        {
//            if (data.Target.TryGetComponent(out Health targetHealth))
//            {
//                AttackBehaviour(targetHealth);
//                Task.current.Succeed();
//            }
//            else
//            {
//                Task.current.Fail();
//            }
//        }        
//    }

//    private void AttackBehaviour(Health targetHealth)
//    {
//        if (health != null && health.IsDead) return;

//        timeSinceLastAttack = 0;

//        inputEvents.OnPointerPositionChangedCallback(data.GetTargetPosition());
//        movement.StopMoving();

//        targetHealth.TakeDamage(damager);
//    }

//    private void Update()
//    {
//        timeSinceLastAttack += Time.deltaTime;
//    }
//}