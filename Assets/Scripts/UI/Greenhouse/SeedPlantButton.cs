using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SeedPlantButton : MonoBehaviour
{
    public event Action<Plant_Data> Clicked;

    [SerializeField] TMP_Text plantLabel;
    [SerializeField] Image plantImage;
    [SerializeField] TMP_Text seedCountLabel;
    private Button buttonComp;

    private Plant_Data associatedPlant;

    private void Awake()
    {
        buttonComp = GetComponent<Button>();
        buttonComp.onClick.AddListener(delegate { Clicked?.Invoke(associatedPlant); });
    }

    public void SetPlant(Plant_Data plant, int seedCount)
    {
        associatedPlant = plant;
        plantLabel.text = $"{plant.plantName} Seed";
        plantImage.sprite = plant.plantSprite;
        SetSeedCount(seedCount);
    }

    public void SetSeedCount(int seedCount)
    {
        if (seedCount < 0)
            seedCount = 0;
        seedCountLabel.text = $"x{seedCount}";
        if (seedCount == 0)
            Disable();
        else
            Enable();
    }

    private void Enable()
    {
        plantImage.color = Color.white;
        buttonComp.interactable = true;
    }

    private void Disable()
    {
        plantImage.color = Color.black;
        buttonComp.interactable = false;
    }
}