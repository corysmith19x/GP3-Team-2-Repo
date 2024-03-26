using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    
    public GameObject pauseMenuUI;
    public GameObject controlsPanel;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameIsPaused){ //checks to see if game is paused or not
                Resume();
            }else{
                Pause();
                controlsPanel.SetActive(false);
            }
        }

        if(GameIsPaused == true)
        {
            
        }
    }

    public void Resume(){
        pauseMenuUI.SetActive(false); //turns off canvas
        Time.timeScale = 1f; //continues in-game time
        GameIsPaused = false; //lets script know game is not paused
    }

    void Pause(){
        pauseMenuUI.SetActive(true); //turns on canvas
        Time.timeScale = 0f; //stops time
        GameIsPaused = true; //lets script know that game is paused
    }

    public void Controls()
    {
        controlsPanel.SetActive(true);
    }

    public void exitControls()
    {
        controlsPanel.SetActive(false);
    }
}