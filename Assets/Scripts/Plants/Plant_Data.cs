using UnityEngine;

[CreateAssetMenu(fileName = "Plant_Data", menuName = "Scriptable Objects/Plant_Data")]
public class Plant_Data : ScriptableObject
{
    public float plantPrice = 15;
    public string plantName;
    public string seedName;
    public int growthTime = 4;
    public Sprite plantSprite;
    [Multiline] public string plantDescription;
}
