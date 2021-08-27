using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEscapedTargetGoal : MonoBehaviour, IGoalStateProvider
{
    public IEnumerable<KeyValuePair<string, object>> GetGoalState()
    {        
        yield return new KeyValuePair<string, object>("EscapedTargetChecked", true);
    }
}