using TMPro;
using UnityEngine;

public class PlayerMoneyManager : MonoBehaviour
{
    [SerializeField] private PlayerMoney playerMoney;
    public GameObject moneyTrackerUI;
    public TextMeshProUGUI moneyTrackerText; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moneyTrackerUI = gameObject; 
        moneyTrackerText = gameObject.GetComponentInChildren<TextMeshProUGUI>();    
    }

    // Update is called once per frame
    void Update()
    {
        string trackerText = "$" + playerMoney.runtimeValue.ToString();
        moneyTrackerText.text = trackerText;
    }

    public void GainMoney(float amount)
    {
        playerMoney.GainMoney(amount);
    }

    public void LoseMoney(float amount)
    {
        playerMoney.LoseMoney(amount);
    }

}
