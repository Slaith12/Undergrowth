using System;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public event Action OnStartGame;
    public event Action OnQuitGame;
    public event Action OnViewCredits;
    public event Action OnViewGreenhouse;

    // Start the game 
    public void StartGame()
    {
        Debug.Log("[MainMenuController] - StartGame() Called");
        OnStartGame?.Invoke();
    }

    // Quit the Game 
    public void QuitGame()
    {
        Debug.Log("[MainMenuController] - QuitGame() Called");

        OnQuitGame?.Invoke();
    }

    // View the credits of the game 
    public void ViewCredits()
    {
        Debug.Log("[MainMenuController] - ViewCredits() Called");
        OnViewCredits?.Invoke();
    }

    // View the greenhouse
    public void ViewGreenhouse()
    {
        Debug.Log("[MainMenuController] - ViewGreenhosue() Called");
        OnViewGreenhouse?.Invoke();
    }
}