using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GoapAgent))]
public class EnemyGoal : MonoBehaviour, IGoap
{
    protected ActorInputEvents inputEvents;
    protected AIPathControl pathControl;

    protected virtual void Awake()
    {
        pathControl = GetComponentInParent<AIPathControl>();
        inputEvents = GetComponentInParent<ActorInputEvents>();
    }

    public void actionsFinished()
    {
        
    }

    public HashSet<KeyValuePair<string, object>> createGoalState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();
        goal.Add(new KeyValuePair<string, object>("AttackTarget", true));
        return goal;
    }

    public HashSet<KeyValuePair<string, object>> getWorldState()
    {
        return new HashSet<KeyValuePair<string, object>>();
    }

    public bool moveAgent(IGoapAction nextAction)
    {
        inputEvents.OnMovementKeyPressedCallback(nextAction.Target.transform.position - transform.position);
        inputEvents.OnPointerPositionChangedCallback(nextAction.Target.transform.position);
        return true;
    }

    public void planAborted(IGoapAction aborter)
    {
        
    }

    public void planFailed(HashSet<KeyValuePair<string, object>> failedGoal)
    {
        
    }

    public void planFound(HashSet<KeyValuePair<string, object>> goal, Queue<IGoapAction> actions)
    {
        
    }
}
