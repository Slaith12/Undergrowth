using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlantInfoDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text plantLabel;
    [SerializeField] Image plantImage;
    [SerializeField] TMP_Text plantDesc;

    public void Clear(string labelText = "")
    {
        plantLabel.text = labelText;
        plantImage.enabled = false;
        plantDesc.text = "";
    }

    public void DisplayPlant(Plant_Data plantType)
    {
        plantLabel.text = plantType.plantName;
        plantImage.enabled = true;
        plantImage.sprite = plantType.plantSprite;
        plantDesc.text = plantType.plantDescription;
    }

    public void DisplaySeed(Plant_Data seedType)
    {
        //For now, have the seed info equal the plant info, but with the seed name instead.
        //In the future, we can decide if we want to show different info for plants vs seeds.
        DisplayPlant(seedType);
        plantLabel.text = seedType.seedName;
    }
}
