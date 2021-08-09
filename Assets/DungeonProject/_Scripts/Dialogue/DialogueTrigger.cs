using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] string action;
        [SerializeField] UnityEvent onTrigger;

        public void Trigger(string actionToTrigger)
        {
            /*DialogueTrigger[] triggers = FindObjectsOfType<DialogueTrigger>();

            foreach (DialogueTrigger trigger in triggers)
                trigger.*/
            InvokeEvent(actionToTrigger);
        }

        public void InvokeEvent(string actionToTrigger)
        {
            if (actionToTrigger == action)
                onTrigger.Invoke();
        }
    }
}