using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadByName(string sceneName)
    {
        string gameManagerState; 

        if (sceneName == "MainMenu")
        {
            gameManagerState = "StartMenu"; 
        }
        else if (sceneName == "Undergrowth")
        {
            gameManagerState = "DayStart";
        }
        else
        {
            gameManagerState = sceneName;
        }

        SceneManager.LoadScene(sceneName);
        //GameManager.Instance.SwitchGameState(gameManagerState);
    }
}
