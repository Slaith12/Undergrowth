using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using System;

public class ShopManager : MonoBehaviour
{
    // An instance of this manager 
    private static ShopManager _instance; 
    public static ShopManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // Try to find ShopManager 
                _instance = FindAnyObjectByType<ShopManager>(); 

                // If not found, create new one 
                if (_instance == null)
                {
                    GameObject gameObject = new GameObject("ShopManager (Auto-Generated)");
                    _instance = gameObject.AddComponent<ShopManager>();
                }
            }
            return _instance;
        }
    }

    public enum ShopState
    {
        ShopStart, 
        Dialogue, 
        Refusing, 
        SellingPlant,
        PostInteraction, 
        ShopEnd
    }

    // The current state of the shop
    [SerializeField] private ShopState shopState;
    // The amount of characters that can show up each day
    [SerializeField] private int customersPerDay = 3;
    // The list of characters that will be attending the shop 
    private List<Character_Data> todaysQueue = new List<Character_Data>();
    // The current character
    private Character_Data currentCustomer;
    [SerializeField] private PlantManager plantManager;
    [SerializeField] private GrateMover grateMover;
    [SerializeField] private LeverClick leverClick;
    [SerializeField] private Dialogue_Controller dialogueController;
    [SerializeField] private Dialogue_View dialogue_view;
    [SerializeField] private TutorialPanel tutorialPanel;
    [SerializeField] private GameObject gameCanvas;

    // Events that get sent to the GameManager 
    public static event Action OnBackToMainMenu;
    public static event Action OnShopDayOver;

    public bool loadingShop;


    private void Awake()
    {
        // Set up the singleton 
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        _instance = this;
    }

    private void Start()
    {
        loadingShop = true;
        gameCanvas.SetActive(false);
    }


    private void Update()
    {
        // If the shop is loading 
        if (loadingShop)
        {
            if (tutorialPanel != null)
            {
                tutorialPanel.Closed += HandleCloseTutorial;
            }
            // If there is a plant manager 
            if (plantManager != null)
            {
                // Subscribe to events from Plant Manager
                plantManager.PlantSold += HandlePlantSold;
            }

            DayManager.Instance.ShowDayTracker();

            // Find the dialogue view 
            if (dialogue_view == null)
            {
                dialogue_view = GameObject.FindFirstObjectByType<Dialogue_View>();
                if (dialogue_view == null)
                {
                    Debug.Log("[ShopManager] - No dialogue_view_object was found");
                }
            }

            if (Dialogue_Controller.Instance != null)
            {
                Dialogue_Controller.Instance.RefusedService += HandleRefuseService;
                Dialogue_Controller.Instance.SellPlant += HandleSellPlant;
                Dialogue_Controller.Instance.DialogueEnded += OnDialogueComplete;
            }
            else
            {
                Debug.LogWarning("No Dialogue_Controller.Instance found!");
            }

            leverClick.LeverClicked += HandleLeverClick;
            
            // If everything has loaded in
            if ((dialogueController != null) && (dialogue_view != null) && (plantManager  != null) && (leverClick != null))
            {
                dialogueController.dialogueView = dialogue_view;
                
                loadingShop = false;
            }
        }

    }
    /// <summary>
    /// Load and create the queue of customers that will be attending the shop 
    /// on this day
    /// </summary>
    public void CreateCustomerQueue()
    {
        // Clear the queue 
        todaysQueue.Clear();

        // Get the list of valid characters 
        List<Character_Data> data = GameManager.Instance.Character_Database.GetAllCharacters();
        
        // TODO: Make this based on the weights of the characters 

        // Loop through the list and pick random characters to spawn 
        for (int i = 0; i < customersPerDay; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, data.Count);
            todaysQueue.Add(data[randomIndex]);
            // Remove dupplicates 
            data.RemoveAt(randomIndex);
        }
    }

    /// <summary>
    /// Spawn the next Customer in the queue
    /// </summary>
    private void SpawnNextCustomer(int dayNumber)
    {
        // If there are customers left to interact with 
        if (todaysQueue.Count > 0)
        {
            // Set the customer
            currentCustomer = todaysQueue[0]; 
            todaysQueue.RemoveAt(0);
            // Start the dialogue of this customer
            Dialogue_Controller.Instance.StartDialogue(currentCustomer,dayNumber);
            // Set the current state to be dialogue 
            shopState = ShopState.Dialogue;
        }
        // If there are no more customers left to interact with
        else
        {
            EndShopDay();
        }
    }

    /// <summary>
    /// Start the day at the shop
    /// </summary>
    public void StartShopDay(int dayNumber)
    {
        Debug.Log("[ShopManager] - StartShopDay()");
        shopState = ShopState.ShopStart;

        // Create the customer queue 
        CreateCustomerQueue();
        // Spawn the first customer 
        SpawnNextCustomer(dayNumber);
    }

    public void LoadShop()
    {
        Debug.Log("[ShopManager] - LoadShop() was called");
        loadingShop = true;
    }

    /// <summary>
    /// End the day at the shop
    /// </summary>
    private void EndShopDay()
    {
        Debug.Log("[ShopManager] - EndShopDay() Called");

        shopState = ShopState.ShopEnd;
        leverClick.canClickLever = true;
    }

    /// <summary>
    /// When a given section of dialogue is complete, spawn the next character
    /// </summary>
    private void OnDialogueComplete()
    {
        SpawnNextCustomer(DayManager.Instance.GetCurrentDay());
    }

    /// <summary>
    /// Handle what needs to happen when the player wants to refuse service 
    /// </summary>
    private void HandleRefuseService()
    {
        Debug.Log("[ShopManager] - HandleRefuseService() Called");
        shopState = ShopState.Refusing;
        OnDialogueComplete();
    }

    /// <summary>
    /// Handle what needs to happen when the player is ready to sell a plant
    /// </summary>
    private void HandleSellPlant()
    {
        shopState = ShopState.SellingPlant;
        plantManager.canSell = true;
    }

    private void HandlePlantSold(Plant plantToSell)
    {
        Debug.Log("[ShopManager] - HandlePlantSold() Called");

        // Increase the player's money 
        PlayerMoneyManager.Instance.GainMoney(plantToSell.plantPrice);

        // TODO: Hide the character and the plant 

        // Dialogue is complete 
        OnDialogueComplete();
        GameObject plant = plantToSell.gameObject;
        plant.SetActive(false);
        plantManager.canSell = false;
    }

    /// <summary>
    /// Handle what happens when the player clicks on the lever
    /// </summary>
    private void HandleLeverClick()
    {
        leverClick.canClickLever = false;
        // Close the grate 
        grateMover.CloseGrate();
        // TODO: Animate the lever closing 

        // TODO: End the day 

        OnShopDayOver?.Invoke();
    }

    /// <summary>
    /// Handles what needs to happen when the player clicks the main menu button
    /// </summary>
    public void BackToMainMenu()
    {
        Debug.Log("[ShopManager] - BackToMainMenu() Called");
        // Fire event to the GameManager 
        OnBackToMainMenu?.Invoke();
    }

    private void HandleCloseTutorial()
    {
        gameCanvas.SetActive(true);
        StartShopDay(DayManager.Instance.GetCurrentDay());
    }
}
