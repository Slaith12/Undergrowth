using System;
using UnityEngine;
using UnityEngine.UI;

public class SeedPlot : MonoBehaviour
{
    /// <summary>
    /// Indicates that this plot's button was clicked.
    /// </summary>
    public event Action ButtonClicked;

    [SerializeField] Button plotButton;
    [SerializeField] SpriteRenderer plantSprite;

    public void SetPlant(GreenhousePlant plant)
    {
        //mostly placeholder code just to get the bare minimum working
        if(plant == null)
        {
            plantSprite.gameObject.SetActive(false);
            plotButton.gameObject.SetActive(true);
            return;
        }
        plantSprite.sprite = plant.plantType.plantSprite;
        plantSprite.gameObject.SetActive(true);
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
