using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAction : GoapAction
{
    protected ActorInputEvents inputEvents;
    protected ActorMovement movement;
    protected Damager damager;
    protected EnemyTaskData data;
    protected FOV fov;
    protected AIPathControl pathControl;

    [SerializeField] float patrolDestinationOffset = .2f;
    [SerializeField] PathNode currentPathNode;
    [SerializeField] MovementDataSO patrolMovementData;

    protected virtual void Awake()
    {
        damager = GetComponentInParent<Damager>();
        movement = GetComponentInParent<ActorMovement>();
        data = GetComponentInParent<EnemyTaskData>();
        inputEvents = GetComponentInParent<ActorInputEvents>();
        fov = movement.GetComponentInChildren<FOV>();
        pathControl = GetComponentInParent<AIPathControl>();

        addEffect("Patrol", true);
    }

    public override bool checkProceduralPrecondition(GameObject agent)
    {
        return true;
    }

    public override bool isDone()
    {
        if (pathControl.IsDestinationReached())
        {
            pathControl.CancelPathFinding();
            AdvancedPath();
            return true;
        }
        return false;
    }

    public override bool perform(GameObject agent)
    {
        if (!pathControl.IsSearchingForPath)
        {
            inputEvents.OnPointerPositionChangedCallback(pathControl.GetCurrentWaypoint());
        }        

        return true;
    }

    public override bool requiresInRange()
    {
        return false;
    }

    public override void reset()
    {
        movement.SetMovementData(patrolMovementData);
        pathControl.SetDestination(currentPathNode.transform.position, patrolDestinationOffset);        
    }

    public bool AdvancedPath()
    {
        if (currentPathNode == null || currentPathNode.neighbors.Count == 0) return false;
        currentPathNode = currentPathNode.neighbors[Random.Range(0, currentPathNode.neighbors.Count - 1)];
        return true;
    }
}
