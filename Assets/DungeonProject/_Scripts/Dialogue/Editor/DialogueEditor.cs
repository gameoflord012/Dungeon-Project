using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

namespace Dialogue.Editor
{
    public class DialogueEditor : EditorWindow
    {
        Dialogue selectedDialogue = null;
        Vector2 scrollPosition;

        [NonSerialized] GUIStyle nodeStyle = null;
        [NonSerialized] GUIStyle playerNodeStyle = null;
        [NonSerialized] DialogueNode draggingNode = null;
        [NonSerialized] Vector2 draggingOffset;
        [NonSerialized] DialogueNode creatingNode = null;
        [NonSerialized] DialogueNode deletedNode = null;
        [NonSerialized] DialogueNode linkingNode = null;
        [NonSerialized] bool draggingCanvas = false;
        [NonSerialized] Vector2 draggingCanvasOffset;

        const float canvasSize = 4000f;
        const float backgroundSize = 50f;

        [MenuItem("Window/Dialogue Editor")]
        public static void ShowWindow()
        {
            GetWindow(typeof(DialogueEditor), false, "Dialogue Editor");
        }

        private void OnEnable()
        {
            Selection.selectionChanged += OnSelectionChanged;

            nodeStyle = new GUIStyle();

            nodeStyle.normal.background = EditorGUIUtility.Load("node0") as Texture2D;
            nodeStyle.normal.textColor = Color.white;
            nodeStyle.padding = new RectOffset(20, 20, 20, 20);
            nodeStyle.border = new RectOffset(12, 12, 12, 12);

            playerNodeStyle = new GUIStyle();

            playerNodeStyle.normal.background = EditorGUIUtility.Load("node1") as Texture2D;
            playerNodeStyle.normal.textColor = Color.white;
            playerNodeStyle.padding = new RectOffset(20, 20, 20, 20);
            playerNodeStyle.border = new RectOffset(12, 12, 12, 12);
        }

        private void OnSelectionChanged()
        {
            Dialogue newDialogue = Selection.activeObject as Dialogue;
            if (newDialogue)
            {
                selectedDialogue = newDialogue;
                Repaint();
            }
        }

        [OnOpenAsset(1)]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            Dialogue dialogue = EditorUtility.InstanceIDToObject(instanceID) as Dialogue;
            if (dialogue)
            {
                ShowWindow();
                return true;
            }
            return false;
        }

        private void OnGUI()
        {
            if (!selectedDialogue)
                EditorGUILayout.LabelField("No Dialogue Selected");
            else
            {
                ProcessEvents();

                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

                Rect canvas = GUILayoutUtility.GetRect(canvasSize, canvasSize);
                Texture2D backgroundTexture = Resources.Load("Editor Background") as Texture2D;
                Rect texCoords = new Rect(0, 0, canvasSize / backgroundSize, canvasSize / backgroundSize);
                GUI.DrawTextureWithTexCoords(canvas, backgroundTexture, texCoords);

                foreach (DialogueNode node in selectedDialogue.GetAllNodes())
                    DrawConnections(node);

                foreach (DialogueNode node in selectedDialogue.GetAllNodes())
                    DrawNode(node);

                EditorGUILayout.EndScrollView();

                if (creatingNode != null)
                {
                    selectedDialogue.CreateNode(creatingNode);
                    creatingNode = null;
                }

                if (deletedNode != null)
                {
                    selectedDialogue.DeleteNode(deletedNode);
                    deletedNode = null;
                }
            }
        }

        private void ProcessEvents()
        {
            if (Event.current.type == EventType.MouseDown && draggingNode == null)
            {
                draggingNode = GetNodeAtPoint(Event.current.mousePosition + scrollPosition);
                if (draggingNode != null)
                {
                    draggingOffset = draggingNode.GetRect().position - Event.current.mousePosition;
                    Selection.activeObject = draggingNode;
                }
                else
                {
                    draggingCanvas = true;
                    draggingCanvasOffset = Event.current.mousePosition + scrollPosition;
                    Selection.activeObject = selectedDialogue;
                }
            }
            else if (Event.current.type == EventType.MouseDrag && draggingNode != null)
            {
                draggingNode.SetRect(Event.current.mousePosition + draggingOffset);
                GUI.changed = true;
            }
            else if (Event.current.type == EventType.MouseDrag && draggingCanvas)
            {
                scrollPosition = draggingCanvasOffset - Event.current.mousePosition;
                GUI.changed = true;
            }
            else if (Event.current.type == EventType.MouseUp && draggingNode != null)
                draggingNode = null;
            else if (Event.current.type == EventType.MouseUp && draggingCanvas)
                draggingCanvas = false;
        }

        private void DrawNode(DialogueNode node)
        {
            GUIStyle style = nodeStyle;
            if (node.IsPlayerSpeaking())
                style = playerNodeStyle;
            GUILayout.BeginArea(node.GetRect(), style);
            EditorGUI.BeginChangeCheck();

            node.SetText(EditorGUILayout.TextField(node.GetText()));

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("+"))
                creatingNode = node;

            if (GUILayout.Button("-"))
                deletedNode = node;

            DrawLinkButton(node);

            GUILayout.EndHorizontal();

            GUILayout.EndArea();
        }

        private void DrawLinkButton(DialogueNode node)
        {
            if (linkingNode == null)
            {
                if (GUILayout.Button("Link"))
                    linkingNode = node;
            }
            else if (linkingNode == node)
            {
                if (GUILayout.Button("Cancel"))
                    linkingNode = null;
            }
            else if (linkingNode.GetChildren().Contains(node.name))
            {
                if (GUILayout.Button("Unlink"))
                {
                    linkingNode.RemoveChild(node.name);
                    linkingNode = null;
                }
            }
            else if (GUILayout.Button("To"))
            {
                linkingNode.AddChild(node.name);
                linkingNode = null;
            }
        }

        private void DrawConnections(DialogueNode node)
        {
            Vector3 startPosition = new Vector2(node.GetRect().xMax, node.GetRect().center.y);
            foreach (DialogueNode childNode in selectedDialogue.GetAllChildren(node))
            {
                Vector3 endPosition = new Vector2(childNode.GetRect().xMin, childNode.GetRect().center.y);
                Vector3 controlPointOffset = new Vector2(endPosition.x - startPosition.x, 0);
                Handles.DrawBezier(startPosition, endPosition,
                    startPosition + controlPointOffset, endPosition - controlPointOffset,
                    Color.white, null, 4f);
            }
        }

        private DialogueNode GetNodeAtPoint(Vector2 point)
        {
            DialogueNode foundNode = null;
            foreach (DialogueNode node in selectedDialogue.GetAllNodes())
                if (node.GetRect().Contains(point))
                    foundNode = node;
            return foundNode;
        }
    }
}