using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Seeker))]
public class AIPathControl : MonoBehaviour
{
    Path path;
    Seeker seeker;
    private float destinationOffset = .2f;    

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
    }

    public void SetDestination(Vector2 destination, float destinationOffset = .2f)
    {
        seeker.CancelCurrentPathRequest();
        seeker.StartPath(transform.position, destination, OnPathComplete);
        this.destinationOffset = destinationOffset;
    }

    private void OnPathComplete(Path p)
    {
        if (p.error) return;
        path = p;
    }

    public bool IsDestinationReached()
    {
        return (path.vectorPath[path.vectorPath.Count - 1] - transform.position).sqrMagnitude < destinationOffset;
    }
}
