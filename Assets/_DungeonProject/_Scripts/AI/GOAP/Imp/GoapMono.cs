using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GoapMono : MonoBehaviour, IGoapAgent, IReceivePlannerCallbacks, IGoalStateProvider, IWorldStateProvider
{
    public abstract IEnumerable<KeyValuePair<string, object>> GetGoalState();
    public abstract IEnumerable<KeyValuePair<string, object>> GetWorldState();

    public virtual void actionsFinished() { }
    public virtual bool moveAgent(IGoapAction nextAction) { return true; }
    public virtual void planAborted(IGoapAction aborter) { }
    public virtual void planFailed(IGoalStateProvider goalStateProvider) { }
    public virtual void planFound(IGoalStateProvider goalStateProvider, Queue<IGoapAction> actions) { }

    
}