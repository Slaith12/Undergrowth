using System;
using System.Collections.Generic;

[Serializable]
public class InventoryData
{
    public Dictionary<Plant_Data, int> plantCounts;
    public Dictionary<Plant_Data, int> seedCounts;

    public InventoryData()
    {
        plantCounts = new();
        seedCounts = new();
    }
}