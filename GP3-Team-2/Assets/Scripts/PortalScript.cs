using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    public GameObject player;
    public string sceneName;
    public GameObject loadingScreen;
    public GameObject playerPortalEffect;


    public Material myMaterial;

    [Range(0f, 1f)]
    public float alpha = 0f;

    float minVal = 0f;
    float maxVal = 1f;
    float timeElapsed;
    public float lerpDuration = 2.5f;

    float lerpValue;

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
        AudioCallerScript portal = other.GetComponent<AudioCallerScript>();

        if (other.gameObject.CompareTag("Player"))
        {
            portal.PlaySoundOneShot(7);
            StartCoroutine (AlphaChange());
            StartCoroutine(LoadScene(sceneName));
            
        }
    }

    private IEnumerator AlphaChange()
    {
        playerPortalEffect.SetActive(true);
        while(timeElapsed < lerpDuration)
        {
            lerpValue = Mathf.Lerp(minVal, maxVal, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            alpha = lerpValue;

            myMaterial.color = new Color(myMaterial.color.r, myMaterial.color.g, myMaterial.color.b, alpha);
            yield return null;
        }
        Time.timeScale = 1f;
        yield return new WaitForSecondsRealtime(3f);
        playerPortalEffect.SetActive(false);
        AlphaZero();

    }
    private void AlphaZero()
    {
        myMaterial.color = new Color(myMaterial.color.r, myMaterial.color.g, myMaterial.color.b, 0f);
    }

    private IEnumerator LoadScene(string sceneName){
        yield return new WaitForSecondsRealtime(4.3f);
        loadingScreen.SetActive(true);
        Time.timeScale = 1f;
        yield return new WaitForSecondsRealtime(1.25f);
        SceneManager.LoadScene(sceneName);
        Debug.Log("player triggered");
    }
}
