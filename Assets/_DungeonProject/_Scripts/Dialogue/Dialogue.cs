using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] DialogueUI dialogueUI;
    [SerializeField] string speakerName;
    [SerializeField] string[] dialogues;
    [SerializeField] bool startBySpeaker = true;

    private void StartDialogue()
    {
        dialogueUI.StartDialogue(this);
    }

    public string GetSpeakerName()
    {
        return speakerName;
    }

    public string[] GetDialogues()
    {
        return dialogues;
    }

    public bool IsStartBySpeaker()
    {
        return startBySpeaker;
    }
}
