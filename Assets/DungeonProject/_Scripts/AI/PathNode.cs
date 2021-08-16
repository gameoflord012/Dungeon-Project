using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class PathNode : MonoBehaviour, ISerializationCallbackReceiver
{
    PatrolPath patrolPath;
    bool isSelected = false;

    public bool IsSelected { get => isSelected; }

    public float radius = .5f;
    public HashSet<PathNode> neighbors = new HashSet<PathNode>();

    [SerializeField, HideInInspector]
    List<PathNode> serializedNeighbors;

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

    private void OnDisable()
    {
        Selection.selectionChanged -= OnSelectionChanged;
    }

    private void OnSelectionChanged()
    {
        isSelected = Selection.activeGameObject == gameObject;

        if(isSelected)
        {
            SceneViewExtension.Focus();
        }
    }    

    public void AddNeighbor(PathNode neighbor)
    {
        if (neighbor == this) return;
        Undo.RecordObject(this, "Add neighbor");
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
        foreach(var from in FindObjectsOfType<PathNode>())
        {
            from.neighbors.Remove(this); 
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = isSelected ? Color.gray : PatrolPath.customColor;
        Gizmos.DrawSphere(transform.position, radius);

        Gizmos.color = PatrolPath.customColor;
        foreach(PathNode neighbor in neighbors)
        {
            DrawArrow.ForGizmo(transform.position, neighbor.transform.position);
        }
    }

    public void OnBeforeSerialize()
    {
        serializedNeighbors = neighbors.ToList();
    }

    public void OnAfterDeserialize()
    {
        neighbors = new HashSet<PathNode>(serializedNeighbors);
    }
}