using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds the data of all the plants in the greenhouse. Also resolves the tick updates for all plants within the greenhouse.
/// Planned to be carried by either a singleton or a ScriptableObject.
/// </summary>
[Serializable]
public class GreenhouseData
{
    [SerializeField] List<GreenhousePlant> plants;

    public GreenhouseData()
    {
        plants = new();
    }

    public void AdvanceTime(int ticks = 1)
    {
        foreach(GreenhousePlant plant in plants)
        {
            if (plant != null)
                plant.age += ticks;
        }
    }

    public void SetPlant(int index, GreenhousePlant plant)
    {
        plants[index] = plant;
    }

    public GreenhousePlant GetPlant(int index)
    {
        return plants[index];
    }

    public List<GreenhousePlant> GetPlantList()
    {
        return plants;
    }

    public GreenhousePlant RemovePlant(int index)
    {
        GreenhousePlant plant = plants[index];
        plants[index] = null;
        return plant;
    }

    public void SetPlantCapacity(int capacity)
    {
        while(plants.Count < capacity)
        {
            plants.Add(null);
        }
        if (plants.Count > capacity)
        {
            plants.RemoveRange(capacity, plants.Count - capacity);
        }
    }
}