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

    public PathNode CreateNode(PathNode fromNode)
    {
        GameObject GO = new GameObject("New node");
        Undo.RegisterCreatedObjectUndo(GO, "Create new Node");

        PathNode newNode = GO.AddComponent<PathNode>();
        newNode.transform.SetParent(transform);

        if(fromNode != null)
        {
            fromNode.AddNeighbor(newNode);
        }

        return newNode;
    }
}
