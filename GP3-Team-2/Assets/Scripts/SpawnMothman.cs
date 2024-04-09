using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMothman : MonoBehaviour
{
    public GameObject enemy;
    public Transform enemyPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Instantiate(enemy, enemyPos.position, enemyPos.rotation);
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            gameObject.transform.GetChild(2).gameObject.SetActive(true);
        }
    }
}
