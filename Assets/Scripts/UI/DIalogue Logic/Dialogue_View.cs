using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue_View : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Button nextDialogueButton;
    [SerializeField] private Button previousDialogueButton;

    // Events exposed to the Presenter 
    public event Action OnNextDialogueClicked;
    public event Action OnPreviousDialogueClicked;

    private void Awake()
    {
        // Route button clicks to events 
        nextDialogueButton.onClick.AddListener(() => OnNextDialogueClicked?.Invoke());
        previousDialogueButton.onClick.AddListener(() => OnNextDialogueClicked?.Invoke());
    }

    // Passive update function called by the Presenter 
    public void UpdateDialogueDisplay(string text)
    {
        Debug.Log("UpdateDialogueDisplay() Called. Text: " + text);
        dialogueText.text = text;
    }
}
