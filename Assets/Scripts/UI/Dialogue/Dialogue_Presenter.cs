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
        // Listen to the inputs from the model 
        _model.OnEvent += HandleEvent;

        _view.ShowDialogue();
        ProgressDialogue();
    }

    private void HandleContinueClicked()
    {
        ProgressDialogue();
    }

    public void ProgressDialogue()
    {
        // If the model can continue 
        if (_model.CanContinue)
        {
            // Advance the story 
            _model.AdvanceStory();
            // Get the tags 
            List<string> tags = _model.story.currentTags;
            // Make a dictionary of the tags 
            Dictionary<string, string> parsedTags = ParseTags(tags);
            string speakerId = parsedTags.GetValueOrDefault("speaker");
            string portraitKey = parsedTags.GetValueOrDefault("portrait");
            string displayName = null;
            Sprite characterSprite = null;

            // If there is a character with this speakerId
            if (!string.IsNullOrEmpty(speakerId))
            {
                // Get the character
                Characater_Data character = _characterDatabase.GetCharacter(speakerId);
                // Set the display name 
                if (character != null)
                {
                    displayName = character.DisplayName;
                    characterSprite = character.GetSprite(portraitKey);

                    if (characterSprite != null && displayName != null)
                    {
                        _view.SetSpeaker(displayName, characterSprite);
                    }else if (characterSprite == null)
                    {
                        Debug.Log("No Character sprite with portrait key: " + portraitKey);
                    }
                }
                else
                {
                    Debug.Log("No Character exists with id: " + speakerId);
                }
            }

            // Pull data from the model and assign it to the view 
            _view.DisplayDialogue(_model.CurrentText);
            _view.ShowContinueButton();

            // If there are choices to be made 
            if (_model.CurrentChoices.Count > 0)
            {
                var choiceTexts = new List<string>();
                foreach (var choice in _model.CurrentChoices)
                {
                    choiceTexts.Add(choice.text);
                }
                // Display the choices 
                _view.DisplayChoices(choiceTexts, HandleChoiceSelected);
                // Remove the continue button 
                _view.HideContinueButton();
                
            }
        }else if (_model.CurrentChoices.Count == 0)
        {
            EndDialogue();
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

    private void HandleEvent()
    {
        Debug.Log("EVENT?");
    }
    private void EndDialogue()
    {
        _view.HideDialogue();
        _view.ShowRetryButton();
        // Remove Subscribers
        _view.OnContinueClicked -= HandleContinueClicked;
        _view.OnChoiceSelected -= HandleChoiceSelected;
        _model.OnEvent -= HandleEvent;
    }
    private Dictionary<string,string> ParseTags(List<string> tags)
    {
        Dictionary<string,string> result = new Dictionary<string,string>();

        foreach (string tag in tags)
        {
            string[] split = tag.Split(':');
            if (split.Length == 2)
            {
                string key = split[0].Trim();
                string value = split[1].Trim();
                result[key] = value;
            }
        }

        return result;
    }
}
