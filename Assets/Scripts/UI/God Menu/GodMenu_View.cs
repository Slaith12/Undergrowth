using Ink.Parsed;
using System;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class GodMenu_View : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown characterDropDown;

    public event Action<int> OnCharacterChanged;

    private void Awake()
    {
        characterDropDown.onValueChanged.AddListener(value  => OnCharacterChanged?.Invoke(value));
    }

    public void InitializeCharacterDropDown(List<string> options)
    {
        characterDropDown.ClearOptions();
        characterDropDown.AddOptions(options);
    }

    public void UpdateGraphicDropdownSelection(int index)
    {
        characterDropDown.value = index;
        characterDropDown.RefreshShownValue();
    }
}
