using Ink.Runtime;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DialogueTestScene : MonoBehaviour
{
    [SerializeField] Image characterSprite;
    [SerializeField] List<Color> colors;

    private DialogueManager manager;

    private bool fadeActive;
    private float fadeStartAlpha;
    private float fadeEndAlpha;
    private float fadeTimer;
    private float fadeMaxTimer;

    void Awake()
    {
        manager = GetComponent<DialogueManager>();
        manager.OnInitInkScript += OnInitNewStory;
    }

    private void OnInitNewStory(Story story)
    {
        //Bind functions
        story.BindExternalFunction<float>("SetCharacterAlpha", SetCharacterAlpha);
        story.BindExternalFunction<float, float, float>("evnt_FadeCharacter", FadeCharacter);
        //Reset character
        characterSprite.color = colors[Random.Range(0, colors.Count)];
        SetCharacterAlpha(0);
        fadeActive = false;
    }

    private void Update()
    {
        if(fadeActive)
        {
            fadeTimer -= Time.deltaTime;
            if(fadeTimer <= 0)
            {
                SetCharacterAlpha(fadeEndAlpha);
                fadeActive = false;
                manager.AdvanceDialogue();
            }
            else
            {
                SetCharacterAlpha(Mathf.Lerp(fadeEndAlpha, fadeStartAlpha, fadeTimer / fadeMaxTimer));
            }
        }

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

    private void SetCharacterAlpha(float alpha)
    {
        Color color = characterSprite.color;
        color.a = alpha;
        characterSprite.color = color;
    }
}
