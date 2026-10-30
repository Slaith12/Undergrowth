using System;
using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    public event Action OnReturnToMainMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReturnToMainMenu()
    {
        Debug.Log("[MainMenuButton] - ReturnToMainMenu() Called");
        OnReturnToMainMenu?.Invoke();
    }
}