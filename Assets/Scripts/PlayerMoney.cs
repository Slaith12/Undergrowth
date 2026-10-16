using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMoney", menuName = "Scriptable Objects/PlayerMoney")]
public class PlayerMoney : ScriptableObject
{
    public int initialValue = 0;
    public int runtimeValue;

    private void OnEnable()
    {
        // Reset to the initial value 
        runtimeValue = initialValue;
    }

    public void GainMoney(int value)
    {
        runtimeValue += value;
    }

    public void LoseMoney(int value)
    {
        runtimeValue -= value;
    }
}
