using UnityEngine;
using System.Collections.Generic;
using System;

public interface IDialogue_View
{
    event Action<int> OnChoiceSelected;
    event Action OnContinueClicked;
    public event Action OnRetryClicked;


    void DisplayDialogue(string text);
    void DisplayChoices(List<string> choiceTexts, Action<int> buttonCallback);
    void HideDialogue();
    void ShowDialogue();
    void HideChoices();

    void HideContinueButton();
    void ShowContinueButton();

    void SetSpeaker(string displayName,Sprite sprite);

    void SetCharacterAlpha(float alpha);

    void ShowRetryButton();
    void HideRetryButton();

}
