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
        StartMenu,
        Credits,
        DayStart,
        SpawningCustomer,
        Dialogue,
        Refusing,
        PostInteraction,
        Closed,
        DayEnd,
        Opened, 
        SellingPlant
    }

    [Header("Database & Config")]
    [SerializeField] private Character_Database character_Database;
    public Character_Database Character_Database
    {
        get { return character_Database; } 
    }

    [SerializeField] private int customersPerDay = 5;
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

    // The Main Menu Controller of the Game 
    [SerializeField] private MainMenuController mainMenuController;
    // The loadingGameplay screen of the game 
    [SerializeField] private LoadingScreenController loadingScreenController;

    [SerializeField] private MainMenuButton gameplayMainMenuButton;
    [SerializeField] private MainMenuButton creditsMainMenuButton;
    [SerializeField] private MainMenuButton greenhouseMainMenuButton;

    private List<Character_Data> todaysQueue = new List<Character_Data>();
    private Character_Data currentCustomer;
    private int currentDay;


    // Active State of the game 
    public GameState currentState;

    // The amount of money the user has 
    public PlayerMoney money;

    public bool canCloseGrate;
    private bool loadingGameplay = false;
    private bool loadingGameOver = false;
    private bool loadingCredits = false;
    private bool loadingMainMenu = false;
    private bool loadingGreenhouse = false;

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
        mainMenuController.OnViewGreenhouse += HandleViewGreenhouse;

        ShopManager.Instance.OnBackToMainMenu += HandleBackToMainMenu;
        ShopManager.Instance.OnShopDayOver += HandleGoToGreenhouse;
    }

    private void Update()
    {

        //else if (loadingGameOver)
        //{
        //    // Show the loadingGameplay screen
        //    loadingScreenController.ShowLoadingScreen();
        //    // Load in everything 
        //    GameObject gameOverObject = GameObject.Find("GameOverScreenManager"); 
        //    if (gameOverObject != null)
        //    {
        //        gameOverScreenManager = gameOverObject.GetComponent<GameOverScreenManager>();
        //        gameOverScreenManager.OnBackToStartScreen += HandleBackToMainMenu;
        //        gameOverScreenManager.LoadInformation(playerMoneyManager.GetMoney());
        //    }

        //    if ((gameOverScreenManager != null))
        //    {
        //        loadingScreenController.HideLoadingScreen();
        //        loadingGameOver = false;
        //    }
        //}
        //else if (loadingCredits)
        //{
        //    // Show the loadingGameplay screen
        //    loadingScreenController.ShowLoadingScreen();
        //    // Load in everything 
        //    GameObject creditsMainMenuButtonObject = GameObject.Find("Back to MainMenu");
        //    if (creditsMainMenuButtonObject != null)
        //    {
        //        creditsMainMenuButton = creditsMainMenuButtonObject.GetComponent<MainMenuButton>();
        //        creditsMainMenuButton.OnReturnToMainMenu += HandleBackToMainMenu;
        //    }

        //    if (creditsMainMenuButton != null)
        //    {
        //        loadingScreenController.HideLoadingScreen();
        //        loadingCredits = false;
        //    }

        //    mainMenuController.OnStartGame -= HandleStartGame;
        //    mainMenuController.OnQuitGame -= HandleQuitGame;
        //    mainMenuController.OnViewCredits -= HandleViewCredits;
        //    mainMenuController.OnViewGreenhouse -= HandleViewGreenhouse;
        //}
        //else if (loadingMainMenu)
        //{
        //    // Show the loadingGameplay screen
        //    loadingScreenController.ShowLoadingScreen();
        //    // Load in everything 
        //    GameObject mainMenuControllerObject = GameObject.Find("MainMenuController");
        //    if (mainMenuControllerObject != null)
        //    {
        //        mainMenuController = mainMenuControllerObject.GetComponent<MainMenuController>();
        //        loadingScreenController.HideLoadingScreen();
        //    }

        //    if (mainMenuController != null)
        //    {
        //        mainMenuController.OnStartGame += HandleStartGame;
        //        mainMenuController.OnQuitGame += HandleQuitGame;
        //        mainMenuController.OnViewCredits += HandleViewCredits;
        //        mainMenuController.OnViewGreenhouse += HandleViewGreenhouse;
        //        loadingMainMenu = false;
        //    }
        //}else if (loadingGreenhouse)
        //{
        //    // Show the loadingGameplay screen
        //    loadingScreenController.ShowLoadingScreen();
        //    // Load in everything 
        //    GameObject greenhouseMainMenuButtonObject = GameObject.Find("Main Menu Button");
        //    if (greenhouseMainMenuButtonObject != null)
        //    {
        //        greenhouseMainMenuButton = greenhouseMainMenuButtonObject.GetComponent<MainMenuButton>();
        //        greenhouseMainMenuButton.OnReturnToMainMenu += HandleBackToMainMenu;
        //    }

        //    GameObject greenhouseManagerObject = GameObject.Find("Greenhouse"); 
        //    if (greenhouseManagerObject != null)
        //    {
        //        greenhouseManager = greenhouseManagerObject.GetComponent<GreenhouseManager>();
        //        greenhouseManager.OnGoToCounter += HandleGoToShop;
        //    }

        //    if ((greenhouseManager != null) && (greenhouseMainMenuButton != null))
        //    {
        //        loadingGreenhouse = false;
        //        loadingScreenController.HideLoadingScreen();
        //    }
        //}
    }

    // Starts the day - queues up the cast of customers 
    public void StartDay()
    {
        if (ShopManager.Instance == null)
        {
            Debug.Log("ShopManager instance is null");
        }
        else
        {
            Debug.Log("ShopManager instance is NOT null");
            ShopManager.Instance.LoadShop();
        }
    }

    private void EndDay()
    {
        currentState = GameState.DayEnd;
        //HandleGameOver();
        Debug.Log("Day is over!");
    }

    public void HandleQuitGame()
    {
        // Save the state of the game 
        SaveGameState(playerMoneyManager.GetMoney(), currentDay);
        Application.Quit();
    }


    public void HandleGoToShop()
    {
        Debug.Log("[GameManager] - HandleGoToCounter() Called");
        // Change the scene to the shop scene 
        SceneManager.LoadScene("Undergrowth");
        StartDay();
    }

    public void HandleGoToGreenhouse()
    {
        Debug.Log("[GameManager] - HandleGoToGreenhouse() Called");
        // Change the scene to the greenhouse scene 
        SceneManager.LoadScene("Greenhouse");
    }


    public void HandleStartGame()
    {
        Debug.Log("[GameManager] - HandleStartGame() Called");

        // If there is a save file, load from it 
        if (SaveSystem.HasSaveFile())
        {
            currentPlayerData = SaveSystem.LoadPlayerData();
        }
        else
        {
            currentPlayerData = CreateDefaultSaveData();
            SaveSystem.SavePlayerData(currentPlayerData);
        }

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

    public void HandleGameOver()
    {
        // Save the game 
        SaveGameState(playerMoneyManager.GetMoney(), currentDay);
        SceneManager.LoadScene("TEMP End Screen");
        loadingGameOver = true;
    }

    public void HandleViewGreenhouse()
    {
        // Change the scene to the Greenhouse 
        SceneManager.LoadScene("Greenhouse");
        loadingGreenhouse = true;
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
        // Advance day for greenhouse
        greenhouseManager.AdvanceDay();
    }
}
