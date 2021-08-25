using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolGoal : MonoBehaviour, IGoap
{
    public void actionsFinished()
    {
        
    }

    public HashSet<KeyValuePair<string, object>> createGoalState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();
        goal.Add(new KeyValuePair<string, object>("Patrol", true));
        return goal;
    }

    public HashSet<KeyValuePair<string, object>> getWorldState()
    {
        return new HashSet<KeyValuePair<string, object>>();
    }

    public bool moveAgent(GoapAction nextAction)
    {
        return true;
    }

    public void planAborted(GoapAction aborter)
    {
        
    }

    public void planFailed(HashSet<KeyValuePair<string, object>> failedGoal)
    {
        
    }

    public void planFound(HashSet<KeyValuePair<string, object>> goal, Queue<GoapAction> actions)
    {
        
    }
}
