using System;
using System.Collections.Generic;
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
    private GameObject currentPlant;

    [SerializeField] private GameObject sellPlantButton;

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

        // If the player can sell plants 
        if (canSell)
        {
            currentPlant = CheckOverPlant(mouseScreenPosition);

            // If the mouse is over a plant 
            if (currentPlant != null)
            {
                // If the plant can be dragged 
                if (canDrag)
                {
                    // If the mouse button was clicked this frame
                    if (Mouse.current.leftButton.wasPressedThisFrame)
                    {
                        StartDrag(mouseScreenPosition);
                    }

                    // If the plant is being dragged, continue the drag 
                    if (isDraggingPlant)
                    {
                        ContinueDrag(mouseScreenPosition);
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
        }
        else
        {
            isDraggingPlant = false;
        }
    }

    public GameObject CheckOverPlant(Vector2 screenPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(screenPosition);

        // If the ray is over a plant 
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, plantLayer))
        {
            return hitInfo.collider.gameObject;
        }
        else
        {
            return null;
        }
    }

    public void ShowPlantInformation(Plant plant)
    {
        // plant.ShowInformation();
    }

    public void HidePlantInformation(Plant plant)
    {
        // plant.HideInformation();
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
