using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using static GameManager;

// Singleton GameManager that manages the game
public class GameManager : MonoBehaviour
{
    // An instance of this 
    public static GameManager Instance { get; private set; }

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
    [SerializeField] private int customersPerDay = 5;



    [SerializeField] private Dialogue_Controller dialogue_Controller;
    [SerializeField] private Dialogue_View dialogue_View;
    [SerializeField] private GrateMover grateMover;
    [SerializeField] private LeverClick leverClick;
    [SerializeField] private PlantManager plantManager;
    [SerializeField] private PlayerMoneyManager playerMoneyManager;

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
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Subscribe to the main menu controller
        mainMenuController.OnStartGame += HandleStartGame; 
        mainMenuController.OnQuitGame += HandleQuitGame;
        mainMenuController.OnViewCredits += HandleViewCredits;
        mainMenuController.OnViewGreenhouse += HandleViewGreenhouse;
    }

    private void Update()
    {
        // If the game is loadingGameplay, gather all of the data we need 
        if (loadingGameplay)
        {
            // Show the loadingGameplay screen
            loadingScreenController.ShowLoadingScreen();
            // Load in everything 

            mainMenuController.OnStartGame -= HandleStartGame;
            mainMenuController.OnQuitGame -= HandleQuitGame;
            mainMenuController.OnViewCredits -= HandleViewCredits;
            mainMenuController.OnViewGreenhouse -= HandleViewGreenhouse;

            // ----- DIALOGUE CONTROLLER ----- 

            GameObject dialogueControllerObject = GameObject.Find("Dialogue_Controller");
            if (dialogueControllerObject != null)
            {
                dialogue_Controller = dialogueControllerObject.GetComponent<Dialogue_Controller>();
                // Subscribe to events 
                dialogue_Controller = dialogueControllerObject.GetComponent<Dialogue_Controller>();
                //dialogue_Controller.DialogueEnded += OnDialogueComplete;
                dialogue_Controller.RefusedService += HandleRefuseService;
                dialogue_Controller.SellPlant += HandleSellPlant;
            }
            else
            {
                //Debug.Log("[GameManager] - No dialogueControllerObject has been found yet");
            }

            // ----- PLANT MANAGER ----- 
            GameObject plantManagerObject = GameObject.Find("Plants");
            if (plantManagerObject != null)
            {
                plantManager = plantManagerObject.GetComponent<PlantManager>();
                // Subscribe to event 
                plantManager.PlantSold += HandlePlantSold;
            }
            else
            {
                //Debug.Log("[GameManager] - No plantManagerObject has been found yet");
            }

            // ----- DIALOGUE VIEW ----- 
            GameObject dialogueViewObject = GameObject.Find("Dialogue Panel");
            if (dialogueViewObject != null)
            {
                dialogue_View = dialogueViewObject.GetComponent<Dialogue_View>();
            }
            else
            {
                //Debug.Log("[GameManager] - No dialogueViewObject has been found yet");
            }

            // ----- GRATE MOVER -----
            GameObject grateMoverObject = GameObject.Find("Grate");
            if (grateMoverObject != null)
            {
                grateMover = grateMoverObject.GetComponent<GrateMover>();
            }
            else
            {
                //Debug.Log("[GameManager] - No grateMoverObject has been found yet");
            }

            // ----- LEVER CLICK -----
            GameObject leverClickObject = GameObject.Find("Lever");
            if (leverClickObject != null)
            {
                leverClick = leverClickObject.GetComponent<LeverClick>();
                leverClick.LeverClicked += HandleLeverClick;
            }
            else
            {
                //Debug.Log("[GameManager] - No leverClickObject has been found yet");
            }

            // ----- PLAYER MONEY -----
            GameObject playerMoneyManagerObject = GameObject.Find("Money Tracker");
            if ( plantManagerObject != null)
            {
                playerMoneyManager = playerMoneyManagerObject.GetComponent<PlayerMoneyManager>();
            }

            // ----- Main Menu Button -----
            GameObject gameplayMainMenuButtonObject = GameObject.Find("Main Menu Button"); 
            if (gameplayMainMenuButtonObject != null)
            {
                gameplayMainMenuButton = gameplayMainMenuButtonObject.GetComponent<MainMenuButton>();
                gameplayMainMenuButton.OnReturnToMainMenu += HandleBackToMainMenu;

            }

            // If everything has been loaded in, start the game 
            if ((leverClick != null) && (grateMover != null) && (plantManager != null) && (dialogue_Controller != null) && (dialogue_View != null) && (playerMoneyManager != null))
            {
                loadingGameplay = false;
                // Hide the loadingGameplay screen 
                loadingScreenController.HideLoadingScreen();
                // Start the day
                StartDay();
            }
        }
        else if (loadingGameOver)
        {
            // Show the loadingGameplay screen
            loadingScreenController.ShowLoadingScreen();
            // Load in everything 
            GameObject gameOverObject = GameObject.Find("GameOverScreenManager"); 
            if (gameOverObject != null)
            {
                gameOverScreenManager = gameOverObject.GetComponent<GameOverScreenManager>();
                gameOverScreenManager.OnBackToStartScreen += HandleBackToMainMenu;
                gameOverScreenManager.LoadInformation(money.runtimeValue);
            }

            if ((gameOverScreenManager != null))
            {
                loadingScreenController.HideLoadingScreen();
                loadingGameOver = false;
            }
        }
        else if (loadingCredits)
        {
            // Show the loadingGameplay screen
            loadingScreenController.ShowLoadingScreen();
            // Load in everything 
            GameObject creditsMainMenuButtonObject = GameObject.Find("Back to MainMenu");
            if (creditsMainMenuButtonObject != null)
            {
                creditsMainMenuButton = creditsMainMenuButtonObject.GetComponent<MainMenuButton>();
                creditsMainMenuButton.OnReturnToMainMenu += HandleBackToMainMenu;
            }

            if (creditsMainMenuButton != null)
            {
                loadingScreenController.HideLoadingScreen();
                loadingCredits = false;
            }

            mainMenuController.OnStartGame -= HandleStartGame;
            mainMenuController.OnQuitGame -= HandleQuitGame;
            mainMenuController.OnViewCredits -= HandleViewCredits;
            mainMenuController.OnViewGreenhouse -= HandleViewGreenhouse;
        }
        else if (loadingMainMenu)
        {
            // Show the loadingGameplay screen
            loadingScreenController.ShowLoadingScreen();
            // Load in everything 
            GameObject mainMenuControllerObject = GameObject.Find("MainMenuController");
            if (mainMenuControllerObject != null)
            {
                mainMenuController = mainMenuControllerObject.GetComponent<MainMenuController>();
                loadingScreenController.HideLoadingScreen();
            }

            if (mainMenuController != null)
            {
                mainMenuController.OnStartGame += HandleStartGame;
                mainMenuController.OnQuitGame += HandleQuitGame;
                mainMenuController.OnViewCredits += HandleViewCredits;
                mainMenuController.OnViewGreenhouse += HandleViewGreenhouse;
                loadingMainMenu = false;
            }
        }
        else if (loadingGreenhouse)
        {
            // Show the loadingGameplay screen
            loadingScreenController.ShowLoadingScreen();
            // Load in everything 
            GameObject greenhouseMainMenuButtonObject = GameObject.Find("Main Menu Button");
            if (greenhouseMainMenuButtonObject != null)
            {
                greenhouseMainMenuButton = greenhouseMainMenuButtonObject.GetComponent<MainMenuButton>();   
                loadingScreenController.HideLoadingScreen();
            }

            if (greenhouseMainMenuButton != null)
            {
                greenhouseMainMenuButton.OnReturnToMainMenu += HandleBackToMainMenu;
                loadingGreenhouse = false;
            }
        }
    }

    // Starts the day - queues up the cast of customers 
    public void StartDay()
    {
        // Set the current state of the day 
        currentState = GameState.DayStart;

        // Clear the queue 
        todaysQueue.Clear();

        // Get the list of valid customers 
        List<Character_Data> data = character_Database.GetAllCharacters();

        // Loop through, and pick random characters to spawn 
        for (int i = 0; i < customersPerDay; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, data.Count);
            todaysQueue.Add(data[randomIndex]);
            // Remove duplicates 
            data.RemoveAt(randomIndex);
        }

        SpawnNextCharacter();
    }

    // Spawn the next customer in the queue 
    private void SpawnNextCharacter()
    {
        // If there are customers left to interact with 
        if (todaysQueue.Count > 0)
        {
            // Change the state 
            currentState = GameState.SpawningCustomer;

            // Set the customer 
            currentCustomer = todaysQueue[0];
            todaysQueue.RemoveAt(0);
            TriggerDialoge(currentCustomer);
            Debug.Log("[GameManager] - SpawnNewCharacter() Character: " + currentCustomer.DisplayName);
        }
        else
        {
            // TODO: End the day 
            EndDay();
        }
    }

    private void EndDay()
    {
        currentState = GameState.DayEnd;
        HandleGameOver();
        Debug.Log("Day is over!");
    }

    // Tells the Dialogue Controller to start the dialogue of this character 
    public void TriggerDialoge(Character_Data character)
    {
        Debug.Log("TriggerDialogue() called for: " + character.DisplayName);
        // Change the state 
        currentState = GameState.Dialogue;

        // Pass the character's information to the controller 
        dialogue_Controller.StartDialogue(character, character_Database, dialogue_View);
    }

    public void OnDialogueComplete()
    {
        SpawnNextCharacter();
    }

    public void HandleRefuseService()
    {
        Debug.Log("[GameManager] - HandleRefuseService() Called");
        currentState = GameState.Refusing;
    }

    public void HandleSellPlant()
    {
        Debug.Log("[GameManager] - HandleSellPlant() Called");
        currentState = GameState.SellingPlant;
        plantManager.canSell = true;
    }

    public void HandleLeverClick()
    {
        Debug.Log("[GameManager] - HandleLeverClick() Called");

        // If the store is closed
        if (currentState == GameState.Closed)
        {
            // Open the door 
            grateMover.OpenGrate();
            currentState = GameState.Opened;
            SpawnNextCharacter();
        }
        else if (currentState == GameState.Refusing)
        {
            // Close the door 
            grateMover.CloseGrate();
            currentState = GameState.Closed;
        }
    }

    public void HandlePlantSold(Plant plantToSell)
    {
        //Debug.Log("[GameManager] - Sold: " + plantToSell.plantName);
        // Add money 
        playerMoneyManager.GainMoney(plantToSell.plantPrice);
        // Change the state 


        // Put away the character and the plant 
        OnDialogueComplete();
        GameObject plant = plantToSell.gameObject; 
        plant.SetActive(false);
        plantManager.canSell = false;
    }

    public void HandleQuitGame()
    {
        Application.Quit();
    }

    public void HandleStartGame()
    {
        Debug.Log("[GameManager] - HandleStartGame() Called");
        // Change the scene to the gameplay scene 
        SceneManager.LoadScene("Undergrowth");

        // Start loadingGameplay things in 
        loadingGameplay = true;
    }

    public void HandleGameOver()
    {
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
}
