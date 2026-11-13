using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    // An instance of this 
    private static DayManager _instance; 
    public static DayManager Instance
    {
        get
        {
            // If the instance is null, find it in the scene 
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<DayManager>();

                // If it still doesn't exist, make one
                if (_instance == null)
                {
                    GameObject gameObject = new GameObject("DayManager (Automatically-Generated");
                    _instance = gameObject.AddComponent<DayManager>();
                }
            }
            return _instance;
        }
    }

    //public Day day;

    public int currentDay;
    public GameObject dayTrackerUI;
    public TextMeshProUGUI dayTrackerText;
    public string trackerText;

    private void Awake()
    {
        // Setting up the singleton 
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }
    private void Start()
    {
        trackerText = "Day " + currentDay;
    }

    private void Update()
    {
        if ( dayTrackerText != null)
        {
            dayTrackerText.text = trackerText;
        }
    }

    public void SetDay(int _day)
    {
        currentDay = _day;
    }

    public void ShowDayTracker()
    {
        dayTrackerUI = gameObject;
        dayTrackerText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        dayTrackerText.text = trackerText;
        dayTrackerUI.SetActive(true);
    }

    public void HideDayTracker()
    {
        dayTrackerUI.SetActive(false);
    }

    public void AdvanceDay()
    {
        currentDay++;
        Debug.Log("[DayManager] - AdvanceDay() Called. Day is now: " + currentDay);
        trackerText = "Day " + currentDay;
    }

    public int GetCurrentDay()
    {
        return currentDay;
    }
}
