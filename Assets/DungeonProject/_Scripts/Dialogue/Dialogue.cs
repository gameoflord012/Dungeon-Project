using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;

namespace Dialogue
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue", order = 0)]
    public class Dialogue : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] List<DialogueNode> nodes = new List<DialogueNode>();
        [SerializeField] Vector2 newNodeOffset = new Vector2(250, 0);

        Dictionary<string, DialogueNode> nodeLookup = new Dictionary<string, DialogueNode>();

        private void OnValidate()
        {
            nodeLookup.Clear();
            foreach (DialogueNode node in GetAllNodes())
                nodeLookup[node.name] = node;
        }

        public IEnumerable<DialogueNode> GetAllNodes()
        {
            return nodes;
        }

        public DialogueNode GetRootNode()
        {
            return nodes[0];
        }

        public IEnumerable<DialogueNode> GetAllChildren(DialogueNode parentNode)
        {
            foreach (string childID in parentNode.GetChildren())
                if (nodeLookup.ContainsKey(childID))
                    yield return nodeLookup[childID];
        }

        public IEnumerable<DialogueNode> GetPlayerChildren(DialogueNode parentNode)
        {
            foreach (DialogueNode node in GetAllChildren(parentNode))
                if (node.IsPlayerSpeaking())
                    yield return node;
        }

        public IEnumerable<DialogueNode> GetAIChildren(DialogueNode parentNode)
        {
            foreach (DialogueNode node in GetAllChildren(parentNode))
                if (!node.IsPlayerSpeaking())
                    yield return node;
        }

#if UNITY_EDITOR
        public void CreateNode(DialogueNode parentNode)
        {
            DialogueNode newNode = MakeNode(parentNode);
            Undo.RegisterCreatedObjectUndo(newNode, "Create Dialogue Node");
            Undo.RecordObject(this, "Add Dialogue Node");
            AddNode(newNode);
        }

        public void DeleteNode(DialogueNode deletedNode)
        {
            Undo.RecordObject(this, "Delete Dialogue Node");
            nodes.Remove(deletedNode);
            OnValidate();
            CleanDanglingChildren(deletedNode);
            Undo.DestroyObjectImmediate(deletedNode);
        }

        private DialogueNode MakeNode(DialogueNode parentNode)
        {
            DialogueNode newNode = CreateInstance<DialogueNode>();
            newNode.name = Guid.NewGuid().ToString();
            if (parentNode != null)
            {
                parentNode.AddChild(newNode.name);
                newNode.SetPlayerSpeaking(!parentNode.IsPlayerSpeaking());
                newNode.SetRect(parentNode.GetRect().position + newNodeOffset);
            }
            return newNode;
        }

        private void AddNode(DialogueNode newNode)
        {
            nodes.Add(newNode);
            OnValidate();
        }

        private void CleanDanglingChildren(DialogueNode deletedNode)
        {
            foreach (DialogueNode node in GetAllNodes())
                node.RemoveChild(deletedNode.name);
        }
#endif

        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            if (nodes.Count == 0)
            {
                DialogueNode newNode = MakeNode(null);
                AddNode(newNode);
            }

            if (AssetDatabase.GetAssetPath(this) != "")
                foreach (DialogueNode node in GetAllNodes())
                    if (AssetDatabase.GetAssetPath(node) == "")
                        AssetDatabase.AddObjectToAsset(node, this);
#endif
        }

        public void OnAfterDeserialize()
        {

        }
    }
}