using Panda;
using UnityEngine;
using UnityEngine.Events;

public class PatrolTask : MonoBehaviour
{
    [SerializeField] float patrolDestinationOffset = .2f;   
    [SerializeField] PathNode currentPathNode;
    [SerializeField] MovementDataSO patrolMovementData;

    ActorMovement actorMovement;
    ActorInputEvents actorControl;

    private void Awake()
    {
        actorControl = GetComponentInParent<ActorInputEvents>();
        actorMovement = GetComponentInParent<ActorMovement>();
    }

    [Task]
    public void MoveToPatrolPosition()
    {
        if(Task.current.isStarting)
        {
            actorMovement.SetMovementData(patrolMovementData);
        }

        actorControl.OnMovementKeyPressed?.Invoke(currentPathNode.transform.position - transform.position);
        actorControl.OnPointerPositionChanged?.Invoke(currentPathNode.transform.position);

        if (IsArrivedAtPatrolPosition())
        {
            actorControl.OnMovementKeyPressed?.Invoke(Vector2.zero);
            Task.current.Succeed();
        }

        if(Task.isInspected)
        {
            Task.current.debugInfo = "Distance: " + (currentPathNode.transform.position - transform.position).magnitude;
        }
    }    

    private bool IsArrivedAtPatrolPosition()
    {
        return ((Vector2)currentPathNode.transform.position - (Vector2)transform.position).sqrMagnitude < 
            patrolDestinationOffset * patrolDestinationOffset;
    }

    [Task]
    public bool AdvancedPath()
    {        
        if (currentPathNode == null || currentPathNode.neighbors.Count == 0) return false;
        currentPathNode = currentPathNode.neighbors[Random.Range(0, currentPathNode.neighbors.Count - 1)];
        return true;
    }
}
