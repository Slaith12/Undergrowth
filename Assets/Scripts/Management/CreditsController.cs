using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    public event Action CreditsBackToMainMenu;
    public LoadingScreenController LoadingScreenController;

    public void Start()
    {
    }

    public void Update()
    {
        GameObject loadingScreenGameObject = GameObject.Find("LoadingScreenCanvas");
        if (loadingScreenGameObject != null)
        {
            LoadingScreenController = loadingScreenGameObject.GetComponent<LoadingScreenController>();
        }
    }

    public void BackToMainMenu()
    {
        CreditsBackToMainMenu?.Invoke();
        Debug.Log("[CreditsController] - BackToMainMenu() called");
    }
}