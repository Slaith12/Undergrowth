using System;
using UnityEngine;

public class CreditsButton : MonoBehaviour
{
    public event Action BackToMainMenu;
    public void OnBackToMainMenu()
    {
        BackToMainMenu?.Invoke();
    }
}
