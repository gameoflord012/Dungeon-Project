using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class PathNode : MonoBehaviour
{
    PatrolPath patrolPath;
    bool isSelected = false;

    public bool IsSelected { get => isSelected; }

    public float radius = .5f;
    public List<PathNode> neighbors = new List<PathNode>();

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
#if UNITY_EDITOR
        Selection.selectionChanged += OnSelectionChanged;
#endif
    }

    private void OnDisable()
    {
#if UNITY_EDITOR
        Selection.selectionChanged -= OnSelectionChanged;
#endif
    }

    private void OnSelectionChanged()
    {
#if UNITY_EDITOR
        isSelected = Selection.activeGameObject == gameObject;

        if (isSelected)
        {
            SceneViewExtension.Focus();
        }
#endif
    }

    public void AddNeighbor(PathNode neighbor)
    {
        if (neighbor == this || neighbors.Contains(neighbor)) return;
#if UNITY_EDITOR
        Undo.RecordObject(this, "Add neighbor");
#endif
        neighbors.Add(neighbor);
    }

    public void RemoveNeighbor(PathNode neighbor)
    {
        neighbors.Remove(neighbor);
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
        foreach (var from in FindObjectsOfType<PathNode>())
        {
            from.RemoveNeighbor(this);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = isSelected ? Color.gray : PatrolPath.customColor;
        Gizmos.DrawSphere(transform.position, radius);

        Gizmos.color = PatrolPath.customColor;
        foreach (PathNode neighbor in neighbors)
        {
            DrawArrow.ForGizmo(transform.position, neighbor.transform.position);
        }
    }
}