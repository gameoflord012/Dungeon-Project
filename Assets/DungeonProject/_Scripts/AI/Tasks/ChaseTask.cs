using Panda;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTask : MonoBehaviour
{
    [SerializeField] FOV fov;
    [SerializeField] LayerMask targetLayerMask;
    [SerializeField] float chaseDestinationOffset = .2f;
    [SerializeField] MovementDataSO chaseMovementData;

    ActorMovement actorMovement;
    ActorInputEvents actorControl = null;
    EnemyTaskData data;

    private void Awake()
    {
        actorControl = GetComponentInParent<ActorInputEvents>();
        actorMovement = GetComponentInParent<ActorMovement>();
        data = GetComponentInParent<EnemyTaskData>();

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

        Vector3 targetPosition = data.target.transform.position;

        actorControl.OnMovementKeyPressedCallback(targetPosition - transform.position);
        actorControl.OnPointerPositionChangedCallback(targetPosition);

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
                data.target = collider.gameObject;
                return true;
            }
        }

        data.target = null;
        return false;
    }
}
