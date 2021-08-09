using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue.UI
{
    public class DialogueUI : MonoBehaviour
    {
        PlayerConversant playerConversant;

        [SerializeField] Text aIText;
        [SerializeField] Button nextButton;
        [SerializeField] GameObject aIResponse;
        [SerializeField] Transform choiceRoot;
        [SerializeField] GameObject choiceButton;
        [SerializeField] Button quitButton;
        [SerializeField] Text currentConversant;

        void Start()
        {
            playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            playerConversant.onConversantionUpdated += UpdateUI;

            nextButton.onClick.AddListener(() => playerConversant.Next());
            quitButton.onClick.AddListener(() => playerConversant.Quit());

            UpdateUI();
        }

        public void Reset()
        {
            playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            playerConversant.onConversantionUpdated += UpdateUI;

            UpdateUI();
        }

        private void UpdateUI()
        {
            gameObject.SetActive(playerConversant.IsActive());
            if (!playerConversant.IsActive())
                return;

            currentConversant.text = playerConversant.GetCurrentConversant();
            aIResponse.SetActive(!playerConversant.IsChoosing());
            choiceRoot.gameObject.SetActive(playerConversant.IsChoosing());

            if (playerConversant.IsChoosing())
                BuildChoiceList();
            else
            {
                aIText.text = playerConversant.GetText();
                nextButton.gameObject.SetActive(playerConversant.HasNext());
            }
        }

        private void BuildChoiceList()
        {
            foreach (Transform child in choiceRoot)
                Destroy(child.gameObject);

            foreach (DialogueNode choiceNode in playerConversant.GetChoices())
            {
                GameObject choiceInstance = Instantiate(choiceButton, choiceRoot);

                choiceInstance.GetComponentInChildren<Text>().text = choiceNode.GetText();
                choiceInstance.GetComponentInChildren<Button>().onClick.AddListener(() =>
                {
                    playerConversant.SelectChoice(choiceNode);
                });
            }
        }
    }
}