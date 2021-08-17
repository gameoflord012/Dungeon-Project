using Panda;
using UnityEngine;
using UnityEngine.Events;

public class PatrolTask : MonoBehaviour
{
    [SerializeField] float patrolDestinationOffset = .2f;   
    [SerializeField] PathNode currentPathNode;

    private ActorInputEvents actorControl;

    private void Awake()
    {
        actorControl = GetComponentInParent<ActorInputEvents>();        
    }

    [Task]
    public void MoveToPatrolPosition()
    {
        actorControl.OnMovementKeyPressed?.Invoke(new Vector2(1, 1));
    }

    bool AdvancedPath()
    {
        if (currentPathNode.neighbors.Count == 0) return false;
        currentPathNode = currentPathNode.neighbors[Random.Range(0, currentPathNode.neighbors.Count - 1)];
        return true;
    }
}
