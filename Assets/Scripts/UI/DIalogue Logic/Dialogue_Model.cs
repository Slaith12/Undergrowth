using System;
using UnityEngine;
using System.Collections.Generic;

public class Dialogue_Model
{
    // Notifies the presenter whenever dialogue has changed 
    public event Action<string> OnDialogueChanged;

    // The variables 
    public int CurrentDialogueIndex { get; private set; }
    public string CharacterName { get; private set; }
    public List<string> DialogueList { get; private set; }
    public string CurrentDialogue { get; private set; }

    // Constructor 
    public Dialogue_Model(string characterName, List<string> dialogueList)
    {
        CurrentDialogueIndex = 0;
        CharacterName = characterName;
        DialogueList = dialogueList;
        CurrentDialogue = DialogueList[CurrentDialogueIndex];
    }

    public void NextDialogue()
    {
        // If there is more dialogue 
        if ((CurrentDialogueIndex + 1) < DialogueList.Count)
        {
            CurrentDialogueIndex++;
            CurrentDialogue = DialogueList[CurrentDialogueIndex];
            OnDialogueChanged?.Invoke(CurrentDialogue);
        }
    }

    public void PreviousDialogue()
    {
        // If there is dialogue before this 
        if ((CurrentDialogueIndex - 1) >= 0)
        {
            CurrentDialogueIndex--;
            CurrentDialogue = DialogueList[CurrentDialogueIndex];
            OnDialogueChanged?.Invoke(CurrentDialogue);
        }
    }
}
