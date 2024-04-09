using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    
    public GameObject pauseMenuUI;
    public GameObject controlsPanel;
    public AimStateManager aimStateManager;
    public SimpleMovement simpleMovementScript;

    private void Start()
    {
        GameObject aimStateManagerObject = GameObject.Find("player_character_BL_rigged");
        if (aimStateManagerObject != null)
        {
            aimStateManager = aimStateManagerObject.GetComponent<AimStateManager>();
        }
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            simpleMovementScript = player.GetComponent<SimpleMovement>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Pause"))
        {
            if (GameIsPaused){ //checks to see if game is paused or not
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Resume();
            }else{
                Pause();
                controlsPanel.SetActive(false);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
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
        if (aimStateManager != null){
            aimStateManager.enabled = true;
        }
        if (simpleMovementScript != null){
            simpleMovementScript.enabled = true;
        }
            
    }

    void Pause(){
        pauseMenuUI.SetActive(true); //turns on canvas
        Time.timeScale = 0f; //stops time
        GameIsPaused = true; //lets script know that game is paused
        if (aimStateManager != null){
            aimStateManager.enabled = false;
        }

        if (simpleMovementScript != null){
            simpleMovementScript.enabled = false;
        }
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