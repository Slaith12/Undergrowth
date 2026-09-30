using Ink;
using Ink.Runtime;
using Ink.UnityIntegration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dialogue_Model
{
    public Story story;

    // Properties to expose structured data to the Presenter 
    public string CurrentText { get; private set; } = string.Empty;
    public List<Choice> CurrentChoices => story.currentChoices;
    public bool CanContinue => story.canContinue;


    public Dialogue_Model(string scriptJson)
    {
        // Initialize the Ink story engine 
        story = new Story(scriptJson);

        // Add the OnInkError Listener 
        story.onError += OnInkError;

        AdvanceStory();
    }

    public void AdvanceStory()
    {
        // If the story cannot continue 
        if (!CanContinue)
        {
            return; 
        }

        // Advance the ink state and pull the text 
        CurrentText = story.Continue().Trim();
    }

    public void ChooseIndex(int index)
    {
        if (index >= 0 && index < CurrentChoices.Count)
        {
            story.ChooseChoiceIndex(index);
        }
    }

    private void OnInkError(string message, ErrorType errorType)
    {
        if (errorType == ErrorType.Error)
            Debug.LogError(message);
        else
            Debug.LogWarning(message);
    }


    //// Notifies the presenter whenever dialogue has changed 
    //public event Action<string> OnDialogueChanged;

    //// The variables 
    //public int CurrentDialogueIndex { get; private set; }
    //public string CharacterName { get; private set; }
    //public List<string> DialogueList { get; private set; }
    //public string CurrentDialogue { get; private set; }

    //// Constructor 
    //public Dialogue_Model(string characterName, List<string> dialogueList)
    //{
    //    CurrentDialogueIndex = 0;
    //    CharacterName = characterName;
    //    DialogueList = dialogueList;
    //    CurrentDialogue = DialogueList[CurrentDialogueIndex];
    //}

    //public void NextDialogue()
    //{
    //    // If there is more dialogue 
    //    if ((CurrentDialogueIndex + 1) < DialogueList.Count)
    //    {
    //        CurrentDialogueIndex++;
    //        CurrentDialogue = DialogueList[CurrentDialogueIndex];
    //        OnDialogueChanged?.Invoke(CurrentDialogue);
    //    }
    //}

    //public void PreviousDialogue()
    //{
    //    // If there is dialogue before this 
    //    if ((CurrentDialogueIndex - 1) >= 0)
    //    {
    //        CurrentDialogueIndex--;
    //        CurrentDialogue = DialogueList[CurrentDialogueIndex];
    //        OnDialogueChanged?.Invoke(CurrentDialogue);
    //    }
    //}
}
