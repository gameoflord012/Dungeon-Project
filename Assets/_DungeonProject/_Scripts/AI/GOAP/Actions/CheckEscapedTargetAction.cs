using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEscapedTargetAction : GoapActionBase
{
    [field: SerializeField] public override float Cost { get; set; } = 0;

    public override bool checkProceduralPrecondition(GoapAgent agent)
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
        yield return new KeyValuePair<string, object>("Running", true);
    }    

    public override IEnumerator<PerformState> perform(GoapAgent agent)
    {
        ((EnemyGoapAgent)agent).StopAgent();

        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime;
            yield return PerformState.succeed;
        }

        LookAround(90);

        time = 0;
        while (time < 2)
        {
            time += Time.deltaTime;
            yield return PerformState.succeed;
        }

        LookAround(-90);

        time = 0;
        while (time < 2)
        {
            time += Time.deltaTime;
            yield return PerformState.succeed;
        }

        data.EscapedTargetChecked = true;
        yield break;
    }

    //IEnumerator<PerformState> WaitForSeconds(float seconds)
    //{
    //    float time = 0;
    //    while(time < seconds)
    //    {
    //        time += Time.deltaTime;
    //        yield return PerformState.succeed;
    //    }
    //}

    public void LookAround(float angle)
    {
        Vector2 pointerDirection = Quaternion.Euler(0, 0, angle) * (data.LastTargetPosition - transform.position);
        inputEvents.OnPointerPositionChangedCallback((Vector2)transform.position + pointerDirection);
    }

    public override bool isInRange()
    {
        return (data.LastTargetPosition - transform.position).LengthSmalllerThan(data.DistanceOffset);
    }

    public override Vector3 GetTargetPosition()
    {
        return data.LastTargetPosition;
    }
}