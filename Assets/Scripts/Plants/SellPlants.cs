using System;
using UnityEngine;

public class SellPlants : MonoBehaviour
{
    public event Action<GameObject> OnPlantPlacedOnMat;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Plant currentPlant = other.gameObject.GetComponent<Plant>();
        // If what triggered this collider is a plant 
        if (currentPlant != null)
        {
            // Place the plant on the mat 
            OnPlantPlacedOnMat.Invoke(currentPlant.gameObject);
        }
    }
}