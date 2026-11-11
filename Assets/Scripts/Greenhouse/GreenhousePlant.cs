using System;
using UnityEngine;

[Serializable]
public class GreenhousePlant
{
    public Plant_Data plantType;
    public int age;

    public GreenhousePlant(Plant_Data type, int age = 0)
    {
        this.plantType = type;
        this.age = age;
    }
}
