using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GoapAgent))]
public class EnemyGoal : GoapMono
{
    protected ActorInputEvents inputEvents;
    protected AIPathControl pathControl;

    protected virtual void Awake()
    {
        pathControl = GetComponentInParent<AIPathControl>();
        inputEvents = GetComponentInParent<ActorInputEvents>();
    }

    public override HashSet<KeyValuePair<string, object>> createGoalState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();
        goal.Add(new KeyValuePair<string, object>("AttackTarget", true));
        return goal;
    }

    public override HashSet<KeyValuePair<string, object>> getWorldState()
    {
        return new HashSet<KeyValuePair<string, object>>();
    }

    public override bool moveAgent(IGoapAction nextAction)
    {
        inputEvents.OnMovementKeyPressedCallback(nextAction.Target.transform.position - transform.position);
        inputEvents.OnPointerPositionChangedCallback(nextAction.Target.transform.position);
        return true;
    }
}
