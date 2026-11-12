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

    public Day day;
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
        day = ScriptableObject.CreateInstance<Day>();
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void SetDay(int _day)
    {
        day.runtimeValue = _day;
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
        //day.AdvanceDay();
        Debug.Log("[DayManager] - AdvanceDay() Called. Day is now: " + GetCurrentDay());
        trackerText = GetCurrentDay().ToString();
    }

    public int GetCurrentDay()
    {
        return day.runtimeValue;
    }
}
