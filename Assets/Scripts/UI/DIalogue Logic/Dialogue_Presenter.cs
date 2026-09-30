using UnityEngine;

public class Dialogue_Presenter
{
    private readonly Dialogue_Model _model;
    private readonly Dialogue_View _view;

    public Dialogue_Presenter(Dialogue_Model model, Dialogue_View view)
    {
        _model = model;
        _view = view;

        // Listen to User Inputs from the View 
        _view.OnNextDialogueClicked += HandleNextDialogueRequest;
        _view.OnPreviousDialogueClicked += HandlePreviousDialogueRequest;

        // Listen to data updates from the model 
        _model.OnDialogueChanged += HandleDialogueChanged;

        // Initialize the UI with starting values 
        _view.UpdateDialogueDisplay(_model.CurrentDialogue);
    }

    public void Disable()
    {
        // Remove subscribers 
        _view.OnNextDialogueClicked -= HandleNextDialogueRequest;
        _view.OnPreviousDialogueClicked -= HandlePreviousDialogueRequest;
        _model.OnDialogueChanged -= HandleDialogueChanged;
    }

    private void HandleNextDialogueRequest()
    {
        Debug.Log("HandleNextDialogueRequest()");
        _model.NextDialogue();
    }
    private void HandlePreviousDialogueRequest()
    {
        Debug.Log("HandlePreviousDialogueRequest()");
        _model.PreviousDialogue();
    }

    private void HandleDialogueChanged(string text)
    {
        // Tell the view what to do 
        Debug.Log("HandleDialogueChanged() called - Text: " + text);
        _view.UpdateDialogueDisplay(text);
    }
}
