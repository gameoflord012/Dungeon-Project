using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(PathNode))]
public class PathNodeEditor : Editor
{    
    bool isOnCreatingEdge = false;

    PathNode GetNode()
    {
        return (PathNode)target;        
    }   

    private void OnSceneGUI()
    {
        if (!GetNode().IsSelected) return;

        if (CreateNodeKeyPressed())
        {
            CreateNewNode();
            return;
        }

        CreatingEdgeInputHandle();
    }

    public void CreatingEdgeInputHandle()
    {
        if(isOnCreatingEdge)
        {
            Handles.DrawLine(GetNode().transform.position, GetMousePosition());
            HandleUtility.Repaint();

            if (CreateEdgeKeyPressed())
            {
                PathNode targetNode = GetPointingAtPathNode();
                if(targetNode != null) GetNode().AddNeighbor(targetNode);

                isOnCreatingEdge = false;
            }
        }
        else if (CreateEdgeKeyPressed())
        {
            isOnCreatingEdge = true;
        }
    }

    private void CreateNewNode()
    {
        PathNode newPathNode = GetNode().CreateNeighbor();
        newPathNode.transform.position = GetMousePosition();
        Selection.activeObject = newPathNode;
    }

    bool CreateNodeKeyPressed()
    {
        Event e = Event.current;        
        return e.type == EventType.KeyDown && e.keyCode == KeyCode.N;
    }

    private bool CreateEdgeKeyPressed()
    {
        Event e = Event.current;
        return e.type == EventType.KeyDown && e.keyCode == KeyCode.M;
    }

    public Vector2 GetMousePosition()
    {
        return HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin;
    }
    
    public PathNode GetPointingAtPathNode()
    {
        foreach(PathNode node in GetNode().PatrolPath.GetNodes())
        {
            if(node.IsPointingTo(GetMousePosition()))
            {
                return node;
            }
        }
        return null;
    }
}
