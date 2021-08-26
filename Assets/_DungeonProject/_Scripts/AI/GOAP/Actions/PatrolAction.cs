using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAction : GoapActionBase
{
    [SerializeField] float patrolDestinationOffset = .2f;
    [SerializeField] PathNode currentPathNode;
    [SerializeField] MovementDataSO patrolMovementData;

    [field: SerializeField] public override float Cost { get; set; } = 1;
    [field: SerializeField] public override GameObject Target { get; set; }

    public override IEnumerator<PerformState> perform(GameObject agent)
    {        
        yield return AdvancedPath() ? PerformState.succeed : PerformState.falied;
    }

    public bool AdvancedPath()
    {
        if (currentPathNode == null || currentPathNode.neighbors.Count == 0) return false;
        currentPathNode = currentPathNode.neighbors[Random.Range(0, currentPathNode.neighbors.Count - 1)];
        return true;
    }

    public override IEnumerable<KeyValuePair<string, object>> GetPreconditions()
    {
        yield return new KeyValuePair<string, object>("Walking", true);
    }

    public override IEnumerable<KeyValuePair<string, object>> GetEffects()
    {
        yield return new KeyValuePair<string, object>("Patrol", true);        
    }    

    public override bool isInRange()
    {
        if (Target == null) return true;
        return (Target.transform.position - transform.position).sqrMagnitude < patrolDestinationOffset * patrolDestinationOffset;
    }

    public override void reset()
    {
        Target = currentPathNode.gameObject;
    }
}
