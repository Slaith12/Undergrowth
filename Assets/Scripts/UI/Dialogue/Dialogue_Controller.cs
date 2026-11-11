using Ink.Parsed;
using Ink.Runtime;
using Ink.UnityIntegration;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue_Controller : MonoBehaviour
{
    // Make the dialogue controller a singleton 
    public static Dialogue_Controller Instance { get; private set; }

    private Dialogue_View dialogueView;
    private InkFile inkFile;
    private Character_Database character_Database;

    private Dialogue_Model _model;
    private Dialogue_Presenter _presenter;

    private bool fadeActive;
    private float fadeStartAlpha;
    private float fadeEndAlpha;
    private float fadeTimer;
    private float fadeMaxTimer;

    public event Action DialogueEnded;
    public event Action RefusedService;
    public event Action SellPlant;

    private void Awake()
    {
        // Setting up the singleton 
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartDialogue(Character_Data character, Character_Database character_Database, Dialogue_View view)
    {
        Debug.Log("[Dialogue Controller] - StartDialogue() was called");
        //Dispose the previous dialogue's data (if any)
        _presenter?.Dispose();

        inkFile = character.InkFile;
        dialogueView = view;
        // Create the model and give it the ink file 
        InitInkScript(inkFile.storyJson);

        // Create the presenter and link the model and view together 
        _presenter = new Dialogue_Presenter(_model, dialogueView, character_Database);
        _presenter.OnEndCalled += EndDialogueInteraction;
        dialogueView.OnRetryClicked += Retry;
    }

    private void InitInkScript(string scriptJson)
    {
        _model = new Dialogue_Model(scriptJson);
        _model.story.BindExternalFunction<object>("DebugLog", Debug.Log);
        _model.story.BindExternalFunction<float>("evnt_Wait", time => { StartCoroutine(WaitThenAdvance(time)); });
        _model.story.BindExternalFunction<float, float, float>("evnt_FadeCharacter", FadeCharacter);
        _model.story.BindExternalFunction("evnt_CopArrest", CopArrest);
        _model.story.BindExternalFunction<float>("SetCharacterAlpha", SetCharacterAlpha);
        _model.story.BindExternalFunction("evnt_sell_plant", OnSellPlant);
        _model.story.BindExternalFunction("evnt_refuse_service", OnRefuseService);
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
        Debug.Log("[Dialogue_Controller] - Fading Character");
        SetCharacterAlpha(startAlpha);
        fadeActive = true;
        fadeStartAlpha = startAlpha;
        fadeEndAlpha = endAlpha;
        fadeTimer = time;
        fadeMaxTimer = time;
    }

    private void CopArrest()
    {
        //Placeholder since game over screen is missing
        //This still needs to eventually call ProgressDialogue() like a regular event, so it uses the Wait event.
        Debug.Log("Arrested! Oh no!");
        StartCoroutine(WaitThenAdvance(0.1f));
    }

    private void OnSellPlant()
    {
        SellPlant.Invoke();
        dialogueView.HideDialogue();
    }

    private void OnRefuseService()
    {
        Debug.Log("[Dialogue_Controller] - OnRefuseService() Called");
        RefusedService.Invoke();
    }

    private void SetCharacterAlpha(float alpha)
    {
        if (dialogueView)
        {
            dialogueView.SetCharacterAlpha(alpha);
        }
    }
    
    private void Retry()
    {
        dialogueView.HideRetryButton();
        // StartDialogue();
    }

    private void EndDialogueInteraction()
    {
        DialogueEnded.Invoke();
    }
}
