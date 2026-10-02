using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour
{
    [SerializeField] TMP_Text textComponent;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void SetText(string newText)
    {
        textComponent.text = newText;
    }

    public void SetCallback(UnityAction callback)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(callback);
    }
}
