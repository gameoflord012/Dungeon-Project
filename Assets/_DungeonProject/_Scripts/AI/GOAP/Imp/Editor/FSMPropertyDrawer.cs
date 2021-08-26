using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(FSM))]
public class FSMPropertyDrawer : PropertyDrawer
{    
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUIStyle richText = new GUIStyle();
        richText.richText = true;

        if(Application.isPlaying)
            EditorGUILayout.LabelField($"<b>CurrentState:</b> {property.FindPropertyRelative("CurrentStateName").stringValue}", richText);
    }
}
