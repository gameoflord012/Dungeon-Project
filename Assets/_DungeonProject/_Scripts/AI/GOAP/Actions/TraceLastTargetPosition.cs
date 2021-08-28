using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceLastTargetPosition : GoapActionBase
{
    [field: SerializeField] public override float Cost { get; set; } = 1;

    public override IEnumerable<KeyValuePair<string, object>> GetEffects()
    {
        yield return new KeyValuePair<string, object>("TracedLastPosition", true);
    }

    public override IEnumerable<KeyValuePair<string, object>> GetPreconditions()
    {
        yield break;
    }

    public override IEnumerator<PerformState> perform(GoapAgent agent)
    {
        yield break;
    }

    public override bool isInRange()
    {
        return (transform.position - data.LastTargetPosition).LengthSmalllerThan(data.DestinationOffset);
    }

    public override Vector3 GetTargetPosition()
    {
        return data.LastTargetPosition;
    }
}