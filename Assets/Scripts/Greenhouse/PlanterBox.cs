using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the seed plots within a single planter box.
/// The main reason this class exists is so that I don't have to assign 24 individual seed plot objects to the greenhouse manager script.
/// </summary>
public class PlanterBox : MonoBehaviour
{
    public List<SeedPlot> seedPlots;
}
