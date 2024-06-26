using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothmanHitbox : MonoBehaviour
{
    private float damage = -25f;

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
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Hit");
            other.gameObject.GetComponent<StatsInventoryManager>().UpdateHealth(damage);
            LevelStatTracker.instance.DamageTaken(damage);
        }
    }
}
