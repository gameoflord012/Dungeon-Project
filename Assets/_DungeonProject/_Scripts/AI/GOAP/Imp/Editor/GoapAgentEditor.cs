using System.Collections;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GoapAgent))]
public class GoapAgentEditor : Editor
{
    GUIStyle greenStyle;
    GUIStyle grayStyle;

    private void OnEnable()
    {
        greenStyle = new GUIStyle();
        greenStyle.normal.textColor = Color.green;

        grayStyle = new GUIStyle();
        grayStyle.normal.textColor = Color.gray;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GoapAgent goapAgent = (GoapAgent)target;

        if(Application.isPlaying)
        {
            EditorGUILayout.LabelField("CurrentState: " + goapAgent.GetCurrentFSMName());

            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("CurrentAction:");
            EditorGUI.indentLevel++;
            {
                foreach (IGoapAction action in goapAgent.GetFinisedAction())                
                    EditorGUILayout.LabelField(action.GetType().Name, greenStyle);                

                foreach (IGoapAction action in goapAgent.GetCurrentActions())
                    EditorGUILayout.LabelField(action.GetType().Name, grayStyle);                
            }
            EditorGUI.indentLevel--;
        }
    }
}