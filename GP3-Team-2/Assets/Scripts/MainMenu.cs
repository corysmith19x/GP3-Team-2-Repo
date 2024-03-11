using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // This function will make it so if you press the play button, you will be moved to the next scene (game) 
    public void PlayButton()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void GoBackToMain()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void CreditScene()
    {
        SceneManager.LoadScene("Credits");
    }

    // This function will make it so if you press the quit button, the application will close
    public void QuitButton()
    {
        Application.Quit();
    }
}
