using Panda;
using UnityEngine;
using UnityEngine.Events;

public class PatrolTask : EnemyTaskBase
{
    [SerializeField] float patrolDestinationOffset = .2f;   
    [SerializeField] PathNode currentPathNode;
    [SerializeField] MovementDataSO patrolMovementData;

    [Task]
    public void MoveToPatrolPosition()
    {
        if(Task.current.isStarting)
        {
            movement.SetMovementData(patrolMovementData);
        }

        inputEvents.OnMovementKeyPressedCallback (currentPathNode.transform.position - transform.position);
        inputEvents.OnPointerPositionChangedCallback(currentPathNode.transform.position);

        if (IsArrivedAtPatrolPosition())
        {
            inputEvents.OnMovementKeyPressedCallback(Vector2.zero);
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
