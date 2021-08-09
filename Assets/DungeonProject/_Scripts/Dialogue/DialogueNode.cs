using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Core;

namespace Dialogue
{
    public class DialogueNode : ScriptableObject
    {
        [SerializeField] bool isPlayerSpeaking = false;
        [SerializeField] string text = "New text";
        [SerializeField] List<string> children = new List<string>();
        [SerializeField] Rect rect = new Rect(100, 100, 200, 100);
        [SerializeField] string onEnterAction;
        [SerializeField] string onExitAction;
        [SerializeField] Condition condition;

        public Rect GetRect()
        {
            return rect;
        }

#if UNITY_EDITOR
        public void SetRect(Vector2 newPosition)
        {
            Undo.RecordObject(this, "Move Dialogue Node");
            rect.position = newPosition;
            EditorUtility.SetDirty(this);
        }
#endif

        public string GetText()
        {
            return text;
        }

        public void SetText(string newText)
        {
            if (newText != text)
            {
                Undo.RecordObject(this, "Update Dialogue Node");
                text = newText;
                EditorUtility.SetDirty(this);
            }
        }

        public List<string> GetChildren()
        {
            return children;
        }

        public void AddChild(string childID)
        {
            Undo.RecordObject(this, "Add Dialogue Link");
            children.Add(childID);
            EditorUtility.SetDirty(this);
        }

        public void RemoveChild(string childID)
        {
            Undo.RecordObject(this, "Remove Dialogue Link");
            children.Remove(childID);
            EditorUtility.SetDirty(this);
        }

        public bool IsPlayerSpeaking()
        {
            return isPlayerSpeaking;
        }

        public void SetPlayerSpeaking(bool newIsPlayerSpeaking)
        {
            Undo.RecordObject(this, "Change Dialogue Speaker");
            isPlayerSpeaking = newIsPlayerSpeaking;
            EditorUtility.SetDirty(this);
        }

        public string GetOnEnterAction()
        {
            return onEnterAction;
        }

        public string GetOnExitAction()
        {
            return onExitAction;
        }

        public bool IsSatisfiedCondition(IEnumerable<IPredicateEvaluator> evaluators)
        {
            return condition.IsSatisfied(evaluators);
        }
    }
}