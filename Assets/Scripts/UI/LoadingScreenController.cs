using UnityEngine;

public class LoadingScreenController : MonoBehaviour
{
    // An instance of this 
    public static LoadingScreenController Instance { get; private set; }

    private GameObject loadingScreen;
    private bool loadingScreenIsActive = false;

    private void Awake()
    {
        // Setting up the singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Save the loadingGameplay screen
        loadingScreen = gameObject.transform.GetChild(0).gameObject;    
    }

    public void ShowLoadingScreen()
    {
        loadingScreen.SetActive(true);
        loadingScreenIsActive = true;
    }

    public void HideLoadingScreen()
    {
        loadingScreen.SetActive(false);
        loadingScreenIsActive = false;
    }
}
