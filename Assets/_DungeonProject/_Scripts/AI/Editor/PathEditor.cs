using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PatrolPath))]
public class PathEditor : Editor
{
    private void OnSceneGUI()
    {
        Debug.Log("Parent update");
    }
}