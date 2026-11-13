using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using static GameManager;

// Singleton GameManager that manages the game
public class GameManager : MonoBehaviour
{
    // An instance of this 
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            // If the instance is null, find it in the scene 
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<GameManager>();

                // If it still doesn't exist, make one 
                if (_instance == null)
                {
                    GameObject gameObject = new GameObject("GameManager (Automated");
                    _instance = gameObject.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    public enum GameState
    {
        MainMenu,
        Credits,
        Greenhouse,
        Shop,
        DayStart, 
        DayEnd
    }

    [Header("Database & Config")]
    [SerializeField] private Character_Database character_Database;
    public Character_Database Character_Database
    {
        get { return character_Database; } 
    }

    private PlayerData currentPlayerData;


    [SerializeField] private Dialogue_Controller dialogue_Controller;
    [SerializeField] private Dialogue_View dialogue_View;
    public Dialogue_View Dialogue_View
    {
        get { return dialogue_View; }
    }


    [SerializeField] private PlayerMoneyManager playerMoneyManager;
    [SerializeField] private GreenhouseManager greenhouseManager;
    [SerializeField] private GameOverScreenManager gameOverScreenManager;
    [SerializeField] private CreditsController creditsController;
    [SerializeField] private DayOverManager dayOverManager;
   

    // The Main Menu Controller of the Game 
    [SerializeField] private MainMenuController mainMenuController;
    // The loadingGameplay screen of the game 
    [SerializeField] private LoadingScreenController loadingScreenController;

    [SerializeField] private MainMenuButton gameplayMainMenuButton;
    [SerializeField] private MainMenuButton creditsMainMenuButton;
    [SerializeField] private MainMenuButton greenhouseMainMenuButton;
    [SerializeField] private GreenhouseData currentGreenhouseData;
    [SerializeField] private TutorialPanel tutorialPanel;

    [SerializeField] public int currentDay; 


    // Active State of the game 
    public GameState currentState;

    // The amount of money the user has 
    public PlayerMoney money;

    public bool canCloseGrate;
    private bool loadingMainMenu = false;
    private bool loadingShop = false;
    private bool loadingDayOver = false;
    private bool loadingGreenhouse = false;
    private bool loadingCredits = false;

    private void Awake()
    {
        // Setting up the singleton 
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);

        // Subscribe to the main menu controller
        mainMenuController.OnStartGame += HandleStartGame; 
        mainMenuController.OnQuitGame += HandleQuitGame;
        mainMenuController.OnViewCredits += HandleViewCredits;
    }

    private void Update()
    {
        if (loadingMainMenu)
        {
            // Show loading screen 
            loadingScreenController.ShowLoadingScreen();
            // Load in everything 
            GameObject mainMenuControllerObject = GameObject.Find("MainMenuController");
            if (mainMenuControllerObject != null)
            {
                mainMenuController = mainMenuControllerObject.GetComponent<MainMenuController>();
                if (mainMenuController != null)
                {
                    mainMenuController.OnStartGame += HandleStartGame;
                    mainMenuController.OnQuitGame += HandleQuitGame;
                    mainMenuController.OnViewCredits += HandleViewCredits;
                    loadingScreenController.HideLoadingScreen();
                    loadingMainMenu = false;
                }
            }
        }
        else if (loadingShop)
        {
            // Show loading screen 
            loadingScreenController.ShowLoadingScreen();
            if (ShopManager.Instance == null)
            {
                Debug.Log("[GameManager] - There is no shop manager!");
            }
            else
            {
                ShopManager.Instance.LoadShop();
                ShopManager.OnBackToMainMenu += HandleBackToMainMenu;
                ShopManager.OnShopDayOver += HandleGoToGreenhouse;
                loadingShop = false;
                loadingScreenController.HideLoadingScreen();
            }
        }
        else if (loadingGreenhouse)
        {
            // Show loading screen
            loadingScreenController.ShowLoadingScreen();
            if (GreenhouseManager.Instance == null)
            {
                Debug.Log("[GameManager] - There is no greenhouse manager!");
            }
            else
            {

                // If it is not the first day 
                if (currentDay > 1)
                {
                    GreenhouseManager.Instance.LoadGreenhouseData(currentGreenhouseData);

                    GreenhouseManager.Instance.SetUpGreenhouse();
                    // Advance 
                    GreenhouseManager.Instance.AdvanceDay();
                }
                else
                {
                    GreenhouseManager.Instance.SetUpGreenhouse();
                }

                GreenhouseManager.OnEndGreenhouseDay += EndDay;
                loadingGreenhouse = false;
                loadingScreenController.HideLoadingScreen();
            }
        }
        else if (loadingDayOver)
        {
            // Show loading screen
            loadingScreenController.ShowLoadingScreen();
            if (dayOverManager == null)
            {
                GameObject dayOverManagerObject = GameObject.Find("DayOverManager");
                if (dayOverManagerObject != null)
                {
                    dayOverManager = dayOverManagerObject.GetComponent<DayOverManager>();
                    if (dayOverManager != null)
                    {
                        dayOverManager.OnQuit += HandleQuitGame;
                        dayOverManager.OnNextDay += AdvanceDay;
                        dayOverManager.LoadInformation(currentDay, PlayerMoneyManager.Instance.GetMoney());
                        loadingDayOver = false;
                        loadingScreenController.HideLoadingScreen();
                    }
                }
            }
        }
        else if (loadingCredits)
        {
            // Show loading screen
            loadingScreenController.ShowLoadingScreen();
            if (creditsMainMenuButton == null)
            {
                GameObject creditsButtonObject = GameObject.Find("Back to MainMenu");
                if (creditsButtonObject != null)
                {
                    creditsMainMenuButton = creditsButtonObject.GetComponent<MainMenuButton>();
                    if (creditsMainMenuButton != null)
                    {
                        creditsMainMenuButton.OnReturnToMainMenu += HandleBackToMainMenu;
                        loadingScreenController.HideLoadingScreen();
                        loadingCredits = false;
                    }
                }
            }
        }
        else if (loadingMainMenu)
        {
            // Show loading screen 
            loadingScreenController.ShowLoadingScreen();
            if (mainMenuController == null)
            {
                GameObject mainMenuControllerObject = GameObject.Find("MainMenuController");
                if (mainMenuControllerObject != null)
                {
                    mainMenuController = mainMenuControllerObject.GetComponent<MainMenuController>();
                    if (mainMenuController != null)
                    {
                        mainMenuController.OnQuitGame += HandleQuitGame;
                        mainMenuController.OnStartGame += HandleStartGame;
                        mainMenuController.OnViewCredits += HandleViewCredits;
                        loadingScreenController.HideLoadingScreen();
                        loadingMainMenu = false;
                    }
                }
            }
        }
    }

