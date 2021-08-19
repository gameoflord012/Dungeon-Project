﻿using UnityEditor;

public static class SceneViewExtension
{
    public static void Focus()
    {
        if (SceneView.sceneViews.Count > 0)
        {
            SceneView sceneView = (SceneView)SceneView.sceneViews[0];
            sceneView.Focus();
        }
    }
}