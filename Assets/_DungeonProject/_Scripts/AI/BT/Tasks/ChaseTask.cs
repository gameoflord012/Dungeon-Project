//using Panda;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ChaseTask : EnemyTaskBase
//{
//    [SerializeField] LayerMask targetLayerMask;
//    [SerializeField] float chaseDestinationOffset = .2f;
//    [SerializeField] float closeRangeChaseDistance = 2f;
//    [SerializeField] float pathBakingTime = .5f;
//    [SerializeField] MovementDataSO chaseMovementData;

//    private GameObject unknownAttacker;
//    private float timeSinceLastPathBaking;

//    public void SetUnknownAttacker(Damager unknownAttacker)
//    {
//        this.unknownAttacker = unknownAttacker.damageDealer;
//    }

//    [Task]
//    public bool AttackedByUnknown()
//    {
//        return unknownAttacker != null;
//    }

//    [Task]
//    public bool TargetAvaiable()
//    {
//        return data.Target != null;
//    }

//    [Task]
//    public void ChaseTarget()
//    {        
//        if(Task.current.isStarting)
//        {
//            movement.SetMovementData(chaseMovementData);
//            timeSinceLastPathBaking = Mathf.Infinity;
//        }
//        if (timeSinceLastPathBaking > pathBakingTime)
//        {
//            StartPathFinding();
//        }

//        inputEvents.OnPointerPositionChangedCallback(data.GetTargetPosition());

//        if (pathControl.IsDestinationReached())
//        {            
//            Task.current.Succeed();
//        }
//    }

//    private void StartPathFinding()
//    {
//        pathControl.SetDestination(data.GetTargetPosition(), chaseDestinationOffset);
//        timeSinceLastPathBaking = 0f;
//    }

//    [Task]
//    public void GetChaseTarget()
//    {
//        foreach (GameObject chaseTarget in FindChaseTargets())
//        {
//            data.Target = chaseTarget;
//        }

//        Task.current.Succeed();
//    }

//    [Task]
//    public bool TargetEscaped()
//    {
//        if (data.Target == null) return false;

//        foreach(GameObject chaseTarget in FindChaseTargets())
//        {
//            if (chaseTarget == data.Target)
//                return false;
//        }

//        return true;
//    }

//    [Task]
//    public void TryToTraceLastTargetPosition()
//    {
//        if (Task.current.isStarting)
//        {
//            movement.SetMovementData(chaseMovementData);
//            StartPathFinding();
//        }

//        inputEvents.OnPointerPositionChangedCallback(data.GetTargetPosition());

//        if (pathControl.IsDestinationReached())
//        {
//            Task.current.Succeed();
//        }
//    }

//    [Task]
//    public void ResetTarget()
//    {
//        data.Target = null;
//        Task.current.Succeed();
//    }

//    private IEnumerable<GameObject> FindChaseTargets()
//    {
//        // Close range check
//        RaycastHit2D hit = Physics2D.CircleCast(transform.position, closeRangeChaseDistance, Vector2.zero, 0, targetLayerMask);

//        if (hit.collider && !ObscuredByObstacle(hit.transform.position))
//        {            
//            yield return GetTarget(hit.collider.gameObject);
//            //data.target = hit.collider.gameObject;
//            //return true;
//        }

//        // FOV check
//        foreach (Collider2D collider in fov.GetHitColliders())
//        {
//            if (((1 << collider.gameObject.layer) & targetLayerMask) != 0)
//            {
//                yield return GetTarget(collider.gameObject);
//                /*data.target = collider.gameObject;*/
//            }
//        }
//    }

//    private GameObject GetTarget(GameObject go)
//    {
//        return go.GetComponentInParent<ActorMovement>().gameObject;
//    }

//    private bool ObscuredByObstacle(Vector2 targetPosition)
//    {
//        RaycastHit2D hit = Physics2D.Raycast(
//            transform.position,
//            (targetPosition - (Vector2)transform.position),
//            closeRangeChaseDistance,
//            1 << LayerMask.NameToLayer("Obstacle"));

//        return hit.collider != null;
//    }

//    private void OnDrawGizmosSelected()
//    {
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireSphere(transform.position, closeRangeChaseDistance);
//    }

//    private void Update()
//    {
//        timeSinceLastPathBaking += Time.deltaTime;
//    }
//}
