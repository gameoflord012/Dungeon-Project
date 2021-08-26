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
            EditorGUILayout.LabelField($"CurrentState:  { goapAgent.GetCurrentFSMName()}", EditorStyles.boldLabel);

            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("CurrentAction:", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            {
                foreach (IGoapAction action in goapAgent.GetFinisedAction())                
                    EditorGUILayout.LabelField(action.GetType().Name, greenStyle);                

                foreach (IGoapAction action in goapAgent.GetCurrentActions())
                    EditorGUILayout.LabelField(action.GetType().Name, grayStyle);                
            }

            EditorGUI.indentLevel--;

            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("WorldStates:", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            {
                foreach (var worldState in goapAgent.GetWorldStates())
                    EditorGUILayout.LabelField(worldState.Key + ": " + worldState.Value);
            }
            EditorGUI.indentLevel--;

            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("GoalStates:", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            {
                foreach (var goalState in goapAgent.GetGoalStates())
                    EditorGUILayout.LabelField(goalState.Key + ": " + goalState.Value);
            }
            EditorGUI.indentLevel--;

            GUI.changed = true;
        }
    }
}