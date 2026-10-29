using UnityEngine;

[CreateAssetMenu(fileName = "Plant_Data", menuName = "Scriptable Objects/Plant_Data")]
public class Plant_Data : ScriptableObject
{
    public string plantName;
    public string seedName;
    public int plantPrice = 15;
    public Sprite plantSprite;
    [Multiline] public string plantDescription;
}
