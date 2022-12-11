using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStateManager : MonoBehaviour
{
    public GameObject credits;
    public GameObject tutorial;

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void toggleCredits(){
         tutorial.SetActive( false);
        credits.SetActive( !credits.activeSelf);
    }

    public void toggleTutorial(){
        credits.SetActive( false);
        tutorial.SetActive( !tutorial.activeSelf);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
