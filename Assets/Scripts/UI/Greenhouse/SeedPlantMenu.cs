using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SeedPlantMenu : MonoBehaviour
{
    public event Action<Plant_Data> SeedButtonClicked;

    [SerializeField] SeedPlantButton seedButtonPrefab;
    [SerializeField] RectTransform seedGrid;
    [SerializeField] PlantInfoDisplay seedDisplay;

    private List<SeedPlantButton> buttons;

    private void Awake()
    {
        buttons = new();

        //Clear seed grid so that only tracked buttons will be there
        for(int i = seedGrid.childCount - 1; i >= 0; i--)
        {
            Destroy(seedGrid.GetChild(i).gameObject);
        }
    }

    public void Open()
    {
        seedDisplay.Clear("Choose a Seed");
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void SetSeedData(InventoryData data)
    {
        SetNumSeedButtons(data.seedCounts.Count);
        var seedList = data.seedCounts.ToList();
        for(int i = 0; i < buttons.Count; i++)
        {
            buttons[i].SetPlant(seedList[i].Key, seedList[i].Value);
        }
    }

    private void SetNumSeedButtons(int num)
    {
        if (num > buttons.Count)
        {
            //add extra buttons as needed
            for (int i = buttons.Count; i < num; i++)
            {
                SeedPlantButton newButton = Instantiate(seedButtonPrefab, seedGrid);
                newButton.Clicked += (plant) => SeedButtonClicked?.Invoke(plant);
                newButton.Hovered += seedDisplay.DisplaySeed;
                buttons.Add(newButton);
            }
        }
        else if (num < buttons.Count)
        {
            //remove excess buttons as needed
            for (int i = num; i < buttons.Count; i++)
            {
                Destroy(buttons[i].gameObject);
            }
            buttons.RemoveRange(num, buttons.Count - num);
        }
    }
}