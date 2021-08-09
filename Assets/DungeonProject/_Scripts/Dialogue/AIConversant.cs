using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    public class AIConversant : MonoBehaviour
    {
        [SerializeField] Dialogue dialogue;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (!this.enabled || dialogue == null)
                return;

            if (collider.GetType() == typeof(CapsuleCollider2D))
            {
                collider.GetComponent<PlayerConversant>().StartDialogue(this, dialogue);
            }
        }
    }
}