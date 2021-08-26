using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTargetAction : GoapActionBase
{
    [SerializeField] LayerMask targetLayerMask;
    [SerializeField] float closeRangeDetectDistance = 2f;

    [field: SerializeField] public override GameObject Target { get; set; }
    [field: SerializeField] public override float Cost { get; set; }

    public override IEnumerable<KeyValuePair<string, object>> GetEffects()
    {
        yield return new KeyValuePair<string, object>("HasTarget", true);
    }

    public override IEnumerable<KeyValuePair<string, object>> GetPreconditions()
    {
        yield break;
    }

    public override IEnumerator<PerformState> perform(GameObject agent)
    {
        foreach (GameObject chaseTarget in FindChaseTargets())
        {
            data.Target = chaseTarget;
            yield break;
        }
        yield return PerformState.falied;
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