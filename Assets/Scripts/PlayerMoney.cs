using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMoney", menuName = "Scriptable Objects/PlayerMoney")]
public class PlayerMoney : ScriptableObject
{
    public float initialValue = 0;
    public float runtimeValue;

    private void OnEnable()
    {
        // Reset to the initial value 
        runtimeValue = initialValue;
    }

    public void GainMoney(float value)
    {
        runtimeValue += value;
    }

    public void LoseMoney(float value)
    {
        runtimeValue -= value;
    }
}
