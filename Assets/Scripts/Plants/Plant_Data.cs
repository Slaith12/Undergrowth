using UnityEngine;

[CreateAssetMenu(fileName = "Plant_Data", menuName = "Scriptable Objects/Plant_Data")]
public class Plant_Data : ScriptableObject
{
    public string plantName; 
    public float plantPrice = 15;
    public string plantInformation;
    public Sprite plantSprite;
}
