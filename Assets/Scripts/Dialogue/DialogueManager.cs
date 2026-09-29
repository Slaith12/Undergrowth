using Ink;
using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DialogueManager : MonoBehaviour
{
    public event Action<Story> OnInitInkScript;

    [SerializeField] List<Ink.UnityIntegration.InkFile> inkScripts;
    [SerializeField] List<int> scriptWeights;
    [SerializeField] DialogueBox dialogueBox;

    private Story activeScript;

    void Start()
    {
        BeginNewScript();
    }

    public void BeginNewScript(int index = -1)
    {
        if(index == -1)
        {
            int totalWeight = scriptWeights.Sum();
            int randWeight = Random.Range(0, totalWeight);
            while(randWeight >= 0)
            {
                index++;
                randWeight -= scriptWeights.Count;
            }
        }

        InitInkScript(inkScripts[index].storyJson);
    }

    /// <summary>
    /// Loads a new ink script from an asset and initializes it.
    /// </summary>
    private void InitInkScript(string scriptAsset)
    {
        activeScript = new Story(scriptAsset);
        activeScript.onError += OnInkError;

        //add common ink funcs
        activeScript.BindExternalFunction<object>("DebugLog", Debug.Log);
        activeScript.BindExternalFunction<float>("evnt_Wait", time => { StartCoroutine(WaitThenAdvance(time)); });

        OnInitInkScript?.Invoke(activeScript);
        AdvanceDialogue();
    }

    private IEnumerator WaitThenAdvance(float time)
    {
        yield return new WaitForSeconds(time);
        AdvanceDialogue();
    }

    /// <summary>
    /// 
    /// </summary>
    public void AdvanceDialogue()
    {
        // There are 4 possible results of advancing the dialogue:
        //
        // If the next part of the story is a regular line, that line will be displayed in the dialogue box
        //
        // If the next part of the story is a choice, those choices will be displayed in the choice boxes (previous dialogue will stay on screen)
        //
        // If the next part of the story calls an external function that blocks dialogue (usually because it plays an animation), this function
        // will return without doing anything extra, and it will wait for the external function to advance dialogue again. External functions are
        // indicated by the story returning the keyword "!!EVENT!!" as the entire line.
        //
        // If there's no more content in the story, then EndInkScript() will be called.

        if(activeScript.canContinue)
        {
            string nextLine = activeScript.Continue();
            //handle tags

            if (nextLine.Trim() == "!!EVENT!!")
            {
                dialogueBox.HideDialogue();
            }
            else
            {
                dialogueBox.DisplayDialogue(nextLine);
            }
        }
        else if(activeScript.currentChoices.Count > 0)
        {
            DisplayChoices(activeScript.currentChoices);
        }
        else
        {
            //End dialogue, move on
            BeginNewScript();
        }
    }

    private void DisplayChoices(List<Choice> choices)
    {

    }

    public void SelectChoiceAndAdvance(int index)
    {
        activeScript.ChooseChoiceIndex(index);
        AdvanceDialogue();
    }

    /// <summary>
    /// Error handler for ink scripts.
    /// </summary>
    private void OnInkError(string message, ErrorType errorType)
    {
        if (errorType == ErrorType.Error)
            Debug.LogError(message);
        else
            Debug.LogWarning(message);
    }
}
