using System.Collections;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GoapAgent), true)]
public class GoapAgentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();        

        GoapAgent goapAgent = (GoapAgent)target;
        GUIStyle richText = new GUIStyle();
        richText.richText = true;

        if (Application.isPlaying)
        {            
            //EditorGUILayout.LabelField($"<b>CurrentGoap:</b>  {goapAgent.GetCurrentGoapName()}", richText);            
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("<color=blue><b>Goap Planner Status</b></color>", richText);
            EditorGUILayout.LabelField("CurrentAction:", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            {
                foreach (IGoapAction action in goapAgent.GetFinisedAction())                
                    EditorGUILayout.LabelField($"<color=green>{action.GetType().Name}</color>", richText);                

                foreach (IGoapAction action in goapAgent.GetCurrentActions())
                    EditorGUILayout.LabelField($"<color=grey>{action.GetType().Name}</color>", richText);
            }

            EditorGUI.indentLevel--;

            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("WorldStates:", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            {
                foreach (var worldState in goapAgent.GetCurrentWorldState())
                    EditorGUILayout.LabelField(worldState.Key + ": " + worldState.Value);
            }
            EditorGUI.indentLevel--;

            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("GoalStates:", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            {
                foreach (var goalState in goapAgent.GetCurrentGoalState())
                    EditorGUILayout.LabelField(goalState.Key + ": " + goalState.Value);
            }
            EditorGUI.indentLevel--;
        }        
    }
}