using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Handles the Greenhouse Scene and UI. Holds a copy of the GreenhouseData and updates it based on player actions.
/// </summary>
public class GreenhouseManager : MonoBehaviour
{
    private static GreenhouseManager _instance; 
    public static GreenhouseManager Instance
    {
        get
        {
            // If the instance is null, find it in the scene 
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<GreenhouseManager>();

                // If it still doesn't exist, make one
                if (_instance == null)
                {
                    GameObject gameObject = new GameObject("Greenhouse (Automatically-Generated");
                    _instance = gameObject.AddComponent<GreenhouseManager>();
                }
            }
            return _instance;
        }
    }

    [SerializeField] SeedPlantMenu seedMenu;
    [SerializeField] List<PlanterBox> planters;

    [SerializeField] public GreenhouseData greenhouseData;
    [SerializeField] InventoryData inventory;
    [SerializeField] List<Plant_Data> testPlants;

    private List<SeedPlot> plots;
    private int selectedPlotIndex;

    public static event Action OnGoToCounter;
    public static event Action<GreenhouseData> OnEndGreenhouseDay;

    private void Awake()
    {
        // Setting up the singleton 
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }

    //void Start()
    //{
    //    plots = new();
    //    foreach(PlanterBox planter in planters)
    //    {
    //        plots.AddRange(planter.seedPlots);
    //    }

    //    greenhouseData.SetPlantCapacity(plots.Count);
    //    for (int i = 0; i < plots.Count; i++)
    //    {
    //        int index = i;
    //        plots[i].ButtonClicked += delegate { OnPlotClicked(index); };
    //        plots[i].PlantClicked += delegate { OnPlotHarvest(index); };
    //        plots[i].SetPlant(greenhouseData.GetPlant(i));
    //    }
    //    seedMenu.SeedButtonClicked += PlantSeed;
    //    inventory = new InventoryData();
    //    foreach(Plant_Data plant in testPlants)
    //    {
    //        inventory.seedCounts.Add(plant, 10);
    //    }
    //    seedMenu.SetSeedData(inventory);
    //    seedMenu.Close();
    //}

    public void SetUpGreenhouse()
    {
        plots = new();
        foreach (PlanterBox planter in planters)
        {
            plots.AddRange(planter.seedPlots);
        }

        greenhouseData.SetPlantCapacity(plots.Count);
        for (int i = 0; i < plots.Count; i++)
        {
            int index = i;
            plots[i].ButtonClicked += delegate { OnPlotClicked(index); };
            plots[i].PlantClicked += delegate { OnPlotHarvest(index); };
            plots[i].SetPlant(greenhouseData.GetPlant(i));
        }
        seedMenu.SeedButtonClicked += PlantSeed;
        inventory = new InventoryData();
        foreach (Plant_Data plant in testPlants)
        {
            inventory.seedCounts.Add(plant, 10);
        }
        seedMenu.SetSeedData(inventory);
        seedMenu.Close();
    }

    /// <summary>
    /// Debug function to advance a day within the greenhouse.
    /// Calls GreenhouseData.AdvanceTime() and refreshes the view.
    /// </summary>
    public void AdvanceDay()
    {
        greenhouseData.AdvanceTime();
        for(int i = 0; i < plots.Count; i++)
        {
            if (greenhouseData.GetPlant(i) != null) {
                plots[i].SetPlant(greenhouseData.GetPlant(i));
            }
            else
            {
                Debug.Log("There is no plant in the greenhouse at index: " + i);    
            }
        }
    }

    private void OnPlotClicked(int index)
    {
        //TODO: Add functionality for interacting with planted seeds
        selectedPlotIndex = index;
        seedMenu.SetSeedData(inventory);
        seedMenu.Open();
    }

    private void OnPlotHarvest(int index)
    {
        Plant_Data plantType = greenhouseData.GetPlant(index).plantType;
        if(!inventory.plantCounts.ContainsKey(plantType))
        {
            inventory.plantCounts[plantType] = 1;
        }
        else
        {
            inventory.plantCounts[plantType]++;
        }
        greenhouseData.SetPlant(index, null);
        plots[index].SetPlant(null);
    }

    private void PlantSeed(Plant_Data seedType)
    {
        GreenhousePlant plantInstance = new GreenhousePlant(seedType);
        inventory.seedCounts[seedType]--;
        greenhouseData.SetPlant(selectedPlotIndex, plantInstance);
        plots[selectedPlotIndex].SetPlant(plantInstance);
        seedMenu.Close();
    }

    public void GoToCounter()
    {
        OnGoToCounter?.Invoke();
    }

    public void EndGreenhouseDay()
    {
        OnEndGreenhouseDay?.Invoke(greenhouseData);
    }
}

