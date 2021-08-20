using Panda;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTask : EnemyTaskBase
{
    [SerializeField] LayerMask targetLayerMask;
    [SerializeField] float chaseDestinationOffset = .2f;
    [SerializeField] float closeRangeChaseDistance = 2f;
    [SerializeField] float pathBakingTime = .5f;
    [SerializeField] MovementDataSO chaseMovementData;


    private float timeSinceLastPathBaking;

    [Task]
    public void ChaseTarget()
    {        
        if(Task.current.isStarting)
        {
            movement.SetMovementData(chaseMovementData);
            timeSinceLastPathBaking = Mathf.Infinity;
        }
        if (timeSinceLastPathBaking > pathBakingTime)
        {
            StartPathFinding();
        }

        inputEvents.OnPointerPositionChangedCallback(data.GetTargetPosition());

        if (pathControl.IsDestinationReached())
        {            
            Task.current.Succeed();
        }

        if(TargetEscaped())
        {
            Task.current.Fail();
        }

        //if (Task.current.status == Status.Failed)
        //{
        //    pathControl.CancelPathFinding();
        //}
    }

    private void StartPathFinding()
    {
        pathControl.SetDestination(data.GetTargetPosition(), chaseDestinationOffset);
        timeSinceLastPathBaking = 0f;
    }

    [Task]
    public bool GetChaseTarget()
    {
        foreach(GameObject chaseTarget in FindChaseTargets())
        {
            data.target = chaseTarget;
            return true;
        }

        return false;
    }

    [Task]
    public bool TargetEscaped()
    {
        if (data.target == null) return false;
        foreach(GameObject chaseTarget in FindChaseTargets())
        {
            if (chaseTarget == data.target)
                return false;
        }
        return true;
    }

    [Task]
    public void TryToTraceLastTargetPosition()
    {
        if (Task.current.isStarting)
        {
            movement.SetMovementData(chaseMovementData);
            StartPathFinding();
        }

        inputEvents.OnPointerPositionChangedCallback(data.GetTargetPosition());

        if (pathControl.IsDestinationReached())
        {
            Task.current.Succeed();
        }
    }
    
    private IEnumerable<GameObject> FindChaseTargets()
    {
        // Close range check
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, closeRangeChaseDistance, Vector2.zero, 0, targetLayerMask);
        if (hit.collider)
        {
            yield return hit.collider.gameObject;
            //data.target = hit.collider.gameObject;
            //return true;
        }

        // FOV check
        foreach (Collider2D collider in fov.hitColliders)
        {
            if (((1 << collider.gameObject.layer) & targetLayerMask.value) != 0)
            {
                yield return collider.gameObject;
                /*data.target = collider.gameObject;*/
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, closeRangeChaseDistance);
    }

    private void Update()
    {
        timeSinceLastPathBaking += Time.deltaTime;
    }
}
