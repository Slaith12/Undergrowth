using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SeedPlot : MonoBehaviour
{
    /// <summary>
    /// Indicates that the empty plot's marker was clicked.
    /// </summary>
    public event Action ButtonClicked;
    /// <summary>
    /// Indicates that the plant was clicked to be harvested.
    /// </summary>
    public event Action PlantClicked;

    [SerializeField] Button plotButton;
    //TODO: Consider making a dedicated GrowthIndicator component
    [SerializeField] Button growthIndicator;
    [SerializeField] TMP_Text growthLabel;
    [SerializeField] Slider growthSlider;
    [SerializeField] SpriteRenderer plantSprite;

    public void SetPlant(GreenhousePlant plant)
    {
        //mostly placeholder code just to get the bare minimum working
        if(plant == null)
        {
            plantSprite.gameObject.SetActive(false);
            growthIndicator.gameObject.SetActive(false);
            plotButton.gameObject.SetActive(true);
            return;
        }
        else
        {
            Debug.Log("[SeedPlot] - SetPlant() plant is not null");

            if (plantSprite != null)
            {
                if (plant.plantType != null)
                {
                    if (plant.plantType.plantSprite != null)
                    {
                        plantSprite.sprite = plant.plantType.plantSprite;

                        bool harvestable = plant.age >= plant.plantType.growthTime;
                        growthSlider.value = plant.age;
                        growthSlider.maxValue = plant.plantType.growthTime;
                        growthLabel.text = harvestable ? "Harvest Ready!" : $"Ready in {plant.plantType.growthTime - plant.age} days.";
                        growthIndicator.interactable = harvestable;
                        growthIndicator.gameObject.SetActive(true);
                        plotButton.gameObject.SetActive(false);
                        plantSprite.gameObject.SetActive(true);
                    }
                    else
                    {
                        plantSprite.gameObject.SetActive(false);
                        growthIndicator.gameObject.SetActive(false);
                        plotButton.gameObject.SetActive(true);
                        Debug.LogWarning("[SeedPlot] - SetPlant plant.plantType.plantSprite is null!");
                        return;
                    }
                }
                else
                {
                    plantSprite.gameObject.SetActive(false);
                    growthIndicator.gameObject.SetActive(false);
                    plotButton.gameObject.SetActive(true);
                    Debug.LogWarning("[SeedPlot] - SetPlant() plant.plantType is null!");
                    return;
                }
            }
            else
            {
                plantSprite.gameObject.SetActive(false);
                growthIndicator.gameObject.SetActive(false);
                plotButton.gameObject.SetActive(true);
                Debug.LogWarning("[SeedPlot] - SetPlant() plantSprite is null!");
                return;
            }
        }
    }

    /// <summary>
    /// Callback for seed plot button
    /// </summary>
    public void OnButtonClick()
    {
        ButtonClicked?.Invoke();
    }

    public void OnPlantHarvest()
    {
        PlantClicked?.Invoke();
    }
}
