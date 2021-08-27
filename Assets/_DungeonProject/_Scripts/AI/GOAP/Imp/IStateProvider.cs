using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWorldStateProvider
{
    IEnumerable<KeyValuePair<string, object>> GetWorldState();
}

public interface IGoalStateProvider
{
    IEnumerable<KeyValuePair<string, object>> GetGoalState();
}