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

    [SerializeField] ChoiceButton choiceButtonPrefab; 
    private List<ChoiceButton> choiceButtons;

    // Events exposed to the Presenter 
    public event Action OnContinueClicked;
    public event Action<int> OnChoiceSelected;

    private void Awake()
    {
        // Hook button up to fire presenter's event 
        nextDialogueButton.onClick.AddListener(() => OnContinueClicked?.Invoke());
        choiceButtons = new();
    }
   
    // Logic for displaying the text 
    public void DisplayDialogue(string text)
    {
        // For now, 
        dialogueText.text = text;

        // TODO: Make typewritter effect
    }

    public void ShowDialogue() => dialoguePanel.SetActive(true);
    public void HideDialogue() => dialoguePanel.SetActive(false);

    public void DisplayChoices(List<string> choiceTexts, Action<int> buttonCallback)
    {
        // If button cache doesn't have enough buttons, create more of them 
        for (int i = choiceButtons.Count; i < choiceTexts.Count; i++)
        {
            // Add new buttons 
            choiceButtons.Add(Instantiate(choiceButtonPrefab, transform));
        }

        // Initialize all used buttons 
        for(int i = 0; i < choiceTexts.Count; i++)
        {
            //We don't have to worry about the buttons' positions, the VerticalLayoutGroup component will handle it
            choiceButtons[i].gameObject.SetActive(true);
            choiceButtons[i].SetText(choiceTexts[i]);
            int buttonIndex = i; //temp variable needed for delegate, using i directly won't work
            choiceButtons[i].SetCallback(delegate { buttonCallback(buttonIndex); });
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
}
