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

        StartPathFinding();
        inputEvents.OnPointerPositionChangedCallback(data.GetTargetPosition());

        if (pathControl.IsDestinationReached())
        {
            pathControl.CancelPathFinding();
            Task.current.Succeed();
        }

        //if (Task.current.status == Status.Failed)
        //{
        //    pathControl.CancelPathFinding();
        //}
    }

    private void StartPathFinding()
    {
        if(timeSinceLastPathBaking > pathBakingTime)
        {
            timeSinceLastPathBaking = 0f;
            pathControl.SetDestination(data.GetTargetPosition(), chaseDestinationOffset);
        }
    }

    [Task]
    public bool GetChaseTarget()
    {
        // Close range check
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, closeRangeChaseDistance, Vector2.zero, 0, targetLayerMask);
        if(hit.collider)
        {
            data.target = hit.collider.gameObject;
            return true;
        }

        // FOV check
        foreach (Collider2D collider in fov.hitColliders)
        {
            if (((1 << collider.gameObject.layer) & targetLayerMask.value) != 0)
            {
                data.target = collider.gameObject;
                return true;
            }
        }

        data.target = null;
        return false;
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
