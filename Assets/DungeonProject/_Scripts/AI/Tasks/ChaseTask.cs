using Panda;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTask : MonoBehaviour
{
    [SerializeField] FOV fov;
    [SerializeField] LayerMask targetLayerMask;
    [SerializeField] float chaseDestinationOffset = .2f;

    private ActorInputEvents actorControl = null;

    private GameObject chaseTarget;

    private void Awake()
    {
        if(fov == null)
            fov = GetComponentInParent<ActorMovement>().GetComponentInChildren<FOV>();

        actorControl = GetComponentInParent<ActorInputEvents>();
    }


    [Task]
    public bool IsTargetDetected()
    {
        return GetFOVChaseTarget() != null;
    }    

    [Task]
    public void ChaseTarget()
    {        
        if(Task.current.isStarting)
        {
            chaseTarget = GetFOVChaseTarget();
        }

        Vector3 targetPosition = chaseTarget.transform.position;

        actorControl.OnMovementKeyPressed?.Invoke(targetPosition - transform.position);
        actorControl.OnPointerPositionChanged?.Invoke(targetPosition);

        if ((targetPosition - transform.position).sqrMagnitude < chaseDestinationOffset)
            Task.current.Succeed();
    }

    private GameObject GetFOVChaseTarget()
    {
        foreach (Collider2D collider in fov.hitColliders)
        {
            if (((1 << collider.gameObject.layer) & targetLayerMask.value) != 0)
                return collider.gameObject;
        }
        return null;
    }
}
