using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private static PauseMenu _pauseMenu;
    
    public static PauseMenu PauseMenuInstance
    {
        get => _pauseMenu;
        set => _pauseMenu = value;
    }

    private void Awake()
    {
        _pauseMenu = this;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

      public void ExitGame()
    {
        Application.Quit();
    }
}
