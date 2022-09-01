using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManagerMainMenu : MonoBehaviour
{

    SceneChanger sceneChanger;

    void Awake() 
    {
        GameObject[] buttonManagers = GameObject.FindGameObjectsWithTag("ButtonManager");

        if (buttonManagers.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);    
    }
    
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        string sceneName = currentScene.name;

        sceneChanger = GameObject.Find("SceneChanger").GetComponent<SceneChanger>();
    }

    public void LoadMainMenu()
    {
        sceneChanger.LoadMainMenu();
        Debug.Log("main menu");
    }

    public void StartNormalPVP()
    {
        sceneChanger.LoadGame();
        Debug.Log("normal pvp");
    }

    public void SetupCustomPVP()
    {
        sceneChanger.LoadCustomGame();
        Debug.Log("custom pvp");
    }

    public void Options()
    {
        sceneChanger.LoadOptions();
        Debug.Log("options");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("exit game");
    }
}
