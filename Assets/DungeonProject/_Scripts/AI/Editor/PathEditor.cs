using System.Collections;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(PatrolPath))]
public class PathEditor : Editor
{
    private void OnSceneGUI()
    {
        Debug.Log("Parent update");
    }
}