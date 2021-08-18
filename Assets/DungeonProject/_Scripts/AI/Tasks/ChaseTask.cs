using Panda;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTask : EnemyTaskBase
{
    [SerializeField] FOV fov;
    [SerializeField] LayerMask targetLayerMask;
    [SerializeField] float chaseDestinationOffset = .2f;
    [SerializeField] MovementDataSO chaseMovementData;

    ActorMovement actorMovement;
    ActorInputEvents actorControl = null;

    private void Awake()
    {
        actorControl = GetComponentInParent<ActorInputEvents>();
        actorMovement = GetComponentInParent<ActorMovement>();

        if(fov == null)
            fov = actorMovement.GetComponentInChildren<FOV>();
    }
    [Task]
    public void ChaseTarget()
    {        
        if(Task.current.isStarting)
        {
            actorMovement.SetMovementData(chaseMovementData);
        }

        Vector3 targetPosition = target.transform.position;

        actorControl.OnMovementKeyPressed?.Invoke(targetPosition - transform.position);
        actorControl.OnPointerPositionChanged?.Invoke(targetPosition);

        if ((targetPosition - transform.position).sqrMagnitude < chaseDestinationOffset)
            Task.current.Succeed();
    }

    [Task]
    public bool GetFOVChaseTarget()
    {
        foreach (Collider2D collider in fov.hitColliders)
        {
            if (((1 << collider.gameObject.layer) & targetLayerMask.value) != 0)
            {
                target = collider.gameObject;
                return true;
            }
        }

        target = null;
        return false;
    }
}
