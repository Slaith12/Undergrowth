using UnityEngine;
using System.Collections.Generic;
using System;

public interface IDialogue_View
{
    public event Action<int> OnChoiceSelected;
    public event Action OnContinueClicked;
    public event Action OnRetryClicked;

    public void DisplayDialogue(string text);
    public void DisplayChoices(List<string> choiceTexts);
    public void HideDialogue();
    public void ShowDialogue();
    public void HideChoices();

    public void HideContinueButton();
    public void ShowContinueButton();

    public void SetSpeakerName(string displayName);
    public void SetPortraitSprite(Sprite sprite);

    public void SetCharacterAlpha(float alpha);

    public void ShowRetryButton();
    public void HideRetryButton();
}
