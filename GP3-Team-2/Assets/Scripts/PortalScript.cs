using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    public GameObject player;
    public string sceneName;
    public GameObject loadingScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(LoadScene(sceneName));
        }
    }

    private IEnumerator LoadScene(string sceneName){
        loadingScreen.SetActive(true);
        Time.timeScale = 1f;
        yield return new WaitForSecondsRealtime(1.25f);
        SceneManager.LoadScene(sceneName);
        Debug.Log("player triggered");
    }
}
