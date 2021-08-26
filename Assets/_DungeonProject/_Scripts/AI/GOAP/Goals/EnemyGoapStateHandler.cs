using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GoapAgent))]
public class EnemyGoapStateHandler : GoapMono
{
    [SerializeField] LayerMask targetLayerMask;
    [SerializeField] float closeRangeDetectDistance = 2f;

    protected ActorInputEvents inputEvents;
    protected AIPathControl pathControl;
    protected EnemyTaskData data;
    protected FOV fov;

    protected virtual void Awake()
    {
        pathControl = GetComponentInParent<AIPathControl>();
        inputEvents = GetComponentInParent<ActorInputEvents>();
        fov = inputEvents.GetComponentInChildren<FOV>();
        data = GetComponentInParent<EnemyTaskData>();
    }

    private void Update()
    {
        data.Target = null;
        foreach(GameObject chaseTarget in FindChaseTargets())
            data.Target = chaseTarget;
    }

    public override HashSet<KeyValuePair<string, object>> createGoalState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();
        goal.Add(new KeyValuePair<string, object>("AttackTarget", true));
        return goal;
    }

    public override HashSet<KeyValuePair<string, object>> getWorldState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();
        goal.Add(new KeyValuePair<string, object>("HasTarget", true));
        return goal;
    }

    public override bool moveAgent(IGoapAction nextAction)
    {
        if (nextAction.Target == null)
        {
            inputEvents.OnMovementKeyPressedCallback(Vector2.zero);
            inputEvents.OnPointerPositionChangedCallback(transform.position);
            return false;
        }

        inputEvents.OnMovementKeyPressedCallback(nextAction.Target.transform.position - transform.position);
        inputEvents.OnPointerPositionChangedCallback(nextAction.Target.transform.position);
        return true;
    }

    private IEnumerable<GameObject> FindChaseTargets()
    {
        // Close range check
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, closeRangeDetectDistance, Vector2.zero, 0, targetLayerMask);

        if (hit.collider && !ObscuredByObstacle(hit.transform.position))
        {
            yield return GetTarget(hit.collider.gameObject);
            //data.target = hit.collider.gameObject;
            //return true;
        }

        // FOV check
        foreach (Collider2D collider in fov.GetHitColliders())
        {
            if (((1 << collider.gameObject.layer) & targetLayerMask) != 0)
            {
                yield return GetTarget(collider.gameObject);
                /*data.target = collider.gameObject;*/
            }
        }
    }

    private GameObject GetTarget(GameObject go)
    {
        return go.GetComponentInParent<ActorMovement>().gameObject;
    }

    private bool ObscuredByObstacle(Vector2 targetPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            (targetPosition - (Vector2)transform.position),
            closeRangeDetectDistance,
            1 << LayerMask.NameToLayer("Obstacle"));

        return hit.collider != null;
    }
}
