using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
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
        DayEnd
    }

    [Header("Database & Config")]
    [SerializeField] private Character_Database character_Database;
    [SerializeField] private int customersPerDay = 5;
    [SerializeField] private Dialogue_Controller dialogue_Controller;
    [SerializeField] private Dialogue_View dialogue_View;
    [SerializeField] private GrateMover grateMover;
    [SerializeField] private LeverClick leverClick;
    [SerializeField] private PlantManager plantManager;


    private List<Character_Data> todaysQueue = new List<Character_Data>();
    private Character_Data currentCustomer;

    // Active State of the game 
    public GameState currentState;

    // The amount of money the user has 
    public PlayerMoney money;

    public bool tryStartDay;
    public bool canCloseGrate;

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
        dialogue_Controller.DialogueEnded += OnDialogueComplete;
        dialogue_Controller.RefusedService += HandleRefuseService;
        dialogue_Controller.SellPlant += HandleSellPlant;
        tryStartDay = false;
    }

    private void Update()
    {
        // If gamemanager was told to start the day 
        if (tryStartDay)
        {
            // If all things that needed to be loaded in are loaded in 
            if (dialogue_View && grateMover && leverClick && plantManager)
            {
                // Start the day 
                StartDay(); 
                tryStartDay=false;
            }
            else
            {
                // If there's no dialogue view 
                if (!dialogue_View)
                {
                    GameObject dialogueViewObject = GameObject.Find("Dialogue Panel");

                    if (dialogueViewObject != null)
                    {
                        dialogue_View = dialogueViewObject.GetComponent<Dialogue_View>();
                    }
                }

                // If there's no grate mover 
                if (!grateMover)
                {
                    GameObject grateMoverObject = GameObject.Find("Grate");

                    if (grateMoverObject != null)
                    {
                        grateMover = grateMoverObject.GetComponent<GrateMover>();
                    }
                }

                // If there's no lever click 
                if (!leverClick)
                {
                    GameObject leverClickObject = GameObject.Find("Lever");

                    if (leverClickObject != null)
                    {
                        leverClick = leverClickObject.GetComponent<LeverClick>();
                        leverClick.LeverClicked += HandleLeverClick;
                    }
                }

                if (!plantManager)
                {
                    GameObject plantManagerObject = GameObject.Find("Plants");
                    if (plantManagerObject != null)
                    {
                        plantManager = plantManagerObject.GetComponent<PlantManager>();
                        plantManager.PlantSold += HandlePlantSold;
                    }
                }
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
        Debug.Log("Day is over!");
    }

    // Tells the Dialogue Controller to start the dialogue of this character 
    public void TriggerDialoge(Character_Data character)
    {
        canCloseGrate = false;
        // Change the state 
        currentState = GameState.Dialogue;

        // Pass the character's information to the controller 
        dialogue_Controller.StartDialogue(character, character_Database, dialogue_View);
    }

    public void OnDialogueComplete()
    {
        // TODO: Add logic which changes the weights of each character 

        // Then, spawn in the next character
        SpawnNextCharacter();
    }

    // Switches the current state of the game 
    public void SwitchGameState(string state)
    {
        if (Enum.TryParse<GameState>(state, true, out GameState gameState))
        {
            currentState = gameState;
            // TODO: Add the logic of what this looks like 
            if (gameState == GameState.DayStart)
            {
                tryStartDay = true;
            }
        }
        else
        {
            Debug.Log("No Gamestate was found with the name: " + state);
        }
    }

    public void HandleRefuseService()
    {
        currentState = GameState.Refusing;
        canCloseGrate = true;
    }

    public void HandleSellPlant()
    {
        plantManager.canSell = true;
    }

    public void HandleLeverClick()
    {
        if (canCloseGrate)
        {
            grateMover.DetermineAction();
            canCloseGrate = false;
        }
    }

    public void HandlePlantSold()
    {
        Debug.Log("[GameManager] - HandlePlantSold called");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
