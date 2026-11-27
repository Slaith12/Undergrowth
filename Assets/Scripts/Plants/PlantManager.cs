using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlantManager : MonoBehaviour
{
    public bool canSell = false;
    public event Action<Plant> PlantSold;
    public SellPlants sellCounter;

    [SerializeField] private LayerMask plantLayer;
    [SerializeField] private Vector3 onMatPosition = new Vector3(0, 3.336f, -8.412f);
    public bool isDraggingPlant = false;
    private float initialZDistance;
    private Vector3 dragOffset;
    public bool canDrag = true;

    private Plant plantToSell;
    private Plant currentPlant;



    [SerializeField] private GameObject sellPlantButton;
    [SerializeField] private GameObject plantInformationPanel;


    [SerializeField] private TextMeshProUGUI plantNameText;
    [SerializeField] private TextMeshProUGUI plantPriceText;
    [SerializeField] private TextMeshProUGUI plantSizeText;
    [SerializeField] private TextMeshProUGUI plantGrowthSpeedText;
    [SerializeField] private TextMeshProUGUI plantSunlightNeedsText;
    [SerializeField] private TextMeshProUGUI plantWaterNeedsText;
    [SerializeField] private TextMeshProUGUI plantFertilizerNeedsText;
    [SerializeField] private TextMeshProUGUI plantHealthRisksText;
    [SerializeField] private TextMeshProUGUI plantSoilPreferenceText;
    [SerializeField] private TextMeshProUGUI plantFlavorText;
    [SerializeField] private TextMeshProUGUI plantScientificName; 


    [SerializeField] private TextMeshProUGUI plantDescriptionText;

    private Camera mainCamera;
    private void Awake()
    {
        mainCamera = Camera.main;
        sellCounter.OnPlantPlacedOnMat += HandlePlantPlacedOnMat;
    }

    private void Update()
    {
        // Getting the position of the mouse
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();

        // Get the current plant 
        currentPlant = CheckOverPlant(mouseScreenPosition);

        // If the mouse is over a plant 
        if (currentPlant != null)
        {
            ShowPlantInformation(currentPlant);
            // If the player can sell plants 
            if (canSell)
            {
                // If the plant can be dragged 
                if (canDrag)
                {
                    // If the mouse button was clicked this frame
                    if (Mouse.current.leftButton.wasPressedThisFrame)
                    {
                        StartDrag(mouseScreenPosition);
                        // Hide the information screen 
                        HidePlantInformation();
                    }

                    // If the plant is being dragged, continue the drag 
                    if (isDraggingPlant)
                    {
                        ContinueDrag(mouseScreenPosition);
                        HidePlantInformation();
                    }

                    // If the plant was being dragged, but the mouse button was released 
                    if (Mouse.current.leftButton.wasReleasedThisFrame && isDraggingPlant)
                    {
                        EndDrag();
                    }
                }
                else
                {
                    isDraggingPlant = false;
                }
            }
            else
            {
                isDraggingPlant = false;
            }
        }
        else
        {
            isDraggingPlant = false;
            HidePlantInformation();
        }
    }

    public Plant CheckOverPlant(Vector2 screenPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(screenPosition);

        // If the ray is over a plant 
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, plantLayer))
        {
            return hitInfo.collider.gameObject.GetComponent<Plant>();   
        }
        else
        {
            return null;
        }
    }

    public void ShowPlantInformation(Plant plant)
    {
        plantNameText.text = plant.plantName;
        plantPriceText.text = "$" + plant.plantPrice.ToString();
        plantPriceText.text = $"Price: <font=\"AlegreyaSans-Medium SDF\">{ plant.plantPrice.ToString()}";
        plantSizeText.text = $"Size: <font=\"AlegreyaSans-Medium SDF\">{plant.plantSize}";
        plantGrowthSpeedText.text = $"Growth Speed: <font=\"AlegreyaSans-Medium SDF\">{plant.plantGrowthSpeed}";
        plantSunlightNeedsText.text = $"Sunlight Needs: <font=\"AlegreyaSans-Medium SDF\">{plant.plantSunlightNeeds}";
        plantWaterNeedsText.text = $"Water Needs: <font=\"AlegreyaSans-Medium SDF\">{plant.plantWaterNeeds}";
        plantFertilizerNeedsText.text = $"Fertilizer Needs: <font=\"AlegreyaSans-Medium SDF\">{plant.plantFertilizerNeeds}";
        plantHealthRisksText.text = $"Health Risks: <font=\"AlegreyaSans-Medium SDF\">{plant.plantHealthRisks}";
        plantSoilPreferenceText.text = $"Soil Preference: <font=\"AlegreyaSans-Medium SDF\">{plant.plantSoilPreference}";
        plantFlavorText.text = plant.plantFlavorText;
        plantScientificName.text = plant.plantScientificName;

        // Activate the information panel 
        plantInformationPanel.gameObject.SetActive(true);
    }

    public void HidePlantInformation()
    {
        // Deactivate the information panel
        plantInformationPanel.gameObject.SetActive(false);
    }

    public void ShowSellButton()
    {
        sellPlantButton.SetActive(true);
    }

    public void HideSellButton()
    {
        sellPlantButton.SetActive(false);

    }

    public void SoldPlant()
    {
        // Hide the button 
        HideSellButton();
        canSell = false;
        PlantSold?.Invoke(plantToSell);
        canDrag = true;
    }


    private void HandlePlantPlacedOnMat(GameObject currentPlant)
    {
        canDrag = false;
        // Place the given plant on the mat 
        currentPlant.transform.localPosition = onMatPosition;
        plantToSell = currentPlant.GetComponent<Plant>();
        ShowSellButton();
        // Hide the information 
        HidePlantInformation();
    }


    private void StartDrag(Vector2 screenPosition)
    {
        isDraggingPlant = true;
        initialZDistance = mainCamera.WorldToScreenPoint(currentPlant.transform.position).z;
        Plant plant = currentPlant.GetComponent<Plant>();

        //Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        //Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(screenPosition);

        //if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, plantLayer))
        //{
        //    isDraggingPlant = true;
        //    initialZDistance = mainCamera.WorldToScreenPoint(test_plant.transform.position).z;
        //    Plant plant = hitInfo.collider.gameObject.GetComponent<Plant>();
        //    plant.HideInformation();
        //}
    }

    private void ContinueDrag(Vector2 screenPosition)
    {
        // Transfor screen coordinates back into 3d point 
        Vector3 mouseWorldSpace = mainCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, initialZDistance));

        // Update plant position 
        currentPlant.transform.position = mouseWorldSpace + dragOffset;
    }

    private void EndDrag()
    {
        isDraggingPlant = false;
    }
}
