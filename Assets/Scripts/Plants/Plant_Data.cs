using UnityEngine;

[CreateAssetMenu(fileName = "Plant_Data", menuName = "Scriptable Objects/Plant_Data")]
public class Plant_Data : ScriptableObject
{
    public float price = 15;
    public string name;
    public string scientificName; 
    public string seedName;
    public int growthTime = 4;
    public Sprite plantSprite;
    public string size;
    public string growthSpeed;
    public string sunlightNeeds;
    public string waterNeeds;
    public string fertilizerNeeds;
    public string healthRisks;
    public string soilPreference; 
    public string plantFlavorText;
    [Multiline] public string plantSeedDescription;
}
