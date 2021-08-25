using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GoapActionBase : MonoBehaviour, IGoapAction
{
    protected ActorInputEvents inputEvents;
    protected ActorMovement movement;
    protected Damager damager;
    protected EnemyTaskData data;
    protected FOV fov;
    protected AIPathControl pathControl;

    public abstract GameObject Target { get; set; }
    public abstract float Cost { get; set; }

    protected virtual void Awake()
    {
        damager = GetComponentInParent<Damager>();
        movement = GetComponentInParent<ActorMovement>();
        data = GetComponentInParent<EnemyTaskData>();
        inputEvents = GetComponentInParent<ActorInputEvents>();
        fov = movement.GetComponentInChildren<FOV>();
        pathControl = GetComponentInParent<AIPathControl>();
        data.OnTargetChanged.AddListener(OnTargetChanged);
    }

    public abstract IEnumerable<KeyValuePair<string, object>> GetEffects();
    public abstract IEnumerable<KeyValuePair<string, object>> GetPreconditions();
    public abstract bool isDone();
    public abstract bool perform(GameObject agent);

    public virtual bool checkProceduralPrecondition(GameObject agent) { return true; }
    public virtual void OnTargetChanged(GameObject target) { }
    public virtual bool isInRange() { return true; }
    public virtual void reset() { }  

}
