using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the Greenhouse Scene and UI. Holds a copy of the GreenhouseData and updates it based on player actions.
/// </summary>
public class GreenhouseManager : MonoBehaviour
{
    [SerializeField] SeedPlantMenu seedMenu;
    [SerializeField] List<PlanterBox> planters;

    [SerializeField] GreenhouseData greenhouseData;
    [SerializeField] InventoryData inventory;
    [SerializeField] List<Plant_Data> testPlants;

    private List<SeedPlot> plots;
    private int selectedPlotIndex;

    void Start()
    {
        plots = new();
        foreach(PlanterBox planter in planters)
        {
            plots.AddRange(planter.seedPlots);
        }

        greenhouseData.SetPlantCapacity(plots.Count);
        for (int i = 0; i < plots.Count; i++)
        {
            int index = i;
            plots[i].ButtonClicked += delegate { OnPlotClicked(index); };
            plots[i].SetPlant(greenhouseData.GetPlant(i));
        }
        seedMenu.SeedButtonClicked += PlantSeed;
        inventory = new InventoryData();
        foreach(Plant_Data plant in testPlants)
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
            plots[i].SetPlant(greenhouseData.GetPlant(i));
        }
    }

    private void OnPlotClicked(int index)
    {
        Debug.Log($"Plot {index} clicked");
        //TODO: Add functionality for interacting with planted seeds
        selectedPlotIndex = index;
        seedMenu.SetSeedData(inventory);
        seedMenu.Open();
    }

    private void PlantSeed(Plant_Data seedType)
    {
        GreenhousePlant plantInstance = new GreenhousePlant(seedType);
        inventory.seedCounts[seedType]--;
        greenhouseData.SetPlant(selectedPlotIndex, plantInstance);
        plots[selectedPlotIndex].SetPlant(plantInstance);
        seedMenu.Close();
    }
}
