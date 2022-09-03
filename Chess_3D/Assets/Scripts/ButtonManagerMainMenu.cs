using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManagerMainMenu : MonoBehaviour
{

    SceneChanger sceneChanger;

    // void Awake() 
    // {
    //     GameObject[] buttonManagers = GameObject.FindGameObjectsWithTag("ButtonManager");

    //     if (buttonManagers.Length > 1)
    //     {
    //         Destroy(this.gameObject);
    //     }

    //     DontDestroyOnLoad(this.gameObject);    
    // }
    
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        string sceneName = currentScene.name;

        sceneChanger = GameObject.Find("SceneChanger").GetComponent<SceneChanger>();
    }

    // void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     if(scene.name == "MainMenu")
    //     {
    //         if(GameObject.Find("Button01QuickStart"))
    //         {
    //             Button button01 = GameObject.Find("Button01QuickStart").GetComponent<Button>();
    //             button01.onClick.AddListener(StartNormalPVP);
    //         }

    //         if(GameObject.Find("Button02SetupCustom"))
    //         {
    //             Button button02 = GameObject.Find("Button02SetupCustom").GetComponent<Button>();
    //             button02.onClick.AddListener(SetupCustomPVP);
    //         }

    //         if(GameObject.Find("Button03Options"))
    //         {
    //             Button button03 = GameObject.Find("Button03Options").GetComponent<Button>();
    //             button03.onClick.AddListener(Options);
    //         }

    //         if(GameObject.Find("Button04ExitGame"))
    //         {
    //             Button button04 = GameObject.Find("Button04ExitGame").GetComponent<Button>();
    //             button04.onClick.AddListener(ExitGame);
    //         }
    //     }
    //     else if(scene.name == "Game")
    //     {
    //         if(GameObject.Find("Button01Restart"))
    //         {
    //             Button button01 = GameObject.Find("Button01Restart").GetComponent<Button>();
    //             button01.onClick.AddListener(StartNormalPVP);
    //         }

    //         if(GameObject.Find("Button02MainMenu"))
    //         {
    //             Button button02 = GameObject.Find("Button02MainMenu").GetComponent<Button>();
    //             button02.onClick.AddListener(LoadMainMenu);
    //         }
    //     }
    //     else if(scene.name == "CustomGame")
    //     {
    //         if(GameObject.Find("Button01MainMenu"))
    //         {
    //             Button button01 = GameObject.Find("Button01MainMenu").GetComponent<Button>();
    //             button01.onClick.AddListener(LoadMainMenu);
    //         }
    //     }
    //     else if(scene.name == "Options")
    //     {
    //         if(GameObject.Find("Button01MainMenu"))
    //         {
    //             Button button01 = GameObject.Find("Button01MainMenu").GetComponent<Button>();
    //             button01.onClick.AddListener(LoadMainMenu);
    //         }
    //     }
    // }

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
