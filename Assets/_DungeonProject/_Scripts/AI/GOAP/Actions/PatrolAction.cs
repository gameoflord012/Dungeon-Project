using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAction : GoapActionBase
{
    [field: SerializeField] public override float Cost { get; set; } = 1;

    [SerializeField] PathNode currentPathNode;
    [SerializeField] MovementDataSO patrolMovementData;

    protected override void Awake()
    {
        base.Awake();
        if (currentPathNode == null)
            currentPathNode = FindObjectOfType<PathNode>();
    }

    public override bool checkProceduralPrecondition(GoapAgent agent)
    {        
        return currentPathNode != null && data.Target == null && data.EscapedTargetChecked;
    }

    public override IEnumerator<PerformState> perform(GoapAgent agent)
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
        return (currentPathNode.transform.position - transform.position).LengthSmalllerThan(data.DestinationOffset);
    }

    public override Vector3 GetTargetPosition()
    {
        return currentPathNode.transform.position;
    }
}
