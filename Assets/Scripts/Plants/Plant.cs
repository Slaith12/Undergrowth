using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Plant : MonoBehaviour
{
    [SerializeField] private Plant_Data plant_data;
    public string plantName;
    public string plantSeedInfo;
    public string plantInformation;
    public float plantPrice;
    public string plantSize;
    public string plantGrowthSpeed;
    public string plantSunlightNeeds; 
    public string plantWaterNeeds;
    public string plantFertilizerNeeds;
    public string plantHealthRisks;
    public string plantSoilPreference;
    public string plantFlavorText;
    public string plantScientificName; 





    private Sprite plantSprite; 
    private SpriteRenderer plantRenderer;






    private GameObject plantInfoUI; 


    private Camera mainCamera;

    private void Awake()
    {
        // Set the camera 
        mainCamera = Camera.main;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Assign all of the information 
        plantName = plant_data.name;
        plantSeedInfo = plant_data.plantSeedDescription; 
        plantInformation = plant_data.plantFlavorText;
        plantPrice = plant_data.price; 
        plantSize = plant_data.size;
        plantGrowthSpeed = plant_data.growthSpeed;
        plantSunlightNeeds = plant_data.sunlightNeeds;
        plantWaterNeeds = plant_data.waterNeeds;
        plantFertilizerNeeds = plant_data.fertilizerNeeds;
        plantHealthRisks = plant_data.healthRisks;
        plantSoilPreference = plant_data.soilPreference;
        plantFlavorText = plant_data.plantFlavorText;
        plantScientificName = plant_data.scientificName;
        plantSprite = plant_data.plantSprite;

        // Get the image 
        plantRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        plantRenderer.sprite = plantSprite;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
