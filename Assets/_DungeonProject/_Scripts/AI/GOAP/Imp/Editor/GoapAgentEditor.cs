using System.Collections;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GoapAgent))]
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
            //EditorGUILayout.LabelField($"CurrentState:  { goapAgent.GetCurrentFSMName()}", EditorStyles.boldLabel);
            EditorGUILayout.LabelField($"<b>CurrentGoap:</b>  {goapAgent.GetCurrentGoapName()}", richText);

            EditorGUILayout.Space(5);
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
        }        
    }
}