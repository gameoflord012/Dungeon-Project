using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GoapActionBase : MonoBehaviour, IGoapAction
{
    protected ActorInputEvents inputEvents;
    protected ActorMovement movement;
    protected EnemyTaskData data;
    protected AIPathControl pathControl;    

    public abstract GameObject Target { get; set; }
    public abstract float Cost { get; set; }

    protected virtual void Awake()
    {        
        movement = GetComponentInParent<ActorMovement>();
        data = GetComponentInParent<EnemyTaskData>();
        inputEvents = GetComponentInParent<ActorInputEvents>();
        pathControl = GetComponentInParent<AIPathControl>();
        data.OnTargetChanged.AddListener(OnTargetChanged);
    }

    public abstract IEnumerable<KeyValuePair<string, object>> GetEffects();
    public abstract IEnumerable<KeyValuePair<string, object>> GetPreconditions();
    public abstract IEnumerator<PerformState> perform(GameObject agent);

    public virtual bool checkProceduralPrecondition(GameObject agent) { return true; }
    public virtual void OnTargetChanged(GameObject target) { }
    public virtual bool isInRange() { return true; }
    public virtual void reset() { }  

}
