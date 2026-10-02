using Ink.Parsed;
using Ink.Runtime;
using Ink.UnityIntegration;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue_Controller : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Dialogue_View dialogueView;
    [SerializeField] public InkFile inkFile;
    [SerializeField] private Character_Database character_Database;

    private Dialogue_Model _model;
    private Dialogue_Presenter _presenter;

    private bool fadeActive;
    private float fadeStartAlpha;
    private float fadeEndAlpha;
    private float fadeTimer;
    private float fadeMaxTimer;

    private void Start()
    {
        StartLevel();

    }

    private void StartLevel()
    {
        // Create the model and give it the ink file 
        InitInkScript(inkFile.storyJson);

        // Create the presenter and link the model and view together 
        _presenter = new Dialogue_Presenter(_model, dialogueView, character_Database);
    }


    private void InitInkScript(string scriptJson)
    {
        _model = new Dialogue_Model(scriptJson);
        _model.story.BindExternalFunction<object>("DebugLog", Debug.Log);
        _model.story.BindExternalFunction<float>("evnt_Wait", time => { StartCoroutine(WaitThenAdvance(time)); });
        _model.story.BindExternalFunction<float, float, float>("evnt_FadeCharacter", FadeCharacter);
        _model.story.BindExternalFunction("evnt_CopArrest", CopArrest);
        _model.story.BindExternalFunction<float>("SetCharacterAlpha", SetCharacterAlpha);
        dialogueView.OnRetryClicked += Retry;
        SetCharacterAlpha(0);
        fadeActive = false;
    
    }
    private void Update()
    {
        if (fadeActive)
        {
            fadeTimer -= Time.deltaTime;
            if (fadeTimer <= 0)
            {
                SetCharacterAlpha(fadeEndAlpha);
                fadeActive = false;
                _presenter.ProgressDialogue();
            }
            else
            {
                SetCharacterAlpha(Mathf.Lerp(fadeEndAlpha, fadeStartAlpha, fadeTimer / fadeMaxTimer));
            }
        }
    }

    private IEnumerator WaitThenAdvance(float time)
    {
        yield return new WaitForSeconds(time);
        _presenter.ProgressDialogue();
    }

    private void FadeCharacter(float startAlpha, float endAlpha, float time)
    {
        SetCharacterAlpha(startAlpha);
        fadeActive = true;
        fadeStartAlpha = startAlpha;
        fadeEndAlpha = endAlpha;
        fadeTimer = time;
        fadeMaxTimer = time;
    }

    private void CopArrest()
    {

    }

    private void SetCharacterAlpha(float alpha)
    {
        dialogueView.SetCharacterAlpha(alpha);
    }
    
    private void Retry()
    {
        dialogueView.HideRetryButton();
        StartLevel();
    }
}
