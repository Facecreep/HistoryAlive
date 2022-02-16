using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static string selectedCitySceneName;
    public string previousSceneName;
    
    public void LoadScene(string name)
    {
        if (name == "Moscow_Character_Screen" || name == "Dubna_Character_Screen")
            selectedCitySceneName = name;
        
        SceneManager.LoadScene(name);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            LoadPreviousScene();
    }

    public void LoadPreviousScene()
    {
        switch (previousSceneName)
        {
            case "City_Choise":
                LoadScene(selectedCitySceneName);
                break;
            case "":
                Application.Quit();
                break;
            default:
                LoadScene(previousSceneName);
                break;
        }
    }
}
