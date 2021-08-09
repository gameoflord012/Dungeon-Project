using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using Core;

namespace Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        Dialogue currentDialogue = null;
        DialogueNode currentNode = null;
        AIConversant currentConversant = null;
        bool isChoosing = false;

        public event Action onConversantionUpdated;

        public void StartDialogue(AIConversant newConversant, Dialogue newDialogue)
        {
            currentConversant = newConversant;
            currentDialogue = newDialogue;
            currentNode = currentDialogue.GetRootNode();
            TriggerEnterAction();

            onConversantionUpdated();
        }

        public void Quit()
        {
            currentDialogue = null;

            TriggerExitAction();

            currentNode = null;
            isChoosing = false;
            currentConversant = null;

            onConversantionUpdated();
        }

        public bool IsActive()
        {
            return currentDialogue != null;
        }

        public bool IsChoosing()
        {
            return isChoosing;
        }

        public string GetCurrentConversant()
        {
            if (isChoosing)
                return "You";
            return "Guard";
        }

        public string GetText()
        {
            if (currentNode == null)
                return "";

            return currentNode.GetText();
        }

        public IEnumerable<DialogueNode> GetChoices()
        {
            return FilterOnCondition(currentDialogue.GetPlayerChildren(currentNode));
        }

        public void SelectChoice(DialogueNode chosenNode)
        {
            currentNode = chosenNode;
            TriggerEnterAction();

            isChoosing = false;
            Next();
        }

        public void Next()
        {
            if (FilterOnCondition(currentDialogue.GetPlayerChildren(currentNode)).Count() > 0)
            {
                isChoosing = true;

                TriggerExitAction();
                onConversantionUpdated();
                return;
            }

            DialogueNode[] children = FilterOnCondition(currentDialogue.GetAIChildren(currentNode)).ToArray();
            int index = UnityEngine.Random.Range(0, children.Count());

            TriggerExitAction();
            currentNode = children[index];
            TriggerEnterAction();

            onConversantionUpdated();
        }

        public bool HasNext()
        {
            return FilterOnCondition(currentDialogue.GetAllChildren(currentNode)).Count() > 0;
        }

        private IEnumerable<DialogueNode> FilterOnCondition(IEnumerable<DialogueNode> inputNodes)
        {
            foreach (DialogueNode node in inputNodes)
                if (node.IsSatisfiedCondition(GetEvaluators()))
                    yield return node;
        }

        private IEnumerable<IPredicateEvaluator> GetEvaluators()
        {
            return GetComponents<IPredicateEvaluator>();
        }

        private void TriggerEnterAction()
        {
            if (currentNode != null)
                TriggerAction(currentNode.GetOnEnterAction());
        }

        private void TriggerExitAction()
        {
            if (currentNode != null)
                TriggerAction(currentNode.GetOnExitAction());
        }

        private void TriggerAction(string action)
        {
            if (action == "")
                return;

            foreach (DialogueTrigger trigger in currentConversant.GetComponents<DialogueTrigger>())
                trigger.Trigger(action);
        }
    }
}