using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject creditsObject;

    public void LoadScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName, LoadSceneMode.Single);
    }

    public void DisableCredits()
    {
        creditsObject.SetActive(false);
    }

    public void EnableCredits()
    {
        creditsObject.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
