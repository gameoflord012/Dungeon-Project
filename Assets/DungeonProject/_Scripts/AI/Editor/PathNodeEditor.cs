using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathNode))]
public class PathNodeEditor : Editor
{
    PathNode pathNode;

    private void OnEnable()
    {
        pathNode = (PathNode)target;
    }

    private void OnSceneGUI()
    {
        if (NewNodeKeyPressed())
        {
            PathNode newPathNode = pathNode.CreateNeighbor();
            newPathNode.transform.position = GetMousePosition();
            Selection.activeObject = newPathNode;

            Debug.Log("Hello");
        }
    }

    bool NewNodeKeyPressed()
    {
        Event e = Event.current;
        return e.type == EventType.KeyDown && e.keyCode == KeyCode.N;
    }

    public Vector2 GetMousePosition()
    {
        return HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin;
    }
}
