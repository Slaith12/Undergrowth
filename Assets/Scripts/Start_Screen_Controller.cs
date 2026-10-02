using UnityEngine;
using UnityEngine.UI;

public class Start_Screen_Controller : MonoBehaviour
{
    [SerializeField] private Button startButton;


    private void Awake()
    {
        startButton.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartGame()
    {
        SceneController.instance.LoadScene("Game");
    }

    private void OnDisable()
    {
        startButton.onClick.RemoveListener(StartGame);
    }
}
