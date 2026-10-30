using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Plant : MonoBehaviour
{
    [SerializeField] private Plant_Data plant_data;
    public string plantName;
    public string plantInfo;
    public float plantPrice;
    private Sprite plantSprite; 
    private SpriteRenderer plantRenderer;

    private GameObject plantInfoUI; 


    private Camera mainCamera;

    private void Awake()
    {
        // Set the camera 
        mainCamera = Camera.main;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Assign all of the information 
        plantName = plant_data.plantName;
        plantInfo = plant_data.plantDescription; 
        plantSprite = plant_data.plantSprite;
        plantPrice = plant_data.plantPrice;

        // Get the image 
        plantRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        plantRenderer.sprite = plantSprite;

        // Get the UI Canvas
        // plantInfoUI = gameObject.GetComponentInChildren<Canvas>().gameObject; 
        // Hide the UI canvas 
        // plantInfoUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowInformation()
    {
        //Debug.Log("Information: " + plantName);
        plantInfoUI.SetActive(true);
    }

    public void HideInformation()
    {
        //Debug.Log("Information hid");
        plantInfoUI.SetActive(false);
    }
}
