using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class uiCamTrigger : MonoBehaviour
{
    public GameObject uiText;
    public string loadScene;
    public AudioClip buttonSound;
    public AudioSource audioSource;
    public GameObject loadingScreen;

    private bool uiActive = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided");
        if (other.CompareTag("MainCamera"))
        {
            uiText.SetActive(true);
            uiActive = true;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            StartCoroutine(PlayAndLoadScene(loadScene));
        }

        if (uiActive && (Input.anyKeyDown || Input.GetButtonDown("Submit")))
        {
            StartCoroutine(PlayAndLoadScene(loadScene));
        }
    }

    private IEnumerator PlayAndLoadScene(string sceneName)
    {
        PlayButtonSound();
        loadingScreen.SetActive(true);
        yield return new WaitForSecondsRealtime(1.25f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    private void PlayButtonSound()
    {
        if (audioSource != null && buttonSound != null)
        {
            audioSource.PlayOneShot(buttonSound);
        }
    }
}
