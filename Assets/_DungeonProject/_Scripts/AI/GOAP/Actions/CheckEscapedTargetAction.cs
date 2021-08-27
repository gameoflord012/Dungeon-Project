using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEscapedTargetAction : GoapActionBase
{
    [field: SerializeField] public override float Cost { get; set; } = 0;
    [SerializeField] float targetEscapedPositionOffset = .2f;

    public override bool checkProceduralPrecondition(GameObject agent)
    {
        return data.Target == null;
    }

    public override IEnumerable<KeyValuePair<string, object>> GetEffects()
    {
        yield return new KeyValuePair<string, object>("EscapedTargetChecked", true);
    }

    public override IEnumerable<KeyValuePair<string, object>> GetPreconditions()
    {
        yield return new KeyValuePair<string, object>("EscapedTargetChecked", false);
    }

    public override IEnumerator<PerformState> perform(GameObject agent)
    {
        data.EscapedTargetChecked = true;
        yield break;
    }

    public override bool isInRange()
    {
        return (data.LastTargetPosition - transform.position).sqrMagnitude < targetEscapedPositionOffset * targetEscapedPositionOffset;
    }

    public override Vector3 GetTargetPosition()
    {
        return data.LastTargetPosition;
    }
}