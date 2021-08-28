using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class PatrolPath : MonoBehaviour
{
    public Color customColor = Color.red;

    private void Awake()
    {
        if(transform.childCount == 0)
        {
            CreateNode(null);
        } 
    }

    public IEnumerable<PathNode> GetNodes()
    {
        return GetComponentsInChildren<PathNode>();
    }

    public PathNode CreateNode(PathNode fromNode)
    {
        GameObject GO = new GameObject("New node");
#if UNITY_EDITOR
        Undo.RegisterCreatedObjectUndo(GO, "Create new Node");
#endif

        PathNode newNode = GO.AddComponent<PathNode>();
        newNode.transform.SetParent(transform);

        if (fromNode != null)
        {
            fromNode.AddNeighbor(newNode);
        }

        return newNode;
    }
}
