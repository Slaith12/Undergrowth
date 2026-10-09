using Ink;
using Ink.Runtime;
using Ink.UnityIntegration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum StoryState {Dialogue, Choice, Event, End}

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
    }

    public StoryState AdvanceStory()
    {
        // If the story cannot continue 
        if (!CanContinue)
        {
            if(story.currentChoices.Count > 0)
                return StoryState.Choice;
            else
                return StoryState.End; 
        }

        // Advance the ink state and pull the text 
        string nextLine = story.Continue().Trim();

        if (nextLine == "!!EVENT!!")
        {
            return StoryState.Event;
        }
        else
        {
            CurrentText = nextLine;
            return StoryState.Dialogue;
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

    public Dictionary<string,string> GetTags()
    {
        Dictionary<string,string> result = new Dictionary<string,string>();

        foreach (string tag in story.currentTags)
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
