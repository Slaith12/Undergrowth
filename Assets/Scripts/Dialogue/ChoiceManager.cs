using System;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceManager : MonoBehaviour
{
    [SerializeField] ChoiceButton choiceButtonPrefab;

    private List<ChoiceButton> buttons;

    private void Awake()
    {
        buttons = new();
    }

    public void ShowChoices(List<string> choiceTexts, Action<int> buttonCallback)
    {
        //If button cache doesn't have enough buttons, create more of them.
        for(int i = buttons.Count; i < choiceTexts.Count; i++)
        {
            buttons.Add(Instantiate(choiceButtonPrefab, transform));
        }

        //Initialize all used buttons
        for(int i = 0; i < choiceTexts.Count; i++)
        {
            //We don't have to worry about the buttons' positions, the VerticalLayoutGroup component will handle it
            buttons[i].gameObject.SetActive(true);
            buttons[i].SetText(choiceTexts[i]);
            int buttonIndex = i; //temp variable needed for delegate, using i directly won't work
            buttons[i].SetCallback(delegate { buttonCallback(buttonIndex); });
        }

        //Hide all unused buttons
        for(int i = choiceTexts.Count; i < buttons.Count; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
    }

    public void HideChoices()
    {
        foreach(ChoiceButton button in buttons)
        {
            button.gameObject.SetActive(false);
        }
    }
}
