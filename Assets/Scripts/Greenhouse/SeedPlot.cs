using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SeedPlot : MonoBehaviour
{
    /// <summary>
    /// Indicates that this plot's button was clicked.
    /// </summary>
    public event Action ButtonClicked;

    [SerializeField] Button plotButton;
    //TODO: Consider making a dedicated GrowthIndicator component
    [SerializeField] GameObject growthIndicator;
    [SerializeField] TMP_Text growthLabel;
    [SerializeField] Slider growthSlider;
    [SerializeField] SpriteRenderer plantSprite;

    public void SetPlant(GreenhousePlant plant)
    {
        //mostly placeholder code just to get the bare minimum working
        if(plant == null)
        {
            plantSprite.gameObject.SetActive(false);
            growthIndicator.SetActive(false);
            plotButton.gameObject.SetActive(true);
            return;
        }
        plantSprite.sprite = plant.plantType.plantSprite;
        plantSprite.gameObject.SetActive(true);
        growthSlider.value = plant.age;
        growthSlider.maxValue = plant.plantType.growthTime;
        growthLabel.text = plant.age >= plant.plantType.growthTime ? "Click to Harvest!" : $"Ready in {plant.plantType.growthTime - plant.age} days.";
        growthIndicator.SetActive(true);
        plotButton.gameObject.SetActive(false);

    }

    /// <summary>
    /// Callback for seed plot button
    /// </summary>
    public void OnButtonClick()
    {
        ButtonClicked?.Invoke();
    }
}
