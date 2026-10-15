using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Dialogue_Presenter
{
    private readonly Dialogue_Model _model;
    private readonly IDialogue_View _view;
    private readonly Character_Database _characterDatabase;
    private readonly Story _story;


    // Constructor for the Presenter that takes in the Ink story and the Dialogue View
    public Dialogue_Presenter(Dialogue_Model model, IDialogue_View view, Character_Database characterDatabase)
    {
        _model = model;
        _view = view;
        _characterDatabase = characterDatabase;

        // Listen to the User Inputs from the view 
        _view.OnContinueClicked += HandleContinueClicked;
        _view.OnChoiceSelected += HandleChoiceSelected;

        _view.ShowDialogue();
        ProgressDialogue();
    }

    private void HandleContinueClicked()
    {
        ProgressDialogue();
    }

    public void ProgressDialogue()
    {
        StoryState state = _model.AdvanceStory();
        switch(state)
        {
            case StoryState.Dialogue:
                _view.ShowDialogue();
                HandleDialogueTags();
                _view.DisplayDialogue(_model.CurrentText);
                _view.ShowContinueButton();
                break;
            case StoryState.Choice:
                List<string> choiceTexts = _model.CurrentChoices.Select(choice => choice.text).ToList();
                _view.DisplayChoices(choiceTexts);
                _view.HideContinueButton();
                break;
            case StoryState.Event:
                //The presenter does nothing during an event.
                //An event indicates that an external function was called, and that external function handles the event.
                _view.HideDialogue();
                break;
            case StoryState.End:
                EndDialogue();
                break;
            default:
                throw new Exception($"Unhandled story state detected: {state}");
        }
    }

    private void HandleChoiceSelected(int choiceIndex)
    {
        // Choose the choice
        _model.ChooseIndex(choiceIndex);
        _view.HideChoices();
        // Progress Dialogue
        ProgressDialogue();
    }

    private void EndDialogue()
    {
        _view.HideDialogue();
        _view.ShowRetryButton();
        // Remove Subscribers
        _view.OnContinueClicked -= HandleContinueClicked;
        _view.OnChoiceSelected -= HandleChoiceSelected;
    }
    private void HandleDialogueTags()
    {
        Dictionary<string,string> tags = _model.GetTags();
        string speakerId = tags.GetValueOrDefault("speaker");
        if(!string.IsNullOrEmpty(speakerId))
            SetSpeaker(speakerId);
        
        string portraitKey = tags.GetValueOrDefault("portrait");
        if(!string.IsNullOrEmpty(portraitKey))
            SetPortrait(portraitKey, speakerId);
    }

    private void SetSpeaker(string speakerId)
    {
        Character_Data character = _characterDatabase.GetCharacter(speakerId);
        if(character == null)
        {
            Debug.LogError($"Unable to set speaker to missing character \"{speakerId}\"");
            return;
        }
        _view.SetSpeakerName(character.DisplayName);
    }

    private void SetPortrait(string portraitKey, string speakerId)
    {
        string[] keyIds = portraitKey.Split('.');
        string characterId;
        //if portrait tag only includes a key, use the current speaker as the character
        //if portrait tag is listed as [name].[key], ignore speakerId and use the name provided instead
        if(keyIds.Length == 1)
        {
            characterId = speakerId;
        }
        else
        {
            characterId = keyIds[0];
            portraitKey = keyIds[1];
        }

        if(string.IsNullOrEmpty(characterId))
        {
            Debug.LogError("Character id required in order to set portrait.\n" +
                        "Either include a #speaker tag with the #portrait tag, or write the #portrait tag as \"[character].[portrait]\"");
            return;
        }

        Character_Data character = _characterDatabase.GetCharacter(characterId);
        if(character == null)
        {
            Debug.LogError($"Unable to get portrait of missing character \"{characterId}\".");
            return;
        }

        Sprite sprite = character.GetSprite(portraitKey);
        if(sprite == null)
        {
            Debug.LogError($"Unable to get missing portrait \"{portraitKey}\" from character \"{characterId}\".");
        }
        _view.SetPortraitSprite(sprite);
    }
}
