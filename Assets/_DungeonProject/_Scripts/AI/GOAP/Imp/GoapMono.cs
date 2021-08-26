using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GoapMono : MonoBehaviour, IGoap
{
    public abstract HashSet<KeyValuePair<string, object>> createGoalState();
    public abstract HashSet<KeyValuePair<string, object>> getWorldState();

    public virtual void actionsFinished() { }
    public virtual bool moveAgent(IGoapAction nextAction) { return true; }
    public virtual void planAborted(IGoapAction aborter) { }
    public virtual void planFailed(HashSet<KeyValuePair<string, object>> failedGoal) { }
    public virtual void planFound(HashSet<KeyValuePair<string, object>> goal, Queue<IGoapAction> actions) { }
}