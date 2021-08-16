using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathNode))]
public class PathNodeEditor : Editor
{
    private void OnSceneGUI()
    {
        if (CreateNodeKeyPressed())
        {
            CreateNewNode();
        }
    }

    private void CreateNewNode()
    {
        PathNode pathNode = (PathNode)target;
        PathNode newPathNode = pathNode.CreateNeighbor();
        newPathNode.transform.position = GetMousePosition();
        Selection.activeObject = newPathNode;
    }

    bool CreateNodeKeyPressed()
    {
        Event e = Event.current;
        return e.type == EventType.KeyDown && e.keyCode == KeyCode.N;
    }

    public Vector2 GetMousePosition()
    {
        return HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin;
    }
}
