using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    void Awake() 
    {
        GameObject[] sceneManagers = GameObject.FindGameObjectsWithTag("SceneChanger");

        if (sceneManagers.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);    
    }

    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadCustomGame()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadOptions()
    {
        SceneManager.LoadScene(3);
    }
}
