using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] Text speaker;
    [SerializeField] Text dialogue;
    [SerializeField] Button nextButton;
    [SerializeField] Button skipButton;
    [SerializeField] Button quitButton;

    string[] speakerNames = new string[2];
    string[] dialogues = null;
    int currentIndex = 0;

    public void StartDialogue(Dialogue dialogue)
    {
        if (dialogue.IsStartBySpeaker())
        {
            speakerNames[0] = dialogue.GetSpeakerName();
            speakerNames[1] = "player";
        }
        else
        {
            speakerNames[0] = "player";
            speakerNames[1] = dialogue.GetSpeakerName();
        }

        dialogues = dialogue.GetDialogues();
        currentIndex = 0;

        nextButton.gameObject.SetActive(true);
        skipButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(false);
        RefreshUI();
    }

    public void Next()
    {
        ++currentIndex;
        RefreshUI();
    }

    public void Skip()
    {
        currentIndex = dialogues.Length - 1;
        RefreshUI();
    }

    public void Quit()
    {
        gameObject.SetActive(false);
    }

    private void RefreshUI()
    {
        speaker.text = speakerNames[currentIndex % 2];
        dialogue.text = dialogues[currentIndex];

        if (!HasNext())
        {
            nextButton.gameObject.SetActive(false);
            skipButton.gameObject.SetActive(false);
            quitButton.gameObject.SetActive(true);
        }
    }

    private bool HasNext()
    {
        return currentIndex < dialogues.Length - 1;
    }
}
