using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWalkSpeedAction : GoapActionBase
{
    [field: SerializeField] public override float Cost { get; set; } = 0;

    [SerializeField] MovementDataSO walkingMovementData;

    public override IEnumerable<KeyValuePair<string, object>> GetEffects()
    {
        yield return new KeyValuePair<string, object>("Walking", true);
    }

    public override IEnumerable<KeyValuePair<string, object>> GetPreconditions()
    {
        yield break;
    }

    public override IEnumerator<PerformState> perform(GoapAgent agent)
    {
        movement.SetMovementData(walkingMovementData);
        yield break;
    }
}