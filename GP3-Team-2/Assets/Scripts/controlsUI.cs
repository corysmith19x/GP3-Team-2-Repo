using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class controlsUI : MonoBehaviour
{
    public GameObject controlsPanel, playBtn;
    public AudioSource audioSource;
    public AudioClip closeSound;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(controlsPanel);
    }

    void Update()
    {
        if (controlsPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                StartCoroutine(PlayCloseSoundAndClosePanel());
            }
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(playBtn);
        }
    }

    IEnumerator PlayCloseSoundAndClosePanel()
    {
        audioSource.PlayOneShot(closeSound);
        yield return new WaitForSeconds(closeSound.length);
        controlsPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(playBtn);
    }
}
