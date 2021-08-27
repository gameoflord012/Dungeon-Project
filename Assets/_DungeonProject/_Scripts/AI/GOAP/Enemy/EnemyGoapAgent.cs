using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyGoapAgent : GoapAgent, IReceivePlannerCallbacks, IWorldStateProvider
{
    [SerializeField] LayerMask targetLayerMask;
    [SerializeField] float closeRangeDetectDistance = 2f;

    protected ActorInputEvents inputEvents;
    protected AIPathControl pathControl;
    protected EnemyTaskData data;
    protected FOV fov;

    private void Awake()
    {
        pathControl = GetComponentInParent<AIPathControl>();
        inputEvents = GetComponentInParent<ActorInputEvents>();
        fov = inputEvents.GetComponentInChildren<FOV>();
        data = GetComponentInParent<EnemyTaskData>();

        data.OnTargetChanged.AddListener(x => Replan());
    }

    protected override void Update()
    {
        base.Update();

        List<GameObject> chaseTargets = FindChaseTargets().ToList();
        if (chaseTargets.Count == 0)
        {
            if (data.Target != null)
            {
                data.EscapedTargetChecked = false;
                data.LastTargetPosition = data.Target.transform.position;
                data.Target = null;
            }
        }
        else if (!chaseTargets.Contains(data.Target)) data.Target = chaseTargets[0];
    }

    public void OnAgentBeingAttacked(Damager damager)
    {
        data.EscapedTargetChecked = false;
        data.LastTargetPosition = damager.damageDealer.transform.position;
    }

    public IEnumerable<KeyValuePair<string, object>> GetWorldState()
    {
        yield return new KeyValuePair<string, object>("HasTarget", data.Target != null);
        yield return new KeyValuePair<string, object>("EscapedTargetChecked", data.EscapedTargetChecked);
    }

    protected override bool moveAgent(IGoapAction nextAction)
    {
        inputEvents.OnMovementKeyPressedCallback(nextAction.GetTargetPosition() - transform.position);
        inputEvents.OnPointerPositionChangedCallback(nextAction.GetTargetPosition());
        return true;
    }

    #region Callbacks
    public void actionsFinished()
    {
        throw new System.NotImplementedException();
    }    

    public void planAborted(IGoapAction aborter)
    {
        inputEvents.OnMovementKeyPressedCallback(Vector2.zero);
        inputEvents.OnPointerPositionChangedCallback(transform.position);
    }

    public void planFailed(IGoalStateProvider goalStateProvider)
    {
        
    }
    public void planFound(IGoalStateProvider goalStateProvider, Queue<IGoapAction> actions)
    {
    }
    #endregion


    #region FindChaseTarget Logic
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
            if (collider == null) continue;

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
    #endregion
}