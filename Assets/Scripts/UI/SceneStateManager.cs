using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStateManager : MonoBehaviour
{
    public GameObject credits;

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void toggleCredits(){
        Debug.Log("ToggleCredits");
        credits.SetActive( !credits.activeSelf);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
