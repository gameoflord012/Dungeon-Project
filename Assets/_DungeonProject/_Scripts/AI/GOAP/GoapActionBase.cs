using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GoapActionBase : MonoBehaviour, IGoapAction
{
    protected ActorInputEvents inputEvents;
    protected ActorMovement movement;
    protected EnemyWorldData data;
    protected AIPathControl pathControl;    

    public abstract float Cost { get; set; }

    protected virtual void Awake()
    {        
        movement = GetComponentInParent<ActorMovement>();
        data = GetComponentInParent<EnemyWorldData>();
        inputEvents = GetComponentInParent<ActorInputEvents>();
        pathControl = GetComponentInParent<AIPathControl>();
        data.OnTargetChanged.AddListener(OnTargetChanged);
    }

    public abstract IEnumerable<KeyValuePair<string, object>> GetEffects();
    public abstract IEnumerable<KeyValuePair<string, object>> GetPreconditions();
    public abstract IEnumerator<PerformState> perform(GoapAgent agent);

    public virtual bool checkProceduralPrecondition(GoapAgent agent) { return true; }
    public virtual void OnTargetChanged(GameObject target) { }
    public virtual bool isInRange() { return true; }
    public virtual void reset() { }
    public virtual Vector3 GetTargetPosition() { return transform.position; }
}