    // Starts the day - queues up the cast of customers 
    public void StartLoadingShop()
    {
        if (ShopManager.Instance == null)
        {
            Debug.Log("ShopManager instance is null");
        }
        else
        {
            loadingShop = true;
            Debug.Log("ShopManager instance is NOT null");
        }
    }

    private void EndDay(GreenhouseData greenhouseData)
    {
        currentGreenhouseData = greenhouseData;
        currentState = GameState.DayEnd;
        SceneManager.LoadScene("Day Over");
        loadingDayOver = true;
    }

    public void HandleQuitGame()
    {
        // Save the state of the game 
        //SaveGameState(playerMoneyManager.GetMoney(), currentDay);
        Application.Quit();
    }


    public void HandleGoToShop()
    {
        Debug.Log("[GameManager] - HandleGoToCounter() Called");
        // Change the scene to the shop scene 
        SceneManager.LoadScene("Undergrowth");
        StartLoadingShop();
        currentState = GameState.Shop;
    }

    public void HandleGoToGreenhouse()
    {
        Debug.Log("[GameManager] - HandleGoToGreenhouse() Called");
        // Change the scene to the greenhouse scene 
        SceneManager.LoadScene("Greenhouse");
        currentState = GameState.Greenhouse;
        loadingGreenhouse = true;
    }


    public void HandleStartGame()
    {
        Debug.Log("[GameManager] - HandleStartGame() Called");
        currentDay = 1;
        currentPlayerData = CreateDefaultSaveData();
        SaveSystem.SavePlayerData(currentPlayerData);
        HandleGoToShop();
    }

    private PlayerData CreateDefaultSaveData()
    {
        PlayerData newSaveData = new PlayerData
        {
            currentMoney = 0.0f,
            currentDay = 1
        };

        // Populate database 
        foreach (Character_Data character in character_Database.GetAllCharacters())
        {
            newSaveData.SetCharacterWeight(character.CharacterId, character.defaultWeight);
        }

        return newSaveData;
    }
    
    public void HandleViewCredits()
    {
        Debug.Log("[GameManager] - HandleViewCredits() Called");
        // Change the scene to the credits 
        SceneManager.LoadScene("Credits");
        loadingCredits = true;
    }

    public void HandleBackToMainMenu()
    {
        Debug.Log("[GameManager] - HandleBackToMainMenu() Called");
        SceneManager.LoadScene("MainMenu");
        loadingMainMenu = true;
    }

    public void SaveGameState(float money, int day)
    {
        currentPlayerData.currentMoney = money;
        currentPlayerData.currentDay = day;

        SaveSystem.SavePlayerData(currentPlayerData);
    }

    public void AdvanceDay()
    {
        // Increase the day number 
        currentDay++;

        // TODO: Remove any money from player 
        HandleGoToShop();
    }
}
