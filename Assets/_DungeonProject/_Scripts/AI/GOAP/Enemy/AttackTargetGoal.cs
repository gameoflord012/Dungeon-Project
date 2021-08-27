using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTargetGoal : MonoBehaviour, IGoalStateProvider
{
    public IEnumerable<KeyValuePair<string, object>> GetGoalState()
    {
        //yield return new KeyValuePair<string, object>("Patrol", true);
        yield return new KeyValuePair<string, object>("AttackTarget", true);
    }
}
