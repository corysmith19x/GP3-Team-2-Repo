using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosterScript : MonoBehaviour
{

    public GameObject posterHUD;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            posterHUD.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            posterHUD.SetActive(false);
        }
    }
}
