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

    public event Action<float> OnSetCharacterAlpha;
    public event Action OnCopArrest;
    public event Action<float, float, float> OnFadeCharacter;
    public event Action OnEvent;
    public event Action<string> OnSetCharacter;
    public event Action<Sprite> OnSetCharacterSprite;
    public Dialogue_Model(string scriptJson)
    {
        // Initialize the Ink story engine 
        story = new Story(scriptJson);

        // Add the OnInkError Listener 
        story.onError += OnInkError;

        // Bind the external functions 
    }

    public void AdvanceStory()
    {
        // If the story cannot continue 
        if (!CanContinue)
        {
            return; 
        }

        // Advance the ink state and pull the text 
        string nextLine = story.Continue().Trim();

        if (nextLine == "!!EVENT!!")
        {
            OnEvent?.Invoke();
        }
        else
        {
            CurrentText = nextLine;
        }
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

}
