using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

[RequireComponent(typeof(Seeker))]
public class AIPathControl : MonoBehaviour
{
    Path path;
    Seeker seeker;
    private float destinationOffset = .2f;
    private int currentVectorPathIndex = 0;

    public UnityEvent<Vector2> OnPathFindingDirectionUpdate;
    public UnityEvent<Vector2> OnWaypointUpdate;    

    public bool IsSearchingForPath { get => !seeker.IsDone(); }

    private void Awake()
    {
        seeker = GetComponent<Seeker>();        
    }

    public void SetDestination(Vector2 destination, float destinationOffset = .1f)
    {
        enabled = true;
        seeker.CancelCurrentPathRequest();
        currentVectorPathIndex = 0;
        seeker.StartPath(transform.position, destination, OnPathComplete);
        this.destinationOffset = destinationOffset;
    }

    public void CancelPathFinding()
    {
        enabled = false;
        OnPathFindingDirectionUpdate?.Invoke(Vector2.zero);
        OnWaypointUpdate?.Invoke(transform.position);
    }

    private void OnPathComplete(Path p)
    {
        if (p.error) return;        
        path = p;
    }

    private void FixedUpdate()
    {
        if (path == null) return;

        OnPathFindingDirectionUpdate?.Invoke(GetCurrentWaypoint() - (Vector2)transform.position);

        if (((Vector2)transform.position - GetCurrentWaypoint()).sqrMagnitude < destinationOffset * destinationOffset)
        {            
            currentVectorPathIndex++;
            OnWaypointUpdate?.Invoke(GetCurrentWaypoint());
        }
    }

    public void StopMoving()
    {
        seeker.CancelCurrentPathRequest();
        OnPathFindingDirectionUpdate?.Invoke(Vector2.zero);
        path = null;
    }

    public bool HasPath()
    {
        return path != null;
    }

    public Vector2 GetCurrentDestination()
    {
        return path.vectorPath[path.vectorPath.Count - 1];
    }

    public Vector2 GetCurrentWaypoint()
    {
        Assert.IsNotNull(path);
        return path.vectorPath[Mathf.Clamp(currentVectorPathIndex, 0, path.vectorPath.Count - 1)];
    }
}
