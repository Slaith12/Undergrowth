using Ink.UnityIntegration;
using UnityEngine;
using Ink.Runtime;
using System;
using System.Collections;

public class Dialogue_Controller : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Dialogue_View dialogueView;
    [SerializeField] private InkFile inkFile;

    private Dialogue_Model _model;
    private Dialogue_Presenter _presenter;

    private void Start()
    {
        // Create the model and give it the ink file 
        InitInkScript(inkFile.storyJson);

        // Create the presenter and link the model and view together 
        _presenter = new Dialogue_Presenter(_model, dialogueView);
    }

    private void InitInkScript(string scriptJson)
    {
        _model = new Dialogue_Model(scriptJson);
        _model.story.BindExternalFunction<object>("DebugLog", Debug.Log);
        _model.story.BindExternalFunction<float>("evnt_Wait", time => { StartCoroutine(WaitThenAdvance(time)); });
    }

    private IEnumerator WaitThenAdvance(float time)
    {
        yield return new WaitForSeconds(time);
        _presenter.ProgressDialogue();
    }
}
