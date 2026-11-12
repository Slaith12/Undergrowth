using System;
using TMPro;
using UnityEngine;

public class DayOverManager : MonoBehaviour
{
    public event Action OnNextDay;
    public event Action OnQuit;

    [SerializeField] private TextMeshProUGUI currentDayText;
    [SerializeField] private TextMeshProUGUI currentMoneyText;

    public void LoadInformation(int currentDay, float currentMoney)
    {
        string moneyText = "You currently have $" + currentMoney;
        currentMoneyText.text = moneyText;
        string dayText = "You completed Day " + currentDay;
        currentDayText.text = dayText;

    }

    public void NextDay()
    {
        Debug.Log("[DayOverManager] - NextDay() Called");
        OnNextDay?.Invoke();
    }

    public void QuitGame()
    {
        Debug.Log("[DayOverManager] - QuitGame() Called");
        OnQuit?.Invoke();
    }

}
