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

    public UnityEvent<Vector2> OnPathFindingUpdate;
    public UnityEvent<Vector2> OnDestinationUpdate;

    private void Awake()
    {
        seeker = GetComponent<Seeker>();        
    }

    public void SetDestination(Vector2 destination, float destinationOffset = .2f)
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
        OnPathFindingUpdate?.Invoke(Vector2.zero);
        OnDestinationUpdate?.Invoke(transform.position);
    }

    private void OnPathComplete(Path p)
    {
        if (p.error) return;        
        path = p;
    }

    public bool IsDestinationReached()
    {
        if (path == null) return true;
        return currentVectorPathIndex >= path.vectorPath.Count;
    }

    private void FixedUpdate()
    {
        if (IsDestinationReached()) return;
        OnPathFindingUpdate?.Invoke(GetCurrentWaypoint() - (Vector2)transform.position);

        if (((Vector2)transform.position - GetCurrentWaypoint()).sqrMagnitude < destinationOffset * destinationOffset)
        {            
            currentVectorPathIndex++;
            OnDestinationUpdate?.Invoke(GetCurrentWaypoint());
        }
    }

    private Vector2 GetCurrentWaypoint()
    {        
        return path.vectorPath[Mathf.Clamp(currentVectorPathIndex, 0, path.vectorPath.Count - 1)];
    }
}
