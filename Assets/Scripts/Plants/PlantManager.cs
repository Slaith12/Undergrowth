using System;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public bool canSell = false;
    public event Action PlantSold;
    public void SellPlant(GameObject gameObject)
    {

        if (canSell)
        {
            // Move the plant 
            gameObject.transform.localPosition = new Vector3(-1.361f, -14.72f, 340f);
            canSell = false;
            PlantSold.Invoke();
        }
    }
}
