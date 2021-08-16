using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class PathNode : MonoBehaviour
{
    PatrolPath patrolPath;
    bool isSelected = false;

    public PatrolPath PatrolPath
    {
        get
        {
            if (patrolPath == null)
                patrolPath = GetComponentInParent<PatrolPath>();

            return patrolPath;
        }        
    }

    private void OnEnable()
    {
        Selection.selectionChanged += OnSelectionChanged;
    }

    private void OnSelectionChanged()
    {
        isSelected = Selection.activeGameObject == gameObject;
    }

    public float radius = .5f;
    public List<PathNode> neighbors = new List<PathNode>();

    public void AddNeighbor(PathNode neighbor)
    {
        neighbors.Add(neighbor);
    }

    public PathNode CreateNeighbor()
    {
        return PatrolPath.CreateNode(this);
    }

    public bool IsPointingTo(Vector2 pointerPosition)
    {
        return ((pointerPosition - (Vector2)transform.position).sqrMagnitude < radius * radius);
    }

    private void OnDestroy()
    {
        foreach(var neighbor in neighbors)
        {
            neighbor.neighbors.Remove(this);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = isSelected ? Color.gray : PatrolPath.customColor;
        Gizmos.DrawSphere(transform.position, radius);

        Gizmos.color = PatrolPath.customColor;
        foreach(PathNode neighbor in neighbors)
        {
            Gizmos.DrawLine(transform.position, neighbor.transform.position);
        }
    }
}