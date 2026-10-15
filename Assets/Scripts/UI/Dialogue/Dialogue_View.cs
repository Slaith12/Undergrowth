using Ink.Parsed;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Dialogue_View : MonoBehaviour, IDialogue_View
{
    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Button nextDialogueButton;
    [SerializeField] private GameObject choiceButtonPanel;
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private GameObject character;
    [SerializeField] private Button retryButton;

    private SpriteRenderer portraitRenderer; 


    [SerializeField] ChoiceButton choiceButtonPrefab; 
    private List<ChoiceButton> choiceButtons;

    // Events exposed to the Presenter 
    public event Action OnContinueClicked;
    public event Action<int> OnChoiceSelected;
    public event Action OnRetryClicked; 

    private void Awake()
    {
        // Hook button up to fire presenter's event 
        nextDialogueButton.onClick.AddListener(() => OnContinueClicked?.Invoke());
        retryButton.onClick.AddListener(() => OnRetryClicked?.Invoke());
        choiceButtons = new();
        portraitRenderer = character.GetComponentInChildren<SpriteRenderer>();
    }
   
    // Logic for displaying the text 
    public void DisplayDialogue(string _dialogueText)
    {
        // For now, 
        dialogueText.text = _dialogueText;
        // TODO: Make typewritter effect
    }

    public void ShowDialogue()
    {
        dialoguePanel.SetActive(true);
    }
    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    public void DisplayChoices(List<string> choiceTexts)
    {
        // If button cache doesn't have enough buttons, create more of them 
        for (int i = choiceButtons.Count; i < choiceTexts.Count; i++)
        {
            // Add new buttons
            ChoiceButton newButton = Instantiate(choiceButtonPrefab, choiceButtonPanel.transform);
            int buttonIndex = i; //temp variable needed for delegate, using i directly won't work
            newButton.SetCallback(delegate { OnChoiceSelected?.Invoke(buttonIndex); });
            choiceButtons.Add(newButton);
        }

        // Initialize all used buttons 
        for(int i = 0; i < choiceTexts.Count; i++)
        {
            //We don't have to worry about the buttons' positions, the VerticalLayoutGroup component will handle it
            choiceButtons[i].gameObject.SetActive(true);
            choiceButtons[i].SetText(choiceTexts[i]);
        }

        // Hide all of the unused buttons 
        for (int i = choiceTexts.Count; i < choiceButtons.Count; i++)
        {
            choiceButtons[i].gameObject.SetActive(false);
        }
    }
    
    public void HideChoices()
    {
        foreach (ChoiceButton button in choiceButtons)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void HideContinueButton()
    {
        nextDialogueButton.gameObject.SetActive(false);
    }

    public void ShowContinueButton()
    {
        nextDialogueButton.gameObject.SetActive(true);
    }

    public void SetSpeakerName(string name)
    {
        characterName.text = name;
    }

    public void SetPortraitSprite(Sprite sprite)
    {
        portraitRenderer.sprite = sprite;
    }

    public void SetCharacterAlpha(float alpha)
    {
        Color color = character.GetComponentInChildren<SpriteRenderer>().color;
        color.a = alpha;
        character.GetComponentInChildren<SpriteRenderer>().color = color;
    }

    public void ShowRetryButton()
    {
        retryButton.gameObject.SetActive(true);
    }

    public void HideRetryButton()
    {
        retryButton.gameObject.SetActive(false);
    }
}
