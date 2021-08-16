using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{    
    public Color customColor = Color.red;

    public IEnumerable<PathNode> GetNodes()
    {
        return GetComponentsInChildren<PathNode>();
    }

    public PathNode CreateNode(PathNode neighbor)
    {        
        PathNode newNode = new GameObject("New node").AddComponent<PathNode>();
        newNode.transform.SetParent(transform);

        if(neighbor != null)
        {
            newNode.AddNeighbor(neighbor);
            neighbor.AddNeighbor(newNode);
        }

        Undo.RegisterCreatedObjectUndo(newNode, "Create new Node");

        return newNode;
    }
}
