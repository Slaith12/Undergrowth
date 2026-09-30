using UnityEngine;
using System.Collections.Generic;
using System;

public interface IDialogue_View
{
    event Action<int> OnChoiceSelected;
    event Action OnContinueClicked;

    void DisplayDialogue(string text);
    void DisplayChoices(List<string> choiceTexts, Action<int> buttonCallback);
    void HideDialogue();
    void ShowDialogue();
    void HideChoices();
}
