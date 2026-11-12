using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    // An instance of this 
    public static DayManager Instance { get; private set; }

    [SerializeField] private Day day;
    public GameObject dayTrackerUI;
    public TextMeshProUGUI dayTrackerText;

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
        dayTrackerUI = gameObject;
        dayTrackerText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        string trackerText = day.runtimeValue.ToString();
        dayTrackerText.text = trackerText;
    }

    public void SetDay(int _day)
    {
        day.runtimeValue = _day;
    }

    public void AdvanceDay()
    {
        day.AdvanceDay();
    }

    public int GetCurrentDay()
    {
        return day.runtimeValue;
    }
}
