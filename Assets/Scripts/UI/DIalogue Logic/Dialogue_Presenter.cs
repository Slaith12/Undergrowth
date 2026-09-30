using Ink.Runtime;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class Dialogue_Presenter
{
    private readonly Dialogue_Model _model;
    private readonly IDialogue_View _view;
    private readonly Story _story; 

    // Constructor for the Presenter that takes in the Ink story and the Dialogue View
    public Dialogue_Presenter(Dialogue_Model model, IDialogue_View view)
    {
        _model = model;
        _view = view;

        // Listen to the User Inputs from the view 
        _view.OnContinueClicked += HandleContinueClicked;
        _view.OnChoiceSelected += HandleChoiceSelected;
        _view.ShowDialogue();
        ProgressDialogue();
    }

    private void HandleContinueClicked()
    {
        Debug.Log("[Dialogue_Presenter] - HandleContinueClicked()");
        ProgressDialogue();
    }

    public void ProgressDialogue()
    {
        // If the model can continue 
        if (_model.CanContinue)
        {
            // Advance the story 
            _model.AdvanceStory();
            // Pull data from the model and assign it to the view 
            _view.DisplayDialogue(_model.CurrentText);

            // If there are choices to be made 
            if (_model.CurrentChoices.Count > 0)
            {
                var choiceTexts = new List<string>();
                foreach (var choice in _model.CurrentChoices)
                {
                    choiceTexts.Add(choice.text);
                }
                _view.DisplayChoices(choiceTexts, HandleChoiceSelected);
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
        // Progress Dialogue 
        ProgressDialogue();
    }

    private void EndDialogue()
    {
        _view.HideDialogue();
        // Remove Subscribers
        _view.OnContinueClicked -= HandleContinueClicked;
        _view.OnChoiceSelected -= HandleChoiceSelected;
    }
}
