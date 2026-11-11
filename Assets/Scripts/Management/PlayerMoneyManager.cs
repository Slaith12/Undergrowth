using TMPro;
using UnityEngine;

public class PlayerMoneyManager : MonoBehaviour
{
    // An instance of this 
    public static PlayerMoneyManager Instance { get; private set; }

    [SerializeField] private PlayerMoney playerMoney;
    public GameObject moneyTrackerUI;
    public TextMeshProUGUI moneyTrackerText;

    private void Awake()
    {
        // Setting up the singleton 
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


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

    public float GetMoney()
    {
        return playerMoney.runtimeValue; 
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
