using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip buttonSound;
    public AudioSource audioSource;

    // This function will make it so if you press the play button, you will be moved to the next scene (game) 
    public void PlayButton()
    {
        StartCoroutine(PlayAndLoad("NicTest3"));
    }

    public void Controls()
    {
        StartCoroutine(PlayAndLoad("Controls"));
    }

    public void GoBackToMain()
    {
        StartCoroutine(PlayAndLoad("Main Menu"));
    }

    public void CreditScene()
    {
        StartCoroutine(PlayAndLoad("Credits"));
    }

    public void Level1()
    {
        StartCoroutine(PlayAndLoad("Level 1"));
    }

    // This function will make it so if you press the quit button, the application will close
    public void QuitButton()
    {
        StartCoroutine(PlayAndQuit());
    }

    private IEnumerator PlayAndLoad(string Scene){
        PlayButtonSound();
        yield return new WaitForSecondsRealtime(buttonSound.length);
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(Scene);
    }

    private IEnumerator PlayAndQuit(){
        PlayButtonSound();
        yield return new WaitForSecondsRealtime(buttonSound.length);
        Application.Quit();
    }

    private void PlayButtonSound(){
        if(audioSource != null && buttonSound != null){
            audioSource.PlayOneShot(buttonSound);
        }
    }
}
