using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreenManager : MonoBehaviour
{
    public event Action OnBackToStartScreen;

    [SerializeField] private Canvas canvas;
    [SerializeField] private TextMeshProUGUI earningsText;

    public void LoadInformation(float earnings)
    {
        string earningsString = "You earned: $" + earnings;
        earningsText.text = earningsString;
    }

    public void BackToStartScreen()
    {
        Debug.Log("[GameOverScreenManager] - BackToStartScreen() Called");
        OnBackToStartScreen?.Invoke();
    }
}