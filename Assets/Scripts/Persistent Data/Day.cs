using UnityEngine;

[CreateAssetMenu(fileName = "Day", menuName = "Scriptable Objects/Day")]
public class Day : ScriptableObject
{
    public int initialValue = 0;
    public int runtimeValue;

    private void OnDisable()
    {
        runtimeValue = initialValue;
    }

    public void AdvanceDay()
    {
        runtimeValue++;
    }
}
