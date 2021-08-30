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

    string[] speakerNames = null;
    string[] dialogues = null;
    int currentIndex = 0;

    private void Start()
    {
        nextButton.onClick.AddListener(Next);
        skipButton.onClick.AddListener(Skip);
        quitButton.onClick.AddListener(Quit);

        gameObject.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        gameObject.SetActive(true);

        if (dialogue.IsStartBySpeaker())
            speakerNames = new string[2] { dialogue.GetSpeakerName(), "player" };
        else
            speakerNames = new string[2] { "player", dialogue.GetSpeakerName() };

        dialogues = dialogue.GetDialogues();
        currentIndex = 0;

        SetActiveButtons(false);
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
            SetActiveButtons(true);
    }

    private bool HasNext()
    {
        return currentIndex < dialogues.Length - 1;
    }

    private void SetActiveButtons(bool canQuit)
    {
        nextButton.gameObject.SetActive(!canQuit);
        skipButton.gameObject.SetActive(!canQuit);
        quitButton.gameObject.SetActive(canQuit);
    }
}
